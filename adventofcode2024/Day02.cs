namespace adventofcode2024;

class Day02
{
    public void Run()
    {
        Console.WriteLine("Day 02");

        var reports = new List<List<int>>();

        File.ReadAllLines("input/2").ToList().ForEach(line =>
        {
            reports.Add(line.Split(" ").Select(int.Parse).ToList());
        });

        Console.WriteLine($"The amount or reports that are safe is {reports.Count(IsSafe)}");
        Console.WriteLine($"The amount or reports that are safe using the Problem Dampener is {reports.Count(IsSafeWithProblemDampener)}");
    }

    private bool IsSafe(List<int> report)
    {
        if (report.Count <= 1)
        {
            return true;
        }

        var isAscending = report[0] < report[1];

        for (int i = 1; i < report.Count; i++)
        {
            if (isAscending)
            {
                if (report[i - 1] >= report[i])
                {
                    return false;
                }
            }
            else
            {
                if (report[i - 1] <= report[i])
                {
                    return false;
                }
            }

            if (Math.Abs(report[i] - report[i - 1]) > 3)
            {
                return false;
            }

        }

        return true;
    }

    private bool IsSafeWithProblemDampener(List<int> report)
    {
        for (int i = 0; i < report.Count; i++)
        {
            var reportCopy = report.ToList();
            reportCopy.RemoveAt(i);

            if (IsSafe(reportCopy))
            {
                return true;
            }
        }

        return false;
    }
}
