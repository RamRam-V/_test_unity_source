using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// </summary>
public struct Point
{
    public int x;
    public int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Point(float x, float y)
    {
        this.x = (int)x;
        this.y = (int)y;
    }
    public Point(Point rhs)
    {
        this.x = rhs.x;
        this.y = rhs.y;
    }
    public bool IsZero()
    {
        return (this.x == 0) && (this.y == 0);
    }
    public static Point zero
    {
        get
        {
            return new Point(0, 0);
        }
    }
    public static Point operator *(Point lhs, int n)
    {
        return new Point(lhs.x * n, lhs.y * n);
    }
    public static Point operator *(Point lhs, float n)
    {
        return new Point(lhs.x * n, lhs.y * n);
    }
}
