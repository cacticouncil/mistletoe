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
        client.maxMatchesPerPassage = self.maxMatchesPerPassage
        client.language = self.language
        client.comment = self.comment
        client.isComparingDirectories = self.isComparingDirectories
        client.isUsingExperimentalServer = 0
        client.Run(self.studentFiles, self.baseFiles)

    def OnOutput(self, message):
        self.emit(QtCore.SIGNAL("OnOutput(QString)"), message)

    def OnFailed(self, message):
        self.emit(QtCore.SIGNAL("OnFailed(QString)"), message)

    def OnSuccess(self, message):
        self.emit(QtCore.SIGNAL("OnSuccess(QString)"), message)

def runMossAsync():
    global workerThread

    if workerThread is None:
        workerThread = MossThread()
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnOutput(QString)"), actions.moss_output)
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnFailed(QString)"), actions.moss_failed)
        QtCore.QObject.connect(workerThread, QtCore.SIGNAL("OnSuccess(QString)"), actions.moss_success)
    elif not workerThread.isFinished():
        print "Already running"
        return

    isComparingDirectories = 0
    isUsingExperimentalServer = 0

    if shared.mainWindow.findChild(QtGui.QCheckBox, "dirCheckBox").isChecked():
        isComparingDirectories = 1
    
	workerThread.isComparingDirectories = isComparingDirectories;
    workerThread.studentFiles = actions.getFilesFromList("studentFileList")
    workerThread.baseFiles = actions.getFilesFromList("baseFileList")
    workerThread.maxMatchesPerPassage = shared.mainWindow.findChild(QtGui.QSpinBox, "ignoreCountSpinBox").value()
    workerThread.language = shared.mainWindow.findChild(QtGui.QComboBox, "languageBox").currentText()
    workerThread.comment = shared.mainWindow.findChild(QtGui.QLineEdit, "commentEdit").text()
    workerThread.start()
