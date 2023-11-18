using System.ComponentModel;
using System.Runtime.ExceptionServices;
using Extensions;

namespace AdventOfCode2015;

class Ingredient
{
    public string Name { get; set; }
    public long Capacity { get; set; }
    public long Durability { get; set; }
    public long Flavor { get; set; }
    public long Texture { get; set; }
    public long Calories { get; set; }

    public static Ingredient Read(string line)
    {
        var parts = line.Split(' ');

        var ingredient = new Ingredient
        {
            Name = parts[0].TrimEnd(':'),
            Capacity = long.Parse(parts[2].TrimEnd(',')),
            Durability = long.Parse(parts[4].TrimEnd(',')),
            Flavor = long.Parse(parts[6].TrimEnd(',')),
            Texture = long.Parse(parts[8].TrimEnd(',')),
            Calories = long.Parse(parts[10])
        };

        return ingredient;
    }

    public long Score => Capacity * Durability * Flavor * Texture;
}

class Day15
{
    public static void Solve()
    {
        SolvePart1();
        SolvePart2();
    }

    private static void SolvePart1()
    {
        var ingredients = File.ReadAllLines("input/15").Select(Ingredient.Read).ToArray();

        int availableTeaspoons = 100;
        int numberOfIngredients = ingredients.Length;
        long bestScore = long.MinValue;
        var bestDivision = new List<int>();

        foreach (var division in TeaspoonDivisions(numberOfIngredients, availableTeaspoons))
        {
            long totalCapacity = 0;
            long totalDurability = 0;
            long totalFlavor = 0;
            long totalTexture = 0;

            for (int i = 0; i < numberOfIngredients; i++)
            {
                totalCapacity += division[i] * ingredients[i].Capacity;
                totalDurability += division[i] * ingredients[i].Durability;
                totalFlavor += division[i] * ingredients[i].Flavor;
                totalTexture += division[i] * ingredients[i].Texture;
            }

            totalCapacity = totalCapacity < 0 ? 0 : totalCapacity;
            totalDurability = totalDurability < 0 ? 0 : totalDurability;
            totalFlavor = totalFlavor < 0 ? 0 : totalFlavor;
            totalTexture = totalTexture < 0 ? 0 : totalTexture;

            long score = totalCapacity * totalDurability * totalFlavor * totalTexture;

            if (score > bestScore)
            {
                bestScore = score;
                bestDivision = division;
            }
        }

        Console.WriteLine($"Best score is {bestScore}.");
    }

    private static IEnumerable<List<int>> TeaspoonDivisions(int numberOfIngredients, int availableTeaspoons)
    {
        if (numberOfIngredients == 1)
        {
            yield return new List<int> { availableTeaspoons };
            yield break;
        }

        for (int i = 1; i < availableTeaspoons; i++)
        {
            foreach (var subDivisions in TeaspoonDivisions(numberOfIngredients - 1, availableTeaspoons - i))
            {
                var us = new List<int> { i };
                us.AddRange(subDivisions);
                yield return us;
            }
        }
    }

    private static void SolvePart2()
    {
        var ingredients = File.ReadAllLines("input/15").Select(Ingredient.Read).ToArray();

        int availableTeaspoons = 100;
        int numberOfIngredients = ingredients.Length;
        long bestScore = long.MinValue;
        var bestDivision = new List<int>();

        foreach (var division in TeaspoonDivisions(numberOfIngredients, availableTeaspoons))
        {
            long totalCalories = 0;
            long totalCapacity = 0;
            long totalDurability = 0;
            long totalFlavor = 0;
            long totalTexture = 0;

            for (int i = 0; i < numberOfIngredients; i++)
            {
                totalCalories += division[i] * ingredients[i].Calories;
                totalCapacity += division[i] * ingredients[i].Capacity;
                totalDurability += division[i] * ingredients[i].Durability;
                totalFlavor += division[i] * ingredients[i].Flavor;
                totalTexture += division[i] * ingredients[i].Texture;
            }

            if (totalCalories != 500)
            {
                continue;
            }

            totalCapacity = totalCapacity < 0 ? 0 : totalCapacity;
            totalDurability = totalDurability < 0 ? 0 : totalDurability;
            totalFlavor = totalFlavor < 0 ? 0 : totalFlavor;
            totalTexture = totalTexture < 0 ? 0 : totalTexture;

            long score = totalCapacity * totalDurability * totalFlavor * totalTexture;

            if (score > bestScore)
            {
                bestScore = score;
                bestDivision = division;
            }
        }

        Console.WriteLine($"Best score is {bestScore}.");
    }
}
