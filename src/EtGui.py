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
# -*- coding: utf-8 -*-

from PySide2 import QtWidgets, QtCore, QtUiTools
import string

def ignoreEvent(self, event):
    event.ignore()

def acceptEvent(self, event):
    event.ignore()

QtWidgets.QWidget.dragEnterEvent = QtWidgets.QWidget.dragMoveEvent = QtWidgets.QWidget.dragLeaveEvent = QtWidgets.QWidget.dropEvent = ignoreEvent
QtWidgets.QWidget.hideEvent = QtWidgets.QWidget.showEvent = QtWidgets.QWidget.closeEvent = acceptEvent

class EtMainWindow(QtWidgets.QMainWindow):
    def __init__(self, parent=None, flags=0):
        super(EtMainWindow, self).__init__(parent, flags)
    
class EtListWidget(QtWidgets.QListWidget):
    def __init__(self, parent=None):
        super(EtListWidget, self).__init__(parent)

class EtLabel(QtWidgets.QLabel):
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

#class EtApp(QtWidgets.QApplication):
#    def __init__(self, argv, widget=None):
#        super(EtApp, self).__init__(argv)
#        if not widget == None:
#            if widget.__class__ == string:               
#                loadWidgetFile(widget)
                
                
