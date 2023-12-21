public class Cube
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Cube(string input)
    {
        var parts = input.Split(",");

        X = int.Parse(parts[0]);
        Y = int.Parse(parts[1]);
        Z = int.Parse(parts[2]);
    }

    public Cube(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}
