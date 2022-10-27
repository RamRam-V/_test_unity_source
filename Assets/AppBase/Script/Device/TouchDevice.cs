using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public class TouchDevice
{
    Vector3 prevMousePosition;
    bool[] mouseButtons = new bool[3] { false, false, false };

    public bool IsMultiTouch()
    {
        if (Input.touchCount > 1)
        {
            return true;
        }
        UpdateMouseButton();
        return mouseButtons[1];
    }
    public bool IsUp(int index)
    {
        if (index < Input.touchCount)
        {
            return (Input.GetTouch(index).phase == TouchPhase.Ended);
        }
        return Input.GetMouseButtonUp(index);
    }
    public bool IsDown(int index)
    {
        if (index < Input.touchCount)
        {
            return (Input.GetTouch(index).phase == TouchPhase.Began);
        }
        else if (Input.GetMouseButtonDown(index))
        {
            prevMousePosition = Input.mousePosition;
            return true;
        }
        return false;
    }
    public bool IsUp()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touches.Length;i++ )
            {
                if (Input.touches[i].phase != TouchPhase.Ended)
                {
                    return false;
                }
            }
            return true;
        }
        return (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1));
    }
    public bool IsDown()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                if (Input.touches[i].phase != TouchPhase.Began)
                {
                    return false;
                }
            }
            return true;
        }
        return (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1));
    }
    public Vector3 GetPoint(int index)
    {
        if (index < Input.touchCount)
        {
            return Input.GetTouch(index).position;
        }
        return Input.mousePosition;
    }
    public Vector3 GetMovementDelta(int index)
    {
        if (index < Input.touchCount)
        {
            return Input.GetTouch(index).deltaPosition;
        }
        Vector3 delta = Input.mousePosition - prevMousePosition;
        prevMousePosition = Input.mousePosition;
        return delta;
    }
    public float GetDistanceDelta()
    {
        if (Input.touchCount > 1)
        {
            float delta = 0.0f;
            for (int i = 1; i < Input.touchCount; i++)
            {
                Touch prev = Input.GetTouch(i - 1);
                Touch next = Input.GetTouch(i);
                if ((prev.phase == TouchPhase.Moved) || (next.phase == TouchPhase.Moved))
                {
                    float before = Vector2.Distance(prev.position + prev.deltaPosition, next.position + next.deltaPosition);
                    float current = Vector2.Distance(prev.position, next.position);
                    delta += before - current;
                }
            }
            return delta;
        }
        return Input.mouseScrollDelta.y;
    }
    public float GetRotationDelta()
    {
        if (Input.touchCount > 1)
        {
            float delta = 0.0f;
            for (int i = 1; i < Input.touchCount; i++)
            {
                Touch prev = Input.GetTouch(i - 1);
                Touch next = Input.GetTouch(i);
                if ((prev.phase == TouchPhase.Moved) || (next.phase == TouchPhase.Moved))
                {
                    if (!IsSameDirection(prev.deltaPosition, next.deltaPosition))
                    {
                        if (prev.deltaPosition.magnitude < next.deltaPosition.magnitude)
                        {
                            Vector2 beforePosition = next.position + next.deltaPosition;
                            float angle = Vector2.Angle(beforePosition, next.position);
                            delta += ((Meta.Vector.IsClockwise(prev.position, beforePosition, next.position)) ? angle : -angle);
                        }
                        else
                        {
                            Vector2 beforePosition = prev.position + prev.deltaPosition;
                            float angle = Vector2.Angle(beforePosition, prev.position);
                            delta += ((Meta.Vector.IsClockwise(next.position, beforePosition, prev.position)) ? angle : -angle);
                        }
                    }
                }
            }
            return delta;
        }
        UpdateMouseButton();
        if (mouseButtons[1])
        {
            return Meta.Vector.X(GetMovementDelta(1).x).x;
        }
        return 0.0f;
    }
    public float GetRotateAxisYDelta()
    {
        if (Input.touchCount > 1)
        {
            float delta = 0.0f;
            for (int i = 1; i < Input.touchCount; i++)
            {
                Touch prev = Input.GetTouch(i - 1);
                Touch next = Input.GetTouch(i);
                if ((prev.phase == TouchPhase.Moved) || (next.phase == TouchPhase.Moved))
                {
                    if (IsSameDirectionY(prev.deltaPosition, next.deltaPosition))
                    {
                        delta -= next.deltaPosition.y;
                    }
                }
            }
            return delta;
        }
        UpdateMouseButton();
        if (mouseButtons[0] && mouseButtons[1])
        {
            return (Meta.Vector.Y(GetMovementDelta(1).y)).y * -1.0f;
        }
        return 0.0f;
    }
    void UpdateMouseButton()
    {
        for (int i = 0; i < mouseButtons.Length; i++)
        {
            if (mouseButtons[i])
            {
                mouseButtons[i] = !IsUp(i);
            }
            else
            {
                mouseButtons[i] = IsDown(i);
            }
        }
    }
    bool IsSameDirection(Vector2 v1, Vector2 v2)
    {
        return (IsSameDirectionX(v1, v2) || IsSameDirectionY(v1, v2));
    }
    bool IsSameDirectionX(Vector2 v1, Vector2 v2)
    {
        return ((v1.x > 0) && (v2.x > 0)) || ((v1.x < 0) && (v2.x < 0));
    }
    bool IsSameDirectionY(Vector2 v1, Vector2 v2)
    {
        return ((v1.y > 0) && (v2.y > 0)) || ((v1.y < 0) && (v2.y < 0));
    }
}
