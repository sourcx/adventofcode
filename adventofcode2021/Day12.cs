using System.Text;

namespace AdventOfCode2021
{
    class Day12
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var cave = new Cave();
            var lines = File.ReadAllLines("input/12");

            foreach (var line in lines)
            {
                cave.AddPath(line.Split("-")[0], line.Split("-")[1]);
            }

            var paths = cave.FindPathsV1().ToList();
            Console.WriteLine($"Cave paths {paths.Count}.");
            if (paths.Count != 4707)
            {
                throw new Exception();
            }
        }

        private static void SolvePart2()
        {
            var cave = new Cave();
            var lines = File.ReadAllLines("input/12");

            foreach (var line in lines)
            {
                cave.AddPath(line.Split("-")[0], line.Split("-")[1]);
            }

            var paths = cave.FindPathsV2().ToList();
            Console.WriteLine($"Cave paths {paths.Count}.");
            if (paths.Count != 130493)
            {
                throw new Exception();
            }
        }

        class Cave
        {
            private List<Node> _nodes = new List<Node>();

            public void AddPath(string fromName, string toName)
            {
                var from = _nodes.FindLast(x => x.Name == fromName);

                if (from == null)
                {
                    from = new Node(fromName);
                    _nodes.Add(from);
                }

                var to = _nodes.FindLast(x => x.Name == toName);

                if (to == null)
                {
                    to = new Node(toName);
                    _nodes.Add(to);
                }

                from.Neighbours.Add(to);
                to.Neighbours.Add(from);
            }

            public List<string> FindPathsV1()
            {
                return FindPaths("start", "end", new List<string>(), ValidVisitsV1);
            }

            public List<string> FindPathsV2()
            {
                return FindPaths("start", "end", new List<string>(), ValidVisitsV2);
            }

            private List<string> FindPaths(string startName, string endName, List<string> visited, Func<List<string>, Node, bool> ValidVisits)
            {
                if (startName == endName)
                {
                    return new List<string>() { "end" };
                }

                visited.Add(startName);
                var start = _nodes.FindLast(x => x.Name == startName);
                var subPaths = new List<string>();

                foreach (var neighbour in start.Neighbours)
                {
                    if (!ValidVisits(visited, neighbour))
                    {
                        continue;
                    }

                    foreach (var path in FindPaths(neighbour.Name, endName, new List<string>(visited), ValidVisits))
                    {
                        subPaths.Add($"{startName},{path}");
                    }
                }

                return subPaths;
            }

            private bool ValidVisitsV1(List<string> visited, Node neighbour)
            {
                return neighbour.isLarge || !visited.Contains(neighbour.Name);
            }

            private bool ValidVisitsV2(List<string> visited, Node neighbour)
            {
                if (neighbour.isLarge)
                {
                    return true;
                }

                if (neighbour.Name == "start" && visited.Contains(neighbour.Name))
                {
                    return false;
                }

                var visitedHasDoubleOccurence = false;

                foreach (var nodeName in visited)
                {
                    var node = _nodes.FindLast(x => x.Name == nodeName);

                    if (visited.FindAll(x => x == nodeName && !node.isLarge).Count > 1)
                    {
                        visitedHasDoubleOccurence = true;
                    }
                }

                if (visited.Contains(neighbour.Name) && !visitedHasDoubleOccurence)
                {
                    return true;
                }

                return !visited.Contains(neighbour.Name);
            }
        }

        class Node
        {
            public string Name { get; set; }
            public bool isLarge { get; set; }
            public HashSet<Node> Neighbours { get; set; }

            public Node(string name)
            {
                Name = name;
                isLarge = name.ToUpper().Equals(name);
                Neighbours = new HashSet<Node>();
            }
        }
    }
}
