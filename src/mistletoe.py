# -*- coding: utf-8 -*-

# Globals
mainUiFile = "mistletoe.ui"
configFile = "Mistletoe.ini"

from PySide import QtCore, QtGui, QtUiTools

# Load a UI widget from a file
def loadUiWidget(uifilename, parent=None):
    loader = QtUiTools.QUiLoader()
    uifile = QtCore.QFile(uifilename)
    uifile.open(QtCore.QFile.ReadOnly)
    ui = loader.load(uifile, parent)
    uifile.close()
    return ui

# Main routine (when called as main script)
if __name__ == "__main__":
    import sys, os, platform
    import ConfigParser
    import tempfile
    app = QtGui.QApplication(sys.argv)
    MainWindow = loadUiWidget(mainUiFile)

    # Grab the app-specific data folder from the OS (if available.)
    if platform.system() == 'Windows':
        from win32com.shell import shellcon, shell
        homedir = "{}\\".format(shell.SHGetFolderPath(0, shellcon.CSIDL_APPDATA, 0, 0))
    elif platform.system() == 'Darwin':
        homedir = "{}/".format(os.path.expanduser("~/Library"))
    elif os.name == 'posix':
        homedir = "{}/".format(os.path.expanduser("~"))
    else:
        QtGui.QMessageBox.information(None, "Modern OS plz!", "Can't determine home folder - exiting.", QtGui.QMessageBox.Ok)
        sys.exit()

    # Load the configuration file.
    config = ConfigParser.ConfigParser()
    config.read(homedir + configFile)

    if not config.has_section("config"):
        config.add_section("config")

    # If any options are not set, load the defaults.
    if not config.has_option("config","AddFilter"):
        config.set("config","AddFilter", MainWindow.findChild(QtGui.QLineEdit, "filterEdit").text())
    if not config.has_option("config","IgnoreFilter"):
        config.set("config","IgnoreFilter", MainWindow.findChild(QtGui.QLineEdit, "ignoreEdit").text())
    if not config.has_option("config","UseDirectories"):
        config.set("config", "UseDirectories", str(MainWindow.findChild(QtGui.QCheckBox, "dirCheckBox").isChecked()))
    if not config.has_option("config","RunAfterAdd"):
        config.set("config", "RunAfterAdd", str(MainWindow.findChild(QtGui.QCheckBox, "runCheckBox").isChecked()))
    if not config.has_option("config","Language"):
        config.set("config","Language", MainWindow.findChild(QtGui.QComboBox, "languageBox").currentText())
    if not config.has_option("config","IgnoreCount"):
        config.set("config","IgnoreCount", str(MainWindow.findChild(QtGui.QSpinBox, "ignoreCountSpinBox").value()))
    if not config.has_option("config","Comment"):
        config.set("config","Comment", MainWindow.findChild(QtGui.QLineEdit, "commentEdit").text())
    if not config.has_option("config", "AddPath"):
        config.set("config", "AddPath", os.path.expanduser("~"))

    # Set the Gui to match the configuration
    MainWindow.findChild(QtGui.QLineEdit, "filterEdit").setText(config.get("config", "AddFilter"))
    MainWindow.findChild(QtGui.QLineEdit, "ignoreEdit").setText(config.get("config", "IgnoreFilter"))
    MainWindow.findChild(QtGui.QCheckBox, "dirCheckBox").setChecked(config.get("config", "UseDirectories").lower() == 'true')
    MainWindow.findChild(QtGui.QCheckBox, "runCheckBox").setChecked(config.get("config", "RunAfterAdd").lower() == 'true')
    MainWindow.findChild(QtGui.QSpinBox, "ignoreCountSpinBox").setValue(config.getint("config", "IgnoreCount"))
    MainWindow.findChild(QtGui.QLineEdit, "commentEdit").setText(config.get("config", "Comment"))
    languageBox = MainWindow.findChild(QtGui.QComboBox, "languageBox")
    languageBox.setCurrentIndex(languageBox.findText(config.get("config", "Language")))

    # Set up path variables
    addPath = config.get("config", "AddPath")
    tempPath = os.path.join(tempfile.gettempdir(), "MistletoeTemp")
    
    # Launch the GUI
    menubar = MainWindow.menubar
    MainWindow.show()
    app.exec_()

    # Save the configuration file
    fileHandle = open(homedir + configFile, 'w')
    config.set("config","AddFilter", MainWindow.findChild(QtGui.QLineEdit, "filterEdit").text())
    config.set("config","IgnoreFilter", MainWindow.findChild(QtGui.QLineEdit, "ignoreEdit").text())
    config.set("config", "UseDirectories", MainWindow.findChild(QtGui.QCheckBox, "dirCheckBox").isChecked())
    config.set("config", "RunAfterAdd", MainWindow.findChild(QtGui.QCheckBox, "runCheckBox").isChecked())
    config.set("config","Language", MainWindow.findChild(QtGui.QComboBox, "languageBox").currentText())
    config.set("config","IgnoreCount", MainWindow.findChild(QtGui.QSpinBox, "ignoreCountSpinBox").value())
    config.set("config","Comment", MainWindow.findChild(QtGui.QLineEdit, "commentEdit").text())
    config.set("config", "AddPath", addPath)
    config.write(fileHandle)
    fileHandle.close()
    sys.exit()
    