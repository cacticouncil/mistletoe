from EtTools import *
import ConfigParser

global mainUiFile, configFile
global configPath, tempPath, addPath
global config, fileLists, mainWindow

# Fixed data filenames
mainUiFile = "Mistletoe.ui"
configFile = "Mistletoe.ini"

# Grab system-dependent path variables
tempPath = getTempPath() + "MistletoeTemp"
addPath = getHomePath()
configPath = getAppDataPath()

# Allocate a configuration objects and main window reference
config = ConfigParser.ConfigParser()
tempFileLists = {}
mainWindow = None
