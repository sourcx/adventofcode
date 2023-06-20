namespace AdventOfCode2021
{
    class Day06
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var fish = File.ReadAllText("input/6").Split(",").Select(int.Parse).ToList();

            var days = 80;
            for (int day = 0; day < days ; day++)
            {
                fish = NextDay(fish);
            }

            Console.WriteLine($"Fish after {days} interations is {fish.Count}.");
        }

        private static void SolvePart2()
        {
            var fish = File.ReadAllText("input/6").Split(",").Select(int.Parse);
            var fishCounts = new Dictionary<int, long>();

            for (int i = 0; i <= 8; i++)
            {
                fishCounts[i] = 0;
            }

            foreach (var f in fish)
            {
                fishCounts[f] += 1;
            }

            var days = 256;
            for (int day = 0; day < days ; day++)
            {
                fishCounts = NextDay(fishCounts);
            }

            var totalNrFishes = fishCounts.Aggregate(0L, (agg, fishCount) => agg += fishCount.Value);
            Console.WriteLine($"Fish after {days} interations is {totalNrFishes}.");
        }

        private static List<int> NextDay(List<int> fish)
        {
            var nextDayFish = new List<int>();
            var nrReproduced = 0;

            foreach (var timer in fish)
            {
                var newTimer = timer - 1;
                if (newTimer == -1)
                {
                    newTimer = 6;
                    nrReproduced += 1;
                }
                nextDayFish.Add(newTimer);
            }

            for (int i = 0; i < nrReproduced; i++)
            {
                nextDayFish.Add(8);
            }

            return nextDayFish;
        }

        private static Dictionary<int, long> NextDay(Dictionary<int, long> fishCounts)
        {
            var nextDayFishCounts = new Dictionary<int, long>();

            for (int i = 0; i <= 8; i++)
            {
                nextDayFishCounts[i] = fishCounts[(i + 1) % 9];
            }
            nextDayFishCounts[6] += fishCounts[0];

            return nextDayFishCounts;
        }
    }
}
