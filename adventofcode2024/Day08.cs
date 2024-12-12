using Extensions;

namespace adventofcode2024;

class Day08
{
    public void Run()
    {
        Console.WriteLine(GetType().Name);

        var fileName = "input/8";
        RunPart1(fileName);
        RunPart2(fileName);
    }

    private void RunPart1(string fileName)
    {
        char[,] antennaMap = Util.ReadMatrix(fileName);
        // Console.WriteLine("Antenna map");
        // Util.Print(antennaMap);

        var uniqueFrequencies = antennaMap.Cast<char>().Where(c => c != '.').ToList().Distinct();
        Console.WriteLine($"Loaded map and found unique frequencies {string.Join(", ", uniqueFrequencies)}.");

        char[,] antinodeMap = Util.NewMatrix(antennaMap.GetLength(0), antennaMap.GetLength(1), '.');

        foreach (var frequency in uniqueFrequencies)
        {
            MarkAntiNodes(antennaMap, antinodeMap, frequency);
        }

        // Console.WriteLine("Antenna map");
        // Util.Print(antennaMap);

        // Console.WriteLine("Antinode map");
        // Util.Print(antinodeMap);

        var antiNodeCount = antinodeMap.Cast<char>().Where(c => c == '#').ToList().Count;
        Console.WriteLine($"How many unique locations within the bounds of the map contain an antinode? {antiNodeCount}");
    }

    private void MarkAntiNodes(char[,] antennaMap, char[,] antinodeMap, char frequency)
    {
        var antennaLocations = GetAntennas(antennaMap, frequency);

        // Console.WriteLine($"Checking {frequency}");

        foreach (var lhs in antennaLocations)
        {
            foreach (var rhs in antennaLocations)
            {
                if (lhs == rhs)
                {
                    continue;
                }

                var offset = lhs.Min(rhs);
                var antiNodeLocation = rhs.Add(offset);
                Util.Set(antinodeMap, antiNodeLocation, '#');

                // Console.WriteLine($"permutation {lhs} {rhs} -> {offset} -> {antiNodeLocation}");
                // Util.Print(antennaMap);
                // Util.Print(antinodeMap);
            }
        }
    }

    private List<(int x, int y)> GetAntennas(char[,] antennaMap, char frequency)
    {
        var antennaLocations = new List<(int x, int y)>();

        for (int y = 0; y < antennaMap.GetLength(0); y++)
        {
            for (int x = 0; x < antennaMap.GetLength(1); x++)
            {
                if (antennaMap[y, x] == frequency)
                {
                    antennaLocations.Add((x, y));
                }
            }
        }

        return antennaLocations;
    }

    private void RunPart2(string fileName)
    {
        char[,] antennaMap = Util.ReadMatrix(fileName);
        // Util.Print(antennaMap);

        var uniqueFrequencies = antennaMap.Cast<char>().Where(c => c != '.').ToList().Distinct();
        Console.WriteLine($"Loaded map and found unique frequencies {string.Join(", ", uniqueFrequencies)}.");

        char[,] antinodeMap = Util.Copy(antennaMap);

        foreach (var frequency in uniqueFrequencies)
        {
            MarkAntiNodesPart2(antennaMap, antinodeMap, frequency);
        }

        // Console.WriteLine($"Antinode map");
        // Util.Print(antinodeMap);

        var antiNodeCount = antinodeMap.Cast<char>().Where(c => c != '.').ToList().Count;
        Console.WriteLine($"How many unique locations within the bounds of the map contain an antinode? {antiNodeCount}");
    }

    private void MarkAntiNodesPart2(char[,] antennaMap, char[,] antinodeMap, char frequency)
    {
        var antennaLocations = GetAntennas(antennaMap, frequency);

        // Console.WriteLine($"Checking {frequency}");

        foreach (var lhs in antennaLocations)
        {
            foreach (var rhs in antennaLocations)
            {
                if (lhs == rhs)
                {
                    continue;
                }

                var offset = lhs.Min(rhs);
                var antiNodeLocation = rhs.Add(offset);

                // Util.Print(antennaMap);
                while (Util.Set(antinodeMap, antiNodeLocation, '#'))
                {
                    // Console.WriteLine($"permutation {lhs} {rhs} -> {offset} -> {antiNodeLocation}");
                    // Util.Print(antinodeMap);
                    antiNodeLocation = antiNodeLocation.Add(offset);
                }

            }
        }
    }
}
