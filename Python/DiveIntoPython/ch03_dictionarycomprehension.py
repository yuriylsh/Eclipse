from customhelper import getScriptsDir
import os
import glob
fileList = glob.glob(os.path.join(getScriptsDir(), '*.py'))
metadataDict = {f: os.stat(f).st_size for f in fileList}
print(metadataDict)

print('\n\n*** Full power of comprehension')
from ch01_first import approximate_size
metadataDict = {f: os.stat(f) for f in fileList}
humansizeDict = {os.path.splitext(f)[0]: approximate_size(meta.st_size)
                 # list of D's (key,value) pairs, as 2-tuple
                 for f, meta in metadataDict.items()
                 if meta.st_size > 1200}
print(humansizeDict)


print('\n\n*** Inverting keys and values')
a_dict = {'a': 1, 'b': 2, 'c': 3}
print(a_dict)
print({value: key for key, value in a_dict.items()})
