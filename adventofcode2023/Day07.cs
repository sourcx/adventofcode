namespace Aoc;

public class Day07
{
    public Day07 Part1()
    {
        var res = 0;

        var hands = File.ReadAllLines("In/7")
            .Select(line => new Hand(line.Split(" ")[0], line.Split(" ")[1]))
            .ToList();
        hands.Sort();

        var rank = 1;

        foreach (var hand in hands)
        {
            res += hand.Bid * rank;
            rank += 1;
        }

        Console.WriteLine($"Day07.1: {res}");

        return this;
    }

    partial class Hand : IComparable
    {
        public readonly short[] Cards;
        public readonly string CardsString;

        public int Bid;

        public int HandType;

        public Hand(string cards, string bid)
        {
            CardsString = cards;
            Cards = new short[cards.Length];

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == 'A') Cards[i] = 14;
                else if (cards[i] == 'K') Cards[i] = 13;
                else if (cards[i] == 'Q') Cards[i] = 12;
                else if (cards[i] == 'J') Cards[i] = 11;
                else if (cards[i] == 'T') Cards[i] = 10;
                else Cards[i] = short.Parse(cards[i].ToString());
            }

            Bid = int.Parse(bid);

            var differentCards = Cards.ToHashSet();

            if (differentCards.Count == 1)
            {
                HandType = 1; // 5 of a kind
            }
            else if (differentCards.Count == 2)
            {
                if (Cards.Count(c => c == Cards[0]) == 4 || Cards.Count(c => c == Cards[1]) == 4)
                {
                    HandType = 2; // 4 of a kind
                }
                else
                {
                    HandType = 3; // Full house
                }
            }
            else if (differentCards.Count == 3)
            {
                if (Cards.Count(c => c == Cards[0]) == 3 || Cards.Count(c => c == Cards[1]) == 3 || Cards.Count(c => c == Cards[2]) == 3)
                {
                    HandType = 4; // 3 of a kind
                }
                else
                {
                    HandType = 5; // 2 pairs
                }
            }
            else if (differentCards.Count == 4)
            {
                HandType = 6; // 1 pair
            }
            else
            {
                HandType = 7; // High card
            }
        }

        public int CompareTo(object? obj)
        {
            if (obj is Hand other)
            {
                if (HandType < other.HandType)
                {
                    return 1;
                }
                else if (HandType > other.HandType)
                {
                    return -1;
                }
                else
                {
                    for (int i = 0; i < Cards.Length; i++)
                    {
                        if (Cards[i] > other.Cards[i])
                        {
                            return 1;
                        }
                        else if (Cards[i] < other.Cards[i])
                        {
                            return -1;
                        }
                    }
                }

                throw new Exception("Nothing compares to you :(");
            }

            throw new Exception("Nothing compares to you :(");
        }
    }

    partial class JokerHand : IComparable
    {
        public readonly short[] Cards;
        public readonly string CardsString;

        public int Bid;

        public int HandType = int.MaxValue;

        public short[] JokerValues = new short[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14 };

        public JokerHand(string cards, string bid)
        {
            CardsString = cards;
            Cards = new short[cards.Length];

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == 'A') Cards[i] = 14;
                else if (cards[i] == 'K') Cards[i] = 13;
                else if (cards[i] == 'Q') Cards[i] = 12;
                else if (cards[i] == 'J') Cards[i] = 11;
                else if (cards[i] == 'T') Cards[i] = 10;
                else Cards[i] = short.Parse(cards[i].ToString());
            }

            Bid = int.Parse(bid);

            foreach (var permutation in CardPermutations((short[])Cards.Clone()))
            {
                var differentCards = permutation.ToHashSet();

                if (HandType == 1)
                {
                    break;
                }

                if (differentCards.Count == 1)
                {
                    HandType = 1; // 5 of a kind
                }
                else if (differentCards.Count == 2)
                {
                    if (permutation.Count(c => c == permutation[0]) == 4 || permutation.Count(c => c == permutation[1]) == 4)
                    {
                        if (HandType > 2) HandType = 2; // 4 of a kind
                    }
                    else
                    {
                        if (HandType > 3) HandType = 3; // Full house
                    }
                }
                else if (differentCards.Count == 3)
                {
                    if (permutation.Count(c => c == permutation[0]) == 3 || permutation.Count(c => c == permutation[1]) == 3 || permutation.Count(c => c == permutation[2]) == 3)
                    {
                        if (HandType > 4) HandType = 4; // 3 of a kind
                    }
                    else
                    {
                        if (HandType > 5) HandType = 5; // 2 pairs
                    }
                }
                else if (differentCards.Count == 4)
                {
                    if (HandType > 6) HandType = 6; // 1 pair
                }
                else
                {
                    if (HandType > 7) HandType = 7; // High card
                }
            }
        }

        public IEnumerable<short[]> CardPermutations(short[] cards)
        {
            // if (cards[0] == 11 && cards[1] == 11 && cards[2] == 11 && cards[3] == 11 && cards[4] == 11)
            // {
            yield return cards;
            // }

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == 11) // 'J'
                {
                    foreach (var jokerValue in JokerValues)
                    {
                        var oldCardI = cards[i];
                        cards[i] = jokerValue;

                        foreach (var permutation in CardPermutations(cards))
                        {
                            yield return permutation;
                        }

                        cards[i] = oldCardI;
                    }
                }
            }

            yield return cards;
        }

        public int CompareTo(object? obj)
        {
            if (obj is JokerHand that)
            {
                Console.WriteLine($"Compare {CardsString}({HandType}) with {that.CardsString}({that.HandType})");

                if (HandType < that.HandType)
                {
                    return 1;
                }
                else if (HandType > that.HandType)
                {
                    return -1;
                }
                else
                {
                    var thisCards = ((short[])Cards.Clone()).ToList().Select(x => x == 11 ? 1 : x).ToArray();
                    var thatCards = ((short[])that.Cards.Clone()).ToList().Select(x => x == 11 ? 1 : x).ToArray();

                    for (int i = 0; i < thisCards.Length; i++)
                    {
                        if (thisCards[i] > thatCards[i])
                        {
                            return 1;
                        }
                        else if (thisCards[i] < thatCards[i])
                        {
                            return -1;
                        }
                    }
                }

                // throw new Exception("Nothing compares to you :(");
                return 0;
            }

            throw new Exception("Nothing compares to you :(");
        }
    }

    // Jonges jonges...
    // Because you have guessed incorrectly 4 times on this puzzle, please wait 5 minutes before trying again.
    public Day07 Part2()
    {
        var res = 0;

        var hands = File.ReadAllLines("In/7")
            .Select(line => new JokerHand(line.Split(" ")[0], line.Split(" ")[1]))
            .ToList();
        hands.Sort();

        var rank = 0;

        foreach (var hand in hands)
        {
            Console.WriteLine($"{hand.CardsString} {hand.HandType} {hand.Bid} {rank}");
            rank += 1;
            res += hand.Bid * rank;
        }

        Console.WriteLine($"Day07.2: {res}"); // too high 249513489, 249509276 too high
                                              // 249512998
                                              // 249366868 too high
                                              // 249366533 wrong
                                              // 249356515
        return this;
    }
}
