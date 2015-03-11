############################################################################################
#      NSIS Installation Script created by NSIS Quick Setup Script Generator v1.09.18
#               Entirely Edited with NullSoft Scriptable Installation System                
#              by Vlasis K. Barkas aka Red Wine red_wine@freemail.gr Sep 2006               
############################################################################################

!define APP_NAME "Mistletoe"
!define COMP_NAME "Full Sail University"
!define WEB_SITE "https://edo.fullsail.edu/underdog/projects/mistletoe"
!define VERSION "00.8.1.00"
!define COPYRIGHT "Full Sail University © 2015"
!define DESCRIPTION "Mistletoe is a GUI for MOSS (Measure Of Software Similarity) system."
!define INSTALLER_NAME "C:\Work\NSIS\Output\Mistletoe\setup.exe"
!define MAIN_APP_EXE "mistletoe.exe"
!define INSTALL_TYPE "SetShellVarContext current"
!define REG_ROOT "HKCU"
!define REG_APP_PATH "Software\Microsoft\Windows\CurrentVersion\App Paths\${MAIN_APP_EXE}"
!define UNINSTALL_PATH "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}"

!define REG_START_MENU "Start Menu Folder"

var SM_Folder

######################################################################

VIProductVersion  "${VERSION}"
VIAddVersionKey "ProductName"  "${APP_NAME}"
VIAddVersionKey "CompanyName"  "${COMP_NAME}"
VIAddVersionKey "LegalCopyright"  "${COPYRIGHT}"
VIAddVersionKey "FileDescription"  "${DESCRIPTION}"
VIAddVersionKey "FileVersion"  "${VERSION}"

######################################################################

SetCompressor ZLIB
Name "${APP_NAME}"
Caption "${APP_NAME}"
OutFile "${INSTALLER_NAME}"
BrandingText "${APP_NAME}"
XPStyle on
InstallDirRegKey "${REG_ROOT}" "${REG_APP_PATH}" ""
InstallDir "$PROGRAMFILES\Mistletoe"

######################################################################

!include "MUI.nsh"

!define MUI_ABORTWARNING
!define MUI_UNABORTWARNING

!insertmacro MUI_PAGE_WELCOME

!ifdef LICENSE_TXT
!insertmacro MUI_PAGE_LICENSE "${LICENSE_TXT}"
!endif

!insertmacro MUI_PAGE_DIRECTORY

!ifdef REG_START_MENU
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "Mistletoe"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${REG_ROOT}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${UNINSTALL_PATH}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${REG_START_MENU}"
!insertmacro MUI_PAGE_STARTMENU Application $SM_Folder
!endif

!insertmacro MUI_PAGE_INSTFILES

!define MUI_FINISHPAGE_RUN "$INSTDIR\${MAIN_APP_EXE}"
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM

!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

######################################################################

Section -MainProgram
${INSTALL_TYPE}
SetOverwrite ifnewer
SetOutPath "$INSTDIR"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\bz2.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\library.zip"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\Microsoft.VC90.CRT.manifest"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\mistletoe.exe"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\mistletoe.ui"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\msvcm90.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\msvcp90.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\MSVCR90.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\pyexpat.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\pyside-python2.7.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\PySide.QtCore.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\PySide.QtGui.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\PySide.QtNetwork.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\PySide.QtUiTools.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\PySide.QtXml.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\python27.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\pythoncom27.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\pywintypes27.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\QtCore4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\QtGui4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\QtNetwork4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\QtXml4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\select.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\shiboken-python2.7.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\unicodedata.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\win32api.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\win32com.shell.shell.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\win32pipe.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\_ctypes.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\_hashlib.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\_socket.pyd"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\_ssl.pyd"
SetOutPath "$INSTDIR\imageformats"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qgif4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qgifd4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qico4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qicod4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qjpeg4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qjpegd4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qmng4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qmngd4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qsvg4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qsvgd4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qtga4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qtgad4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qtiff4.dll"
File "C:\Work\Mistletoe\src\build\exe.win32-2.7\imageformats\qtiffd4.dll"
SectionEnd

######################################################################

Section -Icons_Reg
SetOutPath "$INSTDIR"
WriteUninstaller "$INSTDIR\uninstall.exe"

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_WRITE_BEGIN Application
CreateDirectory "$SMPROGRAMS\$SM_Folder"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
!ifdef WEB_SITE
WriteIniStr "$INSTDIR\${APP_NAME} website.url" "InternetShortcut" "URL" "${WEB_SITE}"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk" "$INSTDIR\${APP_NAME} website.url"
!endif
!insertmacro MUI_STARTMENU_WRITE_END
!endif

