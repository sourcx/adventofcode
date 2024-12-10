import scala.io.StdIn.readLine

@main def hello() =
  println("Hello, World! give name")
  val name = readLine()
  println(s"Hello, $name!")
