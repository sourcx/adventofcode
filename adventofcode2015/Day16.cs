namespace AdventOfCode2015
{
    class Day16
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var lines = File.ReadAllLines("input/16");

            foreach (var line in lines)
            {
                var sue = new Sue();

                var parts = line.Split(' ');
                sue.Number = int.Parse(parts[1].TrimEnd(':'));

                for (int i = 2; i < parts.Length; i += 2)
                {
                    var property = parts[i].TrimEnd(':');
                    var value = int.Parse(parts[i + 1].TrimEnd(','));

                    switch (property)
                    {
                        case "children":
                            sue.Children = value;
                            break;
                        case "cats":
                            sue.Cats = value;
                            break;
                        case "samoyeds":
                            sue.Samoyeds = value;
                            break;
                        case "pomeranians":
                            sue.Pomeranians = value;
                            break;
                        case "akitas":
                            sue.Akitas = value;
                            break;
                        case "vizslas":
                            sue.Vizslas = value;
                            break;
                        case "goldfish":
                            sue.Goldfish = value;
                            break;
                        case "trees":
                            sue.Trees = value;
                            break;
                        case "cars":
                            sue.Cars = value;
                            break;
                        case "perfumes":
                            sue.Perfumes = value;
                            break;
                    }
                }

                if (sue.Children != null && sue.Children != 3) continue;
                if (sue.Cats != null && sue.Cats != 7) continue;
                if (sue.Samoyeds != null && sue.Samoyeds != 2) continue;
                if (sue.Pomeranians != null && sue.Pomeranians != 3) continue;
                if (sue.Akitas != null && sue.Akitas != 0) continue;
                if (sue.Vizslas != null && sue.Vizslas != 0) continue;
                if (sue.Goldfish != null && sue.Goldfish != 5) continue;
                if (sue.Trees != null && sue.Trees != 3) continue;
                if (sue.Cars != null && sue.Cars != 2) continue;
                if (sue.Perfumes != null && sue.Perfumes != 1) continue;

                Console.WriteLine($"Sue {sue.Number} is the one!");
            }
        }

        private static void SolvePart2()
        {
            var lines = File.ReadAllLines("input/16");

            foreach (var line in lines)
            {
                var sue = new Sue();

                var parts = line.Split(' ');
                sue.Number = int.Parse(parts[1].TrimEnd(':'));

                for (int i = 2; i < parts.Length; i += 2)
                {
                    var property = parts[i].TrimEnd(':');
                    var value = int.Parse(parts[i + 1].TrimEnd(','));

                    switch (property)
                    {
                        case "children":
                            sue.Children = value;
                            break;
                        case "cats":
                            sue.Cats = value;
                            break;
                        case "samoyeds":
                            sue.Samoyeds = value;
                            break;
                        case "pomeranians":
                            sue.Pomeranians = value;
                            break;
                        case "akitas":
                            sue.Akitas = value;
                            break;
                        case "vizslas":
                            sue.Vizslas = value;
                            break;
                        case "goldfish":
                            sue.Goldfish = value;
                            break;
                        case "trees":
                            sue.Trees = value;
                            break;
                        case "cars":
                            sue.Cars = value;
                            break;
                        case "perfumes":
                            sue.Perfumes = value;
                            break;
                    }
                }

                if (sue.Children != null && sue.Children != 3) continue;
                if (sue.Cats != null && sue.Cats <= 7) continue;
                if (sue.Samoyeds != null && sue.Samoyeds != 2) continue;
                if (sue.Pomeranians != null && sue.Pomeranians >= 3) continue;
                if (sue.Akitas != null && sue.Akitas != 0) continue;
                if (sue.Vizslas != null && sue.Vizslas != 0) continue;
                if (sue.Goldfish != null && sue.Goldfish >= 5) continue;
                if (sue.Trees != null && sue.Trees <= 3) continue;
                if (sue.Cars != null && sue.Cars != 2) continue;
                if (sue.Perfumes != null && sue.Perfumes != 1) continue;

                Console.WriteLine($"Sue {sue.Number} is the one!");
            }
        }
    }

    internal class Sue
    {
        public int Number { get; set; }
        public int? Children { get; set; }
        public int? Cats { get; set; }
        public int? Samoyeds { get; set; }
        public int? Pomeranians { get; set; }
        public int? Akitas { get; set; }
        public int? Vizslas { get; set; }
        public int? Goldfish { get; set; }
        public int? Trees { get; set; }
        public int? Cars { get; set; }
        public int? Perfumes { get; set; }
    }
}
