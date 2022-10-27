using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// </summary>
public class Rectangle
{
    public Point position;
    public Point size;

    public Rectangle(int x, int y, int width, int height)
    {
        this.position.x = x;
        this.position.y = y;
        this.size.x = width;
        this.size.y = height;
    }
    public Rectangle(Point position, Point size)
    {
        this.position = position;
        this.size = size;
    }
    public Rectangle(Rectangle rhs)
    {
        this.position = rhs.position;
        this.size = rhs.size;
    }

    public Vector3 Center()
    {
        float x = (position.x) + (size.x * 0.5f);
        float y = (position.y) - (size.y * 0.5f);

        return new Vector3(x, 0.0f, y);
    }
    public static bool IsOverlapX(Rectangle lhs, Rectangle rhs)
    {
        return lhs.position.x < rhs.position.x + rhs.size.x && lhs.position.x + lhs.size.x > rhs.position.x;
    }
    public static bool IsOverlapY(Rectangle lhs, Rectangle rhs)
    {
        return lhs.position.y < rhs.position.y + rhs.size.y && lhs.position.y + lhs.size.y > rhs.position.y;
    }
    public static bool IsOverlap(Rectangle lhs, Rectangle rhs)
    {
        if (IsOverlapX(lhs, rhs) && IsOverlapX(lhs, rhs))
        {
            return true;
        }
        return false;
    }
    //public static bool IsContains(Rectangle rect)
    //{
    //    return lhs.position.x < ;
    //}
    //public bool IsContains(Point pos)
    //{

    //}
}
