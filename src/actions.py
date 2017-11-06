import os

from PySide import QtCore
from PySide import QtGui
import EtGui, EtFile

import shared
import webbrowser
from mossfrontend import *

import fileManagement

tempFileManager = fileManagement.FileManager()

def actionExit_trigger():
    shared.mainWindow.close()

def actionSettings_trigger():
    print("settings")

def fileList_drop(source, event):
    if event.mimeData().hasUrls():
        event.accept()
        files = []
        for url in event.mimeData().urls():
            if url.isLocalFile(): # or is relative
                files.append(url.toLocalFile())
        addFilesToList(source.objectName(), files)

        if source.count() != 0:
            source.findChild(EtGui.EtLabel).hide()
    else:
        event.ignore()
    
def fileList_drag(source, event):
    if event.mimeData().hasUrls():
        event.accept()
    else:
        event.ignore()
    
def addStudentButton_click():
    filePath = QtGui.QFileDialog.getExistingDirectory(None, "Select Student Source Folder to Add Files From", shared.addPath)
    if not filePath == "":
        shared.addPath = os.path.abspath(os.path.join(filePath,".."))
        addFilesToList("studentFileList", [os.path.join(filePath, filename) for filename in os.listdir(filePath)])

        if shared.mainWindow.findChild(QtGui.QCheckBox, "runCheckBox").isChecked():
            runQueryButton_click()

def clearStudentButton_click():
    shared.mainWindow.findChild(EtGui.EtListWidget, "studentFileList").clear()
    shared.mainWindow.findChild(EtGui.EtLabel, "studentDragLabel").show()

def addBaseButton_click():
    filePath = QtGui.QFileDialog.getExistingDirectory(None, "Select Base Source Folder to Add Files From", shared.addPath)
    if not filePath == "":
        shared.addPath = os.path.abspath(os.path.join(filePath,".."))
        addFilesToList("baseFileList", [os.path.join(filePath, filename) for filename in os.listdir(filePath)])

def clearBaseButton_click():
    shared.mainWindow.findChild(EtGui.EtListWidget, "baseFileList").clear()
    shared.mainWindow.findChild(EtGui.EtLabel, "baseDragLabel").show()

def moss_output(message):
    outputMessage(message)

def moss_warning(message):
    outputMessage('<span style="color: red;">{}</span>'.format(message))

def moss_failed(message):
    outputMessage(message)

def moss_success(message):
    outputMessage("Result: <a href={}>{}</a>".format(message, message))
    outputMessage("Opening result in browser...")
    webbrowser.open(message, new=2)

def runQueryButton_click():
    runMossAsync()

def saveQueryButton_click():
    None

def clearQueryButton_click():
    None

def saveOutputButton_click():
    None

def clearOutputButton_click():
    # TODO: Was having some trouble clearing the QTextBrowser contents after inserting
    #   html links (<a href=...>...</a>). The link would persist through the next few
    #   calls to append
    outputControl = shared.mainWindow.findChild(QtGui.QTextBrowser)
    outputControl.clear()
    outputControl.setHtml("")

def mainWindow_close(source, event):
    config = shared.config
    tempFileManager.cleanup()

    fileHandle = open(shared.configPath + shared.configFile, 'w')
    config.set("config","AddFilter", source.findChild(QtGui.QLineEdit, "filterEdit").text())
    config.set("config","IgnoreFilter", source.findChild(QtGui.QLineEdit, "ignoreEdit").text())
    config.set("config", "UseDirectories", source.findChild(QtGui.QCheckBox, "dirCheckBox").isChecked())
    config.set("config", "RunAfterAdd", source.findChild(QtGui.QCheckBox, "runCheckBox").isChecked())
    config.set("config","Language", source.findChild(QtGui.QComboBox, "languageBox").currentText())
    config.set("config","IgnoreCount", source.findChild(QtGui.QSpinBox, "ignoreCountSpinBox").value())
    config.set("config","Comment", source.findChild(QtGui.QLineEdit, "commentEdit").text())
    config.set("config", "AddPath", shared.addPath)
    config.write(fileHandle)
    fileHandle.close()
    event.accept()

def addFilesToList(listName, files):
    fileList = shared.mainWindow.findChild(EtGui.EtListWidget, listName)
    addFilter = shared.mainWindow.findChild(QtGui.QLineEdit, "filterEdit").text()
    ignoreFilter = shared.mainWindow.findChild(QtGui.QLineEdit, "ignoreEdit").text()
    filesAlreadyInList = getFilesFromList(listName)
    sourceFiles = []

    for filename in files:
        sourceFiles.extend(getFiles(filename, addFilter, ignoreFilter))

    for filename in sourceFiles:
        if filename not in filesAlreadyInList:
            fileList.addItem(filename)

    if fileList.count() != 0:
        fileList.findChild(EtGui.EtLabel).hide()

def outputMessage(message):
    shared.mainWindow.findChild(QtGui.QTextBrowser).append(message)

def getFilesFromList(listName):
    fileList = shared.mainWindow.findChild(EtGui.EtListWidget, listName)
    files = []
    for i in range(fileList.count()):
        files.append(fileList.item(i).text())

    return files

def getFilesFromPath(filename, addFilter, ignoreFilter, outputMessage):
    return getFiles(filename, addFilter, ignoreFilter, outputMessage)

def getFiles(filePath, fileFilter = "", ignored = ""):
    files = []

    if not EtFile.isIgnoredFile(filePath, fileFilter.split(), ignored.split()):
        if os.path.isfile(filePath):
            if os.path.splitext(filePath)[1].lower() == ".zip":
                tempFileManager.extractFiles(filePath, files, fileFilter.split(), ignored.split())
            else:
                files.append(filePath)
        elif os.path.isdir(filePath):
            for filename in os.listdir(filePath):
                filename = os.path.join(filePath, filename)
                files.extend(getFiles(filename, fileFilter, ignored))

    return files

