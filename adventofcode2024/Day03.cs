namespace adventofcode2024;

class Day03
{
    private readonly int MaxDigits = 3;
    private bool MulEnabled = true;

    public void Run()
    {
        Console.WriteLine(GetType().Name);

        RunPart1();
        RunPart2();
    }

    private void RunPart1()
    {
        using var reader = new StreamReader("input/3");

        var addedUpMuls = 0;

        while (reader.Peek() != -1)
        {
            if (TryReadMul(reader) is int mulValue)
            {
                addedUpMuls += MulEnabled ? mulValue : 0;
            }
            else
            {
                reader.Read();
            }
        }

        Console.WriteLine($"The sum of all the multiplications is {addedUpMuls}");
    }

    private void RunPart2()
    {
        using var reader = new StreamReader("input/3");

        var addedUpMuls = 0;

        while (reader.Peek() != -1)
        {
            if (TryReadMul(reader) is int mulValue)
            {
                addedUpMuls += MulEnabled ? mulValue : 0;
            }
            else if (TryReadDoDont(reader) is bool doValue)
            {
                MulEnabled = doValue;
            }
            else
            {
                reader.Read();
            }
        }

        Console.WriteLine($"The sum of all the multiplications using do/don't is {addedUpMuls}");
    }

    // Try to read all parts that make up a mul instruction.
    // We take one char at a time and return if we find it or find something unexpected.
    private int? TryReadMul(StreamReader reader)
    {
        if ((char)reader.Peek() != 'm')
        {
            return null;
        }

        reader.Read();

        if ((char)reader.Peek() != 'u')
        {
            return null;
        }

        reader.Read();

        if ((char)reader.Peek() != 'l')
        {
            return null;
        }

        reader.Read();


        if ((char)reader.Peek() != '(')
        {
            return null;
        }

        reader.Read();

        var lhs = TryReadNumber(reader);

        if (!lhs.HasValue)
        {
            return null;
        }

        if ((char)reader.Peek() != ',')
        {
            return null;
        }

        reader.Read();

        var rhs = TryReadNumber(reader);

        if (!rhs.HasValue)
        {
            return null;
        }

        if ((char)reader.Peek() != ')')
        {
            return null;
        }

        reader.Read();

        return lhs.Value * rhs.Value;
    }

    // Read between 1 - 3 digits into a number.
    // Use peek and don't snoep away the potential other useful char.
    private int? TryReadNumber(StreamReader reader)
    {
        var number = string.Empty;

        for (int i = 0; i < MaxDigits; i++)
        {
            if (char.IsDigit((char)reader.Peek()))
            {
                number += (char)reader.Read();
            }
            else
            {
                break;
            }
        }

        if (string.IsNullOrEmpty(number))
        {
            return null;
        }
        else
        {
            return int.Parse(number);
        }
    }

    // Reads either "do" or "don't" from the stream. Both start with 2 of the same letters so we can't use peek
    private bool? TryReadDoDont(StreamReader reader)
    {
        if ((char)reader.Peek() != 'd')
        {
            return null;
        }

        reader.Read();

        if ((char)reader.Peek() != 'o')
        {
            return null;
        }

        reader.Read();

        if (TryReadDo(reader) is bool doValue)
        {
            return doValue;
        }
        else if (TryReadDont(reader) is bool doValue2)
        {
            return doValue2;
        }

        return null;
    }

    // Read without the do.
    private bool? TryReadDo(StreamReader reader)
    {
        if ((char)reader.Peek() != '(')
        {
            return null;
        }

        reader.Read();

        if ((char)reader.Peek() != ')')
        {
            return null;
        }

        reader.Read();

        return true;
    }

    // Read without the do.
    private bool? TryReadDont(StreamReader reader)
    {
        if ((char)reader.Peek() != 'n')
        {
            return null;
        }

        reader.Read();

        if ((char)reader.Peek() != '\'')
        {
            return null;
        }

        reader.Read();

        if ((char)reader.Peek() != 't')
        {
            return null;
        }

        reader.Read();

        if ((char)reader.Peek() != '(')
        {
            return null;
        }

        reader.Read();

        if ((char)reader.Peek() != ')')
        {
            return null;
        }

        reader.Read();

        return false;
    }
}
