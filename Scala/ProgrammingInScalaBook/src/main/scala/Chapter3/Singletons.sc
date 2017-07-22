class ChecksumAccumulator3{
  private var sum = 0

  def add(value: Int): Unit = sum += value

  def checksum(): Int = ~(sum & 0xFF) + 1
}

import scala.collection.mutable

object ChecksumAccumulator3{
  private val cache = mutable.Map.empty[String, Int]

  def calculate(s: String): Int = cache.getOrElse(s, calculateAndCache(s))

  private def calculateAndCache(input: String): Int = {
    val checksum = calculateForString(input)
    cache += (input -> checksum)
    checksum
  }

  private def calculateForString(input: String): Int = {
    val accumulator = new ChecksumAccumulator3
    for (inputChar <- input) accumulator.add(inputChar.toByte)
    accumulator.checksum
  }
}

ChecksumAccumulator3.calculate("Every value is an object")