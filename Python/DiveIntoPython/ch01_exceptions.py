try:
    import chardet
except ImportError:
    chardet = None
if chardet:
    print('chardet found')
else:
    print('chardet not found')

try:
    from lxml import etree
except ImportError:
    from xml.etree import ElementTree as etree
print(dir(etree))
