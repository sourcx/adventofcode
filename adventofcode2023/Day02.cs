namespace Aoc;

public class Day02
{
    public Day02 Part1()
    {
        var maxRed = 12;
        var maxGreen = 13;
        var maxBlue = 14;

        var sumPossibleIds = 0;

        foreach (var game in File.ReadAllLines("In/2").Select(line => line.Trim()))
        {
            var gameParts = game.Split(":");
            var gameId = int.Parse(gameParts[0].Split(" ")[1]);
            var grabs = gameParts[1].Split(";");

            var gameIsPossible = true;

            foreach (var grab in grabs)
            {
                var colors = grab.Trim().Split(",");

                foreach (var color in colors)
                {
                    var colorParts = color.Trim().Split(" ");
                    var colorCount = int.Parse(colorParts[0]);
                    var colorName = colorParts[1];

                    switch (colorName)
                    {
                        case "red":
                            if (colorCount > maxRed)
                            {
                                gameIsPossible = false;
                            }
                            break;
                        case "green":
                            if (colorCount > maxGreen)
                            {
                                gameIsPossible = false;
                            }
                            break;
                        case "blue":
                            if (colorCount > maxBlue)
                            {
                                gameIsPossible = false;
                            }
                            break;
                        default:
                            throw new Exception($"Unknown color: {colorName}");
                    }
                }
            }

            if (gameIsPossible)
            {
                sumPossibleIds += gameId;
            }
        }

        Console.WriteLine($"Day2.1: {sumPossibleIds}");

        return this;
    }

    public Day02 Part2()
    {
        var power = 0L;

        foreach (var game in File.ReadAllLines("In/2").Select(line => line.Trim()))
        {
            var gameParts = game.Split(":");
            var gameId = int.Parse(gameParts[0].Split(" ")[1]);
            var grabs = gameParts[1].Split(";");

            var maxRed = long.MinValue;
            var maxGreen = long.MinValue;
            var maxBlue = long.MinValue;

            foreach (var grab in grabs)
            {
                var colors = grab.Trim().Split(",");

                foreach (var color in colors)
                {
                    var colorParts = color.Trim().Split(" ");
                    var colorCount = int.Parse(colorParts[0]);
                    var colorName = colorParts[1];

                    switch (colorName)
                    {
                        case "red":
                            if (colorCount > maxRed)
                            {
                                maxRed = colorCount;
                            }
                            break;
                        case "green":
                            if (colorCount > maxGreen)
                            {
                                maxGreen = colorCount;
                            }
                            break;
                        case "blue":
                            if (colorCount > maxBlue)
                            {
                                maxBlue = colorCount;
                            }
                            break;
                        default:
                            throw new Exception($"Unknown color: {colorName}");
                    }
                }
            }

            power += maxRed * maxGreen * maxBlue;
        }

        Console.WriteLine($"Day2.1: {power}");

        return this;
    }
}
