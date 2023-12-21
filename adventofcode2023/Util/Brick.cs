namespace Aoc.Util;

public class Brick
{
    public int Id { get; set; }

    public List<Cube> Cubes { get; set; } = new List<Cube>();

    public Brick(string line, int id)
    {
        Id = id;
        var parts = line.Split("~");
        var start = new Cube(parts[0]);
        var end = new Cube(parts[1]);

        if (start.X != end.X)
        {
            var hi = Math.Max(start.X, end.X);
            var lo = Math.Min(start.X, end.X);

            for (int i = lo; i <= hi; i++)
            {
                Cubes.Add(new Cube(i, start.Y, start.Z));
            }
        }
        else if (start.Y != end.Y)
        {
            var hi = Math.Max(start.Y, end.Y);
            var lo = Math.Min(start.Y, end.Y);

            for (int i = lo; i <= hi; i++)
            {
                Cubes.Add(new Cube(start.X, i, start.Z));
            }
        }
        else if (start.Z != end.Z)
        {
            var hi = Math.Max(start.Z, end.Z);
            var lo = Math.Min(start.Z, end.Z);

            for (int i = lo; i <= hi; i++)
            {
                Cubes.Add(new Cube(start.X, start.Y, i));
            }
        }
        else
        {
            Cubes.Add(start);
        }
    }

    public Brick(Brick copy)
    {
        Id = copy.Id;
        Cubes = copy.Cubes.Select(c => new Cube(c.X, c.Y, c.Z)).ToList();
    }

    public bool ContainsCube(int x, int y, int z)
    {
        return Cubes.Any(c => c.X == x && c.Y == y && c.Z == z);
    }
}
