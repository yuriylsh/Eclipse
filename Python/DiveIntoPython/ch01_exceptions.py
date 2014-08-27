try:
    import chardet #looks like this does not work in Python 2
catch ImportError: 
    chardet = None
if chardet:
    print('chardet found')
else:
    print('chardet not found')

try:
    from lxml import etree
catch ImportError:
    from xml.etree import ElementTree as etree
print(dir(etree))
