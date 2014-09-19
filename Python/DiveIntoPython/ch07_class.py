class PapayaWhip:
    pass

from ch07_iterators_fibonacci import Fib
fib = Fib(2000)
print(fib)
print(fib.__class__)
print(fib.__doc__)
print(Fib(100).max)
print(Fib(200).max)

print('\n\n*** How iterators are called')
fib = Fib(2000)
fib_iter = iter(fib)
try:
    while True:
        print(next(fib_iter))
except StopIteration:
    pass

print('\*\* Class variables')


class SomeClass:
    classVariable = 'initial value'

    def __init__(self):
        self.instanceVariable = "I'm instance variable"
print('\n\n*** Difference between class variables and instance variables')
sc1 = SomeClass()
sc2 = SomeClass()
print(sc1.classVariable)
print(sc2.classVariable)

sc1.classVariable = 'changed value'
print('\n' + sc1.classVariable)
print(sc2.classVariable)

print('\n' + sc1.__class__.classVariable)
sc1.__class__.classVariable = 'changing prototype'
print(sc1.classVariable)
print(sc2.classVariable)
