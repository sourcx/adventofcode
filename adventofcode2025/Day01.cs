namespace adventofcode2025;

class Day01
{
    public void Run()
    {
        Console.WriteLine(GetType().Name);

        Part1();
        Part2();
    }

    private void Part1()
    {
        var pos = 50;
        var max = 100;
        var password = 0;

        foreach (var line in File.ReadAllLines("input/1"))
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);

            if (direction == 'L')
            {
                pos = (pos - distance) % max;

                if (pos < 0)
                {
                    pos += max;
                }
            }

            if (direction == 'R')
            {
                pos = (pos + distance) % max;
            }

            if (pos == 0)
            {
                password += 1;
            }
        }

        Console.WriteLine($"The password is {password}");
    }

    private void Part2()
    {
        var pos = 50;
        var max = 100;
        var password = 0;

        foreach (var line in File.ReadAllLines("input/1"))
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);

            if (direction == 'L')
            {
                for (int i = 0; i < distance; i++)
                {
                    pos--;

                    if (pos == 0)
                    {
                        password += 1;
                    }

                    if (pos < 0)
                    {
                        pos += max;
                    }
                }
            }

            if (direction == 'R')
            {
                for (int i = 0; i < distance; i++)
                {
                    pos++;

                    if (pos == 100)
                    {
                        password += 1;
                    }

                    pos %= max;
                }
            }
        }

        Console.WriteLine($"The password is {password}");
    }
}
