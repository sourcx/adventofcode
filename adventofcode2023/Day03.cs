namespace Aoc;

public class Day03
{
    public Day03 Part1()
    {
        long sum = 0;
        var lines = File.ReadAllLines("In/3");
        var width = lines[0].Length;
        var matrix = new char[lines.Length + 2, width + 2]; // +2 for padding

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var j = 0; j < width; j++)
            {
                matrix[i + 1, j + 1] = line[j];
            }
        }

        for (var y = 1; y < lines.Length + 1; y++)
        {
            var currentNumber = "";
            var isPartNumber = false;

            for (var x = 1; x < width + 1; x++)
            {
                var c = matrix[y, x];

                if (char.IsDigit(c))
                {
                    currentNumber += c;

                    if (IsAdjacentToSymbol(x, y, matrix))
                    {
                        isPartNumber = true;
                    }
                }
                else
                {
                    if (isPartNumber)
                    {
                        sum += long.Parse(currentNumber);
                    }

                    currentNumber = "";
                    isPartNumber = false;
                }
            }

            if (isPartNumber)
            {
                sum += long.Parse(currentNumber);
            }
        }

        Console.WriteLine($"Day3.1: {sum}");

        return this;
    }

    private bool IsAdjacentToSymbol(int x, int y, char[,] matrix)
    {
        if (IsSymbol(matrix[y, x - 1]) ||
            IsSymbol(matrix[y, x + 1]) ||
            IsSymbol(matrix[y - 1, x]) ||
            IsSymbol(matrix[y + 1, x]) ||
            IsSymbol(matrix[y - 1, x - 1]) ||
            IsSymbol(matrix[y - 1, x + 1]) ||
            IsSymbol(matrix[y + 1, x - 1]) ||
            IsSymbol(matrix[y + 1, x + 1]))
        {
            return true;
        }

        return false;
    }

    private bool TryGetAdjacentStar(int x, int y, char[,] matrix, out (int x, int y) star)
    {
        if ('*' == matrix[y, x - 1])
        {
            star = (x - 1, y);
            return true;
        }

        if ('*' == matrix[y, x + 1])
        {
            star = (x + 1, y);
            return true;
        }

        if ('*' == matrix[y - 1, x])
        {
            star = (x, y - 1);
            return true;
        }

        if ('*' == matrix[y + 1, x])
        {
            star = (x, y + 1);
            return true;
        }

        if ('*' == matrix[y - 1, x - 1])
        {
            star = (x - 1, y - 1);
            return true;
        }

        if ('*' == matrix[y - 1, x + 1])
        {
            star = (x + 1, y - 1);
            return true;
        }

        if ('*' == matrix[y + 1, x - 1])
        {
            star = (x - 1, y + 1);
            return true;
        }

        if ('*' == matrix[y + 1, x + 1])
        {
            star = (x + 1, y + 1);
            return true;
        }

        star = (0, 0);
        return false;
    }

    private bool IsSymbol(char c)
    {
        return !char.IsDigit(c) && c != '.' && c != '\0';
    }

    public Day03 Part2()
    {
        var sum = 0L;
        var lines = File.ReadAllLines("In/3");
        var width = lines[0].Length;
        var matrix = new char[lines.Length + 2, width + 2];

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var j = 0; j < width; j++)
            {
                matrix[i + 1, j + 1] = line[j];
            }
        }

        var starsWithNumbers = new Dictionary<(int x, int y), List<int>>();

        for (var y = 1; y < lines.Length + 1; y++)
        {
            var currentNumber = "";
            var adjacentStars = new HashSet<(int x, int y)>();

            for (var x = 1; x < width + 1; x++)
            {
                var c = matrix[y, x];

                if (char.IsDigit(c))
                {
                    currentNumber += c;

                    if (TryGetAdjacentStar(x, y, matrix, out var adjacentStar))
                    {
                        adjacentStars.Add(adjacentStar);
                    }
                }
                else
                {
                    foreach (var star in adjacentStars)
                    {
                        if (!starsWithNumbers.ContainsKey(star))
                        {
                            starsWithNumbers[star] = new List<int>();
                        }

                        starsWithNumbers[star].Add(int.Parse(currentNumber));
                    }

                    currentNumber = "";
                    adjacentStars.Clear();
                }
            }

            foreach (var star in adjacentStars)
            {
                if (!starsWithNumbers.ContainsKey(star))
                {
                    starsWithNumbers[star] = new List<int>();
                }

                starsWithNumbers[star].Add(int.Parse(currentNumber));
            }
        }

        foreach (var star in starsWithNumbers)
        {
            if (star.Value.Count == 2)
            {
                sum += star.Value[0] * star.Value[1];
            }
        }

        Console.WriteLine($"Day3.2: {sum}");

        return this;
    }
}
