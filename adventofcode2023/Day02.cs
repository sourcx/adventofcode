namespace Aoc;

public class Day02
{
    public Day02 Part1()
    {
        var res = File.ReadAllLines("In/1")
            .Select(line => line.Trim().Where(c => char.IsDigit(c)).ToArray())
            .Select(digits => int.Parse($"{digits.First()}{digits.Last()}"))
            .Sum();
        Console.WriteLine($"Day1.1: {res}");

        return this;
    }
}
