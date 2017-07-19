import java.math.BigInteger

var capital = Map("US" -> "Washington", "France" -> "Paris")
capital += ("Japan" -> "Tokyo")
capital("France")

val name = "abCdefJ"
val nameHasUpperCase = name.exists(_.isUpper) // _.isUpper is an example of function literal

class MyClass(index: Int, name: String) // constructor will set two private fields index and name
val mc = new MyClass(1, "Yuriy")

val msg = "Hello, World"
val msg2: java.lang.String = "Hello again, world!"
//msg = "Can't reassign val, use var instead"
var greeting = "Hello, World!"
greeting = "Leave me alone, world!"

def max(x: Int, y: Int): Int = {
  if(x > y) x
  else y
}
max(3, 5)

def max2(x: Int, y: Int) = if (x > y) x else y
max2(3, 5)

var big = new BigInteger("123456789123456789123456789")

val greetStrings = new Array[String](3)
greetStrings(0) = "Hello"
greetStrings(1) = ", "
greetStrings(2) = "world! \n"
for(i <- 0 to 2 ) // same as 0.to(2)
  print(greetStrings(i)) // greetString(i) is the same as greetString.apply(i)

val numNames = Array("zero", "one", "two")

val oneTwo = List(1, 2)
val threeFour = List(3, 4)
val oneTwoThreeFour = oneTwo ::: threeFour
oneTwo + " and " + threeFour + " were not mutated."
"Thus, " + oneTwoThreeFour + " is a new list."


