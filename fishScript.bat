cd %appdata%\fishScript
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/fishScript/0.1.2/fishScript.py -o %appdata%\fishScript\fishScript.py
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/fishScript/0.1.2/fsUpdater.py -o %appdata%\fishScript\fsUpdater.py
python fsUpdater.py
python fishScript.py