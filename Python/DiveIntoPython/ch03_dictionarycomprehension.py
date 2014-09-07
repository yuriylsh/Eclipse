from customhelper import getScriptsDir
import os, glob
fileList = glob.glob(os.path.join(getScriptsDir(), '*.py'))
metadataDict = {f: os.stat(f).st_size for f in fileList}
print(metadataDict)