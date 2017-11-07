"""
Usage:
    Test Build:
        python setup.py build
    Distribution:
        python setup.py dist
    Windows Distribution (not yet working):
        python setup.py (Eventually will use bdist_nsi)
"""

# Application setup variables
NAME = 'Mistletoe'
VERSION = '0.9'
DESCRIPTION = 'Mistletoe is a GUI for MOSS (Measure Of Software Similarity) system.'

ENTRYPOINT = 'mistletoe.py'
DATA_FILES = [ 'mistletoe.ui' ]
MODULES = [ 'PySide.QtXml' ]
MAC_ICON = 'mistletoe.icns'
WIN_ICON = 'mistletoe.ico'
BASE = None

import sys
import os
from cx_Freeze import setup, Executable

# Deal with platform-specific requirements
if sys.platform == 'win32':
    import setuptools
    BASE = 'Win32GUI'

    if sys.argv[1] == 'dist':
        sys.argv[1] = 'bdist_msi'

else
    import ez_setup
    ez_setup.use_setuptools(no_fake=False)

    if sys.platform == 'darwin':
        if sys.argv[1] == 'dist':
            sys.argv[1] = 'bdist_dmg'

    else:
        pass

# Prepare setup options
EXECUTABLES = [ Executable(ENTRYPOINT, base=BASE, icon=WIN_ICON, shortcutName="Mistletoe", shortcutDir="ProgramMenuFolder") ]
FREEZE_OPTIONS = {'includes': MODULES, 'include_files': DATA_FILES, 'include_msvcr': True }

MSI_OPTIONS = {}
NSIS_OPTIONS = {}
BUNDLE_OPTIONS = {'iconfile': MAC_ICON, 'bundle_name': NAME }
DMG_OPTIONS = {'volume_label': NAME }
#DMG_OPTIONS = {'volume_label': NAME, 'applications-shortcut': True }

# Perform application setup (execute command)
setup(
    name=NAME,
    version=VERSION,
    description=DESCRIPTION,
    executables = EXECUTABLES,
    options =
    {
        'build_exe': FREEZE_OPTIONS, 'bdist_nsi': NSIS_OPTIONS,
        'bdist_mac': BUNDLE_OPTIONS, 'bdist_dmg': DMG_OPTIONS, 'bdist_msi': MSI_OPTIONS
    }
)
