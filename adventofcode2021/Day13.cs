using System.Text;

namespace AdventOfCode2021
{
    class Day13
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var dots = File.ReadAllText("input/13").Split("\r\n\r\n")[0].Split("\r\n");
            var folds = File.ReadAllText("input/13").Split("\r\n\r\n")[1].Split("\r\n");

            var paper = new Paper(dots);

            Console.WriteLine(paper.ToString(folds.First()));
            paper.Fold(folds.First());
            Console.WriteLine(paper.NrDots);
        }

          private static void SolvePart2()
        {
            var dots = File.ReadAllText("input/13").Split("\r\n\r\n")[0].Split("\r\n");
            var folds = File.ReadAllText("input/13").Split("\r\n\r\n")[1].Split("\r\n");

            var paper = new Paper(dots);

            foreach (var fold in folds)
            {
                if (string.IsNullOrEmpty(fold))
                {
                    continue;
                }

                paper.Fold(fold);
            }

            Console.WriteLine(paper);
        }

        class Paper
        {
            bool [,] _dots;

            public int NrDots
            {
                get
                {
                    int nr = 0;
                    for (var y = 0; y < _dots.GetLength(1); y++)
                    {
                        for (var x = 0; x < _dots.GetLength(0); x++)
                        {
                            if (_dots[x, y])
                            {
                                nr += 1;
                            }
                        }
                    }

                    return nr;
                }
            }

            public Paper(string [] dots)
            {
                var w = dots.Aggregate(0, (agg, dot) => Math.Max(agg, int.Parse(dot.Split(",")[0])));
                var h = dots.Aggregate(0, (agg, dot) => Math.Max(agg, int.Parse(dot.Split(",")[1])));

                _dots = new bool[w + 1, h + 1];

                foreach (var dot in dots)
                {
                    var x = int.Parse(dot.Split(",")[0]);
                    var y = int.Parse(dot.Split(",")[1]);
                    _dots[x, y] = true;
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (var y = 0; y < _dots.GetLength(1); y++)
                {
                    for (var x = 0; x < _dots.GetLength(0); x++)
                    {
                        sb.Append(_dots[x, y] ? "#" : ".");
                    }

                    sb.Append("\n");
                }

                return sb.ToString();
            }

            public string ToString(string fold)
            {
                var axis = fold.Split("fold along ")[1].Split("=")[0];
                var foldLine = int.Parse(fold.Split("fold along ")[1].Split("=")[1]);

                var sb = new StringBuilder();

                for (var y = 0; y < _dots.GetLength(1); y++)
                {
                    for (var x = 0; x < _dots.GetLength(0); x++)
                    {
                        if (axis == "x" && x == foldLine)
                        {
                            sb.Append("|");
                        }
                        else if (axis == "y" && y == foldLine)
                        {
                            sb.Append("-");
                        }
                        else
                        {
                            sb.Append(_dots[x, y] ? "#" : ".");
                        }
                    }

                    sb.Append("\n");
                }

                return sb.ToString();
            }

            // Assumes folds are always middle of the paper.
            public void Fold(string fold)
            {
                var axis = fold.Split("fold along ")[1].Split("=")[0];
                var foldLine = int.Parse(fold.Split("fold along ")[1].Split("=")[1]);

                if (axis == "x")
                {
                    _dots = FoldAlongX(foldLine);
                }
                else
                {
                    _dots = FoldAlongY(foldLine);
                }
            }

            private bool[,] FoldAlongX(int foldLine)
            {
                var newWidth = (_dots.GetLength(0) - 1) / 2;
                var newHeight = _dots.GetLength(1);
                var newDots = new bool [newWidth, newHeight];

                for (var x = 0; x <= _dots.GetLength(0) / 2 - 1; x++)
                {
                    for (var y = 0; y < _dots.GetLength(1); y++)
                    {
                        var oppositeX = _dots.GetLength(0) - 1 - x;
                        newDots[x, y] = _dots[oppositeX, y] || _dots[x, y];
                    }
                }

                return newDots;
            }

            private bool[,] FoldAlongY(int foldLine)
            {
                var newWidth = _dots.GetLength(0);
                var newHeight = (_dots.GetLength(1) - 1) / 2;
                var newDots = new bool [newWidth, newHeight];

                for (var y = 0; y <= _dots.GetLength(1) / 2 - 1; y++)
                {
                    for (var x = 0; x < _dots.GetLength(0); x++)
                    {
                        var oppositeY = _dots.GetLength(1) - 1 - y;
                        newDots[x, y] = _dots[x, oppositeY] || _dots[x, y];
                    }
                }

                return newDots;
            }
        }
    }
}
