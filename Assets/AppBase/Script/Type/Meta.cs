using UnityEngine;
using System.Collections;

namespace Meta
{
    static public class Colors
    {
        static public Color R(Color color)
        {
            return new Color(color.r, 0.0f, 0.0f, 0.0f);
        }
        static public Color G(Color color)
        {
            return new Color(0.0f, color.g, 0.0f, 0.0f);
        }
        static public Color B(Color color)
        {
            return new Color(0.0f, 0.0f, color.b, 0.0f);
        }
        static public Color A(Color color)
        {
            return new Color(0.0f, 0.0f, 0.0f, color.a);
        }
        static public Color R(Color color, float r)
        {
            return new Color(r, color.g, color.b, color.a);
        }
        static public Color G(Color color, float g)
        {
            return new Color(color.r, g, color.b, color.a);
        }
        static public Color B(Color color, float b)
        {
            return new Color(color.r, color.g, b, color.a);
        }
        static public Color A(Color color, float a)
        {
            return new Color(color.r, color.g, color.b, a);
        }
        static public Color Color(int r, int g, int b)
        {
            return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
        }
        static public Color Color(int r, int g, int b, int a)
        {
            return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, (float)a / 255.0f);
        }
    }
    static public class Vector
    {
        static public Vector3 X(float x)
        {
            return new Vector3(x, 0.0f, 0.0f);
        }
        static public Vector3 Y(float y)
        {
            return new Vector3(0.0f, y, 0.0f);
        }
        static public Vector3 Z(float z)
        {
            return new Vector3(0.0f, 0.0f, z);
        }
        static public Vector3 New(float value)
        {
            return new Vector3(value, value, value);
        }
        static public Vector3 X(Vector3 vector)
        {
            return new Vector3(vector.x, 0.0f, 0.0f);
        }
        static public Vector3 Y(Vector3 vector)
        {
            return new Vector3(0.0f, vector.y, 0.0f);
        }
        static public Vector3 Z(Vector3 vector)
        {
            return new Vector3(0.0f, 0.0f, vector.z);
        }
        static public Vector3 X(Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }
        static public Vector3 Y(Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }
        static public Vector3 Z(Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }
        static public Vector3 Add(Vector3 lhs, float value)
        {
            return new Vector3(lhs.x + value, lhs.y + value, lhs.z + value);
        }
        static public Vector3 Multiply(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
        }
        static public Vector3 Multiply(Vector3 lhs, float value)
        {
            return new Vector3(lhs.x * value, lhs.y * value, lhs.z * value);
        }
        static public Vector3 Divide(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
        }
        static public Vector3 Divide(Vector3 lhs, float value)
        {
            return new Vector3(lhs.x / value, lhs.y / value, lhs.z / value);
        }
        static public float Angle(Vector3 direction, Vector3 lhs, Vector3 rhs)
        {
            if ((direction == Vector3.up) || (direction == Vector3.down))
            {
                return Mathf.Atan2(lhs.x - rhs.x, lhs.z - rhs.z) * Mathf.Rad2Deg;
            }
            else if ((direction == Vector3.left) || (direction == Vector3.right))
            {
                return Mathf.Atan2(lhs.y - rhs.y, lhs.z - rhs.z) * Mathf.Rad2Deg;
            }
            else if ((direction == Vector3.forward) || (direction == Vector3.back))
            {
                return Mathf.Atan2(lhs.x - rhs.x, lhs.y - rhs.y) * Mathf.Rad2Deg;
            }
            return 0.0f;
        }
        static public Vector3 Ignore(Vector3 lhs, Vector3 ignore)
        {
            return new Vector3(((ignore.x == 0.0f) ? 0.0f : lhs.x), ((ignore.y == 0.0f) ? 0.0f : lhs.y), ((ignore.z == 0.0f) ? 0.0f : lhs.z));
        }
        static public bool IsClockwise(Vector2 v1, Vector2 v2, Vector3 v3)
        {
            return (v1.y * (v2.x - v3.x) + v2.y * (v3.x - v1.x) + v3.y * (v1.x - v2.x)) > 0.0f;
        }
        static public class Circle
        {
            static public Vector3 X(Vector3 center, Vector3 radius, float angle, float degree, float ratio)
            {
                float sin = Mathf.Sin(Mathf.Deg2Rad * (angle + (degree * ratio)));
                float cos = Mathf.Cos(Mathf.Deg2Rad * (angle + (degree * ratio)));
                float y = center.y + (radius.y * sin);
                float z = center.z + (radius.z * cos);
                return new Vector3(radius.x, y, z);
            }
            static public Vector3 Y(Vector3 center, Vector3 radius, float angle, float degree, float ratio)
            {
                float sin = Mathf.Sin(Mathf.Deg2Rad * (angle + (degree * ratio)));
                float cos = Mathf.Cos(Mathf.Deg2Rad * (angle + (degree * ratio)));
                float x = center.x + (radius.x * sin);
                float z = center.z + (radius.z * cos);
                return new Vector3(x, radius.y, z);
            }
            static public Vector3 Z(Vector3 center, Vector3 radius, float angle, float degree, float ratio)
            {
                float sin = Mathf.Sin(Mathf.Deg2Rad * (angle + (degree * ratio)));
                float cos = Mathf.Cos(Mathf.Deg2Rad * (angle + (degree * ratio)));
                float x = center.x + (radius.x * sin);
                float y = center.z + (radius.y * cos);
                return new Vector3(x, y, radius.z);
            }
        }
    }
    static public class Rotation
    {
        static public Quaternion X(Quaternion quaternion)
        {
            return Quaternion.Euler(Vector.X(quaternion.eulerAngles));
            //return new Quaternion(quaternion.x, 0.0f, 0.0f, quaternion.w);
        }
        static public Quaternion Y(Quaternion quaternion)
        {
            return Quaternion.Euler(Vector.Y(quaternion.eulerAngles));
            //return new Quaternion(0.0f, quaternion.y, 0.0f, quaternion.w);
        }
        static public Quaternion Z(Quaternion quaternion)
        {
            return Quaternion.Euler(Vector.Z(quaternion.eulerAngles));
            //return new Quaternion(0.0f, 0.0f, quaternion.z, quaternion.w);
        }
        static public Quaternion X(Quaternion quaternion, float angle)
        {
            return Quaternion.Euler(Vector.X(quaternion.eulerAngles, angle));
            //return new Quaternion(x, quaternion.y, quaternion.z, quaternion.w);
        }
        static public Quaternion Y(Quaternion quaternion, float angle)
        {
            return Quaternion.Euler(Vector.Y(quaternion.eulerAngles, angle));
            //return new Quaternion(quaternion.x, y, quaternion.z, quaternion.w);
        }
        static public Quaternion Z(Quaternion quaternion, float angle)
        {
            return Quaternion.Euler(Vector.Z(quaternion.eulerAngles, angle));
            //return new Quaternion(quaternion.x, quaternion.y, z, quaternion.w);
        }
    }
    static public class Verify
    {
        static public bool InArray(int index, int length)
        {
            return ((index >= 0) && (index < length));
        }
        static public bool InRange(int value, int min, int max)
        {
            return ((value >= min) && (value < max));
        }
        static public bool InRange(float value, float min, float max)
        {
            return ((value >= min) && (value < max));
        }
        static public bool InRectangle(float x, float y, float top, float left, float right, float bottom)
        {
            return (InRange(x, left, right) && InRange(y, top, bottom));
        }
    }
    static public class Set
    {
        static public short Clamp(short value, short min, short max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        static public int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        static public long Clamp(long value, long min, long max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        static public float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        static public double Clamp(double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        static public void Swap<Type>(ref Type lhs, ref Type rhs)
        {
            Type temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
    static public class Random
    {
        static public bool Bool
        {
            get
            {
                return (UnityEngine.Random.Range(0, 2) == 0);
            }
        }
        static public TValue Select<TValue>(TValue[] array)
        {
            return (TValue)array[Range(0, array.Length)];
        }
        static public TValue Select<TValue>(params object[] array)
        {
            return (TValue)array[Range(0, array.Length)];
        }
        static public int Range(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        static public float Range(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        static public Vector3 insideUnitSphere
        {
            get
            {
                return UnityEngine.Random.insideUnitSphere;
            }
        }
        static public Vector3 onUnitSphere
        {
            get
            {
                return UnityEngine.Random.onUnitSphere;
            }
        }
    }
    static public class Text
    {
        static public string Form(params object[] array)
        {
            return Form(null, array);
        }
        static public string Form(string separator, params object[] array)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                if (i == (array.Length - 1))
                {
                    stringBuilder.Append("{" + i + "}");
                }
                else
                {
                    stringBuilder.Append("{" + i + "}" + separator);
                }
            }
            return string.Format(stringBuilder.ToString(), array);
        }
    }
    static public class Update
    {
        static public float Line(float totalDistance, float currentTime, float totalTime)
        {
            float ratio = currentTime / totalTime;
            if (ratio > 1.0f)
            {
                ratio = 1.0f;
            }
            return totalDistance * ratio;
        }
        static public float Slide(float TotalDistance, float currentTime, float TotalTime)
        {
            float FACTOR = 0.96875f;
            float sliding;
            TotalDistance *= 1000.0f;
            currentTime *= 1000.0f;
            TotalTime *= 1000.0f;
            if (TotalDistance >= 0)
            {
                float value = Mathf.Pow(TotalDistance, 1.0f / TotalTime);
                sliding = TotalDistance - Mathf.Pow(value, TotalTime - currentTime);
            }
            else
            {
                float value = Mathf.Pow(-TotalDistance, 1.0f / TotalTime);
                sliding = TotalDistance + Mathf.Pow(value, TotalTime - currentTime);
            }
            float flat = currentTime * TotalDistance / TotalTime;
            return (sliding * FACTOR + flat * (1 - FACTOR)) * 0.001f;
        }
        static public float FadeIn(float size, float current, float time)
        {
            return (current / time) * size;
        }
        static public float FadeOut(float size, float current, float time)
        {
            return (1.0f - (current / time)) * size;
        }
    }
}
