using System.Text;
using System.Linq;

namespace AdventOfCode2015
{
    class Day06
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var grid = new LightGrid(1000);

            foreach (var instructions in File.ReadAllLines("input/6"))
            {
                grid.Apply(instructions);
            }

            // Console.WriteLine(grid.ToString());
            Console.WriteLine($"Amount of lights turned on: {grid.NrLightsOn}.");
        }

        private static void SolvePart2()
        {
             var grid = new LightGrid2(1000);

            foreach (var instructions in File.ReadAllLines("input/6"))
            {
                grid.Apply(instructions);
            }

            // Console.WriteLine(grid.ToString());
            Console.WriteLine($"Total brightness: {grid.TotalBrightness}.");
        }

        class LightGrid
        {
            bool[,] _grid;

            public LightGrid(int size)
            {
                _grid = new bool[size, size];
            }

            public long NrLightsOn
            {
                get
                {
                    long nrLightsOn = 0;
                    foreach (var x in _grid)
                    {
                        if (x)
                        {
                            nrLightsOn += 1;
                        }
                    }

                    return nrLightsOn;
                }
            }

            // turn on 489,959 through 759,964
            public void Apply(string instructions)
            {
                Action<Tuple<int, int>, Tuple<int, int>> method;
                Tuple<int, int> from;
                Tuple<int, int> to;

                if (instructions.StartsWith("turn on"))
                {
                    method = this.TurnOn;
                    instructions = instructions.Split("turn on")[1];
                }
                else if (instructions.StartsWith("turn off"))
                {
                    method = this.TurnOff;
                    instructions = instructions.Split("turn off")[1];
                }
                else if (instructions.StartsWith("toggle"))
                {
                    method = this.Toggle;
                    instructions = instructions.Split("toggle")[1];
                }
                else
                {
                    throw new Exception($"Unknown instructions {instructions}.");
                }

                var fromParts = instructions.Trim().Split(" through ")[0];
                from = new Tuple<int, int>(int.Parse(fromParts.Split(",")[0]), int.Parse(fromParts.Split(",")[1]));
                var toParts = instructions.Trim().Split(" through ")[1];
                to = new Tuple<int, int>(int.Parse(toParts.Split(",")[0]), int.Parse(toParts.Split(",")[1]));

                method(from, to);
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (int x = 0; x < _grid.GetLength(0); x++)
                {
                    for (int y = 0; y < _grid.GetLength(1); y++)
                    {
                        sb.Append(_grid[x, y] ? "x" : " ");
                    }

                    sb.Append("\n");
                }

                return sb.ToString();
            }

            private void TurnOn(Tuple<int, int> from, Tuple<int, int> to)
            {
                for (var x = from.Item1; x <= to.Item1; x++)
                {
                    for (var y = from.Item2; y <= to.Item2; y++)
                    {
                        _grid[x, y] = true;
                    }
                }
            }

            private void TurnOff(Tuple<int, int> from, Tuple<int, int> to)
            {
                for (var x = from.Item1; x <= to.Item1; x++)
                {
                    for (var y = from.Item2; y <= to.Item2; y++)
                    {
                        _grid[x, y] = false;
                    }
                }
            }

            private void Toggle(Tuple<int, int> from, Tuple<int, int> to)
            {
                for (var x = from.Item1; x <= to.Item1; x++)
                {
                    for (var y = from.Item2; y <= to.Item2; y++)
                    {
                        _grid[x, y] = !_grid[x, y];
                    }
                }
            }
        }

         class LightGrid2
        {
            long[,] _grid;

            public LightGrid2(int size)
            {
                _grid = new long[size, size];
            }

            public long TotalBrightness
            {
                get
                {
                    long totalBrightness = 0;
                    foreach (var x in _grid)
                    {
                        totalBrightness += x;
                    }

                    return totalBrightness;
                }
            }

            public void Apply(string instructions)
            {
                short amount = 0;
                Tuple<int, int> from;
                Tuple<int, int> to;

                if (instructions.StartsWith("turn on"))
                {
                    instructions = instructions.Split("turn on")[1];
                    amount = 1;
                }
                else if (instructions.StartsWith("turn off"))
                {
                    instructions = instructions.Split("turn off")[1];
                    amount = -1;
                }
                else if (instructions.StartsWith("toggle"))
                {
                    instructions = instructions.Split("toggle")[1];
                    amount = 2;
                }
                else
                {
                    throw new Exception($"Unknown instructions {instructions}.");
                }

                var fromParts = instructions.Trim().Split(" through ")[0];
                from = new Tuple<int, int>(int.Parse(fromParts.Split(",")[0]), int.Parse(fromParts.Split(",")[1]));
                var toParts = instructions.Trim().Split(" through ")[1];
                to = new Tuple<int, int>(int.Parse(toParts.Split(",")[0]), int.Parse(toParts.Split(",")[1]));

                IncreaseBrightness(from, to, amount);
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (int x = 0; x < _grid.GetLength(0); x++)
                {
                    for (int y = 0; y < _grid.GetLength(1); y++)
                    {
                        if (_grid[x, y] > 9)
                        {
                            sb.Append("*");
                        }
                        else
                        {
                            sb.Append(_grid[x, y]);
                        }
                    }

                    sb.Append("\n");
                }

                return sb.ToString();
            }

            private void IncreaseBrightness(Tuple<int, int> from, Tuple<int, int> to, short amount)
            {
                for (var x = from.Item1; x <= to.Item1; x++)
                {
                    for (var y = from.Item2; y <= to.Item2; y++)
                    {
                        var res = _grid[x, y] + amount;
                        if (res < 0)
                        {
                            _grid[x, y] = 0;
                        }
                        else
                        {
                            _grid[x, y] = res;
                        }
                    }
                }
            }
        }
    }
}
