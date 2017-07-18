var capital = Map("US" -> "Washington", "France" -> "Paris")
capital += ("Japan" -> "Tokyo")
capital("France")

val name = "abCdefJ"
val nameHasUpperCase = name.exists(_.isUpper)
