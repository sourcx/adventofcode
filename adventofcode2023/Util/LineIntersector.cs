using System.Drawing;

namespace Aoc.Util;

/*
 * Copied from https://ideone.com/PnPJgb.
 */
public class LineIntersector
{
    // Utility wrapper around LinesIntersect, expecting a string in the form:
    // A-B:C-D
    // Where A, B, C and D are cartesian coordinates in the form:
    // XX.XX,YY.YY
    public static bool LinesIntersect(string s)
    {
        PointF A, B, C, D;

        string[] split = s.Split(',', '-', ':');

        A = new PointF(float.Parse(split[0]), float.Parse(split[1]));
        B = new PointF(float.Parse(split[2]), float.Parse(split[3]));
        C = new PointF(float.Parse(split[4]), float.Parse(split[5]));
        D = new PointF(float.Parse(split[6]), float.Parse(split[7]));

        return LinesIntersect(A, B, C, D);
    }

    // Determines if the lines AB and CD intersect.
    public static bool LinesIntersect(PointF A, PointF B, PointF C, PointF D)
    {
        PointF CmP = new PointF(C.X - A.X, C.Y - A.Y);
        PointF r = new PointF(B.X - A.X, B.Y - A.Y);
        PointF s = new PointF(D.X - C.X, D.Y - C.Y);

        float CmPxr = CmP.X * r.Y - CmP.Y * r.X;
        float CmPxs = CmP.X * s.Y - CmP.Y * s.X;
        float rxs = r.X * s.Y - r.Y * s.X;

        if (CmPxr == 0f)
        {
            // Lines are collinear, and so intersect if they have any overlap

            return ((C.X - A.X < 0f) != (C.X - B.X < 0f))
                || ((C.Y - A.Y < 0f) != (C.Y - B.Y < 0f));
        }

        if (rxs == 0f)
            return false; // Lines are parallel.

        float rxsr = 1f / rxs;
        float t = CmPxs * rxsr;
        float u = CmPxr * rxsr;

        return (t >= 0f) && (t <= 1f) && (u >= 0f) && (u <= 1f);
    }
}
