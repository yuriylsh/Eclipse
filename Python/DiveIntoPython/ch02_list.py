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
print(a_list == a_list[:]) # compares by elements, but the lists are not the same, a copy has been created

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

