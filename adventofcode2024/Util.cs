namespace adventofcode2024;

class Util
{
    public class AocException : Exception
    {
    }

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
}
