namespace AdventOfCode2015
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
            long totalPaperNeeded = 0;

            foreach (var line in File.ReadAllLines("input/2"))
            {
                Console.WriteLine(line);
                totalPaperNeeded += PaperNeededFor(line);
            }

            Console.WriteLine($"Total paper needed: {totalPaperNeeded}");
        }

        private static void SolvePart2()
        {
            long totalRibbonNeeded = 0;

            foreach (var line in File.ReadAllLines("input/2"))
            {
                Console.WriteLine(line);
                totalRibbonNeeded += RibbonNeededFor(line);
            }

            Console.WriteLine($"Total feet of ribbon needed: {totalRibbonNeeded}");
        }

        private static long PaperNeededFor(string line)
        {
            var parts = line.Split("x").Select(long.Parse).ToArray();

            long x = parts[0] * parts[1];
            long y = parts[0] * parts[2];
            long z = parts[1] * parts[2];

            var areas = new List<long>() { x, y, z };

            return areas.Aggregate(0L, (agg, area) => agg + (2 * area)) + areas.Min();
        }

        private static long RibbonNeededFor(string line)
        {
            var sides = line.Split("x").Select(long.Parse).ToList();
            var bowRibbon = sides.Aggregate(1L, (agg, side) => agg *= side);
            sides.Remove(sides.Max());
            var wrapRibbon = 2 * sides[0] + 2 * sides[1];

            return bowRibbon + wrapRibbon;
        }
    }
}
