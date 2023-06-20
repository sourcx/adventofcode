namespace AdventOfCode2021
{
    class Day22
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var grid = new Grid(File.ReadAllLines("input/22"));
            Console.WriteLine($"Enabled: {grid.NrEnabled()}");
        }

        private static void SolvePart2()
        {
            var cuboidList = new CuboidList(File.ReadAllLines("input/22"));
            Console.WriteLine($"Enabled: {cuboidList.Volume()}");
        }

        class Grid
        {
            private bool[,,] _grid;

            public Grid(string[] lines)
            {
                _grid = new bool[100, 100, 100];

                foreach (var line in lines.SkipLast(400))
                {
                    var toggle = line.Split(" ")[0].Equals("on");
                    var x = line.Split(" ")[1].Split(",")[0];
                    var y = line.Split(" ")[1].Split(",")[1];
                    var z = line.Split(" ")[1].Split(",")[2];
                    var fromX = int.Parse(x.Split("=")[1].Split("..")[0]);
                    var toX = int.Parse(x.Split("=")[1].Split("..")[1]);
                    var fromY = int.Parse(y.Split("=")[1].Split("..")[0]);
                    var toY = int.Parse(y.Split("=")[1].Split("..")[1]);
                    var fromZ = int.Parse(z.Split("=")[1].Split("..")[0]);
                    var toZ = int.Parse(z.Split("=")[1].Split("..")[1]);
                    Toggle(toggle, (fromX, toX), (fromY, toY), (fromZ, toZ));
                }
            }

            private void Toggle(bool toggle, (int, int) x, (int, int) y, (int, int) z)
            {
                for (int u = x.Item1; u <= x.Item2; u++)
                {
                    for (int v = y.Item1; v <= y.Item2; v++)
                    {
                        for (int w = z.Item1; w <= z.Item2; w++)
                        {
                            _grid[u + 50, v + 50, w + 50] = toggle;
                        }
                    }
                }
            }

            public int NrEnabled()
            {
                int enabled = 0;

                for (int x = 0; x < _grid.GetLength(0); x++)
                {
                    for (int y = 0; y < _grid.GetLength(1); y++)
                    {
                        for (int z = 0; z < _grid.GetLength(2); z++)
                        {
                            if (_grid[x, y, z])
                            {
                                enabled += 1;
                            }
                        }
                    }
                }

                return enabled;
            }
        }

        class CuboidList
        {
            List<Cuboid> _list;

            public CuboidList(string[] lines)
            {
                _list = new List<Cuboid>();

                foreach (var line in lines)
                {
                    var cuboid = new Cuboid(line);
                    var toAdd = new List<Cuboid>();

                    if (cuboid.IsOn)
                    {
                        toAdd.Add(cuboid);
                    }

                    foreach (var other in _list)
                    {
                        var intersection = cuboid.Intersect(other, !other.IsOn);
                        if (intersection != null)
                        {
                            toAdd.Add(intersection);
                        }
                    }

                    _list.AddRange(toAdd);
                }
            }

            public long Volume()
                => _list.Aggregate(0L, (agg, cube) => agg + (cube.Volume() * (cube.IsOn ? 1 : -1)));
          }

        // Represents one cuboid by means of from-to coordinates in 3 dimensions.
        class Cuboid
        {
            public (int, int) X;
            public (int, int) Y;
            public (int, int) Z;
            public bool IsOn;

            public Cuboid(string line)
            {
                IsOn = line.Split(" ")[0].Equals("on");
                var xPart = line.Split(" ")[1].Split(",")[0];
                var yPart = line.Split(" ")[1].Split(",")[1];
                var zPart = line.Split(" ")[1].Split(",")[2];
                var fromX = int.Parse(xPart.Split("=")[1].Split("..")[0]);
                var toX = int.Parse(xPart.Split("=")[1].Split("..")[1]);
                var fromY = int.Parse(yPart.Split("=")[1].Split("..")[0]);
                var toY = int.Parse(yPart.Split("=")[1].Split("..")[1]);
                var fromZ = int.Parse(zPart.Split("=")[1].Split("..")[0]);
                var toZ = int.Parse(zPart.Split("=")[1].Split("..")[1]);
                X = (fromX, toX);
                Y = (fromY, toY);
                Z = (fromZ, toZ);
            }

            public Cuboid((int, int) x, (int, int) y, (int, int) z, bool isOn)
            {
                X = x;
                Y = y;
                Z = z;
                IsOn = isOn;
            }

            public long Volume()
                => (X.Item2 - X.Item1 + 1L) * (Y.Item2 - Y.Item1 + 1L) * (Z.Item2 - Z.Item1 + 1L);

            public Cuboid? Intersect(Cuboid other, bool isOn)
            {
                if (X.Item1 > other.X.Item2 || X.Item2 < other.X.Item1 ||
                    Y.Item1 > other.Y.Item2 || Y.Item2 < other.Y.Item1 ||
                    Z.Item1 > other.Z.Item2 || Z.Item2 < other.Z.Item1)
                {
                    return null;
                }

                return new Cuboid(
                    (Math.Max(X.Item1, other.X.Item1), Math.Min(X.Item2, other.X.Item2)),
                    (Math.Max(Y.Item1, other.Y.Item1), Math.Min(Y.Item2, other.Y.Item2)),
                    (Math.Max(Z.Item1, other.Z.Item1), Math.Min(Z.Item2, other.Z.Item2)), isOn);
            }
        }
    }
}
