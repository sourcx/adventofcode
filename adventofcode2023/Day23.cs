using Aoc.Util;

namespace Aoc;

public class Day23
{
    public Day23 Part1()
    {
        var maze = new Maze(File.ReadAllLines("In/23t"));
        // maze.Print();
        var res = maze.FindLongestPath();

        Console.WriteLine($"Day23.1: {res}");

        return this;
    }

    public Day23 Part2()
    {
        var graph = new Graph(File.ReadAllLines("In/23"));
        var res = graph.FindLongestPath();

        Console.WriteLine($"Day23.2: {res}");

        return this;
    }
}