!ifndef REG_START_MENU
CreateDirectory "$SMPROGRAMS\Mistletoe"
CreateShortCut "$SMPROGRAMS\Mistletoe\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
!ifdef WEB_SITE
WriteIniStr "$INSTDIR\${APP_NAME} website.url" "InternetShortcut" "URL" "${WEB_SITE}"
CreateShortCut "$SMPROGRAMS\Mistletoe\${APP_NAME} Website.lnk" "$INSTDIR\${APP_NAME} website.url"
!endif
!endif

WriteRegStr ${REG_ROOT} "${REG_APP_PATH}" "" "$INSTDIR\${MAIN_APP_EXE}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayName" "${APP_NAME}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "UninstallString" "$INSTDIR\uninstall.exe"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayIcon" "$INSTDIR\${MAIN_APP_EXE}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayVersion" "${VERSION}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "Publisher" "${COMP_NAME}"

!ifdef WEB_SITE
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "URLInfoAbout" "${WEB_SITE}"
!endif
SectionEnd

######################################################################

Section Uninstall
${INSTALL_TYPE}
Delete "$INSTDIR\bz2.pyd"
Delete "$INSTDIR\library.zip"
Delete "$INSTDIR\Microsoft.VC90.CRT.manifest"
Delete "$INSTDIR\mistletoe.exe"
Delete "$INSTDIR\mistletoe.ui"
Delete "$INSTDIR\msvcm90.dll"
Delete "$INSTDIR\msvcp90.dll"
Delete "$INSTDIR\MSVCR90.dll"
Delete "$INSTDIR\pyexpat.pyd"
Delete "$INSTDIR\pyside-python2.7.dll"
Delete "$INSTDIR\PySide.QtCore.pyd"
Delete "$INSTDIR\PySide.QtGui.pyd"
Delete "$INSTDIR\PySide.QtNetwork.pyd"
Delete "$INSTDIR\PySide.QtUiTools.pyd"
Delete "$INSTDIR\PySide.QtXml.pyd"
Delete "$INSTDIR\python27.dll"
Delete "$INSTDIR\pythoncom27.dll"
Delete "$INSTDIR\pywintypes27.dll"
Delete "$INSTDIR\QtCore4.dll"
Delete "$INSTDIR\QtGui4.dll"
Delete "$INSTDIR\QtNetwork4.dll"
Delete "$INSTDIR\QtXml4.dll"
Delete "$INSTDIR\select.pyd"
Delete "$INSTDIR\shiboken-python2.7.dll"
Delete "$INSTDIR\unicodedata.pyd"
Delete "$INSTDIR\win32api.pyd"
Delete "$INSTDIR\win32com.shell.shell.pyd"
Delete "$INSTDIR\win32pipe.pyd"
Delete "$INSTDIR\_ctypes.pyd"
Delete "$INSTDIR\_hashlib.pyd"
Delete "$INSTDIR\_socket.pyd"
Delete "$INSTDIR\_ssl.pyd"
Delete "$INSTDIR\imageformats\qgif4.dll"
Delete "$INSTDIR\imageformats\qgifd4.dll"
Delete "$INSTDIR\imageformats\qico4.dll"
Delete "$INSTDIR\imageformats\qicod4.dll"
Delete "$INSTDIR\imageformats\qjpeg4.dll"
Delete "$INSTDIR\imageformats\qjpegd4.dll"
Delete "$INSTDIR\imageformats\qmng4.dll"
Delete "$INSTDIR\imageformats\qmngd4.dll"
Delete "$INSTDIR\imageformats\qsvg4.dll"
Delete "$INSTDIR\imageformats\qsvgd4.dll"
Delete "$INSTDIR\imageformats\qtga4.dll"
Delete "$INSTDIR\imageformats\qtgad4.dll"
Delete "$INSTDIR\imageformats\qtiff4.dll"
Delete "$INSTDIR\imageformats\qtiffd4.dll"
 
RmDir "$INSTDIR\imageformats"
 
Delete "$INSTDIR\uninstall.exe"
!ifdef WEB_SITE
Delete "$INSTDIR\${APP_NAME} website.url"
!endif

RmDir "$INSTDIR"

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_GETFOLDER "Application" $SM_Folder
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk"
!ifdef WEB_SITE
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk"
!endif
RmDir "$SMPROGRAMS\$SM_Folder"
!endif

!ifndef REG_START_MENU
Delete "$SMPROGRAMS\Mistletoe\${APP_NAME}.lnk"
!ifdef WEB_SITE
Delete "$SMPROGRAMS\Mistletoe\${APP_NAME} Website.lnk"
!endif
RmDir "$SMPROGRAMS\Mistletoe"
!endif

DeleteRegKey ${REG_ROOT} "${REG_APP_PATH}"
DeleteRegKey ${REG_ROOT} "${UNINSTALL_PATH}"
SectionEnd

######################################################################

