a_set = {1}
print(a_set)
a_set = set()  # a_set = {} would create a dictionary
print(a_set)

print("\n\n*** Adding items to a set")
a_set = {1, 2}
print(a_set)
a_set.add(4)
print(a_set)
print(len(a_set))
a_set.update({2, 3, 4, 6})
print(a_set)
a_set.update({3, 4, 5}, {10, 20, 30})
print(a_set)
a_set.update([40, 50, 60])
print(a_set)

print("\n\n*** Removing items from a set")
a_set = {1, 3, 6, 10, 15, 21, 28, 36, 45}
print(a_set)
a_set.discard(10)
print(a_set)
a_set.discard(10)  # no exception is thrown
a_set.remove(21)
print(a_set)
try:
    a_set.remove(21)
except KeyError:
    print('set.remove(item) for non-existent item causes KeyError')
print(a_set.pop())
print(a_set)
a_set.clear()
print(a_set)
try:
    a_set.pop()
except KeyError:
    print('set().pop() causes KeyError exception.')


a_set = {2, 4, 5, 9, 12, 21, 30, 51, 76, 127, 195}
b_set = {1, 2, 3, 5, 6, 8, 9, 12, 15, 17, 18, 21}
print("\n\n*** Common set operations")
print(30 in a_set)
print(31 in a_set)
print(a_set.union(b_set))
print(a_set.intersection(b_set))
print(a_set.difference(b_set))
print(a_set.symmetric_difference(b_set))

a_set = {1, 2, 3}
b_set = {1, 2, 3, 4}
print(a_set.issubset(b_set))
print(b_set.issuperset(a_set))
a_set.add(5)
print(a_set.issubset(b_set))
print(b_set.issuperset(a_set))
