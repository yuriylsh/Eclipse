a_set = {1}
print(a_set)
a_set = set() # a_set = {} would create a dictionary
print(a_set)

print("\n\n*** Adding items to a set")
a_set = {1, 2}
print(a_set)
a_set.add(4)
print(a_set)
print(len(a_set))
a_set.update({2, 3, 4, 6})
print(a_set)
a_set.update({3,4,5}, {10, 20, 30})
print(a_set)
a_set.update([40, 50, 60])
print(a_set)
