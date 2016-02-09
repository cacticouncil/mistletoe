from EtFile import *
import ConfigParser

global mainUiFile, configFile
global configPath, tempPath, addPath
global config, mainWindow

# Fixed data filenames
mainUiFile = "mistletoe.ui"
configFile = "Mistletoe.ini"

# Grab system-dependent path variables
tempPath = getTempPath() + "MistletoeTemp"
addPath = getHomePath()
configPath = getAppDataPath()

# Allocate a configuration object and main window reference
config = ConfigParser.ConfigParser()
mainWindow = None
