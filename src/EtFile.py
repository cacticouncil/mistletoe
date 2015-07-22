import os
import platform
import tempfile
import fnmatch
import gzip
import json
import pickle

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

def isGzipFile(filePath):
    loadFile = open(filePath, 'rb')
    head = loadFile.read(2)
    loadFile.close()

    if ord(head[0]) == 0x1f and ord(head[1]) == 0x8b:
        return True
    else:
        return False

def isIgnoredFile(filename, filterList, ignoreList):
    if not ignoreList == None:
        for pattern in ignoreList:
            if fnmatch.fnmatch(filename, pattern):
                return True
    if not filterList == []:
        for pattern in filterList:
            if fnmatch.fnmatch(filename, pattern):
                return False
    else:
        return False
            
    return True

def getFilesRecursive(filePath, fileFilter = "", ignored = ""):
    files = []
    if os.path.isfile(filePath):
        if not isIgnoredFile(filePath, fileFilter.split(), ignored.split()):
            files.append(filePath)
    elif os.path.isdir(filePath):
        for filename in os.listdir(filePath):
            filename = os.path.join(filePath, filename)
            files.extend(getFilesRecursive(filename, fileFilter, ignored))

    return files

def loadFile(filePath):
    if isGzipFile(filePath):
        loadFile = gzip.open(filePath, 'rb')
    else:
        loadFile = open(filePath, 'rb')

    data = loadFile.read()    
    loadFile.close()
    return data    

def loadJsonFile(filename):
    jsonFile = open(filename, 'r')
    returnObject = json.load(jsonFile)
    jsonFile.close()
    return returnObject

def loadPickleFile(filename):
    pickleFile = open(filename, 'rb')
    returnObject = pickle.load(pickleFile)
    pickleFile.close()
    return returnObject

def openFile(filePath, mode):
    if isGzipFile(filePath):
        return gzip.open(filePath, mode)
    else:
        return open(filePath, mode)

def saveJsonFile(saveObject, filename, indentLevel=2):
    jsonFile = open(filename, 'w')
    jsonFile.write("[\n")
    i = 0
    for item in saveObject:
        i = i + 1
        try:
            json.dump(item, jsonFile, indent=indentLevel)
            if i != len(saveObject):
                jsonFile.write(",\n")
        except:
            print item
#    json.dump(saveObject, jsonFile, indent=indentLevel)
    jsonFile.write("]\n")
    jsonFile.close()
    return

def savePickleFile(saveObject, filename):
    pickleFile = open(filename, 'wb')
    pickle.dump(saveObject, pickleFile)
    pickleFile.close()
    return
