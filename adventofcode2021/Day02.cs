namespace AdventOfCode2021
{
    class Day02
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            long horizontalPos = 0;
            long depth = 0;

            foreach (var line in File.ReadAllLines("input/2"))
            {
                var direction = line.Split(" ")[0];
                var amount = int.Parse(line.Split(" ")[1]);

                switch (direction)
                {
                    case "forward":
                        horizontalPos += amount;
                        break;
                    case "up":
                        depth -= amount;
                        break;
                    case "down":
                        depth += amount;
                        break;
                    default:
                        throw new Exception($"Unknown instruction {line}.");
                }
            }

            Console.WriteLine($"Horizontal pos * depth:  {horizontalPos * depth}.");
        }

        private static void SolvePart2()
        {
            long horizontalPos = 0;
            long depth = 0;
            long aim = 0;

            foreach (var line in File.ReadAllLines("input/2"))
            {
                var direction = line.Split(" ")[0];
                var amount = int.Parse(line.Split(" ")[1]);

                switch (direction)
                {
                    case "forward":
                        horizontalPos += amount;
                        depth += aim * amount;
                        break;
                    case "up":
                        aim -= amount;
                        break;
                    case "down":
                        aim += amount;
                        break;
                    default:
                        throw new Exception($"Unknown instruction {line}.");
                }
            }

            Console.WriteLine($"Horizontal pos * depth:  {horizontalPos * depth}.");
        }
    }
}
