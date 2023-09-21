namespace AdventOfCode2015
{
    class Day14
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var reindeers = new List<Reindeer>();

            var lines = File.ReadAllLines("input/14");

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                var reindeer = new Reindeer
                {
                    Name = parts[0],
                    Speed = int.Parse(parts[3]),
                    FlyTime = int.Parse(parts[6]),
                    RestTime = int.Parse(parts[13])
                };
                reindeers.Add(reindeer);
            }

            var totalTime = 2503;
            Reindeer winner = null;

            foreach (var reindeer in reindeers)
            {
                var distance = reindeer.Travelled(totalTime);

                if (winner == null || distance > winner.Travelled(totalTime))
                {
                    winner = reindeer;
                }
            }

            Console.WriteLine($"The winner is {winner.Name} with distance: {winner.Travelled(totalTime)}");
        }

        private static void SolvePart2()
        {
            var reindeers = new List<Reindeer>();

            var lines = File.ReadAllLines("input/14");

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                var reindeer = new Reindeer
                {
                    Name = parts[0],
                    Speed = int.Parse(parts[3]),
                    FlyTime = int.Parse(parts[6]),
                    RestTime = int.Parse(parts[13])
                };
                reindeers.Add(reindeer);
            }

            var totalTime = 2503;

            foreach (var seconds in Enumerable.Range(1, totalTime))
            {
                var highestDistance = 0;

                foreach (var reindeer in reindeers)
                {
                    var distance = reindeer.Travelled(seconds);

                    if (distance > highestDistance)
                    {
                        highestDistance = distance;
                    }
                }

                foreach (var reindeer in reindeers)
                {
                    if (reindeer.Travelled(seconds) == highestDistance)
                    {
                        reindeer.TicksInLead++;
                    }
                }
            }

            Reindeer winner = null;

            foreach (var reindeer in reindeers)
            {
                Console.WriteLine($"{reindeer.Name} {reindeer.TicksInLead}");
                if (winner == null || reindeer.TicksInLead > winner.TicksInLead)
                {
                    winner = reindeer;
                }
            }

            Console.WriteLine($"The winner is {winner.Name} with ticks: {winner.TicksInLead}"); // 2495 too high, 1085 wrong
        }

        class Reindeer
        {
            public string Name { get; set; }
            public int Speed { get; set; }
            public int FlyTime { get; set; }
            public int RestTime { get; set; }
            public int TicksInLead { get; set; }

            public int Travelled(int time)
            {
                var cycleTime = FlyTime + RestTime;
                var cycles = time / cycleTime;
                var remainder = time % cycleTime;

                var distance = cycles * FlyTime * Speed;
                distance += remainder > FlyTime ? FlyTime * Speed : remainder * Speed;

                return distance;
            }
        }
    }
}
