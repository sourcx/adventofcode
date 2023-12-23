namespace Aoc.Util;

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(string input)
    {
        var parts = input.Split(",");

        X = int.Parse(parts[0]);
        Y = int.Parse(parts[1]);
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X}, {Y}";
    }
}
