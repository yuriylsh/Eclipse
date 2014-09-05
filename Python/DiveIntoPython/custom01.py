from ch01_first import approximate_size
# alternative to the lines above:
#		import ch01_first
#		print(dir(ch01_first))
print(approximate_size.__doc__)
import sys
print(sys.path)
sys.path.insert(0, 'some_dirctory_path_to_look_for_py_files')
print(type(sys.path))
import sys
print(sys.version)
