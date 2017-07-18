var capital = Map("US" -> "Washington", "France" -> "Paris")
capital += ("Japan" -> "Tokyo")
capital("France")

val name = "abCdefJ"
val nameHasUpperCase = name.exists(_.isUpper) // _.isUpper is an example of function literal

class MyClass(index: Int, name: String) // constructor will set two private fields index and name
val mc = new MyClass(1, "Yuriy")

val msg = "Hello, World"
val msg2: java.lang.String = "Hello again, world!"
msg = "Can't reassign val, use var instead"
