a_dict = {'server': 'db.diveintopython3.org', 'database': 'mysql'}
print(a_dict)
print(a_dict['server'])
a_dict['database'] = 'blog'
print(a_dict)
a_dict['user'] = 'Yuriy'
print(a_dict)

from ch01_first import SUFFIXES
print(len(SUFFIXES))
print(1000 in SUFFIXES)
