using System.Text.RegularExpressions;

namespace Aoc;

public partial class Day01
{
    public Day01 Part1()
    {
        // var res = File.ReadAllLines("In/1")
        //     .Select(line => line.Trim().Where(c => char.IsDigit(c)).ToArray())
        //     .Select(digits => int.Parse($"{digits.First()}{digits.Last()}"))
        //     .Sum();
        // Console.WriteLine($"Day1.1: {res}");

        return this;
    }

    // I started out by replacing words "one" with 1 in the strings but answers kept being wrong.
    // Then I read online that you should not replace words but just 'read' them.
    public Day01 Part2()
    {
        var res = File.ReadAllLines("In/1")
           .Select(FixOverlap)
           .Select(ReplaceNumbers)
           .Select(line => line.Trim().Where(c => char.IsDigit(c)).ToArray())
           .Select(digits => int.Parse($"{digits.First()}{digits.Last()}"))
           .Sum();
        Console.WriteLine($"Day1.2: {res}");

        return this;
    }

    private static string ReplaceNumbers(string input)
    {
        var numberWords = new Dictionary<string, string>
        {
            {"one", "1"},
            {"two", "2"},
            {"three", "3"},
            {"four", "4"},
            {"five", "5"},
            {"six", "6"},
            {"seven", "7"},
            {"eight", "8"},
            {"nine", "9"}
        };

        var res = MyRegex().Replace(input, match =>
        {
            string matchedWord = match.Value.ToLower();
            return numberWords.ContainsKey(matchedWord) ? numberWords[matchedWord] : matchedWord;
        });

        return res;
    }

    // Stupid for first day challenge.
    private static string FixOverlap(string input)
    {
        return input.Replace("oneight", "oneeight")
            .Replace("twone", "twoone")
            .Replace("threeight", "threeeight")
            .Replace("fiveight", "fiveeight")
            .Replace("sevenine", "sevennine")
            .Replace("eightwo", "eighttwo")
            .Replace("eighthree", "eightthree")
            .Replace("nineight", "nineeight");
    }

    [GeneratedRegex("one|two|three|four|five|six|seven|eight|nine|\\d", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex MyRegex();
}
