using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    class Day14
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var polymer = new Polymer(File.ReadAllLines("input/14"));

            polymer.Iterate(10);
            Console.WriteLine(polymer.Score());
        }

        private static void SolvePart2()
        {
            var polymer = new FastPolymer(File.ReadAllLines("input/14"));

            polymer.Iterate(40);
            Console.WriteLine(polymer.Score());
        }

        class Polymer
        {
            public string Chain;
            Dictionary<string, char> _rules;

            public Polymer(string[] lines)
            {
                Chain = lines[0];
                _rules = new Dictionary<string, char>();

                foreach (var line in lines.Skip(2))
                {
                    _rules.Add(line.Split(" -> ")[0], line.Split(" -> ")[1][0]);
                }
            }

            public void Iterate(int n = 1)
            {
                for (int i = 0; i < n; i++)
                {
                    Iterate();
                }
            }

            public long Score()
            {
                var chars = Chain.ToCharArray();
                var lowestCount = long.MaxValue;
                var highestCount = long.MinValue;

                foreach (var val in _rules.Values)
                {
                    var count = chars.Count(c => c == val);

                    if (count < lowestCount)
                    {
                        lowestCount = count;
                    }
                    if (count > highestCount)
                    {
                        highestCount = count;
                    }
                }

                return highestCount - lowestCount;
            }

            private void Iterate()
            {
                var newChain = new StringBuilder();

                for (int i = 0; i < Chain.Length - 1; i++)
                {
                    var pair = $"{Chain[i]}{Chain[i + 1]}";

                    if (_rules.TryGetValue(pair, out char val))
                    {
                        newChain.Append(Chain[i]);
                        newChain.Append(val);
                    }
                }

                newChain.Append(Chain.Last());

                Chain = newChain.ToString();
            }
        }

        class FastPolymer
        {
            Dictionary<string, long> _pairCounts = new Dictionary<string, long>();
            Dictionary<string, char> _rules = new Dictionary<string, char>();
            private char _lastChar = '!';

            public FastPolymer(string[] lines)
            {
                var startChain = lines[0];

                for (int i = 0; i < startChain.Length - 1; i++)
                {
                    var pair = $"{startChain[i]}{startChain[i + 1]}";

                    if (_pairCounts.ContainsKey(pair))
                    {
                        _pairCounts[pair] += 1;
                    }
                    else
                    {
                        _pairCounts[pair] = 1L;
                    }
                }
                _lastChar = startChain.Last();

                foreach (var line in lines.Skip(2))
                {
                    _rules.Add(line.Split(" -> ")[0], line.Split(" -> ")[1][0]);
                }
            }

            public void Iterate(int n = 1)
            {
                for (int i = 0; i < n; i++)
                {
                    // Console.WriteLine($"Iterate {i}");
                    Iterate();
                }
            }

            public long Score()
            {
                var lowestCount = long.MaxValue;
                var highestCount = long.MinValue;

                foreach (var val in _rules.Values.Distinct())
                {
                    long count = 0;
                    foreach (var pair in _pairCounts.Keys)
                    {
                        if (pair[0] == val)
                        {
                            count += _pairCounts[pair];
                        }
                    }

                    if (_lastChar == val)
                    {
                        count += 1;
                    }

                    if (count < lowestCount)
                    {
                        lowestCount = count;
                    }
                    if (count > highestCount)
                    {
                        highestCount = count;
                    }

                }

                return highestCount - lowestCount;
            }

            private void Iterate()
            {
                var newPairCounts = new Dictionary<string, long>();

                foreach (var pair in _pairCounts.Keys.Distinct())
                {
                    var count = _pairCounts[pair];

                    if (_rules.TryGetValue(pair, out char val))
                    {
                        var lhs = $"{pair[0]}{val}";

                        if (newPairCounts.ContainsKey(lhs))
                        {
                            newPairCounts[lhs] += count;
                        }
                        else
                        {
                            newPairCounts[lhs] = count;
                        }

                        var rhs = $"{val}{pair[1]}";

                        if (newPairCounts.ContainsKey(rhs))
                        {
                            newPairCounts[rhs] += count;
                        }
                        else
                        {
                            newPairCounts[rhs] = count;
                        }
                    }
                }

                _pairCounts = newPairCounts;
            }
        }
    }
}
