using Aoc.Util;

namespace Aoc;

public class Day21
{
    public Day21 Part1()
    {
        var garden = new Garden(File.ReadAllLines("In/21"));
        garden.TakeSteps(6);
        // Console.WriteLine($"Day21.1: {garden.ReachedPlotCount()}");

        return this;
    }

    // Soo many steps, I need to look for patterns.
    public Day21 Part2()
    {
        // When do we break out?
        var garden1x1 = new Garden(File.ReadAllLines("In/21"));
        var stepsToBreakOut1x1 = garden1x1.GetStepsToBreakOut();

        // It takes 65 steps to break out of the garden. At that time we have 3955 reached plots.
        Console.WriteLine($"It takes {stepsToBreakOut1x1} steps to break out of the garden. At that time we have {garden1x1.ReachedPlotCount()} reached plots.");

        // See what happens if we take neighbouring garden tiles and start filling these up.
        var garden3x3 = new Garden3x3(File.ReadAllLines("In/21"));
        var stepsToBreakOut3x3 = garden3x3.GetStepsToBreakOut();

        // It takes 196 steps to break out of the garden. At that time we have 35214 reached plots.
        // Additional steps to break out: 196 - 65 = 131.
        Console.WriteLine($"It takes {stepsToBreakOut3x3} steps to break out of the garden. At that time we have {garden3x3.ReachedPlotCount()} reached plots.");

        // Now turn to some smarter people on the internet and see what the relationship is.
        // <processing>

        // Got it! It's a quadratic function. We can solve that!

        // For the 3rd part we don't have to break out of the 9x9 garden again but we take 131 * 2 + 65 steps in it.
        var garden9x9 = new Garden9x9(File.ReadAllLines("In/21"));
        garden9x9.TakeSteps(131 * 2 + 65);
        Console.WriteLine($"We took 131 * 2 + 65 steps. At that time we have {garden9x9.ReachedPlotCount()} reached plots.");

        var requiredSteps = 26501365;
        var x = garden1x1.ReachedPlotCount();
        var y = garden3x3.ReachedPlotCount();
        var z = garden9x9.ReachedPlotCount();

        var polyA = x / 2D - y + z / 2D;
        var polyB = -3D * x / 2D + 2D * y - z / 2D;
        var polyC = x;

        var target = (requiredSteps - stepsToBreakOut1x1) / (stepsToBreakOut3x3 - stepsToBreakOut1x1);
        var res = polyA * target * target + polyB * target + polyC;

        Console.WriteLine($"Day21.2: {res}");

        return this;
    }
}
