namespace Aoc.Util;

class Node
{
    public Dictionary<Node, int> Neighbours = new Dictionary<Node, int>();

    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X;

    public int Y;
}

class Graph
{
    public List<Node> Nodes;

    public Graph(string[] lines)
    {
        var nodes = new List<Node>();

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] != '#')
                {
                    var node = new Node(x, y);
                    nodes.Add(node);
                }
            }
        }

        foreach (var node in nodes)
        {
            foreach (var other in nodes)
            {
                if (node == other)
                {
                    continue;
                }

                if (node.X == other.X - 1 && node.Y == other.Y)
                {
                    node.Neighbours.Add(other, 1);
                }
                else if (node.X == other.X + 1 && node.Y == other.Y)
                {
                    node.Neighbours.Add(other, 1);
                }
                else if (node.X == other.X && node.Y == other.Y - 1)
                {
                    node.Neighbours.Add(other, 1);
                }
                else if (node.X == other.X && node.Y == other.Y + 1)
                {
                    node.Neighbours.Add(other, 1);
                }
            }
        }

        var simplified = new List<Node>();

        foreach (var node in nodes)
        {
            if (node.Neighbours.Count == 2)
            {
                var n1 = node.Neighbours.Keys.First();
                var n2 = node.Neighbours.Keys.Last();

                var dist = node.Neighbours[n1] + node.Neighbours[n2];

                n1.Neighbours.Remove(node);
                n2.Neighbours.Remove(node);

                n1.Neighbours.Add(n2, dist);
                n2.Neighbours.Add(n1, dist);
            }
            else
            {
                simplified.Add(node);
            }
        }

        Nodes = simplified;
    }

    public int FindLongestPath()
    {
        var travelledPath = new List<Node>
        {
            Nodes.First()
        };

        var longestPath = FindLongestPath(travelledPath, Nodes.Last());

        return PathDistance(longestPath);
    }

    private List<Node> FindLongestPath(List<Node> travelledPath, Node end)
    {
        var lastNode = travelledPath.Last();

        if (lastNode.X == end.X && lastNode.Y == end.Y)
        {
            return travelledPath;
        }

        var possibleMoves = GetPossibleMoves(lastNode, travelledPath);

        // Dead end.
        if (possibleMoves.Count == 0)
        {
            return travelledPath;
        }

        var longestPath = new List<Node>();

        foreach (var move in possibleMoves)
        {
            var newTravelledPath = new List<Node>(travelledPath)
            {
                move
            };

            var path = FindLongestPath(newTravelledPath, end);


            if (PathDistance(path) > PathDistance(longestPath) && path.Last().X == end.X && path.Last().Y == end.Y)
            {
                longestPath = path;
            }
        }

        return longestPath;
    }
    private List<Node> GetPossibleMoves(Node current, List<Node> travelledPath)
    {
        return current.Neighbours.Keys.Where(n => !travelledPath.Contains(n)).ToList();
    }

    private int PathDistance(List<Node> path)
    {
        var totalDistance = 0;

        for (int i = 0; i < path.Count - 1; i++)
        {
            totalDistance += path[i].Neighbours[path[i + 1]];
        }

        return totalDistance;
    }
}
