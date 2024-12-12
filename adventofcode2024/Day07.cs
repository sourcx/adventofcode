namespace adventofcode2024;

class Day07
{
    private class Equation(ulong expectedResult, List<ulong> operands)
    {
        public ulong ExpectedResult { get; set; } = expectedResult;
        public List<ulong> Operands { get; set; } = operands;

        public override string ToString()
        {
            return $"{ExpectedResult} = {string.Join(" ", Operands)}";
        }

        public bool CanBeSolvedPart1()
        {
            if (Operands.Count == 1)
            {
                return Operands[0] == ExpectedResult;
            }
            else if (Operands.Count == 2)
            {
                return new Equation(ExpectedResult, [Operands[0] + Operands[1]]).CanBeSolvedPart1() ||
                       new Equation(ExpectedResult, [Operands[0] * Operands[1]]).CanBeSolvedPart1();
            }
            else
            {
                var rest = Operands.Skip(2).ToList();
                return new Equation(ExpectedResult, [Operands[0] + Operands[1], .. rest]).CanBeSolvedPart1() ||
                       new Equation(ExpectedResult, [Operands[0] * Operands[1], .. rest]).CanBeSolvedPart1();
            }
        }

        public bool CanBeSolvedPart2()
        {
            if (Operands.Count == 1)
            {
                return Operands[0] == ExpectedResult;
            }
            else if (Operands.Count == 2)
            {
                return new Equation(ExpectedResult, [Operands[0] + Operands[1]]).CanBeSolvedPart2() ||
                       new Equation(ExpectedResult, [Operands[0] * Operands[1]]).CanBeSolvedPart2() ||
                       new Equation(ExpectedResult, [ulong.Parse($"{Operands[0]}{Operands[1]}")]).CanBeSolvedPart2();
            }
            else
            {
                var rest = Operands.Skip(2).ToList();
                return new Equation(ExpectedResult, [Operands[0] + Operands[1], .. rest]).CanBeSolvedPart2() ||
                       new Equation(ExpectedResult, [Operands[0] * Operands[1], .. rest]).CanBeSolvedPart2() ||
                       new Equation(ExpectedResult, [ulong.Parse($"{Operands[0]}{Operands[1]}"), .. rest]).CanBeSolvedPart2();
            }
        }
    }

    public void Run()
    {
        Console.WriteLine(GetType().Name);

        var fileName = "input/7";

        RunPart1(fileName);
        RunPart2(fileName);
    }

    private void RunPart1(string fileName)
    {
        var equations = ReadEquations(fileName);
        var totalCalibrationResult = 0UL;

        foreach (var equation in equations)
        {
            if (equation.CanBeSolvedPart1())
            {
                totalCalibrationResult += equation.ExpectedResult;
            }
        }

        Console.WriteLine($"What is their total calibration result? {totalCalibrationResult}");
    }

    private List<Equation> ReadEquations(string fileName)
    {
        var equations = new List<Equation>();

        var lines = File.ReadAllLines(fileName);
        foreach (var line in lines)
        {
            var parts = line.Split(": ");
            var expectedResult = ulong.Parse(parts[0]);
            var operands = parts[1].Split(" ").ToList().Select(ulong.Parse).ToList();
            equations.Add(new Equation(expectedResult, operands));
        }

        return equations;
    }

    private void RunPart2(string fileName)
    {
        var equations = ReadEquations(fileName);
        var totalCalibrationResult = 0UL;

        foreach (var equation in equations)
        {
            if (equation.CanBeSolvedPart2())
            {
                totalCalibrationResult += equation.ExpectedResult;
            }
        }

        Console.WriteLine($"What is their total calibration result? {totalCalibrationResult}");
    }
}
