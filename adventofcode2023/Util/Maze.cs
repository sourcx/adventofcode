
namespace Aoc.Util;

public class Maze
{
    public char[][] Matrix;

    Point Start;

    Point End;

    public Maze(string[] lines, bool ignoreSlopes = false)
    {
        Matrix = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            Matrix[i] = lines[i].ToCharArray();

            if (i == 0 && Matrix[i].Contains('.'))
            {
                Start = new Point(Array.IndexOf(Matrix[i], '.'), i);
            }

            if (i == lines.Length - 1 && Matrix[i].Contains('.'))
            {
                End = new Point(Array.IndexOf(Matrix[i], '.'), i);
            }
        }

        if (ignoreSlopes)
        {
            for (int y = 0; y < Matrix.Length; y++)
            {
                for (int x = 0; x < Matrix[y].Length; x++)
                {
                    if (Matrix[y][x] != '#')
                    {
                        Matrix[y][x] = '.';
                    }
                }
            }
        }
    }

    public void Print(List<Point> path = null)
    {
        if (path == null)
        {
            path = new List<Point>();
        }

        for (int y = 0; y < Matrix.Length; y++)
        {
            for (int x = 0; x < Matrix[y].Length; x++)
            {
                if (Start.X == x && Start.Y == y)
                {
                    Console.Write('S');
                }
                else if (End.X == x && End.Y == y)
                {
                    Console.Write('E');
                }
                else if (path.Any(p => p.X == x && p.Y == y))
                {
                    Console.Write('O');
                }
                else
                {
                    Console.Write(Matrix[y][x]);
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public int FindLongestPath()
    {
        var travelledPath = new List<Point>();
        travelledPath.Add(Start);

        var longestPath = FindLongestPath(travelledPath);
        Print(longestPath);

        return longestPath.Count - 1;
    }

    public int FindLongestPathNoSlope()
    {
        var travelledPath = new List<Point>();
        travelledPath.Add(Start);

        var longestPath = FindLongestPath(travelledPath);
        Print(longestPath);

        return longestPath.Count - 1;
    }

    private List<Point> FindLongestPath(List<Point> travelledPath)
    {
        var lastPoint = travelledPath.Last();

        if (lastPoint.X == End.X && lastPoint.Y == End.Y)
        {
            Console.WriteLine(travelledPath.Count - 1);
            return travelledPath;
        }

        var possibleMoves = GetPossibleMoves(lastPoint, travelledPath);

        // Dead end.
        if (possibleMoves.Count == 0)
        {
            return travelledPath;
        }

        var longestPath = new List<Point>();

        foreach (var move in possibleMoves)
        {
            var newTravelledPath = new List<Point>(travelledPath);
            newTravelledPath.Add(move);

            var path = FindLongestPath(newTravelledPath);

            if (path.Count > longestPath.Count && path.Last().X == End.X && path.Last().Y == End.Y)
            {
                longestPath = path;
            }
        }

        return longestPath;
    }

    private List<Point> GetPossibleMoves(Point current, List<Point> travelledPath)
    {
        var possibleMoves = new List<Point>();

        // Left
        if (current.X > 0 &&
            Matrix[current.Y][current.X - 1] != '#' &&
            Matrix[current.Y][current.X] != '>' &&
            Matrix[current.Y][current.X] != '^' &&
            Matrix[current.Y][current.X] != 'v' &&
            !travelledPath.Any(p => p.X == current.X - 1 && p.Y == current.Y))
        {
            possibleMoves.Add(new Point(current.X - 1, current.Y));
        }

        // Right
        if (current.X < Matrix[current.Y].Length - 1 &&
            Matrix[current.Y][current.X + 1] != '#' &&
            Matrix[current.Y][current.X] != '<' &&
            Matrix[current.Y][current.X] != '^' &&
            Matrix[current.Y][current.X] != 'v' &&
            !travelledPath.Any(p => p.X == current.X + 1 && p.Y == current.Y))
        {
            possibleMoves.Add(new Point(current.X + 1, current.Y));
        }

        // Up
        if (current.Y > 0 &&
            Matrix[current.Y - 1][current.X] != '#' &&
            Matrix[current.Y][current.X] != '<' &&
            Matrix[current.Y][current.X] != '>' &&
            Matrix[current.Y][current.X] != 'v' &&
            !travelledPath.Any(p => p.X == current.X && p.Y == current.Y - 1))
        {
            possibleMoves.Add(new Point(current.X, current.Y - 1));
        }

        // Down
        if (current.Y < Matrix.Length - 1 &&
            Matrix[current.Y + 1][current.X] != '#' &&
            Matrix[current.Y][current.X] != '<' &&
            Matrix[current.Y][current.X] != '>' &&
            Matrix[current.Y][current.X] != '^' &&
            !travelledPath.Any(p => p.X == current.X && p.Y == current.Y + 1))
        {
            possibleMoves.Add(new Point(current.X, current.Y + 1));
        }

        return possibleMoves;
    }
}
