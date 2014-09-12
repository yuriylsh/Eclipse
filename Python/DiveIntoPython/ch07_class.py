class PapayaWhip:
    pass

from ch07_iterators_fibonacci import Fib
fib = Fib(2000)
print(fib)
print(fib.__class__)
print(fib.__doc__)
print(Fib(100).max)
print(Fib(200).max)

print('\n\n*** How iterators a called')
fib = Fib(2000)
fib_iter = iter(fib)
try:
    while True:
        print(next(fib_iter))
except StopIteration:
    pass
