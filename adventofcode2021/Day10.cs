using System.Net.NetworkInformation;
using System.Text;

namespace AdventOfCode2021
{
    class Day10
    {
        static readonly Dictionary<char, char> _closers = new Dictionary<char, char>();
        static readonly Dictionary<char, int> _corruptScores = new Dictionary<char, int>();
        static readonly Dictionary<char, char> _openers = new Dictionary<char, char>();
        static readonly Dictionary<char, int> _autocompleteScores = new Dictionary<char, int>();

        public static void Solve()
        {
            Init();
            SolvePart1();
            SolvePart2();
        }

        private static void Init()
        {
            _closers[')'] = '(';
            _closers[']'] = '[';
            _closers['}'] = '{';
            _closers['>'] = '<';

            _corruptScores[')'] = 3;
            _corruptScores[']'] = 57;
            _corruptScores['}'] = 1197;
            _corruptScores['>'] = 25137;

            _openers['('] = ')';
            _openers['['] = ']';
            _openers['{'] = '}';
            _openers['<'] = '>';

            _autocompleteScores[')'] = 1;
            _autocompleteScores[']'] = 2;
            _autocompleteScores['}'] = 3;
            _autocompleteScores['>'] = 4;
        }

        private static void SolvePart1()
        {
            long totalScore = 0;

            foreach (var line in File.ReadAllLines("input/10"))
            {
                totalScore += CorruptedScore(line);
            }

            Console.WriteLine($"Parsing error score is {totalScore}.");
        }

        private static void SolvePart2()
        {
            var scores = new List<long>();

            foreach (var line in File.ReadAllLines("input/10").Where(line => !IsCorrupted(line)))
            {
                scores.Add(AutoCompleteScore(line));
            }

            var sortedScores = scores.OrderBy(s => s).ToList();
            var score = sortedScores[sortedScores.Count() / 2];

            Console.WriteLine($"Auto complete score is {score}.");
        }

        private static int CorruptedScore(string line)
        {
            var stack = new Stack<char>();

            foreach (var c in line)
            {
                if (_closers.Values.Contains(c))
                {
                    stack.Push(c);
                }
                else if (_closers.Keys.Contains(c))
                {
                    if (stack.Peek() == _closers[c])
                    {
                        stack.Pop();
                    }
                    else
                    {
                        // Console.WriteLine($"Corrupted line {line}");
                        return _corruptScores[c];
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid instruction {c} on line {line}");
                    return 0;
                }
            }

            if (stack.Count == 0)
            {
                return 0;
            }

            // Console.WriteLine($"Incomplete line {line}");
            return 0;
        }

        private static bool IsCorrupted(string line)
        {
            return CorruptedScore(line) > 0;
        }

        private static long AutoCompleteScore(string line)
        {
            var stack = new Stack<char>();

            foreach (var c in line)
            {
                if (_closers.Values.Contains(c))
                {
                    stack.Push(c);
                }
                else if (_closers.Keys.Contains(c))
                {
                    if (stack.Peek() == _closers[c])
                    {
                        stack.Pop();
                    }
                    else
                    {
                        throw new Exception("Sus");
                    }
                }
                else
                {
                    throw new Exception($"Invalid instruction {c} on line {line}");
                }
            }

            if (stack.Count == 0)
            {
                return 0;
            }

            long totalScore = 0;

            foreach (var c in AutoComplete(stack))
            {
                totalScore *= 5;
                totalScore += _autocompleteScores[c];
            }

            return totalScore;
        }

        private static string AutoComplete(Stack<char> input)
        {
            var suffix = new StringBuilder();

            while (input.Any())
            {
                var last = input.Pop();

                if (_openers.Keys.Contains(last))
                {
                    suffix.Append(_openers[last]);
                }
            }
            return suffix.ToString();
        }
    }
}
