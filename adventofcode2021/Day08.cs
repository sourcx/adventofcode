using System.Data;
using System.Net;
using System.Text;

namespace AdventOfCode2021
{
    class Day08
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            int totalCount = 0;

            foreach (var line in File.ReadAllLines("input/8"))
            {
                var uniqSignalPatterns = line.Split(" | ")[0].Split(" ").ToList();
                var outputValues = line.Split(" | ")[1].Split(" ");
                totalCount += CountUniqLengths(outputValues);
            }

            Console.WriteLine($"There are {totalCount} occurrences of 1, 4, 7 or 8.");
        }

        private static void SolvePart2()
        {
            int sum = 0;

            foreach (var line in File.ReadAllLines("input/8"))
            {
                var uniqSignalPatterns = line.Split(" | ")[0].Split(" ").ToList();
                var outputValues = line.Split(" | ")[1].Split(" ");
                sum += DisplayOutput(uniqSignalPatterns, outputValues);
            }

            Console.WriteLine($"Sum of display outputs is {sum}.");

            if (sum != 1024649)
            {
                throw new Exception();
            }
        }

        private static int CountUniqLengths(string[] outputValues)
        {
            int count = 0;
            foreach(var output in outputValues)
            {
                if (output.Length == 2 || output.Length == 4 || output.Length == 3 || output.Length == 7)
                {
                    count += 1;
                }
            }

            return count;
        }

        private static int DisplayOutput(List<string> signalPatterns, string[] outputValues)
        {
            var patternsForDigits = new Dictionary<int, string>();

            // At this moment in time we only know the sets of characters for the digits 1, 4, 7 and 8.
            foreach (var pattern in signalPatterns)
            {
                if (pattern.Length == 2)
                {
                    patternsForDigits[1] = pattern;
                }
                else if (pattern.Length == 4)
                {
                    patternsForDigits[4] = pattern;
                }
                else if (pattern.Length == 3)
                {
                    patternsForDigits[7] = pattern;
                }
                else if (pattern.Length == 7)
                {
                    patternsForDigits[8] = pattern;
                }
            }

            var mappings = new Dictionary<char, char>();

            // First mapping is the top of the 7, which is 'a'.
            mappings['a'] = GetA(patternsForDigits[1], patternsForDigits[7]);

            // The 3 has three overlapping nrs with seven has length 5.
            patternsForDigits[3] = GetThree(signalPatterns, patternsForDigits[7]);

            // Difference between 3 and 7 are the 'd' and 'g'.
            var dOrG = GetDorG(patternsForDigits[3], patternsForDigits[7]);

            // The 'd' is in the 4.
            mappings['d'] = GetD(dOrG, patternsForDigits[4]);

            // The 'g' is not in the 4.
            mappings['g'] = GetG(dOrG, patternsForDigits[4]);

            // The 'b is in the 4 but not in the 7, disregarding 'a', 'd'.
            mappings['b'] = GetB(patternsForDigits[4], patternsForDigits[7], mappings['a'], mappings['d']);

            // The 6 has only one overlap with 1.
            patternsForDigits[6] = GetSix(signalPatterns, patternsForDigits[1]);

            // The 'c' is in the 7 but not in the 6, disregarding 'a', 'd', 'g'.
            mappings['c'] = GetC(patternsForDigits[7], patternsForDigits[6], mappings['a'], mappings['d'], mappings['g']);

            // The 'c' is in the 6 but not in the 7, disregarding 'a', 'd', 'g', 'b'.
            mappings['e'] = GetE(patternsForDigits[6], patternsForDigits[7], mappings['a'], mappings['d'], mappings['g'], mappings['b']);

            // The 2 contains 'a', 'c', 'd', 'e', 'g'
            patternsForDigits[2] = GetTwo(signalPatterns, mappings['a'], mappings['c'], mappings['d'], mappings['e'], mappings['g']);

            // The 'f' is the only unknown in 7.
            mappings['f'] = GetF(patternsForDigits[7], mappings['a'], mappings['c']);

            // The 0 contains 'a', 'b', 'c', 'e', 'f', 'g'.
            patternsForDigits[0] = GetMatchingNumber(signalPatterns, new List<char>() { mappings['a'], mappings['b'], mappings['c'], mappings['e'], mappings['f'], mappings['g'] }, 6);

            // The 5 contains 'a', 'b', 'd', 'f', 'g'.
            patternsForDigits[5] = GetMatchingNumber(signalPatterns, new List<char>() { mappings['a'], mappings['b'], mappings['d'], mappings['f'], mappings['g'] }, 5);

            // The 9 contains 'a', 'b', 'c', 'd', 'f', 'g'.
            patternsForDigits[9] = GetMatchingNumber(signalPatterns, new List<char>() { mappings['a'], mappings['b'], mappings['c'], mappings['d'], mappings['f'], mappings['g'] }, 6);

            var sb = new StringBuilder();

            foreach (var output in outputValues)
            {
                sb.Append(MatchingPattern(patternsForDigits, output));
            }

            return int.Parse(sb.ToString());
        }

        private static int MatchingPattern(Dictionary<int, string> patternsForDigits, string toSearch)
        {
            foreach (var pattern in patternsForDigits)
            {
                if (pattern.Value.Length != toSearch.Length)
                {
                    continue;
                }

                if (HasOverlappingChars(pattern.Value, toSearch, toSearch.Length))
                {
                    return pattern.Key;
                }
            }

            throw new Exception("Impossibru");
        }

        private static List<char> Diff(string one, string other)
        {
            var diff = new List<char>();

            foreach (var c in one)
            {
                if (!other.Contains(c))
                {
                    diff.Add(c);
                }
            }

            return diff;
        }

        private static char GetA(string one, string seven)
        {
            return Diff(seven, one).First();
        }

        private static List<char> GetDorG(string three, string seven)
        {
            return Diff(three, seven);
        }

        private static string GetThree(List<string> patterns, string seven)
        {
            foreach (var pattern in patterns)
            {
                if (pattern == seven)
                {
                    continue;
                }

                if (pattern.Length == 5 && HasOverlappingChars(seven, pattern, 3))
                {
                    return pattern;
                }
            }

            throw new Exception("Impossibru");
        }

        private static string GetSix(List<string> patterns, string one)
        {
            var sixes = new List<string>();

            foreach (var pattern in patterns)
            {
                if (pattern == one)
                {
                    continue;
                }

                if (pattern.Length == 6 && HasOverlappingChars(one, pattern, 1))
                {
                    sixes.Add(pattern);
                }
            }

            if (sixes.Count != 1)
            {
                throw new Exception("Impossibru");
            }

            return sixes.First();
        }

        private static string GetTwo(List<string> patterns, char a, char c, char d, char e, char g)
        {
            var twos = new List<string>();

            foreach (var pattern in patterns)
            {
                if (pattern.Length == 5 && pattern.Contains(a) && pattern.Contains(c) && pattern.Contains(d) && pattern.Contains(e) && pattern.Contains(g))
                {
                    twos.Add(pattern);
                }
            }

            if (twos.Count != 1)
            {
                throw new Exception("Impossibru");
            }

            return twos.First();
        }

        private static char GetD(List<char> dOrG, string four)
        {
            foreach (var c in dOrG)
            {
                if (four.Contains(c))
                {
                    return c;
                }
            }

            throw new Exception("Impossibru");
        }

        private static char GetG(List<char> dOrG, string four)
        {
            foreach (var c in dOrG)
            {
                if (!four.Contains(c))
                {
                    return c;
                }
            }

            throw new Exception("Impossibru");
        }

        private static bool HasOverlappingChars(string one, string other, int nrShouldOverlap)
        {
            int nrOverlap = 0;

            foreach (var c in one)
            {
                if (other.Contains(c))
                {
                    nrOverlap += 1;
                }
            }

            return nrOverlap == nrShouldOverlap;
        }

        private static char GetB(string four, string seven, char disregardA, char disregardD)
        {
            foreach (var c in four)
            {
                if (c != disregardA && c != disregardD && !seven.Contains(c))
                {
                    return c;
                }
            }

            throw new Exception("Impossibru");
        }

        private static char GetC(string seven, string six, char disregardA, char disregardD, char disregardG)
        {
            foreach (var c in seven)
            {
                if (c != disregardA && c != disregardD && c != disregardG && !six.Contains(c))
                {
                    return c;
                }
            }

            throw new Exception("Impossibru");
        }

        private static char GetE(string six, string seven, char disregardA, char disregardD, char disregardG, char disregardB)
        {
            foreach (var c in six)
            {
                if (c != disregardA && c != disregardD && c != disregardG && c != disregardB && !seven.Contains(c))
                {
                    return c;
                }
            }

            throw new Exception("Impossibru");
        }

        private static char GetF(string seven, char disregardA, char disregardC)
        {
            if (seven.Length != 3)
            {
                throw new Exception("Impossibru");
            }

            foreach (var c in seven)
            {
                if (c != disregardA && c != disregardC)
                {
                    return c;
                }
            }

            throw new Exception("Impossibru");
        }

        private static string GetMatchingNumber(List<string> patterns, List<char> mustContain, int length)
        {
            var numbers = new List<string>();

            foreach (var pattern in patterns)
            {
                if (pattern.Length == length && mustContain.Aggregate(true, (agg, c) => agg && pattern.Contains(c)))
                {
                    numbers.Add(pattern);
                }
            }

            if (numbers.Count != 1)
            {
                throw new Exception("Impossibru");
            }

            return numbers.First();
        }
    }
}
