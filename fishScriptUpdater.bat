REM Download Python
REM powershell "Invoke-WebRequest https://www.python.org/ftp/python/3.8.3/python-3.8.3.exe -o %userprofile%\Downloads\python_installer.exe"
REM cd %userprofile%\Downloads
EEM python_installer.exe

REM Make all the folders and things
cd %appdata%
md fishScript\wrapper
cd fishScript
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/fishScript/0.1.2/fishScript.bat -o %appdata%\fishScript\fishScript.bat"
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/fishScript/master/fishScriptUpdater.bat -o %appdata%\fishScript\fishScriptInstaller.bat"

REM Put fishScript in the user's Start Menu
cd "%appdata%\Microsoft\Windows\Start Menu\Programs"
md fishScript

@echo off

set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"

echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%appdata%\Microsoft\Windows\Start Menu\Programs\fishScript\fishScript 0.1.2 Shell.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "%APPDATA%\fishScript\fishScript.bat" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%

cscript /nologo %SCRIPT%
del %SCRIPT%

@echo off

set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"

echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%appdata%\Microsoft\Windows\Start Menu\Programs\fishScript\fishScript Updater.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "%APPDATA%\fishScript\fishScriptInstaller.bat" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%

cscript /nologo %SCRIPT%
del %SCRIPT%