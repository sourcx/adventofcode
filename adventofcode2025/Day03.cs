namespace adventofcode2025;
using System.Numerics;
class Day03
{
    public void Run()
    {
        Console.WriteLine(GetType().Name);

        Part1();
        Part2();
    }

    private void Part1()
    {
        var sumJoltage = 0L;

        foreach (var line in File.ReadAllLines("testinput/3"))
        {
            var batteries = new List<long>();

            foreach (var c in line)
            {
                batteries.Add(long.Parse(c.ToString()));
            }

            long maxJoltage = 0;

            for (int i = 0; i < batteries.Count; i++)
            {
                for (int j = i + 1; j < batteries.Count; j++)
                {
                    var joltage = batteries[i] * 10 + batteries[j];

                    if (joltage > maxJoltage)
                    {
                        maxJoltage = joltage;
                    }
                }
            }

            sumJoltage += maxJoltage;
        }

        Console.WriteLine($"The Joltage sum is {sumJoltage}");
    }

    private void Part2()
    {
        var sumJoltage = BigInteger.Zero;

        foreach (var line in File.ReadAllLines("input/3"))
        {
            var batteries = new List<long>();

            foreach (var c in line)
            {
                batteries.Add(long.Parse(c.ToString()));
            }

            // Generate all combinations of 12 batteries

            var requiredLength = 12;
            var currentLength = batteries.Count;

            // Start removing the lowest numbers from the list until we reach 12.

            while (currentLength > requiredLength)
            {
                batteries = RemoveBattery(batteries);
                currentLength--;
            }

            Console.WriteLine($"Line: {line} -> Batteries: {string.Join(",", batteries)}");
            var lineJoltage = batteries.Aggregate(0L, (acc, val) => acc * 10 + val);
            sumJoltage += lineJoltage;
        }

        Console.WriteLine($"The Joltage sum is {sumJoltage}");
    }

    private List<long> RemoveBattery(List<long> batteries)
    {
        // Find the battery that when removed will result in the highest remaining number
        // We loop through all the options and remove each battery in turn, then calculate the resulting number.

        var currentHighestJoltage = BigInteger.Zero;

        var batteryToRemoveIndex = -1;

        for (int i = 0; i < batteries.Count; i++)
        {
            var testBatteries = new List<long>(batteries);
            testBatteries.RemoveAt(i);

            var testJoltage = testBatteries.Aggregate(BigInteger.Zero, (acc, val) => acc * 10 + val);

            if (testJoltage > currentHighestJoltage)
            {
                currentHighestJoltage = testJoltage;
                batteryToRemoveIndex = i;
            }
        }

        batteries.RemoveAt(batteryToRemoveIndex);

        return batteries;
    }
}
