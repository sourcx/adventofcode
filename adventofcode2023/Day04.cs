namespace Aoc;

public class Day04
{
    public Day04 Part1()
    {
        var res = 0d;

        foreach (var line in File.ReadAllLines("In/4"))
        {
            var winningNrs = line.Split("|")[0].Split(":")[1].Split(" ").Where(x => x != string.Empty).Select(int.Parse).ToList();
            var myNrs = line.Split("|")[1].Split(" ").Where(x => x != string.Empty).Select(int.Parse).ToList();

            var matches = 0;

            foreach (var nr in myNrs)
            {
                if (winningNrs.Contains(nr))
                {
                    matches++;
                }
            }

            if (matches == 0)
            {
                continue;
            }

            res += Math.Pow(2, matches - 1);
        }

        Console.WriteLine($"Day04.1: {res}");

        return this;
    }

    public Day04 Part2()
    {
        var currentCard = 0;
        var cardsPerIndex = new Dictionary<int, int>();

        foreach (var card in File.ReadAllLines("In/4"))
        {
            cardsPerIndex[currentCard] = 1;
            currentCard += 1;
        }

        currentCard = 0;

        foreach (var card in File.ReadAllLines("In/4"))
        {
            var winningNrs = card.Split("|")[0].Split(":")[1].Split(" ").Where(x => x != string.Empty).Select(int.Parse).ToList();
            var myNrs = card.Split("|")[1].Split(" ").Where(x => x != string.Empty).Select(int.Parse).ToList();

            Console.WriteLine($"Card {currentCard}");

            var score = myNrs.Where(winningNrs.Contains).Count();

            // How many times do I have current card?
            var multiplier = cardsPerIndex[currentCard];

            for (int i = 0; i < score; i++)
            {
                cardsPerIndex[currentCard + i + 1] += multiplier;
            }

            currentCard += 1;
        }

        Console.WriteLine($"Day04.1: {cardsPerIndex.Values.Sum()}");

        return this;
    }
}
