namespace adventofcode2024;

class Day04
{
    readonly (int, int)[] Directions =
    [
        (1, 0),
        (-1, 0),
        (0, 1),
        (0, -1),
        (1, 1),
        (-1, -1),
        (1, -1),
        (-1, 1)
    ];

    public void Run()
    {
        Console.WriteLine(GetType().Name);

        var matrix = Util.ReadMatrix("input/4");
        var h = matrix.GetLength(0);
        var w = matrix.GetLength(1);

        var xmas = NrTimesXmas(matrix, h, w);
        Console.WriteLine($"The number of times XMAS appears is {xmas}");

        xmas = NrTimesX_mas(matrix, h, w);
        Console.WriteLine($"The number of times an X-MAS appears is {xmas}");
    }

    private int NrTimesXmas(char[,] matrix, int height, int width)
    {
        var nrTimesXmas = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                foreach (var (dy, dx) in Directions)
                {
                    if (HasWord(matrix, y, x, "XMAS", dy, dx))
                    {
                        nrTimesXmas++;
                    }
                }
            }
        }

        return nrTimesXmas;
    }

    private bool HasWord(char[,] matrix, int y, int x, string word, int yOffset, int xOffset)
    {
        if (Get(matrix, y, x) != word[0])
        {
            return false;
        }

        for (int i = 1; i < word.Length; i++)
        {
            var charToFind = word[i];

            if (charToFind != Get(matrix, y + (yOffset * i), x + (xOffset * i)))
            {
                return false;
            }
        }

        return true;
    }

    private char Get(char[,] matrix, int y, int x)
    {
        if (y < 0 || y >= matrix.GetLength(0) || x < 0 || x >= matrix.GetLength(1))
        {
            return '.';
        }

        return matrix[y, x];
    }

    private int NrTimesX_mas(char[,] matrix, int height, int width)
    {
        var nrTimesXmas = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (((HasWord(matrix, y, x, "AS", 1, 1) && HasWord(matrix, y, x, "AM", -1, -1)) ||
                     (HasWord(matrix, y, x, "AM", 1, 1) && HasWord(matrix, y, x, "AS", -1, -1))) &&
                    ((HasWord(matrix, y, x, "AS", -1, 1) && HasWord(matrix, y, x, "AM", 1, -1)) ||
                     (HasWord(matrix, y, x, "AM", -1, 1) && HasWord(matrix, y, x, "AS", 1, -1))))
                {
                    nrTimesXmas++;
                }
            }
        }

        return nrTimesXmas;
    }
}
