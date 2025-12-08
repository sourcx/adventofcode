using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace adventofcode2025;

class Day06
{
    public void Run()
    {
        Console.WriteLine(GetType().Name);

        Part1();
        Part2();
    }

    private void Part1()
    {
        var result = BigInteger.Zero;
        var problems = ReadProblems();

        foreach (var problem in problems)
        {
            result += problem.Solve();
        }

        Console.WriteLine($"Result from all lines: {result}");
    }

    private void Part2()
    {
        var result = BigInteger.Zero;
        var problems = ReadProblems2();

        foreach (var problem in problems)
        {
            result += problem.Solve();
        }

        Console.WriteLine($"Result from all lines: {result}");
    }

    private List<Problem> ReadProblems()
    {
        var problems = new List<Problem>();

        var numberLines = new List<List<int>>();
        var ops = new List<char>();

        foreach (var line in File.ReadLines("testinput/6"))
        {
            if (line.Trim().Length == 0)
            {
                continue;
            }

            if (line.Contains('+') || line.Contains('*'))
            {
                ops = ReadOperators(line);
            }
            else
            {
                numberLines.Add(ReadNumbers(line));
            }
        }

        for (int i = 0; i < ops.Count; i++)
        {
            var numbers = new List<BigInteger>();

            foreach (var numberLine in numberLines)
            {
                if (i < numberLine.Count)
                {
                    numbers.Add(numberLine[i]);
                }
            }

            problems.Add(new Problem(ops[i], numbers));
        }

        return problems;
    }


    private List<Problem2> ReadProblems2()
    {
        var problems = new List<Problem2>();
        var lines = new List<string>();
        var ops = new List<char>();

        foreach (var line in File.ReadLines("input/6"))
        {
            if (line.Trim().Length == 0)
            {
                continue;
            }

            if (line.Contains('+') || line.Contains('*'))
            {
                ops = ReadOperators(line);
            }
            else
            {
                lines.Add(line);
            }
        }

        var longestLineLength = lines.Max(l => l.Length);
        // var nrOfLines = lines.Count;
        var nrsPerLine = new List<string>();

        for (int i = 0; i < lines.Count; i++)
        {
            nrsPerLine.Add(string.Empty);
        }

        for (int i = 0; i < longestLineLength; i++)
        {
            var allHaveSpace = true;

            for (int lineNr = 0; lineNr < lines.Count; lineNr++)
            {
                if (i >= lines[lineNr].Length)
                {
                    nrsPerLine[lineNr] += ' ';
                    continue;
                }

                char c = lines[lineNr][i];

                nrsPerLine[lineNr] += c;

                if (c != ' ')
                {
                    allHaveSpace = false;
                }
            }

            // Afronden.
            if (allHaveSpace)
            {
                var op = ops[0];
                ops.RemoveAt(0);
                problems.Add(new Problem2(op, nrsPerLine));

                nrsPerLine = [];

                for (int x = 0; x < lines.Count; x++)
                {
                    nrsPerLine.Add(string.Empty);
                }
            }
        }

        problems.Add(new Problem2(ops[0], nrsPerLine));

        return problems;
    }

    private List<char> ReadOperators(string line)
    {
        return line.Trim().Where(c => c == '+' || c == '*').ToList();
    }

    private List<int> ReadNumbers(string line)
    {
        return line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
    }
}

class Problem
{
    char op;
    List<BigInteger> numbers;

    public Problem(char op, List<BigInteger> numbers)
    {
        this.op = op;
        this.numbers = numbers;
    }

    public BigInteger Solve()
    {
        BigInteger result = op == '+' ? BigInteger.Zero : BigInteger.One;

        foreach (var number in numbers)
        {
            if (op == '+')
            {
                result += number;
            }
            else if (op == '*')
            {
                result *= number;
            }
        }

        return result;
    }
}

class Problem2
{
    char op;
    List<string> numbers;

    public Problem2(char op, List<string> numbers)
    {
        this.op = op;
        this.numbers = numbers;

        var allEndOnSpace = true;

        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i].Length == 0 || numbers[i][^1] != ' ')
            {
                allEndOnSpace = false;
                break;
            }
        }

        if (allEndOnSpace)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] = numbers[i][..^1];
            }
        }
    }

    public BigInteger Solve()
    {
        var longestNrLength = numbers.Max(n => n.Length);
        var actualNumbers = new List<string>(longestNrLength);

        for (int i = 0; i < longestNrLength; i++)
        {
            actualNumbers.Add(string.Empty);
        }

        for (int i = 0; i < longestNrLength; i++)
        {
            foreach (var numberStr in numbers)
            {
                actualNumbers[i] += numberStr[i];
            }
        }

        BigInteger result = op == '+' ? BigInteger.Zero : BigInteger.One;

        foreach (var nrStr in actualNumbers)
        {
            var number = BigInteger.Parse(nrStr.Trim());

            if (op == '+')
            {
                result += number;
            }
            else if (op == '*')
            {
                result *= number;
            }
        }

        return result;
    }
}
