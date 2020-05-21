from urllib.request import urlopen
from os.path import normpath
from os import getcwd

wrapper_time = urlopen("https://raw.githubusercontent.com/Kettle3D/fishScript/master/wrapper/time.fs").read().decode('utf-8')
wrapper_time_ = open(getcwd() + normpath("/wrapper/time.fs"), 'x')
wrapper_time_.write(wrapper_time)
wrapper_time_.close()
