import tempfile
import os
import shutil
import zipfile
import sys
import fnmatch
import EtTools

class FileManager:
    """ Manages all things related to extracting zip archives to a temporary directory. """
    def __init__(self):
        self.tempDirectory = tempfile.mkdtemp()
        if self.tempDirectory == None or len(self.tempDirectory) <= 1:
            raise Exception("Invalid temporary directory created: {}".format(self.tempDirectory))

    def cleanup(self):
        """ Deletes the temporary directory if it exists. """
        if self.tempDirectory != None:
            shutil.rmtree(self.tempDirectory, True)
            self.tempDirectory = None

    def extractFiles(self, archivePath, extractedFiles, filterList = None, ignoreList = None, isExtractingToCurrentDirectory = False):
        """ Extracts the filtered contents of the specified zip archive to the temporary directory. 
        Args:
            archivePath: Path to the zip archive being extracted.
            extractedFiles: Output. Paths to extracted files will be appended to this list.
            filterList: List of file patterns to include. Applied to files not ignored by ignoreList.
            ignoreList: List of file patterns to exclude. Applied to all files. Takes priority over filterList.
            isExtractingToCurrentDirectory: Determines if the contents of the archive will be extracted to the current directory.
        Raises:
            Exception: Error occurred when extracting contents of zip archive.
        """
        archiveNameWithoutExtension = os.path.splitext(os.path.split(archivePath)[1])[0]
        archivePathWithoutName = os.path.split(archivePath)[0]

        if isExtractingToCurrentDirectory:
            directoryToExtractTo = os.path.realpath(os.path.join(archivePathWithoutName, archiveNameWithoutExtension))
        else:
            directoryToExtractTo = os.path.realpath(os.path.join(self.tempDirectory, archiveNameWithoutExtension))
            
        with zipfile.ZipFile(archivePath, 'r') as zip:
            for name in zip.namelist():
                if EtTools.isIgnoredFile(name, filterList, ignoreList):
                    continue

                pathToExtractedFile = os.path.realpath(os.path.join(directoryToExtractTo, name))
                if not pathToExtractedFile.startswith(directoryToExtractTo):
                    # Files inside of zip archives can be maliciously named to cause writing to unexpected locations. All versions
                    # of python prior to 2.7.4 are vulnerable to this sort of attack. Therefore, we must manually guarantee
                    # that all writes are to our temporary directory.
                    raise Exception("Security warning: Blocked directory traversal when extracting: '{}'".format(name))

                zip.extract(name, directoryToExtractTo)
                
                if os.path.splitext(pathToExtractedFile)[1].lower() == ".zip":
                    self.extractFiles(pathToExtractedFile, extractedFiles, filterList, ignoreList, True)
                else:
                    extractedFiles.append(pathToExtractedFile)
