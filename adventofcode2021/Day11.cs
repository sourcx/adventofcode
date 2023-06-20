using System.Text;

namespace AdventOfCode2021
{
    class Day11
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var grid = new OctopusGrid(File.ReadAllLines("input/11"));

            for (int i = 1; i <= 100; i++)
            {
                grid.Step();
            }

            Console.WriteLine($"Flashcount is {grid.FlashCount}.");
        }

        private static void SolvePart2()
        {
            var grid = new OctopusGrid(File.ReadAllLines("input/11"));

            int steps = 1;
            while (!grid.Step())
            {
                steps += 1;
            }

            Console.WriteLine($"All octopuses flash at {steps}.");
        }

        public class OctopusGrid
        {
            int [,] _matrix;
            bool [,] _flashed;
            List<(int, int)> _offsets;

            public int FlashCount { get; set; } = 0;

            public OctopusGrid(string[] lines)
            {
                int w = lines[0].Length;
                int h = lines.Count();

                _matrix = new int[w, h];
                _flashed = new bool[w, h];

                int y = 0;
                foreach (var line in lines)
                {
                    int x = 0;

                    foreach(var c in line)
                    {
                        _matrix[x, y] = int.Parse($"{c}");
                        x += 1;
                    }

                    y += 1;
                }

                _offsets = new List<(int, int)>() { (-1, -1), ( 0, -1), (+1, -1),
                                                    (-1,  0),           (+1,  0),
                                                    (-1, +1), ( 0, +1), (+1, +1) };
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (int y = 0; y < _matrix.GetLength(1); y++)
                {
                    for (int x = 0; x < _matrix.GetLength(0); x++)
                    {
                        if (_matrix[x, y] > 9)
                        {
                            sb.Append("x");
                        }
                        else
                        {
                            sb.Append(_matrix[x, y]);
                        }
                    }

                    sb.Append("\n");
                }

                sb.Append("\n");
                return sb.ToString();
            }

            public bool Step()
            {
                _flashed = new bool[_flashed.GetLength(0), _flashed.GetLength(1)];
                IncreaseAll();
                while (FlashAll());
                ResetAll();

                return AllOctopusesFlashedThisStep();
            }

            private void IncreaseAll()
            {
                for (int y = 0; y < _matrix.GetLength(1); y++)
                {
                    for (int x = 0; x < _matrix.GetLength(0); x++)
                    {
                        _matrix[x, y] += 1;
                    }
                }
            }

            // Flash all greater than 9.
            private bool FlashAll()
            {
                bool hasBeenFlashed = false;

                for (int y = 0; y < _matrix.GetLength(1); y++)
                {
                    for (int x = 0; x < _matrix.GetLength(0); x++)
                    {
                        if (_matrix[x, y] > 9)
                        {
                            hasBeenFlashed = hasBeenFlashed || Flash(x, y);
                        }
                    }
                }

                return hasBeenFlashed;
            }

            // Increase all adjacent octopuses by 1.
            // Returns true if flash has occured, false if octopus already flashed before.
            private bool Flash(int x, int y)
            {
                if (_flashed[x, y])
                {
                    return false;
                }

                foreach (var (offsetX, offsetY) in _offsets)
                {
                    var neighbourX = x + offsetX;
                    var neighbourY = y + offsetY;

                    if (neighbourX >= 0 && neighbourX < _matrix.GetLength(0) &&
                        neighbourY >= 0 && neighbourY < _matrix.GetLength(1))
                    {
                        _matrix[neighbourX, neighbourY] += 1;
                    }
                }

                _flashed[x, y] = true;
                FlashCount += 1;
                return true;
            }

            private void ResetAll()
            {
                for (int y = 0; y < _matrix.GetLength(1); y++)
                {
                    for (int x = 0; x < _matrix.GetLength(0); x++)
                    {
                        if (_matrix[x, y] > 9)
                        {
                            _matrix[x, y] = 0;
                        }
                    }
                }
            }

            private bool AllOctopusesFlashedThisStep()
            {
                for (int y = 0; y < _flashed.GetLength(1); y++)
                {
                    for (int x = 0; x < _flashed.GetLength(0); x++)
                    {
                        if (!_flashed[x, y])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
}
