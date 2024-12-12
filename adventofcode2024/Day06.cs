using System.Diagnostics;

namespace adventofcode2024;

class Day06
{
    const char GuardUp = '^';
    const char GuardRight = '>';
    const char GuardDown = 'v';
    const char GuardLeft = '<';
    const char Obstacle = '#';
    const char PlacedObstacle = 'O';
    const char Empty = '.';
    const char Visited = 'X';
    const char Outside = '_';

    public void Run()
    {
        Util.PrintInPlace(GetType().Name);

        // var fileName = "testinput/6";
        var fileName = "input/6";

        RunPart1(fileName);
        RunPart2(fileName);
    }

    private void RunPart1(string fileName)
    {
        var matrix = Util.ReadMatrix(fileName);
        Util.PrintInPlace(matrix);

        matrix = MoveGuardUntilSheLeaves(matrix);
        var nrVisited = GetDistinctVisited(matrix).Count;
        Util.PrintInPlace(matrix);

        Util.PrintInPlace($"How many distinct positions will the guard visit before leaving the mapped area? {nrVisited}");
        Console.WriteLine();
    }

    private List<(int x, int y)> GetDistinctVisited(char[,] matrix)
    {
        var visited = new List<(int x, int y)>();

        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (matrix[y, x] == Visited)
                {
                    visited.Add((x, y));
                }
            }
        }

        return visited;
    }

    private char[,] MoveGuardUntilSheLeaves(char[,] matrix)
    {
        // Loop detection. If guard in same position with same guardIcon (direction).
        var visited = new HashSet<(char, int, int)>();

        var currentPos = FindGuard(matrix);

        while (true)
        {
            var guardIcon = Get(matrix, currentPos);
            var (dx, dy) = GetDirection(guardIcon);
            var nextPos = (currentPos.x + dx, currentPos.y + dy);
            var thingAtNextPos = Get(matrix, nextPos);

            // Util.PrintInPlace($"Guard {guardIcon} at {currentPos.x}, {currentPos.y} moves to {nextPos.Item1}, {nextPos.Item2} where there is a {thingAtNextPos}");

            if (thingAtNextPos == Obstacle || thingAtNextPos == PlacedObstacle)
            {
                matrix = HandleObstacle(matrix, currentPos, guardIcon);
            }
            else if (thingAtNextPos == Empty || thingAtNextPos == Visited)
            {
                matrix = MoveGuard(matrix, currentPos, nextPos, guardIcon);
                currentPos = nextPos;
            }
            else if (thingAtNextPos == Outside)
            {
                matrix[currentPos.y, currentPos.x] = Visited;
                // Util.PrintInPlace(matrix);
                break;
            }

            if (visited.Contains((guardIcon, nextPos.Item1, nextPos.Item2)))
            {
                // Console.WriteLine($"Loop detected.");
                // Util.PrintInPlace(matrix);
                throw new Util.AocException();
            }
            else
            {
                visited.Add((guardIcon, nextPos.Item1, nextPos.Item2));
            }

            // Util.PrintInPlace(matrix); Thread.Sleep(30);
        }

        return matrix;
    }

    // Rotates guard.
    private char[,] HandleObstacle(char[,] matrix, (int x, int y) pos, char guardIcon)
    {
        matrix[pos.y, pos.x] = guardIcon switch
        {
            GuardUp => GuardRight,
            GuardRight => GuardDown,
            GuardDown => GuardLeft,
            GuardLeft => GuardUp,
            _ => throw new Exception("Invalid guard icon")
        };

        return matrix;
    }

    private char[,] MoveGuard(char[,] matrix, (int x, int y) currentPos, (int x, int y) nextPos, char guardIcon)
    {
        matrix[currentPos.y, currentPos.x] = Visited;
        matrix[nextPos.y, nextPos.x] = guardIcon;

        return matrix;
    }

    private (int x, int y) FindGuard(char[,] matrix)
    {
        for (var y = 0; y < matrix.GetLength(0); y++)
        {
            for (var x = 0; x < matrix.GetLength(1); x++)
            {
                if (matrix[y, x] == GuardUp || matrix[y, x] == GuardDown || matrix[y, x] == GuardLeft || matrix[y, x] == GuardRight)
                {
                    return (x, y);
                }
            }
        }

        return (-1, -1);
    }

    private (int dx, int dy) GetDirection(char guard)
    {
        return guard switch
        {
            GuardUp => (0, -1),
            GuardRight => (1, 0),
            GuardDown => (0, 1),
            GuardLeft => (-1, 0),
            _ => throw new Exception($"Not a valid guard '{guard}'")
        };
    }

    private char Get(char[,] matrix, (int x, int y) pos)
    {
        return Get(matrix, pos.x, pos.y);
    }

    private char Get(char[,] matrix, int x, int y)
    {
        if (y < 0 || y >= matrix.GetLength(0) || x < 0 || x >= matrix.GetLength(1))
        {
            return Outside;
        }

        return matrix[y, x];
    }

    private void RunPart2(string fileName)
    {
        var matrix = Util.ReadMatrix(fileName);
        var guardStartingPos = FindGuard(matrix);
        // Util.PrintInPlace("Original matrix");
        // Util.PrintInPlace(matrix);
        // Thread.Sleep(1000);

        var visits = MoveGuardUntilSheLeaves(Util.Copy(matrix));
        // Util.PrintInPlace("Visits matrix");
        // Util.PrintInPlace(visits);
        // Thread.Sleep(1000);

        var potentialObstacles = GetDistinctVisited(visits);
        var obstaclesGeneratingLoop = new List<(int x, int y)>();

        var stopwatch = new Stopwatch();
        foreach (var obstacle in potentialObstacles)
        {
            if (obstacle == guardStartingPos)
            {
                continue;
            }

            stopwatch.Start();

            bool found;
            try
            {
                var matrix3 = Util.Copy(matrix);
                matrix3[obstacle.y, obstacle.x] = PlacedObstacle;
                MoveGuardUntilSheLeaves(matrix3);
                found = false;
            }
            catch (Util.AocException)
            {
                found = true;
            }

            stopwatch.Stop();

            if (found)
            {
                Util.PrintInPlace($"Loop detected at {obstacle.x}, {obstacle.y} in {stopwatch.ElapsedMilliseconds} ms");
                obstaclesGeneratingLoop.Add(obstacle);
            }
            else
            {
                Util.PrintInPlace($"No loop detected at {obstacle.x}, {obstacle.y} in {stopwatch.ElapsedMilliseconds} ms");
            }

            stopwatch.Reset();
        }

        // 3080 too high
        Util.PrintInPlace($"How many different positions could you choose for this obstruction? {obstaclesGeneratingLoop.Count}");
    }
}
