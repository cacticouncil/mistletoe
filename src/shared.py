from EtFile import *
import configparser

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
config = configparser.ConfigParser()
mainWindow = None

# Shared information for uploading in chunks
allStudentFiles = []
maxAllowedSize = 10000 #size in bytes
maxAllowedFiles = 8 #max allowed files per chunk
