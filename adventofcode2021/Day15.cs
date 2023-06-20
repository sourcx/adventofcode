namespace AdventOfCode2021
{
    class Day15
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var cave = new Cave(File.ReadAllLines("input/15"));
            Console.WriteLine(cave.ShortestPathRisk());
        }

        private static void SolvePart2()
        {
            var cave = new Cave(File.ReadAllLines("input/15"));
            cave.Expand(5);
            Console.WriteLine(cave.ShortestPathRisk());
        }

        public class Cave
        {
            int [,] _cost;
            List<(int, int)> _offsets;
            ValueTuple<int, int> _start = (0, 0);
            ValueTuple<int, int> _end;

            public Cave(string[] lines)
            {
                int w = lines[0].Length;
                int h = lines.Count();

                _cost = new int[w, h];

                int y = 0;
                foreach (var line in lines)
                {
                    int x = 0;

                    foreach(var c in line)
                    {
                        _cost[x, y] = int.Parse($"{c}");
                        x += 1;
                    }

                    y += 1;
                }

                _end = (_cost.GetLength(0) - 1, _cost.GetLength(1) - 1);
                _offsets = new List<(int, int)>() {           ( 0, -1),
                                                    (-1,  0),           (+1,  0),
                                                              ( 0, +1)           };
            }

            private IEnumerable<(int, int)> Neighbours((int, int) location)
            {
                foreach (var (offsetX, offsetY) in _offsets)
                {
                    var neighbour = (location.Item1 + offsetX, location.Item2 + offsetY);

                    if ( (neighbour.Item1 < 0 || neighbour.Item1 >= _cost.GetLength(0)) ||
                         (neighbour.Item2 < 0 || neighbour.Item2 >= _cost.GetLength(1)) )
                    {
                        // out of map
                    }
                    else
                    {
                        yield return neighbour;
                    }
                }
            }

            public long ShortestPathRisk()
            {
                var queue = new PriorityQueue<(int x, int y), long>();
                var visited = new HashSet<(int, int)>();
                var distances = new Dictionary<ValueTuple<int, int>, long>() { };
                distances[_start] = 0;

                queue.Enqueue(_start, 0);

                while (queue.Count > 0)
                {
                    var location = queue.Dequeue();

                    if (visited.Contains(location))
                    {
                        continue;
                    }

                    visited.Add(location);

                    if (location == _end)
                    {
                        break;
                    }

                    var cost = distances[location];

                    foreach (var neighbour in Neighbours(location))
                    {
                        if (visited.Contains(neighbour))
                        {
                            continue;
                        }

                        var totalCost = cost + _cost[neighbour.Item1, neighbour.Item2];

                        if (totalCost < distances.GetValueOrDefault(neighbour, long.MaxValue))
                        {
                            distances[neighbour] = totalCost;
                            queue.Enqueue(neighbour, totalCost);
                        }
                    }
                }

                return distances[_end];
            }

            public void Expand(int times)
            {
                int width = _cost.GetLength(0);
                int height = _cost.GetLength(1);

                var biggerCost = new int[width * times, height * times];

                for (int yMultiply = 0; yMultiply < times; yMultiply++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            for (int xMultiply = 0; xMultiply < times; xMultiply++)
                            {
                                var newCost = 1 + ((_cost[x, y] + (xMultiply) + (yMultiply) - 1) % 9);
                                biggerCost[(width * xMultiply) + x, (height * yMultiply) + y] = newCost;
                            }
                        }
                    }
                }

                _cost = biggerCost;
                _end = (_cost.GetLength(0) - 1, _cost.GetLength(1) - 1);
            }
        }
    }
}
