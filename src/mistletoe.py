# -*- coding: utf-8 -*-
import sys, types, os

from PySide2 import QtWidgets
import EtGui

from actions import *
import shared

# Main routine (when called as main script)
def main():
    config = shared.config

    if getattr(sys, 'frozen', False):
        datadir = os.path.dirname(sys.executable)
    else:
        datadir = os.path.dirname(__file__)

    # Create the app wrapper and main window
    app = QtWidgets.QApplication(sys.argv)
    shared.mainWindow = EtGui.EtUiLoader().loadWidgetFile(os.path.join(datadir, shared.mainUiFile))
    shared.mainWindow.closeEvent = types.MethodType(mainWindow_close, shared.mainWindow)
    mainWindow = shared.mainWindow

    # Load the configuration file.
    config.read(shared.configPath + shared.configFile)

    if not config.has_section("config"):
        config.add_section("config")

    # If any options are not set, load the defaults.
    if not config.has_option("config","AddFilter"):
        config.set("config","AddFilter", mainWindow.findChild(QtWidgets.QLineEdit, "filterEdit").text())
    if not config.has_option("config","IgnoreFilter"):
        config.set("config","IgnoreFilter", mainWindow.findChild(QtWidgets.QLineEdit, "ignoreEdit").text())
    if not config.has_option("config","UseDirectories"):
        config.set("config", "UseDirectories", str(mainWindow.findChild(QtWidgets.QCheckBox, "dirCheckBox").isChecked()))
    if not config.has_option("config","RunAfterAdd"):
        config.set("config", "RunAfterAdd", str(mainWindow.findChild(QtWidgets.QCheckBox, "runCheckBox").isChecked()))
    if not config.has_option("config","Language"):
        config.set("config","Language", mainWindow.findChild(QtWidgets.QComboBox, "languageBox").currentText())
    if not config.has_option("config","IgnoreCount"):
        config.set("config","IgnoreCount", str(mainWindow.findChild(QtWidgets.QSpinBox, "ignoreCountSpinBox").value()))
    if not config.has_option("config","Comment"):
        config.set("config","Comment", mainWindow.findChild(QtWidgets.QLineEdit, "commentEdit").text())
    if not config.has_option("config", "AddPath"):
        config.set("config", "AddPath", shared.addPath)

    # Set the Gui to match the configuration
    mainWindow.findChild(QtWidgets.QLineEdit, "filterEdit").setText(config.get("config", "AddFilter"))
    mainWindow.findChild(QtWidgets.QLineEdit, "ignoreEdit").setText(config.get("config", "IgnoreFilter"))
    mainWindow.findChild(QtWidgets.QCheckBox, "dirCheckBox").setChecked(config.get("config", "UseDirectories").lower() == 'true')
    mainWindow.findChild(QtWidgets.QCheckBox, "runCheckBox").setChecked(config.get("config", "RunAfterAdd").lower() == 'true')
    mainWindow.findChild(QtWidgets.QSpinBox, "ignoreCountSpinBox").setValue(config.getint("config", "IgnoreCount"))
    mainWindow.findChild(QtWidgets.QLineEdit, "commentEdit").setText(config.get("config", "Comment"))
    languageBox = mainWindow.findChild(QtWidgets.QComboBox, "languageBox")
    languageBox.setCurrentIndex(languageBox.findText(config.get("config", "Language")))
    shared.addPath = config.get("config", "AddPath")

    # Connect Gui buttons to actions
    mainWindow.findChild(QtWidgets.QAction, "actionExit").triggered.connect(actionExit_trigger)
    mainWindow.findChild(QtWidgets.QAction, "actionSettings").triggered.connect(actionSettings_trigger)

    studentFileList = mainWindow.findChild(EtGui.EtListWidget, "studentFileList")
    studentFileList.dragEnterEvent = studentFileList.dragMoveEvent = types.MethodType(fileList_drag, studentFileList)
    studentFileList.dropEvent = types.MethodType(fileList_drop, studentFileList)
    studentFileList.setAcceptDrops(True)

    baseFileList = mainWindow.findChild(EtGui.EtListWidget, "baseFileList")
    baseFileList.dragEnterEvent = baseFileList.dragMoveEvent = types.MethodType(fileList_drag, baseFileList)
    baseFileList.dropEvent = types.MethodType(fileList_drop, baseFileList)
    baseFileList.setAcceptDrops(True)

    mainWindow.findChild(QtWidgets.QPushButton, "addStudentButton").clicked.connect(addStudentButton_click)
    mainWindow.findChild(QtWidgets.QPushButton, "clearStudentButton").clicked.connect(clearStudentButton_click)
    mainWindow.findChild(QtWidgets.QPushButton, "addBaseButton").clicked.connect(addBaseButton_click)
    mainWindow.findChild(QtWidgets.QPushButton, "clearBaseButton").clicked.connect(clearBaseButton_click)

    mainWindow.findChild(QtWidgets.QPushButton, "runQueryButton").clicked.connect(runQueryButton_click)
    mainWindow.findChild(QtWidgets.QPushButton, "runQueryChunkButton").clicked.connect(runQuery2ChunkButton_click)
    mainWindow.findChild(QtWidgets.QPushButton, "saveQueryButton").clicked.connect(saveQueryButton_click)
    mainWindow.findChild(QtWidgets.QPushButton, "clearQueryButton").clicked.connect(loadQueryButton_click) #TO-DO: make this load query button?
    mainWindow.findChild(QtWidgets.QPushButton, "saveOutputButton").clicked.connect(saveOutputButton_click)
    mainWindow.findChild(QtWidgets.QPushButton, "clearOutputButton").clicked.connect(clearOutputButton_click)
    mainWindow.findChild(QtWidgets.QPushButton, "addSingleStudentButton").clicked.connect(addSingleStudentButton_click)
    mainWindow.findChild(QtWidgets.QPushButton, "addSingleBaseButton").clicked.connect(addSingleBaseButton_click)

    # Launch the Gui
    mainWindow.show()
    app.exec_()
    sys.exit()

if __name__ == "__main__":
    main()
    
