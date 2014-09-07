import os

print('*** Current working directory')
initialWorkingDir = os.getcwd()
print(os.getcwd())
os.chdir(os.path.expanduser('~'))
print(os.getcwd())
os.chdir('..')
print(os.getcwd())
os.chdir(initialWorkingDir)
print(os.getcwd())

print("\n\n*** File and directory names")
print(os.path.join(os.getcwd(), "custom03.py"))
print(os.path.join(os.getcwd() + "\\", "custom03.py"))
print(os.path.expanduser('~'))
pathname = os.path.join(os.path.expanduser('~'), "Documents", "somefile.txt")
print(pathname)
(dirname, filename) = os.path.split(pathname)
print(dirname)
print(filename)
(shortname, extension) = os.path.splitext(filename)
print(shortname)
print(extension)

os.chdir("..")
print('\n\n*** Listing direcotries')
print(os.getcwd())
import glob
print(glob.glob('DiveIntoPython/*.pyc'))
print(glob.glob('DiveIntoPython/*'))
from customhelper import getScript
metadata = os.stat(getScript("custom03.py"))
print(metadata.st_mtime)
import time
print(time.localtime(metadata.st_mtime))
print(os.path.realpath(getScript('custom03.py')))
