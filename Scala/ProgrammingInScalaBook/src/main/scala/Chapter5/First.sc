val rawString =
  """ This is a
    "raw" string """

val rawString2 =
  """ |This is a
    |"raw" string """.stripMargin

val name = "Yruiy"
s"Hello, $name"
s"Hello, $name ${3 * 5} times!"

raw"No\\\\escape, $name!"
f"${math.Pi}%.5f"