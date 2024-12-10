namespace adventofcode2024;

class Day01
{
    public void Run()
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        foreach (var line in File.ReadAllLines("input/1"))
        {
            Console.WriteLine(line);
            list1.Add(int.Parse(line.Split(" ").First()));
            list2.Add(int.Parse(line.Split(" ").Last()));
        }

        list1.Sort();
        list2.Sort();

        var distance = 0;

        for (int i = 0; i < list1.Count; i++)
        {
            distance += Math.Abs(list1[i] - list2[i]);
        }

        Console.WriteLine($"The total distance between the lists is {distance}");

        var similarity = 0;

        foreach (var item1 in list1)
        {
            similarity += item1 * list2.Count(item2 => item2 == item1);
        }

        Console.WriteLine($"The similarity score of the lists is {similarity}");
    }
}
