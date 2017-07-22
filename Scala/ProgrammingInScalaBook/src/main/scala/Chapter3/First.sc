val oneTwo = List(1, 2)
val threeFour = List(3, 4)
val oneTwoThreeFour = oneTwo ::: threeFour
oneTwo + " and " + threeFour + " were not mutated."
"Thus, " + oneTwoThreeFour + " is a new list."

val twoThree = List(2, 3)
val oneTwoThree = 1 :: twoThree
// since Nil can be used as empty list and :: function is invoked on right operand
val _oneTwoTree = 1 :: 2 :: 3 :: Nil // same as ((((Nil).::(3)).::(2)).::(1))

val pair = (34, "Yuriy")
pair._1
pair._2

var jetSet = Set("Boeing", "Airbus")
jetSet + "SuhoiJet"
jetSet + "Boeing"

import scala.collection.mutable
var treasureMap = mutable.Map[Int, String]()
treasureMap += (1 -> "Go to island.")
treasureMap += (2 -> "Find big X on the ground.")
treasureMap += (3 -> "Dig.")
for(i <- 1 to 3) println(i + ". " + treasureMap(i))
treasureMap.mkString(" ")

import scala.io.Source
val path = "C:\\code\\bitbucket\\eclipse\\Scala\\ProgrammingInScalaBook\\src\\main\\scala\\Chapter3\\First.sc"
val lines = Source.fromFile(path).getLines() toList
val longestLine = lines.reduceLeft((a, b) => if(a.length > b.length) a else b)
def widthOfLength(s: String) = s.length.toString.length
val maxWidth = widthOfLength(longestLine)
for(line <- Source.fromFile(path).getLines()) println((" " * (maxWidth - widthOfLength(line))) + line.length + " | " + line)
