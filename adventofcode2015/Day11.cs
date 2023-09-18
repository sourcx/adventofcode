namespace AdventOfCode2015
{
    class Day11
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var password = "hepxcrrq";

            while (!IsValid(password))
            {
                password = Increment(password);
            }

            Console.WriteLine($"Next password is {password}");
        }

        private static bool IsValid(string password)
        {
            return HasStraight(password) && HasPairs(password) && !HasInvalidChars(password);
        }

        private static bool HasStraight(string password)
        {
            var i = 0;

            while (i < password.Length - 2)
            {
                if (password[i] == password[i + 1] - 1 && password[i] == password[i + 2] - 2)
                {
                    return true;
                }

                i++;
            }

            return false;
        }

        private static bool HasPairs(string password)
        {
            var pairs = 0;
            var i = 0;

            while (i < password.Length - 1)
            {
                if (password[i] == password[i + 1])
                {
                    pairs++;
                    i += 2;
                }
                else
                {
                    i++;
                }
            }

            return pairs >= 2;
        }

        private static bool HasInvalidChars(string password)
        {
            return password.Contains('i') || password.Contains('o') || password.Contains('l');
        }

        private static string Increment(string password)
        {
            var chars = password.ToCharArray();
            var i = chars.Length - 1;

            while (i >= 0)
            {
                if (chars[i] == 'z')
                {
                    chars[i] = 'a';
                    i--;
                }
                else
                {
                    chars[i]++;
                    break;
                }
            }

            return new string(chars);
        }

        private static void SolvePart2()
        {
            var password = "hepxcrrq";

            password = Increment(password);

            while (!IsValid(password))
            {
                password = Increment(password);
            }

            password = Increment(password);

            while (!IsValid(password))
            {
                password = Increment(password);
            }


            Console.WriteLine($"Next password is {password}");
        }
    }
}
