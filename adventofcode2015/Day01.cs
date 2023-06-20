namespace AdventOfCode2015
{
    class Day01
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var instructions = File.ReadAllText("input/1");

            long floor = 0;

            foreach (var instruction in instructions)
            {
                switch (instruction)
                {
                    case '(':
                        floor += 1;
                        break;
                    case ')':
                        floor -= 1;
                        break;
                }
            }

            Console.WriteLine($"Santa ends up on floor {floor}.");
        }

        private static void SolvePart2()
        {
            var instructions = File.ReadAllText("input/1");

            long floor = 0;

            for (int pos = 1; pos < instructions.Length + 1; pos++)
            {
                char instruction = instructions[pos - 1];
                switch (instruction)
                {
                    case '(':
                        floor += 1;
                        break;
                    case ')':
                        floor -= 1;
                        break;
                }

                if (floor == -1)
                {
                    Console.WriteLine($"Santa ends up in the basement {floor} at position {pos}.");
                    return;
                }
            }
        }
    }
}
