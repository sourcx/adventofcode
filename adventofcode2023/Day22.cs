using Aoc.Util;

namespace Aoc;

public class Day22
{
    public Day22 Part1()
    {
        var brickStack = new BrickStack(File.ReadAllLines("In/22"));
        brickStack.Settle();
        // brickStack.PrintSideViewXAxis();
        // brickStack.PrintSideViewYAxis();
        var res = brickStack.GetDesintegratedBrickCount();

        Console.WriteLine($"Day22.1: {res}");

        return this;
    }

    public Day22 Part2()
    {
        var brickStack = new BrickStack(File.ReadAllLines("In/22"));
        brickStack.Settle();
        var res = brickStack.GetBestChainReaction();

        Console.WriteLine($"Day22.2: {res}");
        return this;
    }
}
