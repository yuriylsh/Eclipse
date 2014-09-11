def fib(max):
    a, b = 0, 1
    while a < max:
        yield a
        a, b = b, a + b

if __name__ == '__main__':
    print('*** Fibonacci')
    for fib_num in fib(2000):
        print(fib_num)
    print(list(fib(2000)))


def factorial(max):
    if max < 1:
        return
    yield (1, 1)
    x = 2
    total = 1
    while x <= max:
        total = total * x
        yield (x, total)
        x = x + 1

if __name__ == '__main__':
    print('\n\n*** Factorial')
    for (num, fact) in factorial(10):
        print('{0}! = {1}'.format(num, fact))
