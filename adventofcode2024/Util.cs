namespace adventofcode2024;

class Util
{
    public class AocException : Exception
    {
    }

    const char Outside = '_';

    public static char[,] ReadMatrix(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var height = lines.Length;
        var width = lines[0].Length;
        var matrix = new char[height, width];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                matrix[y, x] = lines[y][x];
            }
        }

        return matrix;
    }

    public static void PrintInPlace(string message)
    {
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(new string(' ', (Console.WindowWidth - message.Length) < 0 ? 0 : Console.WindowWidth - message.Length));
    }

    public static void Print(char[,] matrix)
    {
        for (var y = 0; y < matrix.GetLength(0); y++)
        {
            for (var x = 0; x < matrix.GetLength(1); x++)
            {
                Console.Write(matrix[y, x]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public static void PrintInPlace(char[,] matrix)
    {
        Console.SetCursorPosition(0, 2);

        for (var y = 0; y < matrix.GetLength(0); y++)
        {
            for (var x = 0; x < matrix.GetLength(1); x++)
            {
                Console.Write(matrix[y, x]);
            }

            Console.Write(new string(' ', Console.WindowWidth - matrix.GetLength(1)));
            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public static char[,] Copy(char[,] matrix)
    {
        var copy = new char[matrix.GetLength(0), matrix.GetLength(1)];

        for (var y = 0; y < matrix.GetLength(0); y++)
        {
            for (var x = 0; x < matrix.GetLength(1); x++)
            {
                copy[y, x] = matrix[y, x];
            }
        }

        return copy;
    }

    public static char[,] NewMatrix(int height, int width, char init)
    {
        char[,] matrix = new char[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                matrix[y, x] = init;
            }
        }

        return matrix;
    }

    public char Get(char[,] matrix, (int x, int y) pos)
    {
        return Get(matrix, pos.x, pos.y);
    }

    public char Get(char[,] matrix, int x, int y)
    {
        if (y < 0 || y >= matrix.GetLength(0) || x < 0 || x >= matrix.GetLength(1))
        {
            return Outside;
        }

        return matrix[y, x];
    }

    public static bool Set(char[,] matrix, (int x, int y) pos, char c)
    {
        return Set(matrix, pos.x, pos.y, c);
    }

    public static bool Set(char[,] matrix, int x, int y, char c)
    {
        if (y < 0 || y >= matrix.GetLength(0) || x < 0 || x >= matrix.GetLength(1))
        {
            return false;
        }

        matrix[y, x] = c;
        return true;
    }
}
