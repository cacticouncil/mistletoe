'''
This file is part of the EdTech library project at Full Sail University.

    Foobar is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Foobar is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.

    Copyright (C) 2014, 2015 Full Sail University.
'''

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
        from win32com.shell import shellcon, shell # http://sourceforge.net/projects/pywin32/files/
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

def saveJsonFile(filename, saveObject, **kwargs):
    jsonFile = open(filename, 'w')
    json.dump(saveObject, jsonFile, **kwargs)
#    jsonFile.write("[\n")
#    i = 0
#    for item in saveObject:
#        i = i + 1
#        try:
#            json.dump(item, jsonFile, indent=indentLevel)
#            if i != len(saveObject):
#                jsonFile.write(",\n")
#        except:
#            print item
#    json.dump(saveObject, jsonFile, indent=indentLevel)
#    jsonFile.write("]\n")
    jsonFile.close()
    return

def savePickleFile(filename, saveObject):
    pickleFile = open(filename, 'wb')
    pickle.dump(saveObject, pickleFile)
    pickleFile.close()
    return
