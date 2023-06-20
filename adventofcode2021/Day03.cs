namespace AdventOfCode2021
{
    class Day03
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var bitStrings = File.ReadAllLines("input/3");
            var nrOf1s = new int[bitStrings[0].Length];

            foreach (var line in bitStrings)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        nrOf1s[i] += 1;
                    }
                }
            }

            var gammaRateString = "";
            var epsilonRateString = "";

            foreach (var i in nrOf1s)
            {
                gammaRateString += (i > bitStrings.Length / 2) ? "1" : "0";
                epsilonRateString += (i > bitStrings.Length / 2) ? "0" : "1";
            }

            var gammaRate = Convert.ToInt32(gammaRateString, 2);
            var epsilonRate = Convert.ToInt32(epsilonRateString, 2);

            Console.WriteLine($"Power is {gammaRate * epsilonRate}");
        }

        private static void SolvePart2()
        {
            var submarine = new Submarine(File.ReadAllLines("input/3"));
            Console.WriteLine($"Life support rating is {submarine.LifeSupport}");
        }

        public class Submarine
        {
            public int Oxigen { get; set; }
            public int Co2 { get; set; }

            public int LifeSupport
            {
                get
                {
                    return Oxigen * Co2;
                }
            }

            public Submarine(string[] diagnostics)
            {
                Oxigen = Convert.ToInt32(Diagnose(new List<string>(diagnostics), oxigen: true), 2);
                Co2 = Convert.ToInt32(Diagnose(new List<string>(diagnostics), oxigen: false), 2);
            }

            private string Diagnose(List<string> diagnostics, bool oxigen = true)
            {
                for (int i = 0; i < diagnostics[0].Length; i++)
                {
                    if (diagnostics.Count == 1)
                    {
                        return diagnostics[0];
                    }

                    var nrOf1s = diagnostics.Aggregate(0, (agg, d) => agg += (d[i] == '1' ? 1 : 0));
                    var subSet = new List<string>();

                    foreach (var s in diagnostics)
                    {
                        if (nrOf1s > (diagnostics.Count - 1) / 2)
                        {
                            if (oxigen && s[i] == '1')
                            {
                                subSet.Add(s);
                            }
                            if (!oxigen && s[i] == '0')
                            {
                                subSet.Add(s);
                            }
                        }
                        else
                        {
                            if (oxigen && s[i] == '0')
                            {
                                subSet.Add(s);
                            }
                            if (!oxigen && s[i] == '1')
                            {
                                subSet.Add(s);
                            }
                        }
                    }

                    diagnostics = subSet;
                }

                return diagnostics.First();
            }
        }
    }
}
