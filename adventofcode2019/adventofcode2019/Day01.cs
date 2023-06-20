using System;
using System.IO;
using System.Linq;

namespace adventofcode
{
    class Day01
    {
        public static void Start()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            var fuel = ComponentFuel();
            Console.WriteLine($"Day 01: Total fuel needed is {fuel}."); // 3363033
        }

        private static void Part2()
        {
            var fuel = ComponentFuel2();
            Console.WriteLine($"Day 01: Total fuel needed is {fuel}."); // 5041680
        }

        private static int ComponentFuel()
        {
            return File.ReadLines("input/1").Select(Int32.Parse).Select(x => (x / 3) - 2).Sum();
        }

        private static int ComponentFuel2()
        {
            var totalFuel = 0;

            foreach (var component in File.ReadLines("input/1").Select(Int32.Parse))
            {
                var fuel = (component / 3) - 2;
                totalFuel += fuel;

                while (fuel > 0)
                {
                    fuel = (fuel / 3) - 2;
                    totalFuel += fuel;
                }
            }

            return totalFuel;
        }
    }
}
