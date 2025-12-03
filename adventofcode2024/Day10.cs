namespace adventofcode2024;

class Day10
{
    private (int dx, int dy) Up = (0, -1);
    private (int dx, int dy) Down = (0, 1);
    private (int dx, int dy) Left = (-1, 0);
    private (int dx, int dy) Right = (1, 0);

    public void Run()
    {
        Console.WriteLine(GetType().Name);

        var fileName = "testinput/10";
        RunPart1(fileName);
        RunPart2(fileName);
    }


    private void RunPart1(string fileName)
    {
        int[,] map = Util.ReadIntMatrix(fileName);

        var totalNrTrails = 0;

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (Util.GetInt(map, x, y) == 0)
                {
                    totalNrTrails += FindUniqueTrailEnds(map, (x, y), 0, []).Count;
                }
            }
        }

        Console.WriteLine($"What is the sum of the scores of all trailheads on your topographic map? {totalNrTrails}");
    }

    private HashSet<(int x, int y)> FindUniqueTrailEnds(int[,] map, (int x, int y) pos, int lastHeight, HashSet<(int x, int y)> trailEnds)
    {
        if (lastHeight == 9)
        {
            trailEnds.Add(pos);
        }

        var up = Util.Add(pos, Up);
        if (Util.GetInt(map, up) == Util.GetInt(map, pos) + 1)
        {
            FindUniqueTrailEnds(map, up, lastHeight + 1, trailEnds);
        }

        var down = Util.Add(pos, Down);
        if (Util.GetInt(map, down) == Util.GetInt(map, pos) + 1)
        {
            FindUniqueTrailEnds(map, down, lastHeight + 1, trailEnds);
        }

        var left = Util.Add(pos, Left);
        if (Util.GetInt(map, left) == Util.GetInt(map, pos) + 1)
        {
            FindUniqueTrailEnds(map, left, lastHeight + 1, trailEnds);
        }

        var right = Util.Add(pos, Right);
        if (Util.GetInt(map, right) == Util.GetInt(map, pos) + 1)
        {
            FindUniqueTrailEnds(map, right, lastHeight + 1, trailEnds);
        }

        return trailEnds;
    }

    private void RunPart2(string fileName)
    {
        int[,] map = Util.ReadIntMatrix(fileName);

        var totalNrTrails = 0U;

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (Util.GetInt(map, x, y) == 0)
                {
                    var nrTrails = FindTrails(map, (x, y), 0);
                    Console.WriteLine($"Nr of trails for {x}, {y} is {nrTrails}");
                    totalNrTrails += nrTrails;
                }
            }
        }

        Console.WriteLine($"What is the sum of the scores of all trailheads on your topographic map? {totalNrTrails}");
    }

    private uint FindTrails(int[,] map, (int x, int y) pos, int lastHeight)
    {
        if (lastHeight == 9)
        {
            return 1;
        }

        var nrTrails = 0U;

        var up = Util.Add(pos, Up);
        if (Util.GetInt(map, up) == Util.GetInt(map, pos) + 1)
        {
            nrTrails += FindTrails(map, up, lastHeight + 1);
        }

        var down = Util.Add(pos, Down);
        if (Util.GetInt(map, down) == Util.GetInt(map, pos) + 1)
        {
            nrTrails += FindTrails(map, down, lastHeight + 1);
        }

        var left = Util.Add(pos, Left);
        if (Util.GetInt(map, left) == Util.GetInt(map, pos) + 1)
        {
            nrTrails += FindTrails(map, left, lastHeight + 1);
        }

        var right = Util.Add(pos, Right);
        if (Util.GetInt(map, right) == Util.GetInt(map, pos) + 1)
        {
            nrTrails += FindTrails(map, right, lastHeight + 1);
        }

        return nrTrails;
    }
}
