using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    class Day04
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var reader = File.OpenText("input/4");

            var draws = reader.ReadLine();
            reader.ReadLine();

            var boards = new List<Board>();

            while (!reader.EndOfStream)
            {
                var board = new Board(reader);
                boards.Add(board);
                reader.ReadLine();
            }

            foreach (var nr in draws.Split(',').Select(int.Parse))
            {
                foreach(var board in boards)
                {
                    board.Check(nr);
                    if (board.Bingo())
                    {
                        Console.WriteLine($"Winning board score is {board.Score(nr)}");
                        return;
                    }
                }
            }

            Console.WriteLine("No winner.");
        }

        private static void SolvePart2()
        {
            var reader = File.OpenText("input/4");

            var draws = reader.ReadLine();
            reader.ReadLine();

            var boards = new List<Board>();

            while (!reader.EndOfStream)
            {
                var board = new Board(reader);
                boards.Add(board);
                reader.ReadLine();
            }

            foreach (var nr in draws.Split(',').Select(int.Parse))
            {
                foreach(var board in boards)
                {
                    if (board.HasBingo)
                    {
                        continue;
                    }

                    board.Check(nr);

                    if (board.Bingo())
                    {
                        var nrTotalBingos = boards.Aggregate(0, (agg, board) => agg += (board.HasBingo) ? 1 : 0);
                        if (nrTotalBingos == boards.Count)
                        {
                            Console.WriteLine($"Losing board score is {board.Score(nr)}");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("No loser.");
        }

        public class Board
        {
            int [,] _numbers;
            bool [,] _hits = new bool[5,5];
            public bool HasBingo { get; set; }

            public Board(StreamReader reader)
            {
                _numbers = new int[5,5];

                for (int x = 0; x < 5; x++)
                {
                    var line = reader.ReadLine().Trim();
                    var parts = Regex.Split( line, @"\s{1,}");

                    for (int y = 0; y < 5; y++)
                    {
                        _numbers[x, y] = int.Parse(parts[y]);
                    }
                }
            }

            public void Check(int nr)
            {
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (_numbers[x, y] == nr)
                        {
                            _hits[x, y] = true;
                        }
                    }
                }
            }

            public bool Bingo()
            {
                if (HasBingo)
                {
                    return true;
                }

                HasBingo = HorizontalBingo() || VerticalBingo();
                return HasBingo;
            }

            public int Score(int lastCalledNr)
            {
                int sum = 0;

                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (!_hits[x, y])
                        {
                            sum += _numbers[x, y];
                        }
                    }
                }

                return sum * lastCalledNr;
            }

            private bool HorizontalBingo()
            {
                for (int x = 0; x < 5; x++)
                {
                    var bingo = true;

                    for (int y = 0; y < 5; y++)
                    {
                        if (!_hits[x, y])
                        {
                            bingo = false;
                        }
                    }

                    if (bingo)
                    {
                        return true;
                    }
                }

                return false;
            }

            private bool VerticalBingo()
            {
                for (int y = 0; y < 5; y++)
                {
                    var bingo = true;

                    for (int x = 0; x < 5; x++)
                    {
                        if (!_hits[x, y])
                        {
                            bingo = false;
                        }
                    }

                    if (bingo)
                    {
                        return true;
                    }
                }

                return false;
            }

            private bool DiagonalBingo()
            {
                return (_hits[0, 0] && _hits[1, 1] && _hits[2, 2] && _hits[3, 3] && _hits[4, 4]) ||
                       (_hits[4, 0] && _hits[3, 1] && _hits[2, 2] && _hits[1, 3] && _hits[0, 4]);
            }
        }
    }
}
