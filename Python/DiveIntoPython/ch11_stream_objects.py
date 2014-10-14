''' files are located at
https://github.com/pcsforeducation/diveintopython3/tree/master/examples'''
a_file = open('examples/russian.txt')
print((a_file.name, a_file.encoding, a_file.mode))
a_file.close()

print('\n\n*** to be able to output in encoding ohter than cp-1252')
'''based on http://stackoverflow.com/questions/14630288/
unicodeencodeerror-charmap-codec-cant-encode-character-maps-to-undefined'''
import codecs
import sys
print('original stdout encoding: ' + sys.stdout.encoding)
'''if sys.stdout.encoding is not compatible with russian.txt characters,
   it will throw exception trying to encodo into that uncompatible encoding'''
sys.stdout = codecs.getwriter('utf-8')(sys.stdout.buffer, 'strict')


print('\n\n*** Reading data from text file')
a_file = open('examples/russian.txt', encoding='utf-8')
print(a_file.read())
print(a_file.read())
print(a_file.read())
a_file.seek(0)  # the argument is bytes
print(a_file.read(17))  # number of characters, not bytes
print(a_file.read(1))
print(a_file.read(1))
print(a_file.tell())  # but this returns number of bytes

print('\n\n*** Closing a file')
a_file.close()
try:
    a_file.read()
    a_file.seek(0)
except ValueError as e:
    print(e)
print(a_file.closed)

print('\n\n*** Automatically closing files')
with open('examples/russian.txt', encoding='utf-8') as a_file:
    a_file.seek(24)
    a_character = a_file.read(1)
    print(a_character)

print('\n\n*** Reading one line at a time')
line_number = 0
with open('examples/favorite-people.txt', encoding='utf-8') as a_file:
    for a_line in a_file:
        line_number += 1
        print('{:>4} {}'.format(line_number, a_line.rstrip()))

print('\n\n*** writing to a file')
with open('examples/log.txt', mode='w', encoding='utf-8') as a_file:
    a_file.write('test succeeded')
with open('examples/log.txt', encoding='utf-8') as a_file:
    print(a_file.read())
with open('examples/log.txt', mode='a', encoding='utf-8') as a_file:
    a_file.write('and again')
with open('examples/log.txt', encoding='utf-8') as a_file:
    print(a_file.read())

print('\n\n***Reading binary files')
an_image = open('examples/beauregard.jpg', mode='rb')
print((an_image.mode, an_image.name))
try:
    print(an_image.encoding)
except AttributeError as e:
    print(e)
print(an_image.tell())
data = an_image.read(3)
print(data)
print(type(data))
print(an_image.tell())
an_image.seek(0)
data = an_image.read()
print(len(data))
an_image.close()

print('\n\n*** Streams from non-file sources')
'''anything with read(optional_size_parameter) is a stream'''
a_string = 'PapayaWhip is the new black.'
import io
a_file = io.StringIO(a_string)
print(a_file.read())
a_file.seek(0)
print(a_file.read(10))
print(a_file.tell())
