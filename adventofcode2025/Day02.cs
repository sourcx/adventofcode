namespace adventofcode2025;

class Day02
{
    public void Run()
    {
        Console.WriteLine(GetType().Name);

        Part1();
        Part2();
    }

    private void Part1()
    {
        var sumInvalidIds = 0L;

        var input = File.ReadAllText("input/2");
        var segments = input.Split(',');

        foreach (var segment in segments)
        {
            var parts = segment.Split('-').Select(long.Parse).ToArray();
            var from = parts[0];
            var to = parts[1];

            for (long i = from; i <= to; i++)
            {
                if (IsInvalid1(i))
                {
                    Console.WriteLine($"ID {i} is invalid");
                    sumInvalidIds += i;
                }
            }
        }

        Console.WriteLine($"The sum of invalid ids is {sumInvalidIds}");
    }

    // An id is invalid if it consists exactly of two identical halves.
    private bool IsInvalid1(long id)
    {
        if (id < 10)
        {
            return false;
        }

        var s = id.ToString();

        if (s.Length % 2 == 1) // Odd length is always valid
        {
            return false;
        }

        var half = s.Length / 2;
        var firstHalf = s[..half];
        var secondHalf = s[half..];

        return firstHalf == secondHalf;
    }

    private void Part2()
    {
        var sumInvalidIds = 0L;

        var input = File.ReadAllText("input/2");
        var segments = input.Split(',');

        foreach (var segment in segments)
        {
            var parts = segment.Split('-').Select(long.Parse).ToArray();
            var from = parts[0];
            var to = parts[1];

            for (long i = from; i <= to; i++)
            {
                if (IsInvalid2(i))
                {
                    Console.WriteLine($"ID {i} is invalid");
                    sumInvalidIds += i;
                }
            }
        }

        Console.WriteLine($"The sum of invalid ids is {sumInvalidIds}");
    }

    // An id is invalid if it consists exactly of segments that repeat more than once.
    private bool IsInvalid2(long id)
    {
        if (id < 10)
        {
            return false;
        }

        var s = id.ToString();

        for (int len = 1; len <= s.Length / 2; len++)
        {
            if (s.Length % len != 0)
            {
                continue;
            }

            var segment = s[..len];
            var repeated = string.Concat(Enumerable.Repeat(segment, s.Length / len));

            if (repeated == s)
            {
                return true;
            }
        }

        return false;

        // var half = s.Length / 2;
        // var firstHalf = s[..half];
        // var secondHalf = s[half..];

        // return firstHalf == secondHalf;
    }
}
