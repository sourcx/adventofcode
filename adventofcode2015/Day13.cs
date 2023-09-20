using Extensions;

namespace AdventOfCode2015;

class Day13
{
    public static void Solve()
    {
        SolvePart1();
        SolvePart2();
    }

    private static void SolvePart1()
    {
        var guestList = ReadGuestList();
        var highestHappiness = HighestHappiness(guestList);

        Console.WriteLine($"Highest happiness is {highestHappiness}");
    }

    private static List<Guest> ReadGuestList()
    {
        var guestList = new List<Guest>();

        foreach (var line in File.ReadAllLines("input/13"))
        {
            var parts = line.Split(' ');
            var name = parts[0];
            var happiness = int.Parse(parts[3]);
            var neighbor = parts[10].TrimEnd('.');
            var isNegative = parts[2] == "lose";

            if (isNegative)
            {
                happiness = -happiness;
            }

            var guest = guestList.FirstOrDefault(guest => guest.Name == name);

            if (guest == null)
            {
                guest = new Guest { Name = name };
                guestList.Add(guest);
            }

            guest.Happiness.Add(neighbor, happiness);
        }

        return guestList;
    }

    private static int HighestHappiness(List<Guest> guestList)
    {
        var guestNames = guestList.Select(guest => guest.Name).ToList();
        var permutations = guestNames.Permute();

        var highestHappiness = int.MinValue;

        foreach (var permutation in guestNames.Permute())
        {
            var totalHappiness = 0;

            for (int i = 0; i < permutation.Count(); i++)
            {
                var guest = guestList.FirstOrDefault(guest => guest.Name == permutation.ElementAt(i));
                var leftNeighbor = i == 0 ? permutation.Last() : permutation.ElementAt(i - 1);
                var rightNeighbor = i == permutation.Count() - 1 ? permutation.First() : permutation.ElementAt(i + 1);

                totalHappiness += guest.Happiness[leftNeighbor];
                totalHappiness += guest.Happiness[rightNeighbor];
            }

            if (totalHappiness > highestHappiness)
            {
                highestHappiness = totalHappiness;
            }
        }

        return highestHappiness;
    }

    private static void SolvePart2()
    {
        var guestList = ReadGuestList();
        var me = new Guest { Name = "Me" };

        foreach (var guest in guestList)
        {
            guest.Happiness.Add(me.Name, 0);
            me.Happiness.Add(guest.Name, 0);
        }

        guestList.Add(me);
        var highestHappiness = HighestHappiness(guestList);

        Console.WriteLine($"Highest happiness with me is {highestHappiness}");
    }
}

class Guest
{
    public string Name { get; set; } = string.Empty;

    public Dictionary<string, int> Happiness { get; set; } = new();
}
