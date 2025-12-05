using System.Numerics;

namespace adventofcode2025;

class Day05
{
    public void Run()
    {
        Console.WriteLine(GetType().Name);

        Part1();
        Part2();
    }

    private void Part1()
    {
        var freshIngredientRanges = new List<Tuple<BigInteger, BigInteger>>();
        var allIngredients = new List<BigInteger>();

        foreach (var line in File.ReadLines("input/5"))
        {
            // Read ranges
            if (line.Contains('-'))
            {
                var parts = line.Split('-');

                var start = BigInteger.Parse(parts[0]);
                var end = BigInteger.Parse(parts[1]);

                freshIngredientRanges.Add(new Tuple<BigInteger, BigInteger>(start, end));
            }
            else if (line == "")
            {
                // Separator
                continue;
            }
            else
            {
                // Read ingredients
                allIngredients.Add(BigInteger.Parse(line));
            }
        }

        var nrFresh = 0;

        foreach (var ingredient in allIngredients)
        {
            foreach (var range in freshIngredientRanges)
            {
                if (ingredient >= range.Item1 && ingredient <= range.Item2)
                {
                    nrFresh++;
                    break;
                }
            }
        }

        Console.WriteLine($"Number of fresh ingredients: {nrFresh}");
    }

    private void Part2()
    {
        var ranges = new List<Tuple<BigInteger, BigInteger>>();

        foreach (var line in File.ReadLines("testinput/5b"))
        {
            var parts = line.Split('-');

            var start = BigInteger.Parse(parts[0]);
            var end = BigInteger.Parse(parts[1]);

            if (start > end)
            {
                throw new Exception("Invalid range: " + line);
            }

            ranges.Add(new Tuple<BigInteger, BigInteger>(start, end));
        }

        // Sort ranges by start position
        ranges.Sort((a, b) => a.Item1.CompareTo(b.Item1));

        // Merge overlapping ranges
        var mergedRanges = new List<Tuple<BigInteger, BigInteger>>();
        var currentRange = ranges[0];

        for (int i = 1; i < ranges.Count; i++)
        {
            var nextRange = ranges[i];

            // Check if ranges overlap or are adjacent
            if (nextRange.Item1 <= currentRange.Item2 + 1)
            {
                // Merge: extend current range to cover both
                currentRange = new Tuple<BigInteger, BigInteger>(
                    currentRange.Item1,
                    BigInteger.Max(currentRange.Item2, nextRange.Item2)
                );
            }
            else
            {
                // No overlap: save current range and start new one
                mergedRanges.Add(currentRange);
                currentRange = nextRange;
            }
        }

        // Don't forget to add the last range
        mergedRanges.Add(currentRange);

        // Count total unique numbers in merged ranges
        var uniqueNrsInRanges = BigInteger.Zero;
        foreach (var range in mergedRanges)
        {
            uniqueNrsInRanges += range.Item2 - range.Item1 + 1;
        }

        Console.WriteLine("Unique numbers in ranges: " + uniqueNrsInRanges);
    }
}
