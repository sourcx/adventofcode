namespace adventofcode2024;

class Day05
{
    public void Run()
    {
        Console.WriteLine(GetType().Name);

        var input = "input/5";

        RunPart1(input);
        RunPart2(input);
    }

    private void RunPart1(string input)
    {
        var pageOrderingRules = ReadRules(input);
        var updates = ReadUpdates(input);

        var correctUpdates = updates.Where(update => IsCorrect(update, pageOrderingRules)).ToList();
        var sumOfMiddlePages = correctUpdates.Aggregate(0, (sum, update) => sum + update[update.Count / 2]);

        Console.WriteLine($"The sum of middle pages of correct updates is {sumOfMiddlePages}");
    }

    private List<(int, int)> ReadRules(string file)
    {
        // Read.
        var rules = File.ReadAllLines(file).Where(line => line.Contains('|')).Select(line =>
        {
            var parts = line.Split('|');
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }).ToList();

        return rules;
    }

    private List<List<int>> ReadUpdates(string file)
    {
        var updates = File.ReadAllLines(file).Where(line => line.Contains(',')).Select(line =>
        {
            return line.Split(',').Select(int.Parse).ToList();
        }).ToList();

        return updates;
    }

    private bool IsCorrect(List<int> pageList, List<(int, int)> pageOrderingRules)
    {
        for (int i = 0; i < pageList.Count; i++)
        {
            var currentPage = pageList[i];

            foreach (var rule in pageOrderingRules)
            {
                if (rule.Item1 != currentPage)
                {
                    continue;
                }

                var indexOther = pageList.IndexOf(rule.Item2);

                if (indexOther == -1)
                {
                    continue;
                }

                if (indexOther < i)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private List<int> FixOrder(List<int> update, List<(int, int)> pageOrderingRules)
    {
        if (IsCorrect(update, pageOrderingRules))
        {
            return update;
        }

        while (!IsCorrect(update, pageOrderingRules))
        {
            foreach (var rule in pageOrderingRules)
            {
                var indexOne = update.IndexOf(rule.Item1);

                if (indexOne == -1)
                {
                    continue;
                }

                var indexOther = update.IndexOf(rule.Item2);

                if (indexOther == -1)
                {
                    continue;
                }

                if (indexOne > indexOther)
                {
                    (update[indexOne], update[indexOther]) = (update[indexOther], update[indexOne]);
                }

                if (IsCorrect(update, pageOrderingRules))
                {
                    return update;
                }
            }
        }

        return update;
    }

    private void RunPart2(string input)
    {
        var pageOrderingRules = ReadRules(input);
        var updates = ReadUpdates(input);

        var incorrectUpdates = updates.Where(update => !IsCorrect(update, pageOrderingRules)).ToList();
        var fixedUpdates = incorrectUpdates.Select(update => FixOrder(update, pageOrderingRules));
        var sumOfMiddlePages = fixedUpdates.Aggregate(0, (sum, update) => sum + update[update.Count / 2]);

        Console.WriteLine($"The sum of middle pages of correct updates is {sumOfMiddlePages}");
    }
}
