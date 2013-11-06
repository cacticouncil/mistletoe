# -*- coding: utf-8 -*-

from PySide import QtGui, QtCore, QtUiTools
import string

def ignoreEvent(self, event):
    event.ignore()

def acceptEvent(self, event):
    event.ignore()

QtGui.QWidget.dragEnterEvent = QtGui.QWidget.dragMoveEvent = QtGui.QWidget.dragLeaveEvent = QtGui.QWidget.dropEvent = ignoreEvent
QtGui.QWidget.hideEvent = QtGui.QWidget.showEvent = QtGui.QWidget.closeEvent = acceptEvent

class EtMainWindow(QtGui.QMainWindow):
    def __init__(self, parent=None, flags=0):
        super(EtMainWindow, self).__init__(parent, flags)
    
class EtListWidget(QtGui.QListWidget):
    def __init__(self, parent=None):
        super(EtListWidget, self).__init__(parent)

class EtLabel(QtGui.QLabel):
    def __init(self, parent=None):
        super(EtLabel, self).__init__(parent)
        self.setAcceptDrops(True)
        
    def dragEnterEvent(self, event):
        event.accept()

    def dragMoveEvent(self, event):
        event.accept()
        
    def dropEvent(self, event):
        event.accept()

class EtUiLoader(QtUiTools.QUiLoader):
    def __init__(self):
        super(EtUiLoader, self).__init__()
        self.registerCustomWidget(EtMainWindow)
        self.registerCustomWidget(EtListWidget)
        self.registerCustomWidget(EtLabel)
        
    def loadWidgetFile(self, filename, parent=None):
        uiFile = QtCore.QFile(filename)
        uiFile.open(QtCore.QFile.ReadOnly)
        widget = self.load(uiFile, parent)
        uiFile.close()
        return widget

#class EtApp(QtGui.QApplication):
#    def __init__(self, argv, widget=None):
#        super(EtApp, self).__init__(argv)
#        if not widget == None:
#            if widget.__class__ == string:               
#                loadWidgetFile(widget)
                
                