using System.Runtime.ExceptionServices;

namespace Aoc;

public class Day06
{
    public Day06 Part1()
    {
        var res = 1;

        var lines = File.ReadAllLines("In/6").ToArray();
        var times = lines[0].Split(":")[1].Trim().Split(" ").Where(x => x != string.Empty).Select(int.Parse).ToArray();
        var distances = lines[1].Split(":")[1].Trim().Split(" ").Where(x => x != string.Empty).Select(int.Parse).ToArray();
        var races = times.Zip(distances).ToArray();

        foreach (var race in races)
        {
            var winningWays = FindWinningWays(race);
            res *= winningWays.Count;
        }

        Console.WriteLine($"Day06.1: {res}");

        return this;
    }

    private List<long> FindWinningWays((long, long) race)
    {
        var winningWays = new List<long>();

        var raceTime = race.Item1;
        var distanceToWin = race.Item2;

        for (int holdTime = 1; holdTime < raceTime - 1; holdTime++)
        {
            var distanceTravelled = (raceTime - holdTime) * holdTime;

            if (distanceTravelled > distanceToWin)
            {
                winningWays.Add(holdTime);
            }
        }

        return winningWays;
    }

    public Day06 Part2()
    {
        var lines = File.ReadAllLines("In/6").ToArray();
        var time = long.Parse(lines[0].Split(":")[1].Replace(" ", ""));
        var distance = long.Parse(lines[1].Split(":")[1].Replace(" ", ""));

        var winningWays = FindWinningWays((time, distance));

        Console.WriteLine($"Day06.2: {winningWays.Count}");

        return this;
    }
}
