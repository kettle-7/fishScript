cd %appdata%\fishScript
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/fishScript/0.1.2/fishScript.py -o %appdata%\fishScript\fishScript.py
python fishScript.py