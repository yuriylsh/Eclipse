a_set = set(range(10))
print(a_set)
print({n**2 for n in a_set})
print({n for n in a_set if n % 2 == 0})
