; WinMossInstaller.nsi
;
;--------------------------------

!include "MUI2.nsh"

; The name of the installer
Name "WinMossInstaller"

; The file to write
OutFile "WinMossInstaller.exe"

; The default installation directory
InstallDir $DESKTOP\WinMoss

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\WinMoss" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel user

; Interface
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP "WinMossHeader.bmp"
!define MUI_ABORTWARNING

;--------------------------------

; Pages
!insertmacro MUI_PAGE_LICENSE "WinMossLicense.txt"
;Page components
!insertmacro MUI_PAGE_COMPONENTS
;Page directory
!insertmacro MUI_PAGE_DIRECTORY
;Page instfiles
!insertmacro MUI_PAGE_INSTFILES

;UninstPage uninstConfirm
!insertmacro MUI_UNPAGE_CONFIRM
;UninstPage instfiles
!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_LANGUAGE "English"

; Install Types
InstType "x86"
InstType "x64"
InstType "Custom"

;--------------------------------

; The stuff to install
Section "WinMoss (required)" WinMoss

	SectionIn RO
	
	; Set output path to the installation directory.
	SetOutPath $INSTDIR

	; Put file there
	File WinMoss.exe
	File mosswin.pl
	File ICSharpCode.SharpZipLib.dll
	
	; Write uninstall info for windows
	WriteRegStr HKLM  "Software\Microsoft\Windows\CurrentVersion\Uninstall\WinMoss"\
		"DisplayName" "WinMoss"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WinMoss"\
		"UninstallString" '"$INSTDIR\uninstall.exe"'
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WinMoss"\
		"NoModify" 1
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WinMoss"\
		"NoRepair" 1
	WriteUninstaller "uninstall.exe"
  
SectionEnd ; end the section

; Installs Active Perl
Section "Active Perl x86" Perl86
	SectionIn 1
	InitPluginsDir
	SetOutPath $INSTDIR
	File ActivePerl-5.10.1.1006-MSWin32-x86-291086.msi
	ExecWait '"msiexec" /i ActivePerl-5.10.1.1006-MSWin32-x86-291086.msi'
	Delete ActivePerl-5.10.1.1006-MSWin32-x86-291086.msi
SectionEnd

Section "Active Perl x64" Perl64
	SectionIn 2
	InitPluginsDir
	SetOutPath $INSTDIR
	File ActivePerl-5.10.1.1006-MSWin32-x64-291086.msi
	ExecWait '"msiexec" /i ActivePerl-5.10.1.1006-MSWin32-x64-291086.msi'
	Delete ActivePerl-5.10.1.1006-MSWin32-x64-291086.msi
SectionEnd

; Start Menu Icons
Section "Start Menu Shortcuts" ShortCuts
	SectionIn 1 2
	CreateDirectory "$SMPROGRAMS\WinMoss"
	CreateShortCut "$SMPROGRAMS\WinMoss\Uninstall.lnk" "$INSTDIR\uninstall.exe" ""\
		"$INSTDIR\uninstall.exe" 0
	CreateShortCut "$SMPROGRAMS\WinMoss\WinMoss.lnk" "$INSTDIR\WinMoss.exe"\
		"" "$INSTDIR\WinMoss.exe" 0
SectionEnd

; Descriptions
LangString DESC_WinMoss ${LANG_ENGLISH} "Installs the WinMss tool to your machine"
LangString DESC_ShortCuts ${LANG_ENGLISH} "Adds short cuts to you start menu"
LangString DESC_Perl86 ${LANG_ENGLISH} "Installs Perl for x86 machine"
LangString DESC_Perl64 ${LANG_ENGLISH} "Installs Perl for x64 machine"

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
	!insertmacro MUI_DESCRIPTION_TEXT ${WinMoss} $(DESC_WinMoss)
	!insertmacro MUI_DESCRIPTION_TEXT ${ShortCuts} $(DESC_ShortCuts)
	!insertmacro MUI_DESCRIPTION_TEXT ${Perl86} $(DESC_Perl86)
	!insertmacro MUI_DESCRIPTION_TEXT ${Perl64} $(DESC_Perl64)
!insertmacro MUI_FUNCTION_DESCRIPTION_END

; Uninstaller
Section "Uninstall"
	  
	; Remove registry keys
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WinMoss"
	DeleteRegKey HKLM SOFTWARE\NSIS_WinMoss

	; Remove files and uninstaller
	Delete $INSTDIR\WinMoss.exe
	Delete $INSTDIR\mosswin.pl
	Delete $INSTDIR\ICSharpCode.SharpZipLib.dll
	Delete $INSTDIR\uninstall.exe

	; Remove shortcuts, if any
	Delete "$SMPROGRAMS\WinMoss\WinMoss.lnk"
	Delete "$SMPROGRAMS\WinMoss\Uninstall.lnk"
	Delete "$SMPROGRAMS\WinMoss\*.*"

	; Remove directories used
	RMDir "$SMPROGRAMS\WinMoss"
	RMDir "$INSTDIR"
	
SectionEnd

  