using System.Drawing;
using Aoc.Util;

namespace Aoc;

public class Day24
{
    public Day24 Part1()
    {
        var res = 0;

        var lines = new HashSet<(PointF, PointF)>();

        var minVal = 7;
        var maxVal = 27;

        foreach (var line in File.ReadAllLines("In/24"))
        {
            var coords = line.Split('@')[0].Trim().Split(',');
            var x = int.Parse(coords[0]);
            var y = int.Parse(coords[1]);
            var z = int.Parse(coords[2]);

            var delta = line.Split('@')[1].Trim().Split(',');
            var dx = int.Parse(delta[0]);
            var dy = int.Parse(delta[1]);
            var dz = int.Parse(delta[2]);

            var endX = x;
            var endY = y;

            // Go in the positive direction of (dx, dy).
            while ((endX + dx) <= maxVal && (endX + dx) >= minVal)
            {
                if ((endY + dx) < minVal || (endY + dx) > maxVal)
                {
                    break;
                }

                endX += dx;
                endY += dy;
            }

            var startX = endX;
            var startY = endY;

            // Go in the negative direction of (dx, dy).
            while ((startX - dx) <= maxVal && (startX - dx) >= minVal)
            {
                if ((startY - dy) < minVal || (startY - dy) > maxVal)
                {
                    break;
                }

                startX -= dx;
                startY -= dy;
            }

            x = int.Parse(coords[0]);
            y = int.Parse(coords[1]);
            z = int.Parse(coords[2]);

            var start = new PointF(endX, endY);
            var end = new PointF(startX, startY);

            // if (foundMinX != foundMaxX || foundMinY != foundMaxY)
            // {
            lines.Add((start, end));
            // }
        }

        var pairs = lines
            .SelectMany(line1 => lines
                .Where(line2 => line1 != line2)
                .Select(line2 => (line1, line2)));

        var pairsDone = new HashSet<((PointF, PointF) line1, (PointF, PointF) line2)>();

        foreach (var pair in pairs)
        {
            if (pairsDone.Contains(pair))
            {
                continue;
            }

            if (LineIntersector.LinesIntersect(pair.line1.Item1, pair.line1.Item2, pair.line2.Item1, pair.line2.Item2))
            {
                res += 1;
            }

            pairsDone.Add((pair.line2, pair.line1));
        }

        Console.WriteLine($"Day24.1: {res}");
        // collide

        Console.WriteLine(LineIntersector.LinesIntersect("0,0-2,8:8,0-0,20")); // No intersect

        Console.WriteLine(LineIntersector.LinesIntersect("0,10-2,0:10,0-0,5")); // Intersect

        Console.WriteLine(LineIntersector.LinesIntersect("0,0-0,10:2,0-2,10")); // Parallel, vertical
        Console.WriteLine(LineIntersector.LinesIntersect("0,0-5,5:2,0-7,5")); // Parallel, diagonal

        Console.WriteLine(LineIntersector.LinesIntersect("0,0-5,5:2,2-7,7")); // Collinear, overlap
        Console.WriteLine(LineIntersector.LinesIntersect("0,0-5,5:7,7-10,10")); // Collinear, no overlap


        Console.WriteLine($"Day24.1: {res}");

        return this;
    }

    public Day24 Part2()
    {
        var res = 0;

        Console.WriteLine($"Day24.2: {res}");

        return this;
    }
}
