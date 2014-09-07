a_list = [1, 9, 8, 4]
print(a_list)
print([elem * 2 for elem in a_list])
import os, glob
from customhelper import getScriptsDir
print([(os.stat(f).st_size, os.path.realpath(f))
	for f in glob.glob(os.path.join(getScriptsDir(), "*.py"))
	if os.stat(f).st_size > 1200 ])

