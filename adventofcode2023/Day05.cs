namespace Aoc;

public class Day05
{
    public Day05 Part1()
    {
        var res = long.MaxValue;

        (var seeds, var maps) = ReadInput(File.ReadLines("In/5"));

        foreach (var seed in seeds)
        {
            var location = MapAll(seed, maps);

            if (location < res)
            {
                res = location;
            }
        }

        Console.WriteLine($"Day05.1: {res}");

        return this;
    }

    private static (IEnumerable<long>, List<List<MappingRule>>) ReadInput(IEnumerable<string> lines)
    {
        var seeds = new List<long>();
        var maps = new List<List<MappingRule>>();

        var currentMapList = new List<MappingRule>();
        var currentMapName = string.Empty;

        foreach (var line in lines)
        {
            if (line.Contains("seeds:"))
            {
                seeds = line.Split(": ")[1].Split(" ").Select(long.Parse).ToList();
            }
            else if (line.Contains("map:"))
            {
                currentMapList = new List<MappingRule>();
                currentMapName = line.Split("-")[0]; // or line.Split(" ")[0]
            }
            else if (line.Contains(' '))
            {
                var nrs = line.Split(" ").Select(long.Parse).ToArray();
                var map = new MappingRule()
                {
                    DstRangeStart = nrs[0],
                    SrcRangeStart = nrs[1],
                    RangeLength = nrs[2],
                };
                currentMapList.Add(map);
            }
            else
            {
                if (currentMapList.Count > 0)
                {
                    maps.Add(currentMapList);
                }
            }
        }

        maps.Add(currentMapList);

        return (seeds, maps);
    }

    private static long MapAll(long seed, List<List<MappingRule>> mappingRules)
    {
        var res = seed;

        foreach (var mappingRule in mappingRules)
        {
            var mappedSeed = mappingRule
                .Select(rule => rule.Map(res))
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .ToList();

            if (mappedSeed.Count > 0)
            {
                res = mappedSeed.Min();
            }
        }

        return res;
    }

    partial class MappingRule
    {
        public long DstRangeStart { get; set; }
        public long SrcRangeStart { get; set; }
        public long RangeLength { get; set; }

        public long? Map(long src)
        {
            if (src >= SrcRangeStart && src < SrcRangeStart + RangeLength)
            {
                return src - (SrcRangeStart - DstRangeStart);
            }

            return null;
        }
    }

    // Part 2 is properly shit if you don't expect it. Starting over again...
    public Day05 Part2()
    {
        var res = long.MaxValue;

        (var seedRanges, var maps) = ReadInput2(File.ReadLines("In/5"));

        foreach (var seedRange in seedRanges)
        {
            // var location = MapAll(seed, maps);
            var location = 42;

            if (location < res)
            {
                res = location;
            }
        }

        Console.WriteLine($"Day05.2: {res}");

        return this;
    }

    private static (IEnumerable<SeedRange>, List<List<MappingRule>>) ReadInput2(IEnumerable<string> lines)
    {
        IEnumerable<SeedRange> seeds = null;
        var maps = new List<List<MappingRule>>();

        var currentMapList = new List<MappingRule>();
        var currentMapName = string.Empty;

        foreach (var line in lines)
        {
            if (line.Contains("seeds:"))
            {
                seeds = ReadSeedRanges(line);
            }
            else if (line.Contains("map:"))
            {
                currentMapList = new List<MappingRule>();
                currentMapName = line.Split("-")[0]; // or line.Split(" ")[0]
            }
            else if (line.Contains(' '))
            {
                var nrs = line.Split(" ").Select(long.Parse).ToArray();
                var map = new MappingRule()
                {
                    DstRangeStart = nrs[0],
                    SrcRangeStart = nrs[1],
                    RangeLength = nrs[2],
                };
                currentMapList.Add(map);
            }
            else
            {
                if (currentMapList.Count > 0)
                {
                    maps.Add(currentMapList);
                }
            }
        }

        maps.Add(currentMapList);

        return (seeds, maps);
    }

    partial class SeedRange
    {
        public long Start;
        public long Length;
    }

    private static IEnumerable<SeedRange> ReadSeedRanges(string line)
    {
        var ranges = line.Split(": ")[1].Split(" ").Select(long.Parse).ToArray();

        var res = new List<SeedRange>();

        for (var i = 0; i < ranges.Length; i += 2)
        {
            res.Add(new SeedRange()
            {
                Start = ranges[i],
                Length = ranges[i + 1],
            });
        }

        return res;
    }

    // private static long MapAll(List<SeedRange> seedRanges, List<List<MappingRule>> mappingRules)
    // {
    //     var res = seed;

    //     foreach (var mappingRule in mappingRules)
    //     {
    //         var mappedSeeds = mappingRule
    //             .Select(rule => rule.Map(res))
    //             .Where(x => x.HasValue)
    //             .Select(x => x.Value)
    //             .ToList();

    //         if (mappedSeeds.Count > 0)
    //         {
    //             res = mappedSeeds.Min();
    //         }
    //     }

    //     return res;
    // }

}
