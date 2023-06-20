using System.Text;

namespace AdventOfCode2021
{
    class Day05
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var oceanFloor = new OceanFloor(1000);

            foreach (var line in File.ReadAllLines("input/5"))
            {
                var x1 = int.Parse(line.Split(" -> ")[0].Split(",")[0]);
                var y1 = int.Parse(line.Split(" -> ")[0].Split(",")[1]);
                var x2 = int.Parse(line.Split(" -> ")[1].Split(",")[0]);
                var y2 = int.Parse(line.Split(" -> ")[1].Split(",")[1]);
                oceanFloor.AddVents(x1, y1, x2, y2);
            }

            // Console.WriteLine(oceanFloor);
            Console.WriteLine($"Two or more vents on {oceanFloor.NrDangerAreas}");
        }

        private static void SolvePart2()
        {
            var oceanFloor = new OceanFloor(1000, diagonal: true);

            foreach (var line in File.ReadAllLines("input/5"))
            {
                var x1 = int.Parse(line.Split(" -> ")[0].Split(",")[0]);
                var y1 = int.Parse(line.Split(" -> ")[0].Split(",")[1]);
                var x2 = int.Parse(line.Split(" -> ")[1].Split(",")[0]);
                var y2 = int.Parse(line.Split(" -> ")[1].Split(",")[1]);
                oceanFloor.AddVents(x1, y1, x2, y2);
            }

            Console.WriteLine(oceanFloor);
            Console.WriteLine($"Two or more vents on {oceanFloor.NrDangerAreas}");
        }

        public class OceanFloor
        {
            int[,] _matrix;
            bool _diagonal;

            public int NrDangerAreas
            {
                get
                {
                    int nrDangerAreas = 0;
                    foreach (var n in _matrix)
                    {
                        if (n >= 2)
                        {
                            nrDangerAreas += 1;
                        }
                    }

                    return nrDangerAreas;
                }
            }
            public OceanFloor(int size, bool diagonal = false)
            {
                _matrix = new int[size, size];
                _diagonal = diagonal;
            }

            public void AddVents(int x1, int y1, int x2, int y2)
            {
                Console.WriteLine($"Add {x1},{y1} {x2},{y2}");

                if (x1 == x2)
                {
                    AddHorizontalVents(x1, y1, y2);
                }
                else if (y1 == y2)
                {
                    AddVerticalVents(y1, x1, x2);
                }
                else
                {
                    if (_diagonal)
                    {
                        AddDiagonalVents(x1, y1, x2, y2);
                    }
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (int x = 0; x < _matrix.GetLength(0); x++)
                {
                    for (int y = 0; y < _matrix.GetLength(1); y++)
                    {
                        if (_matrix[y, x] == 0)
                        {
                            sb.Append('.');
                        }
                        else
                        {
                            sb.Append(_matrix[y, x]);
                        }
                    }

                    sb.Append("\n");
                }

                return sb.ToString();
            }

            private void AddHorizontalVents(int x, int y1, int y2)
            {
                var yStart = Math.Min(y1, y2);
                var yEnd = Math.Max(y1, y2);

                for (int y = yStart; y <= yEnd; y++)
                {
                    _matrix[x, y] += 1;
                }
            }

            private void AddVerticalVents(int y, int x1, int x2)
            {
                var xStart = Math.Min(x1, x2);
                var xEnd = Math.Max(x1, x2);

                for (int x = xStart; x <= xEnd; x++)
                {
                    _matrix[x, y] += 1;
                }
            }

            private void AddDiagonalVents(int x1, int y1, int x2, int y2)
            {
                var xOffset = (x2 - x1) / Math.Abs(x2 - x1);
                var yOffset = (y2 - y1) / Math.Abs(y2 - y1);

                var x = x1;
                var y = y1;

                while (x != x2 && y != y2)
                {
                    _matrix[x, y] += 1;
                    x += xOffset;
                    y += yOffset;
                }
                _matrix[x, y] += 1;
            }
        }
    }
}
