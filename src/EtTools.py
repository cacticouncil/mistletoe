import platform, os, tempfile, fnmatch

def getTempPath():
    return os.path.join(tempfile.gettempdir(), "")

def getHomePath():
    return "{}/".format(os.path.expanduser("~"))

def getAppDataPath():
    # Grab the app-specific data folder from the OS (if available.)
    if platform.system() == 'Windows':
        from win32com.shell import shellcon, shell
        return "{}\\".format(shell.SHGetFolderPath(0, shellcon.CSIDL_APPDATA, 0, 0))
    elif platform.system() == 'Darwin':
        return "{}/".format(os.path.expanduser("~/Library"))
    else:
        return getHomePath()

def getFiles(filePath, filter = None, ignored = None):
    files = []
    if os.path.isfile(filePath):
        if not fnmatch.fnmatch(filePath, "*" + ignored + "*"):
            isMatching = False
            for pattern in filter.split():
                isMatching = isMatching or fnmatch.fnmatch(filePath, pattern)
            if isMatching:
                files.append(filePath)
    elif os.path.isdir(filePath):
        for filename in os.listdir(filePath):
            filename = os.path.join(filePath, filename)
            files.extend(getFiles(filename, filter, ignored))

    return files
