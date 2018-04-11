from PySide import QtGui
from PySide import QtCore
import actions
import shared
import moss

workerThread = None

class MossThread(QtCore.QThread):
    def __init__(self):
        self.maxMatchesPerPassage = 10
        self.language = "cc"
        self.comment = ""
        self.isComparingDirectories = 0
        self.baseFiles = []
        self.studentFiles = []
        QtCore.QThread.__init__(self)

    def run(self):
        client = moss.Client()
        client.OnFailure = self.OnFailed
        client.OnSuccess = self.OnSuccess
        client.Output = self.OnOutput
        client.OnWarning = self.OnWarning
        client.maxMatchesPerPassage = self.maxMatchesPerPassage
        client.language = self.language
        client.comment = self.comment
        client.isComparingDirectories = self.isComparingDirectories
        client.isUsingExperimentalServer = 0
        client.Run(self.studentFiles, self.baseFiles)

    #TO-DO: Test and ensure this will work correctly
    def saveQuery(self):
        maxMatchesPerPassage = self.maxMatchesPerPassage
        language = self.language
        comment = self.comment
        isComparingDirectories = self.isComparingDirectories
        isUsingExperimentalServer = 0
        print(type(maxMatchesPerPassage) + type(language))


    def OnOutput(self, message):
        self.emit(QtCore.SIGNAL("OnOutput(QString)"), message)

    def OnWarning(self, message):
        self.emit(QtCore.SIGNAL("OnWarning(QString)"), message)

    def OnFailed(self, message):
        self.emit(QtCore.SIGNAL("OnFailed(QString)"), message)

    def OnSuccess(self, message):
        self.emit(QtCore.SIGNAL("OnSuccess(QString)"), message)

def runMossAsync():
    global workerThread

    if workerThread is None:
        workerThread = MossThread()
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnOutput(QString)"), actions.moss_output)
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnWarning(QString)"), actions.moss_warning)
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnFailed(QString)"), actions.moss_failed)
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnSuccess(QString)"), actions.moss_success)
    elif not workerThread.isFinished():
        print("Already running")
        return

    isComparingDirectories = 0
    isUsingExperimentalServer = 0

    if shared.mainWindow.findChild(QtGui.QCheckBox, "dirCheckBox").isChecked():
        isComparingDirectories = 1
    
    workerThread.isComparingDirectories = isComparingDirectories
    workerThread.studentFiles = actions.getFilesFromList("studentFileList")
    workerThread.baseFiles = actions.getFilesFromList("baseFileList")
    workerThread.maxMatchesPerPassage = shared.mainWindow.findChild(QtGui.QSpinBox, "ignoreCountSpinBox").value()
    workerThread.language = shared.mainWindow.findChild(QtGui.QComboBox, "languageBox").currentText()
    workerThread.comment = shared.mainWindow.findChild(QtGui.QLineEdit, "commentEdit").text()
    workerThread.start()

def runMossChunkAsync(to_upload):
    global workerThread

    if workerThread is None:
        workerThread = MossThread()
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnOutput(QString)"), actions.moss_output)
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnWarning(QString)"), actions.moss_warning)
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnFailed(QString)"), actions.moss_failed)
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnSuccess(QString)"), actions.moss_success)
    elif not workerThread.isFinished():
        print("Already running")
        return

    isComparingDirectories = 0
    isUsingExperimentalServer = 0

    if shared.mainWindow.findChild(QtGui.QCheckBox, "dirCheckBox").isChecked():
        isComparingDirectories = 1
    
    workerThread.isComparingDirectories = isComparingDirectories
    workerThread.studentFiles = to_upload
    workerThread.baseFiles = actions.getFilesFromList("baseFileList")
    workerThread.maxMatchesPerPassage = shared.mainWindow.findChild(QtGui.QSpinBox, "ignoreCountSpinBox").value()
    workerThread.language = shared.mainWindow.findChild(QtGui.QComboBox, "languageBox").currentText()
    workerThread.comment = shared.mainWindow.findChild(QtGui.QLineEdit, "commentEdit").text()
    workerThread.start()

def runMossChunkAsync2(to_upload):
    global workerThread
    size = len(to_upload)
    count = -1
    while (count < size - 1):
        if (workerThread is None) or (workerThread.isFinished()):
            workerThread = MossThread()
            QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnOutput(QString)"), actions.moss_output)
            QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnWarning(QString)"), actions.moss_warning)
            QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnFailed(QString)"), actions.moss_failed)
            QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnSuccess(QString)"), actions.moss_success)
            count += 1
            isComparingDirectories = 0
            isUsingExperimentalServer = 0

            if shared.mainWindow.findChild(QtGui.QCheckBox, "dirCheckBox").isChecked():
                isComparingDirectories = 1
            
            workerThread.isComparingDirectories = isComparingDirectories
            workerThread.studentFiles = to_upload[count]
            workerThread.baseFiles = actions.getFilesFromList("baseFileList")
            workerThread.maxMatchesPerPassage = shared.mainWindow.findChild(QtGui.QSpinBox, "ignoreCountSpinBox").value()
            workerThread.language = shared.mainWindow.findChild(QtGui.QComboBox, "languageBox").currentText()
            workerThread.comment = shared.mainWindow.findChild(QtGui.QLineEdit, "commentEdit").text()
            workerThread.start()
            print(count)

def runMossChunkAsync3(to_upload):
    global workerThread
    size = len(to_upload)
    currentIndex = 0
    for fileList in to_upload:
        count = currentIndex
        print("current Index: ", currentIndex)
        while (count < size - 1):
            if (workerThread is None) or (workerThread.isFinished()):
                workerThread = MossThread()
                QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnOutput(QString)"), actions.moss_output)
                QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnWarning(QString)"), actions.moss_warning)
                QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnFailed(QString)"), actions.moss_failed)
                QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnSuccess(QString)"), actions.moss_success)
                isComparingDirectories = 0
                isUsingExperimentalServer = 0

                if shared.mainWindow.findChild(QtGui.QCheckBox, "dirCheckBox").isChecked():
                    isComparingDirectories = 1
                
                workerThread.isComparingDirectories = isComparingDirectories
                workerThread.studentFiles.clear()

                for file in workerThread.studentFiles:
                    print("before assign/append: ", file)
                #append current file chunk to current upload thread
                for file in fileList:
                    workerThread.studentFiles.append(file)
                #append next chunk to current upload thread
                for file in to_upload[count + 1]:
                    workerThread.studentFiles.append(file)
                    print("appending file: ", file)
                for file in workerThread.studentFiles:
                    print("uploading file: ", file)
                workerThread.baseFiles = actions.getFilesFromList("baseFileList")
                workerThread.maxMatchesPerPassage = shared.mainWindow.findChild(QtGui.QSpinBox, "ignoreCountSpinBox").value()
                workerThread.language = shared.mainWindow.findChild(QtGui.QComboBox, "languageBox").currentText()
                workerThread.comment = shared.mainWindow.findChild(QtGui.QLineEdit, "commentEdit").text()
                workerThread.start()
                count += 1
                print("current count:", count)
        currentIndex += 1
