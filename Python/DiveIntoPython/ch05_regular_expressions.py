s = '100 NORTH MAIN ROAD'
print(s)
print(s.replace('ROAD', 'RD.'))
s = '100 NORTH BROAD ROAD'
print(s)
print(s.replace('ROAD', 'RD.'))
print(s[:-4] + s[-4:].replace("ROAD", "RD."))
import re
print(re.sub('ROAD$', 'RD.', s))

print('\n\n*** More complex regex')
s = '100 BROAD'
print(s)
print(re.sub('ROAD$', 'RD.', s))
print(re.sub('\\bROAD$', 'RD.', s))
print(re.sub(r'\bROAD$', 'RD.', s))
s = '100 BROAD ROAD APT. 3'
print(s)
print(re.sub(r'\bROAD$', 'RD.', s))
print(re.sub(r'\bROAD\b', 'RD.', s))

print('\n\n*** Even more regular expressions')
compiled = re.compile('(\d{1,3})\s*(\d{1,3})')
print(compiled.search('1234 923 33').groups())
print(compiled.findall('1234 923 33'))
