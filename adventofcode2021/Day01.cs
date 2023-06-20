namespace AdventOfCode2021
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
            var lines = File.ReadAllLines("input/1");
            long lastVal = int.Parse(lines[0]);
            long nrIncreases = 0;

            foreach (var line in lines.Skip(1))
            {
                var val = int.Parse(line);
                if (val > lastVal)
                {
                    nrIncreases += 1;
                }
                lastVal = val;
            }

            Console.WriteLine($"Nr of increases is {nrIncreases}.");
        }

        private static void SolvePart2()
        {
            var measurements = File.ReadAllLines("input/1").Select(int.Parse).ToArray();
            long lastVal = measurements[0] + measurements[1] + measurements[2];
            long nrIncreases = 0;
            
            for (var i = 1; i < measurements.Length - 2; ++i)
            {
                var val = measurements[i] + measurements[i + 1] + measurements[i + 2];
                if (val > lastVal)
                {
                    nrIncreases += 1;
                }
                lastVal = val;
            }

            Console.WriteLine($"Nr of windows increases is {nrIncreases}.");
        }
    }
}
