using System.Text;

namespace AdventOfCode2021
{
    class Day25
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var seaCucumbers = new SeaCucumbers(File.ReadAllLines("input/25"));
            var steps = seaCucumbers.StepsToStable() + 1;
            Console.WriteLine($"Steps until stable: {steps}");
        }

        private static void SolvePart2()
        {
        }

        class SeaCucumbers
        {
            char[,] _matrix;

            public SeaCucumbers(string[] lines)
            {
                int w = lines[0].Length;
                int h = lines.Count();

                _matrix = new char[w, h];

                int y = 0;
                foreach (var line in lines)
                {
                    int x = 0;

                    foreach(var c in line)
                    {
                        _matrix[x, y] = c;
                        x += 1;
                    }

                    y += 1;
                }
            }

            public void Move(int steps)
            {
                for (int i = 0; i < steps; i++)
                {
                    Move();
                }
            }

            public int StepsToStable()
            {
                int steps = 0;

                while (Move())
                {
                    steps += 1;
                }

                return steps;
            }

            private bool Move()
            {
                var newMatrix = NewMatrix();

                for (var y = 0; y < _matrix.GetLength(1); y++)
                {
                    for (var x = 0; x < _matrix.GetLength(0); x++)
                    {
                        if (_matrix[x, y] == '>')
                        {
                            int newX = (x + 1) % _matrix.GetLength(0);

                            if (_matrix[newX, y] == '.')
                            {
                                newMatrix[newX, y] = '>';
                            }
                            else
                            {
                                newMatrix[x, y] = '>';
                            }
                        }
                    }
                }

                for (var y = 0; y < _matrix.GetLength(1); y++)
                {
                    for (var x = 0; x < _matrix.GetLength(0); x++)
                    {
                        if (_matrix[x, y] == 'v')
                        {
                            int newY = (y + 1) % _matrix.GetLength(1);

                            if (_matrix[x, newY] == 'v')
                            {
                                newMatrix[x, y] = 'v';
                            }
                            else
                            {
                                if (newMatrix[x, newY] == '.')
                                {
                                    newMatrix[x, newY] = 'v';
                                }
                                else
                                {
                                    newMatrix[x, y] = 'v';
                                }
                            }
                        }
                    }
                }

                var same = Same(_matrix, newMatrix);
                _matrix = newMatrix;

                return !same;
            }

            private char[,] NewMatrix()
            {
                var newMatrix = new char[_matrix.GetLength(0), _matrix.GetLength(1)];

                for (var y = 0; y < newMatrix.GetLength(1); y++)
                {
                    for (var x = 0; x < newMatrix.GetLength(0); x++)
                    {
                       newMatrix[x, y] = '.';
                    }
                }

                return newMatrix;
            }

            private bool Same(char[,] one, char [,] other)
            {
                for (var y = 0; y < one.GetLength(1); y++)
                {
                    for (var x = 0; x < one.GetLength(0); x++)
                    {
                       if (one[x, y] != other[x,y])
                       {
                           return false;
                       }
                    }
                }

                return true;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (var y = 0; y < _matrix.GetLength(1); y++)
                {
                    for (var x = 0; x < _matrix.GetLength(0); x++)
                    {
                        sb.Append(_matrix[x, y]);
                    }

                    sb.Append("\n");
                }

                return sb.ToString();
            }
        }
    }
}
