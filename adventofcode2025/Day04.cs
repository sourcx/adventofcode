namespace adventofcode2025;

class Day04
{
    public void Run()
    {
        Console.WriteLine(GetType().Name);

        Part1();
        Part2();
    }

    private void Part1()
    {
        var grid = new Grid(File.ReadAllLines("input/4"));
        var accessibleLocations = grid.FindAccessibleRolls();

        Console.WriteLine($"Number of accessible rolls: {accessibleLocations.Count}");
    }

    private void Part2()
    {
        var grid = new Grid(File.ReadAllLines("input/4"));

        var accessibleLocations = grid.FindAccessibleRolls();
        var nrRemovedRolls = 0;

        while (accessibleLocations.Count > 0)
        {
            Console.WriteLine($"Number of accessible rolls: {accessibleLocations.Count}");
            nrRemovedRolls += accessibleLocations.Count;

            grid.RemoveRolls(accessibleLocations);
            accessibleLocations = grid.FindAccessibleRolls();
        }

        Console.WriteLine($"Total number of removed rolls: {nrRemovedRolls}");
    }
}

class Grid
{
    public static char FORKLIFT = 'x';
    public static char ROLL = '@';
    public static char EMPTY = '.';

    char [,] data;

    public Grid(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;

        data = new char[rows, cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                data[y, x] = lines[y][x];
            }
        }
    }

    public List<Tuple<int, int>> FindAccessibleRolls()
    {
        var accessibleLocations = new List<Tuple<int, int>>();

        for (int y = 0; y < data.GetLength(0); y++)
        {
            for (int x = 0; x < data.GetLength(1); x++)
            {
                if (data[y, x] == ROLL && hasLessThanFourRollsAround(y, x))
                {
                    accessibleLocations.Add(Tuple.Create(y, x));
                }
            }
        }

        return accessibleLocations;
    }

    private bool hasLessThanFourRollsAround(int y, int x)
    {
        var nrRollsAround = 0;

        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                if (dy == 0 && dx == 0)
                {
                    continue;
                }

                int nr = y + dy;
                int nc = x + dx;

                if (nr >= 0 && nr < data.GetLength(0) &&
                    nc >= 0 && nc < data.GetLength(1))
                {
                    if (data[nr, nc] == ROLL)
                    {
                        nrRollsAround++;
                    }
                }
            }
        }

        return nrRollsAround < 4;
    }

    public void RemoveRolls(List<Tuple<int, int>> locations)
    {
        foreach (var loc in locations)
        {
            data[loc.Item1, loc.Item2] = EMPTY;
        }
    }
}
