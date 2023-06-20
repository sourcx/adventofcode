using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2015
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
            string key = "yzbqklnj";
            long suffix = 0;
            using var md5 = MD5.Create();

            while (true)
            {
                var hash = Hash(md5, key, suffix);

                if (hash[0] == '0' && hash[1] == '0' && hash[2] == '0' && hash[3] == '0' && hash[4] == '0')
                {
                    break;
                }

                suffix += 1;
            }
            
            Console.WriteLine($"Magic number for {key} is {suffix}.");            
        }

        private static void SolvePart2()
        {
            string key = "yzbqklnj";
            long suffix = 0;
            using var md5 = MD5.Create();

            while (true)
            {
                var hash = Hash(md5, key, suffix);

                if (hash[0] == '0' && hash[1] == '0' && hash[2] == '0' && hash[3] == '0' && hash[4] == '0' && hash[5] == '0')
                {
                    break;
                }

                suffix += 1;
            }
            
            Console.WriteLine($"Magic number for {key} is {suffix}.");         
        }

        public static string Hash(MD5 md5, string input, long suffix)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{input}{suffix.ToString()}");
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
