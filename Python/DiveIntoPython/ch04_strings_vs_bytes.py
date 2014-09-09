# -*- coding: windows-1252 -*-
by = b'abcd\x65'
print(by)
print(type(by))
print(len(by))
print(by[0])
barr = bytearray(by)
print(barr)
print(type(barr))
barr[0] = 102
print(barr)
#print(by.count(by.decode('ascii')))
a_string = '深入 Python'
print(a_string)
print(a_string.encode('utf-8'))
