import random
import string

import os
import fileManagement, EtFile

tempFileManager = fileManagement.FileManager()

#deprecated
def isIgnoredFile(filename, filterList, ignoreList):
    return EtFile.isIgnoredFile(filename, filterList, ignoreList)

#Soon to deprecate?
def getFiles(filePath, fileFilter = "", ignored = ""):
    files = []
    if os.path.isfile(filePath):
        if not isIgnoredFile(filePath, fileFilter.split(), ignored.split()):
            if os.path.splitext(filePath)[1].lower() == ".zip":
                tempFileManager.extractFiles(filePath, files, fileFilter.split(), ignored.split())
            else:
                files.append(filePath)
    elif os.path.isdir(filePath):
        for filename in os.listdir(filePath):
            filename = os.path.join(filePath, filename)
            files.extend(getFiles(filename, fileFilter, ignored))

    return files

def getRandomPassword(rangeLow = 7, rangeHigh = 16):
    size = int(round(random.SystemRandom().uniform(rangeLow, rangeHigh)))
    return ''.join(random.SystemRandom().choice(string.uppercase + string.lowercase + string.digits) for _ in xrange(size))

def transposeData(dataset):
    return [list(i) for i in zip(*dataset)]