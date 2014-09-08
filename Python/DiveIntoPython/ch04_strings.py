username = 'Mark'
password = "abracadabra"
print("{0}'s password is {1}".format(username, password))

import ch01_first
print('\n\n*** Compound fields')
si_suffixes = ch01_first.SUFFIXES[1000]
print(si_suffixes)
print("1000{0[0]} = 1{0[1]}".format(si_suffixes))
import sys
print("1MB = 1000{0.modules[ch01_first].SUFFIXES[1000][0]}".format(sys))

print("\n\n*** Format specifiers")
# https://docs.python.org/3.1/library/string.html#format-specification-mini-language
print("{0:.2f}".format(13.345))
print("{0:.1e}".format(13.345))

print("\n\n*** Common methods")
s = '''Finished files are the re-
sult of years of scienti-
fic study combined with the
experience of years.'''
print(s.splitlines())
print(s.lower())
print(s.lower().count('f'))
query = 'user=pilgrim&database=master&password=PapayaWhip'
a_list = query.split('&')
print(a_list)
#urllib.parse.parse_qs() is 'proper' way on handling query strings
a_list_of_lists = [v.split('=', 1) for v in a_list] # 1 is number of splits
print(a_list_of_lists)
a_dict = dict(a_list_of_lists)
print a_dict

print('\n\n*** Slicing a string')
a_string = 'My alphabet starts where your alphabet ends.'
print(a_string[3:11])
print(a_string[3:-3])
print(a_string[:18])
print(a_string[18:])
