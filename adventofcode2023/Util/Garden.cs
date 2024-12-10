namespace Aoc.Util;

public class Garden
{
    char[][] _matrix;

    Point _start;

    HashSet<Point> _reachedPlots = new();

    int _stepsTaken = 0;

    public Garden(string[] lines)
    {
        _matrix = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            _matrix[i] = lines[i].ToCharArray();

            if (lines[i].Contains('S'))
            {
                _start = new Point(lines[i].IndexOf('S'), i);
                _matrix[i][_start.X] = '.';
            }
        }
    }

    public void TakeSteps(int times)
    {
        for (int i = 0; i < times; i++)
        {
            TakeStep();
        }
    }

    public void TakeStep()
    {
        if (_stepsTaken == 0)
        {
            _reachedPlots.Add(_start);
        }

        var newPlots = new HashSet<Point>();

        foreach (var plot in _reachedPlots)
        {
            if (plot.X > 0 && _matrix[plot.Y][plot.X - 1] == '.')
            {
                newPlots.Add(new Point(plot.X - 1, plot.Y));
            }

            if (plot.X < _matrix[plot.Y].Length - 1 && _matrix[plot.Y][plot.X + 1] == '.')
            {
                newPlots.Add(new Point(plot.X + 1, plot.Y));
            }

            if (plot.Y > 0 && _matrix[plot.Y - 1][plot.X] == '.')
            {
                newPlots.Add(new Point(plot.X, plot.Y - 1));
            }

            if (plot.Y < _matrix.Length - 1 && _matrix[plot.Y + 1][plot.X] == '.')
            {
                newPlots.Add(new Point(plot.X, plot.Y + 1));
            }
        }

        _reachedPlots = newPlots;
        _stepsTaken += 1;
    }

    public int GetStepsToBreakOut()
    {
        var steps = 0;

        while (true)
        {
            TakeStep();
            steps += 1;

            if (_reachedPlots.Any(p => p.X == 0 || p.X == _matrix[0].Length - 1 || p.Y == 0 || p.Y == _matrix.Length - 1))
            {
                break;
            }
        }

        return steps;
    }

    public void Print()
    {
        Console.WriteLine($"Step {_stepsTaken}, possible plots: {_reachedPlots.Count}");

        for (int y = 0; y < _matrix.Length; y++)
        {
            for (int x = 0; x < _matrix[y].Length; x++)
            {
                if (_reachedPlots.Any(p => p.X == x && p.Y == y))
                {
                    Console.Write('O');
                }
                else if (_start.X == x && _start.Y == y)
                {
                    Console.Write('S');
                }
                else
                {
                    Console.Write(_matrix[y][x]);
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public long ReachedPlotCount()
    {
        return _reachedPlots.Count;
    }
}
