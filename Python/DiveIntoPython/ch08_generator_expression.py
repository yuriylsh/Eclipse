unique_characters = {'E', 'D', 'M', 'O', 'N', 'S', 'R', 'Y'}
gen = (ord(c) for c in unique_characters)
print(gen)
print(next(gen))
print(next(gen))
'''
Using a generator expression instead of a list comprehension
can save both CPU and RAM .
If you’re building an list just to throw it away
(e.g. passing it to tuple() or set()),
use a generator expression instead!
'''
print(tuple(ord(c) for c in unique_characters))

# same as above but using generator function
#(see ch09_generators_essence.py):


def ord_map(a_string):
    for c in a_string:
        yield ord(c)

print(ord_map(unique_characters))
