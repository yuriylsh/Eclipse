import os
def getScriptsDir():
	(dirname, filename) = os.path.split(os.path.realpath(__file__))
	return dirname


def getScript(scriptFileName):
	return os.path.join(getScriptsDir(), scriptFileName)

if __name__ == '__main__':
	print(getScriptsDir())