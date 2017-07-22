class ChecksumAccumulator{
  private var sum = 0;

  def add(value: Int): Unit = {
    sum += value
  }

  def checksum(): Int = {
    return ~(sum &0xFF) + 1
  }
}

class ChecksumAccumulator2{
  private var sum = 0

  def add(value: Int) = sum += value

  def checksum() = ~(sum & 0xFF) + 1
}