using System.Text;

namespace AdventOfCode2021
{
    class Day09
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var cave = new Cave(File.ReadAllLines("input/9"));
            Console.WriteLine($"Risk level total of cave {cave.RiskLevel()}");
        }

        private static void SolvePart2()
        {
            var cave = new Cave(File.ReadAllLines("input/9"));
            // Console.WriteLine($"{cave.ToString()}");
            Console.WriteLine($"Basin score of cave {cave.TotalBasinScore()}");
        }

        public class Cave
        {
            int [,] _matrix;
            bool [,] _lowPoints;
            bool [,] _visited;

            public Cave(string[] lines)
            {
                int w = lines[0].Length;
                int h = lines.Count();

                _matrix = new int[w + 2, h + 2];
                _lowPoints = new bool[w + 2, h + 2];
                _visited = new bool[w + 2, h + 2];

                int y = 1;
                foreach (var line in lines)
                {
                    int x = 1;

                    foreach(var c in line)
                    {
                        _matrix[x, y] = int.Parse($"{c}");
                        x += 1;
                    }

                    y += 1;
                }

                AddBorder();
                DetermineLowPoints();
            }

            public long RiskLevel()
            {
                long totalRisk = 0;

                for (int x = 1; x < _matrix.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < _matrix.GetLength(1) - 1; y++)
                    {
                        if (_lowPoints[x, y])
                        {
                            totalRisk += _matrix[x, y] + 1;
                        }
                    }
                }

                return totalRisk;
            }

            private void AddBorder()
            {
                for (int x = 0; x < _matrix.GetLength(0); x++)
                {
                    _matrix[x, 0] = 9;
                    _matrix[x, _matrix.GetLength(1) - 1] = 9;
                }

                for (int y = 0; y < _matrix.GetLength(1); y++)
                {
                    _matrix[0, y] = 9;
                    _matrix[_matrix.GetLength(0) - 1, y] = 9;
                }
            }

            private void DetermineLowPoints()
            {
                for (int x = 1; x < _matrix.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < _matrix.GetLength(1) - 1; y++)
                    {
                        if (_matrix[x, y] < _matrix[x - 1, y] &&
                            _matrix[x, y] < _matrix[x + 1, y] &&
                            _matrix[x, y] < _matrix[x, y - 1] &&
                            _matrix[x, y] < _matrix[x, y + 1])
                        {
                            _lowPoints[x, y] = true;
                        }
                    }
                }
            }

            public int TotalBasinScore()
            {
                var basinScores = new List<int>();

                for (int x = 1; x < _matrix.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < _matrix.GetLength(1) - 1; y++)
                    {
                        if (_lowPoints[x, y])
                        {
                            basinScores.Add(BasinScore(x, y));
                        }
                    }
                }

                var orderedBasinScores = basinScores.OrderByDescending(b => b).ToList();
                return orderedBasinScores[0] * orderedBasinScores[1] * orderedBasinScores[2];
            }

            private int BasinScore(int x, int y)
            {
                return 1 + GetNeighbours(x, y).Count();
            }

            private List<(int, int)> GetNeighbours(int x, int y)
            {
                _visited[x, y] = true;
                var neighbours = new List<(int, int)>();

                if (!_visited[x - 1, y] && _matrix[x - 1, y] != 9)
                {
                    neighbours.Add((x - 1, y));
                    neighbours.AddRange(GetNeighbours(x - 1, y));
                }

                if (!_visited[x + 1, y] && _matrix[x + 1, y] != 9)
                {
                    neighbours.Add((x + 1, y));
                    neighbours.AddRange(GetNeighbours(x + 1, y));
                }

                if (!_visited[x, y - 1] && _matrix[x, y - 1] != 9)
                {
                    neighbours.Add((x, y - 1));
                    neighbours.AddRange(GetNeighbours(x, y - 1));
                }

                if (!_visited[x, y + 1] && _matrix[x, y + 1] != 9)
                {
                    neighbours.Add((x, y + 1));
                    neighbours.AddRange(GetNeighbours(x, y + 1));
                }

                return neighbours;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (int x = 0; x < _matrix.GetLength(0); x++)
                {
                    for (int y = 0; y < _matrix.GetLength(1); y++)
                    {
                        sb.Append(_matrix[x, y] == 9 ? "." : _matrix[x, y]);
                    }

                    sb.Append("\n");
                }

                return sb.ToString();
            }
        }
    }
}
