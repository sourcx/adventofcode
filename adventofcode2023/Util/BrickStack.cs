namespace Aoc.Util;

public class BrickStack
{
    // A list of all the bricks within the stack.
    List<Brick> Bricks = new List<Brick>();

    // For each coordinate in the stack a reference to the Id of the brick in that space.
    // x, y, z (starts at 0, 0, 1)
    // A value of 0 represents no cube in that spot as Cube Ids start at 1.
    int[][][] Matrix;

    public BrickStack(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var brick = new Brick(line, i + 1);

            Bricks.Add(brick);
        }

        // Initialize the matrix based on the max values of x, y, z.
        var maxX = Bricks.Max(b => b.Cubes.Max(c => c.X));
        var maxY = Bricks.Max(b => b.Cubes.Max(c => c.Y));
        var maxZ = Bricks.Max(b => b.Cubes.Max(c => c.Z));

        Matrix = new int[maxX + 1][][];

        for (int x = 0; x <= maxX; x++)
        {
            Matrix[x] = new int[maxY + 1][];
            for (int y = 0; y <= maxY; y++)
            {
                Matrix[x][y] = new int[maxZ + 1];
            }
        }

        // Fill the matrix with the brick Ids.
        foreach (var brick in Bricks)
        {
            foreach (var cube in brick.Cubes)
            {
                Matrix[cube.X][cube.Y][cube.Z] = brick.Id;
            }
        }
    }

    public BrickStack(BrickStack copy)
    {
        var matrixToCopy = copy.Matrix;
        Matrix = new int[matrixToCopy.Length][][];

        for (var x = 0; x < matrixToCopy.Length; x++)
        {
            Matrix[x] = new int[matrixToCopy[0].Length][];

            for (var y = 0; y < matrixToCopy[0].Length; y++)
            {
                Matrix[x][y] = new int[matrixToCopy[0][0].Length];

                for (var z = 0; z < matrixToCopy[0][0].Length; z++)
                {
                    Matrix[x][y][z] = matrixToCopy[x][y][z];
                }
            }
        }

        foreach (var brick in copy.Bricks)
        {
            Bricks.Add(new Brick(brick));
        }
    }

    public void PrintSideViewXAxis()
    {
        for (int x = 0; x < Matrix.Length / 2; x++)
        {
            Console.Write(" ");
        }
        Console.WriteLine("x");

        for (int x = 0; x < Matrix.Length; x++)
        {
            Console.Write(x);
        }
        Console.WriteLine();

        for (int z = Matrix[0][0].Length - 1; z > 0; z--)
        {
            for (int x = 0; x < Matrix.Length; x++)
            {
                var found = false;

                // Find first matching y to look at.
                for (int y = 0; y < Matrix[x].Length; y++)
                {
                    if (Matrix[x][y][z] != 0)
                    {
                        found = true;
                        Console.Write((char)(Matrix[x][y][z] + 'A' - 1));
                        break;
                    }
                }

                if (!found)
                {
                    Console.Write(".");
                }
            }

            if (z == Matrix[0][0].Length / 2)
            {
                Console.WriteLine($" {z} z");
            }
            else
            {
                Console.WriteLine($" {z}");
            }
        }

        for (int x = 0; x < Matrix.Length; x++)
        {
            Console.Write("-");
        }
        Console.WriteLine(" 0\n");
    }

    public void PrintSideViewYAxis()
    {
        for (int y = 0; y < Matrix[0].Length / 2; y++)
        {
            Console.Write(" ");
        }
        Console.WriteLine("y");

        for (int y = 0; y < Matrix[0].Length; y++)
        {
            Console.Write(y);
        }
        Console.WriteLine();

        for (int z = Matrix[0][0].Length - 1; z > 0; z--)
        {
            for (int y = 0; y < Matrix[0].Length; y++)
            {
                var found = false;

                // Find first matching x to look at.
                for (int x = 0; x < Matrix.Length; x++)
                {
                    if (Matrix[x][y][z] != 0)
                    {
                        found = true;
                        Console.Write((char)(Matrix[x][y][z] + 'A' - 1));
                        break;
                    }
                }

                if (!found)
                {
                    Console.Write(".");
                }
            }

            if (z == Matrix[0][0].Length / 2)
            {
                Console.WriteLine($" {z} z");
            }
            else
            {
                Console.WriteLine($" {z}");
            }
        }

        for (int y = 0; y < Matrix[0].Length; y++)
        {
            Console.Write("-");
        }
        Console.WriteLine(" 0\n");
    }

    public int Settle()
    {
        var settled = false;
        var movedBricks = new HashSet<int>();

        while (!settled)
        {
            settled = true;

            foreach (var brick in Bricks)
            {
                if (CanMoveDown(brick))
                {
                    MoveBrickDownOneRow(brick);
                    movedBricks.Add(brick.Id);
                    // PrintSideViewXAxis();
                    // PrintSideViewYAxis();
                    // MoveBrickDownAllTheWay(brick);
                    // PrintSideViewXAxis();
                    // PrintSideViewYAxis();
                    settled = false;
                }
            }
        }

        return movedBricks.Count;
    }

    private bool CanMoveDown(Brick brick)
    {
        foreach (var cube in brick.Cubes)
        {
            if (cube.Z <= 1)
            {
                return false;
            }

            if (Matrix[cube.X][cube.Y][cube.Z - 1] != 0 && !brick.ContainsCube(cube.X, cube.Y, cube.Z - 1))
            {
                return false;
            }
        }

        return true;
    }

    private void MoveBrickDownOneRow(Brick brick)
    {
        foreach (var cube in brick.Cubes)
        {
            Matrix[cube.X][cube.Y][cube.Z] = 0;
            cube.Z--;
            Matrix[cube.X][cube.Y][cube.Z] = brick.Id;
        }
    }

    private void MoveBrickDownAllTheWay(Brick brick)
    {
        var maxOffsets = new HashSet<int>();

        foreach (var cube in brick.Cubes)
        {
            var maxZOffset = 1;

            while (Matrix[cube.X][cube.Y][cube.Z - maxZOffset - 1] == 0 && cube.Z - maxZOffset > 1)
            {
                maxZOffset++;
            }

            maxOffsets.Add(maxZOffset);
        }

        var minOffset = maxOffsets.Min();

        foreach (var cube in brick.Cubes)
        {
            for (int i = minOffset; i > 0; i--)
            {
                Matrix[cube.X][cube.Y][cube.Z] = 0;
                cube.Z--;
                Matrix[cube.X][cube.Y][cube.Z] = brick.Id;
            }
        }
    }

    public int GetDesintegratedBrickCount()
    {
        var desintegrations = 0;

        foreach (var brick in Bricks)
        {
            if (CanDesintegrate(brick))
            {
                desintegrations += 1;
            }
        }

        return desintegrations;
    }

    public int GetBestChainReaction()
    {
        var chainReactionScore = 0;

        foreach (var brick in Bricks)
        {
            // if (!CanDesintegrate(brick))
            // {
            var nrFallingBricks = GetNrFallingBricks(brick);
            Console.WriteLine($"Brick {brick.Id} will cause {nrFallingBricks} bricks to fall.");
            chainReactionScore += nrFallingBricks;
            // }
        }

        return chainReactionScore;
    }

    private bool CanDesintegrate(Brick brick)
    {
        var canMoveDown = true;

        // Set positions temparily empty.
        foreach (var cube in brick.Cubes)
        {
            Matrix[cube.X][cube.Y][cube.Z] = 0;
        }

        foreach (var otherBrick in Bricks)
        {
            if (otherBrick.Id == brick.Id)
            {
                continue;
            }

            if (CanMoveDown(otherBrick))
            {
                canMoveDown = false;
                break;
            }
        }

        // Set positions back.
        foreach (var cube in brick.Cubes)
        {
            Matrix[cube.X][cube.Y][cube.Z] = brick.Id;
        }

        return canMoveDown;
    }

    private int GetNrFallingBricks(Brick desintegratedBrick)
    {
        var brickStackCopy = new BrickStack(this);

        var removed = brickStackCopy.Bricks.RemoveAll(b => b.Id == desintegratedBrick.Id);

        foreach (var cube in desintegratedBrick.Cubes)
        {
            brickStackCopy.Matrix[cube.X][cube.Y][cube.Z] = 0;
        }

        var x = brickStackCopy.Settle();
        return x;
    }
}
