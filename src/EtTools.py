import platform, os, tempfile, fnmatch, fileManagement

tempFileManager = fileManagement.FileManager()

def getTempPath():
    return os.path.join(tempfile.gettempdir(), "")

def getHomePath():
    return "{}/".format(os.path.expanduser("~"))

def getAppDataPath():
    # Grab the app-specific data folder from the OS (if available.)
    if platform.system() == 'Windows':
        from win32com.shell import shellcon, shell
        return "{}\\".format(shell.SHGetFolderPath(0, shellcon.CSIDL_APPDATA, 0, 0))
    elif platform.system() == 'Darwin':
        return "{}/".format(os.path.expanduser("~/Library"))
    else:
        return getHomePath()

def isIgnoredFile(filename, filterList, ignoreList):
    if not ignoreList == None:
        for pattern in ignoreList:
            if fnmatch.fnmatch(filename, pattern):
                return True
    if not filterList == None:
        for pattern in filterList:
            if fnmatch.fnmatch(filename, pattern):
                return False
    return True

def getFiles(filePath, filter = None, ignored = None, outputMessage = None):
    files = []
    if os.path.isfile(filePath):
        if not isIgnoredFile(filePath, filter.split(), ignored.split()):
            if os.path.splitext(filePath)[1].lower() == ".zip":
                tempFileManager.extractFiles(filePath, files, filter.split(), ignored.split(), outputMessage = outputMessage)
            else:
                files.append(filePath)
    elif os.path.isdir(filePath):
        for filename in os.listdir(filePath):
            filename = os.path.join(filePath, filename)
            files.extend(getFiles(filename, filter, ignored, outputMessage))

    return files
