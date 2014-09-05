a_list = ['a', 'b', 'mpilgrim', 'z', 'example']
print(a_list)
print(a_list[0])
print(a_list[-1])
negative_index = -1
print(a_list[negative_index] == (a_list[len(a_list) + negative_index]))

print("\n*** slicing a list")
print(a_list[1:3])
print(a_list[:3])
print(a_list[:])
# compares by elements, but the lists are not the same, a copy has been created
print(a_list == a_list[:])

print("\n*** addint items to list")
a_list = ["a"]
print(a_list)
a_list = a_list + [2.0, 3]
print(a_list)
a_list.append(True)
print(a_list)
a_list.extend(["four", 'c'])
print(a_list)
a_list.insert(0, 'first!')
print(a_list)

print("\n\n*** Searching for values in a list")
a_list = ['a', 'b', 'new', 'mpilgrim', 'new']
print(a_list.count('new'))
print('new' in a_list)
print(a_list.index('mpilgrim'))
try:
    print(a_list.index('xxxz'))
except ValueError:
    print(" ".join([str(a_list), 'does not contain', "'xxxz'"]))


print("\n\n*** Removing items from list")
a_list = ['a', 'b', 'new', 'mpilgrim', 'new']
print(a_list)  # ['a', 'b', 'new', 'mpilgrim', 'new']
a_list_copy = a_list[:]
del a_list[1]
print(a_list)  # ['a', 'new', 'mpilgrim', 'new']
a_list.remove('new')
print(a_list)  # ['a', 'mpilgrim', 'new']


while True:
    try:
        a_list.remove("new")
    except ValueError:
        break
print(a_list)  # ['a', mpilgrim']

a_list = ['a', 'b', 'new', 'mpilgrim']
print(a_list)  # ['a', 'b', 'new', 'mpilgrim']
print(a_list.pop())  # mpilgrim
print(a_list)  # ['a', 'b', 'new']
print(a_list.pop(1))  # b
print(a_list)  # ['a', 'new']
while True:
    try:
        a_list.pop()
    except IndexError:
        break
print (a_list)  # []
