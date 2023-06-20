using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

namespace adventofcode
{
    public class Day03
    {
        // Coordinate system
        // Right    x+1
        // Left     x-1
        // Up       y+1
        // Down     y-1
        public void Start()
        {
            Part1();
            Part2();
        }

        private void Part1()
        {
            var wires = File.ReadAllLines("input/3").Select(line => ReadWire(line)).ToArray();

            var wire1 = wires[0];
            var wire2 = wires[1];

            int closestDistance = int.MaxValue;

            foreach (var point in wire1)
            {
                if (wire2.Contains(point))
                {
                    var distance = Math.Abs(point.X) + Math.Abs(point.Y);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                    }
                }
            }

            Console.WriteLine($"Closest distance: {closestDistance}");
        }

        private HashSet<Point> ReadWire(string line)
        {
            var points = new HashSet<Point>();

            int x = 0;
            int y = 0;

            foreach (var instruction in line.Split(","))
            {
                var direction = instruction[0];
                var steps = int.Parse(instruction.Substring(1));

                for (int i = 0; i < steps; i++)
                {
                    switch(direction)
                    {
                        case 'U':
                            y += 1;
                            break;
                        case 'R':
                            x += 1;
                            break;
                        case 'D':
                            y -= 1;
                            break;
                        case 'L':
                            x -= 1;
                            break;
                    }

                    var point = new Point(x, y);
                    points.Add(point);
                }
            }

            return points;
        }

        private void Part2()
        {
            var wires = File.ReadAllLines("input/3").Select(line => ReadWireWithSteps(line)).ToArray();

            var wire1 = wires[0];
            var wire2 = wires[1];

            int closestDistance = int.MaxValue;

            foreach (var point in wire1.Keys)
            {
                if (wire2.Keys.Contains(point))
                {
                    var distance = wire1[point] + wire2[point];

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                    }
                }
            }

            Console.WriteLine($"Smallest amount of steps to intersection: {closestDistance}");
        }

        private Dictionary<Point, int> ReadWireWithSteps(string line)
        {
            var points = new Dictionary<Point, int>();

            int x = 0;
            int y = 0;
            int totalSteps = 0;

            foreach (var instruction in line.Split(","))
            {
                var direction = instruction[0];
                var steps = int.Parse(instruction.Substring(1));

                for (int i = 0; i < steps; i++)
                {
                    totalSteps += 1;

                    switch(direction)
                    {
                        case 'U':
                            y += 1;
                            break;
                        case 'R':
                            x += 1;
                            break;
                        case 'D':
                            y -= 1;
                            break;
                        case 'L':
                            x -= 1;
                            break;
                    }

                    var point = new Point(x, y);
                    points.TryAdd(point, totalSteps);
                }
            }

            return points;
        }
    }
}
