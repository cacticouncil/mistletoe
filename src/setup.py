"""
Usage:
    MacOS X: python setup.py bdist_dmg
    Windows: python setup.py bdist_msi
"""

# Make sure prereqs are available
import ez_setup
ez_setup.use_setuptools()

import sys
from cx_Freeze import setup, Executable

NAME = 'Mistletoe'
VERSION = '0.8'
DESCRIPTION = 'GUI for MOSS'

ENTRYPOINT = 'mistletoe.py'
DATA_FILES = [ 'mistletoe.ui' ]
MODULES = [ 'PySide.QtXml' ]
MAC_ICON = 'mistletoe.icns'
WIN_ICON = 'mistletoe.ico'

if sys.platform == 'win32':
    BASE = 'Win32GUI'
else:
    BASE = None

EXECUTABLES = [ Executable(ENTRYPOINT, base=BASE) ]
FREEZE_OPTIONS = {'includes': MODULES, 'include_files': DATA_FILES, 'icon': WIN_ICON, 'include_msvcr': True }
BUNDLE_OPTIONS = {'iconfile': MAC_ICON, 'bundle_name': NAME }
DMG_OPTIONS = {'volume_label': NAME }
#DMG_OPTIONS = {'volume_label': NAME, 'applications-shortcut': True }

setup(
    app=[ENTRYPOINT],
    name=NAME,
    version=VERSION,
    description=DESCRIPTION,
    options = {'build_exe': FREEZE_OPTIONS, 'bdist_mac': BUNDLE_OPTIONS, 'bdist_dmg': DMG_OPTIONS },
    executables = EXECUTABLES
)
