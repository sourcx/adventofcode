namespace AdventOfCode2015
{
    class Day05
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            long nice = 0;

            foreach (var line in File.ReadAllLines("input/5"))
            {
                if (IsNice(line))
                {
                    nice +=1 ;
                }
            }

            Console.WriteLine($"Nice strings: {nice}.");
        }

        private static void SolvePart2()
        {
             long nice = 0;

            foreach (var line in File.ReadAllLines("input/5"))
            {
                if (IsNice2(line))
                {
                    nice +=1;
                }
            }

            Console.WriteLine($"Super nice strings: {nice}."); // 29
        }

        private static bool IsNice(string input)
        {
            return ContainsEnoughVowels(input) && ContainsTwiceInARow(input) && NotContainsSpecifics(input);
        }

        private static bool ContainsEnoughVowels(string input)
        {
            int vowels = 0;

            foreach (var c in input)
            {
                if (c == 'a' || c == 'i' || c == 'u' || c == 'e' || c == 'o')
                {
                    vowels += 1;
                    if (vowels >= 3)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool ContainsTwiceInARow(string input)
        {
            for (int i = 0; i < input.Length - 1; ++i)
            {
                if (input[i] == input[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        private static bool NotContainsSpecifics(string input)
        {
            for (int i = 0; i < input.Length - 1; ++i)
            {
                if (input[i] == 'a' && input[i + 1] == 'b')
                {
                    return false;
                }
                if (input[i] == 'c' && input[i + 1] == 'd')
                {
                    return false;
                }
                if (input[i] == 'p' && input[i + 1] == 'q')
                {
                    return false;
                }
                if (input[i] == 'x' && input[i + 1] == 'y')
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsNice2(string input)
        {
            return ContainsTwoNonOverlappingPairs(input) && ContainsInbetweenRepeat(input);
        }

        // It contains a pair of any two letters that appears at least twice in the string without overlapping,
        // like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
        private static bool ContainsTwoNonOverlappingPairs(string input)
        {
            for (int i = 0; i < input.Length - 1; i += 1)
            {
                for (int j = i + 2; j < input.Length - 1; ++j)
                {
                    if (input[i] == input[j] && input[i + 1] == input[j + 1])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
        private static bool ContainsInbetweenRepeat(string input)
        {
            for (int i = 0; i < input.Length - 2; ++i)
            {
                if (input[i] == input[i + 2])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
