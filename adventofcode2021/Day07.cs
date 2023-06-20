namespace AdventOfCode2021
{
    class Day07
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var crabs = File.ReadAllText("input/7").Split(",").Select(int.Parse);

            var distances = new Dictionary<int, int>();

            foreach(var x in crabs.Distinct())
            {
                foreach(var y in crabs)
                {
                    if (!distances.ContainsKey(x))
                    {
                        distances[x] = 0;
                    }
                    distances[x] += Math.Abs(y - x);
                }
            }

            Console.WriteLine($"Fuel cost: {distances.Values.Min()}");
        }

        private static void SolvePart2()
        {
            var crabs = File.ReadAllText("input/7").Split(",").Select(int.Parse);

            var distances = new Dictionary<int, int>();

            for (int x = 0; x < crabs.Max(); x++)
            {
                foreach(var y in crabs)
                {
                    if (!distances.ContainsKey(x))
                    {
                        distances[x] = 0;
                    }

                    distances[x] += Math.Abs(y - x) * (Math.Abs(y - x) + 1) / 2;
                }
            }

            Console.WriteLine($"Fuel cost: {distances.Values.Min()}");
        }
    }
}
