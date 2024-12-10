@main def day01() =
  println("Day01")
  val path: os.Path = os.pwd / "src" / "main" / "input" / "01"
  val lines = os.read.lines(path)
  println(lines)

  val numbersPerLine = os.read.lines(path).map { line =>
    line.filter(_.isDigit).map(_.asDigit).sum
  }

  println(numbersPerLine)
