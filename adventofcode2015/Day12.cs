using System.Text.Json;

namespace AdventOfCode2015
{
    class Day12
    {
        private static long _part2Total = 0;

        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var text = File.ReadAllText("input/12");

            var number = string.Empty;
            bool isNegative = false;
            long total = 0;

            foreach (var c in text)
            {
                if (char.IsDigit(c))
                {
                    number += c;
                }
                else if (c == '-')
                {
                    isNegative = true;
                }
                else
                {
                    if (number != string.Empty)
                    {
                        var n = long.Parse(number);
                        if (isNegative)
                        {
                            n = -n;
                        }
                        total += n;

                        // Console.WriteLine(n);
                        number = string.Empty;
                        isNegative = false;
                    }
                }
            }

            Console.WriteLine($"Total sum is {total}");
        }

        private static void SolvePart2()
        {
            var text = File.ReadAllText("input/12_example");
            var json = JsonDocument.Parse(text);

            if (json.RootElement.ValueKind == JsonValueKind.Array)
            {
                AddArrayValuesToTotal(json.RootElement);
            }
            else if (!HasChildrenWithValueRed(json.RootElement))
            {
                AddValuesToTotal(json.RootElement);
            }

            Console.WriteLine($"Total sum is {_part2Total}");
        }

        private static void AddValuesToTotal(JsonElement root)
        {
            foreach (var property in root.EnumerateObject())
            {
                if (property.Value.ValueKind == JsonValueKind.Number)
                {
                    _part2Total += property.Value.GetInt32();
                }
                else if (property.Value.ValueKind == JsonValueKind.Array)
                {
                    AddArrayValuesToTotal(property.Value);
                }
                else if (property.Value.ValueKind == JsonValueKind.Object)
                {
                    if (!HasChildrenWithValueRed(property.Value))
                    {
                        AddValuesToTotal(property.Value);
                    }
                }
            }
        }

        private static bool HasChildrenWithValueRed(JsonElement root)
        {
            foreach (var property in root.EnumerateObject())
            {
                if (property.Value.ValueKind == JsonValueKind.String && property.Value.GetString() == "red")
                {
                    return true;
                }
            }

            return false;
        }

        private static void AddArrayValuesToTotal(JsonElement array)
        {
            foreach (var element in array.EnumerateArray())
            {
                if (element.ValueKind == JsonValueKind.Number)
                {
                    _part2Total += element.GetInt32();
                }
                else if (element.ValueKind == JsonValueKind.Array)
                {
                    AddArrayValuesToTotal(element);
                }
                else if (element.ValueKind == JsonValueKind.Object)
                {
                    if (!HasChildrenWithValueRed(element))
                    {
                        AddValuesToTotal(element);
                    }
                }
            }
        }
    }
}
