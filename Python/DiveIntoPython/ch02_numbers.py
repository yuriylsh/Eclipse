from __future__ import division
print(type(1))
print("number types")
print(isinstance(1, int))
print(type(2.0))

print("coercing")
print(float(2))
print(int(2.0))

print("common math operations")
print(11 / 2)  # is 5 in Python 2 without __future__
print(11 // 2)
print(11 % 2)
print(11 ** 2)

print("Fractions")
import fractions
x = fractions.Fraction(1, 3)
print(x)
print(x * 2)
print(fractions.Fraction(6, 4))
try:
    print(fractions.Fraction(0, 0))
except ZeroDivisionError:
    print("fractoins are smart!")

print("Trigonometry")
import math
print(math.pi)
print(math.sin(math.pi / 2))
print(math.tan(math.pi / 4))


def is_it_true(anything):
    if anything:
        print("yes, it's true")
    else:
        print("no, it's false")
print("Numbers as booleans(bad idea)")
is_it_true(1)
is_it_true(0)
is_it_true(-1)
