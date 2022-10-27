using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public interface KeyCodeListener
{
    bool TouchKey(KeyCode key, bool touch);
}
/// <summary>
/// 
/// </summary>
public partial class KeyCodeDevice : SingletonMono<KeyCodeDevice>
{
    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
    public bool AddKey(KeyCode key)
    {
        if (!keyList.Contains(key))
        {
            //Debug.Log("AddKey : " + key);
            keyList.Add(key);
            return true;
        }
        return false;
    }
    public bool RemoveKey(KeyCode key)
    {
        if (keyList.Contains(key))
        {
            return keyList.Remove(key);
        }
        return false;
    }
    public bool Register(KeyCodeListener listener)
    {
        if (!listenerList.Contains(listener))
        {
            listenerList.Add(listener);
            return true;
        }
        return false;
    }
    public bool Unregister(KeyCodeListener listener)
    {
        if (listenerList.Contains(listener))
        {
            return listenerList.Remove(listener);
        }
        return false;
    }
    /*public void SortByInstant<TListener>() where TListener : KeyCodeListener
    {
        if (listenerList != null)
        {
            listenerList.Sort(delegate(KeyCodeListener a, KeyCodeListener b)
            {
                if (a is TListener)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });
        }
    }*/
}
/// <summary>
/// 
/// </summary>
partial class KeyCodeDevice
{
    List<KeyCode> keyList = new List<KeyCode>();
    List<KeyCodeListener> listenerList = new List<KeyCodeListener>();

    void Awake()
    {
        //Console.Register(this, false);
    }
    void OnDestroy()
    {
        //Console.Unregister(this);
    }
    void LateUpdate()
    {
        for (int i = 0; i < keyList.Count; i++)
        {
            if (Input.GetKeyUp(keyList[i]))
            {
                //Console.Write(this, string.Format("KeyUp : {0}", keyList[i]));
                NotifyKey(keyList[i], false);
            }
            else if (Input.GetKeyDown(keyList[i]))
            {
                //Console.Write(this, string.Format("KeyDown : {0}", keyList[i]));
                NotifyKey(keyList[i], true);
            }
        }
    }
    void NotifyKey(KeyCode key, bool touch)
    {
        // 
        for (int i = listenerList.Count - 1; i >= 0; i--)
        {
            if (listenerList[i].TouchKey(key, touch))
            {
                break;
            }
        }
    }
}
