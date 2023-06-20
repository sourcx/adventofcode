namespace AdventOfCode2015
{
    class Day03
    {
        public static void Solve()
        {
            // SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var visited = new HashSet<Tuple<int, int>>();
            var santaLocation = new Tuple<int, int>(0, 0);
            
            visited.Add(santaLocation);

            foreach (var instruction in File.ReadAllText("input/3").Trim())
            {
                santaLocation = Move(santaLocation, instruction);
                visited.Add(santaLocation);
            }

            Console.WriteLine($"Santa visited {visited.Count} houses.");
        }

        private static void SolvePart2()
        {
            var visited = new HashSet<Tuple<int, int>>();
            var santaLocation = new Tuple<int, int>(0, 0);
            var robotLocation = new Tuple<int, int>(0, 0);
            
            visited.Add(santaLocation);

            bool santasTurn = true;
            foreach (var instruction in File.ReadAllText("input/3").Trim())
            {
                if (santasTurn)
                {
                    santaLocation = Move(santaLocation, instruction);
                    visited.Add(santaLocation);
                }
                else
                {
                    robotLocation = Move(robotLocation, instruction);
                    visited.Add(robotLocation);
                }
                santasTurn = !santasTurn;
            }

            Console.WriteLine($"Santa visited {visited.Count} houses.");
        }

        private static Tuple<int, int> Move(Tuple<int, int> location, char instruction)
        {
            switch (instruction)
            {
                case '<':
                    return new Tuple<int, int>(location.Item1 - 1, location.Item2);
                case '^':
                    return new Tuple<int, int>(location.Item1, location.Item2 + 1);
                case '>':
                    return new Tuple<int, int>(location.Item1 + 1, location.Item2);
                case 'v':
                    return new Tuple<int, int>(location.Item1, location.Item2 - 1);
                default: 
                    throw new Exception($"Illegal instruction {instruction}.");
            }
        }
    }
}
