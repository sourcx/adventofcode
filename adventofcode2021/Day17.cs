namespace AdventOfCode2021
{
    class Day17
    {
        public static void Solve()
        {
            var input = File.ReadAllText("input/17");
            var inputX = input.Split(", ")[0].Split("x=")[1].Split("..");
            var targetX = (int.Parse(inputX[0]), int.Parse(inputX[1]));
            var inputY = input.Split(", ")[1].Split("y=")[1].Split("..");
            var targetY = (int.Parse(inputY[0]), int.Parse(inputY[1]));

            var pos = (0, 0);
            var success = 0;
            var maxHeight = int.MinValue;

            for (int x = -200; x < 200; x++)
            {
                for (int y = -200; y < 200; y++)
                {
                    var height = TryVelo((pos.Item1, pos.Item2), (x, y), targetX, targetY);
                    if (height > maxHeight)
                    {
                        maxHeight = height;
                    }
                    if (height > int.MinValue)
                    {
                        success += 1;
                    }
                }
            }

            Console.WriteLine($"Highest is {maxHeight}");
            Console.WriteLine($"Nr successes is {success}");
        }

        private static int TryVelo((int, int) pos, (int, int) velo, (int, int) targetX, (int, int) targetY)
        {
            int tries = 0;
            int maxHeight = int.MinValue;

            // while (!InArea(pos, targetX, targetY) && InScope(pos, velo, targetX, targetY))
            while (!InArea(pos, targetX, targetY) && tries < 500)
            {
                pos.Item1 += velo.Item1;
                pos.Item2 += velo.Item2;

                if (velo.Item1 < 0)
                {
                    velo.Item1 += 1;
                }
                else if (velo.Item1 > 0)
                {
                    velo.Item1 -= 1;
                }

                velo.Item2 -= 1;

                if (pos.Item2 > maxHeight)
                {
                    maxHeight = pos.Item2;
                }
                tries++;
            }

            if (InArea(pos, targetX, targetY))
            {
                return maxHeight;
            }
            else
            {
                return int.MinValue;
            }
        }

        private static void SolvePart2()
        {
        }

        private static bool InArea((int, int) pos, (int, int) x, (int, int) y)
        {
            return pos.Item1 >= x.Item1 && pos.Item1 <= x.Item2 &&
                   pos.Item2 >= y.Item1 && pos.Item2 <= y.Item2;
        }

        private static bool InScope((int, int) pos, (int, int) velo, (int, int) x, (int, int) y)
        {
            if (velo.Item1 >= 0 && pos.Item1 > x.Item2)
            {
                return false;
            }

            if (velo.Item1 < 0 && pos.Item1 < x.Item1)
            {
                return false;
            }

            if (velo.Item2 >= 0 && pos.Item2 > y.Item2)
            {
                return false;
            }

            if (velo.Item2 < 0 && pos.Item2 < y.Item1)
            {
                return false;
            }

            return true;
        }
    }
}
