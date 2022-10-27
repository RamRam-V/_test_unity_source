using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public class Pair<TKey, TValue>
{
    public TKey key;
    public TValue value;
    public Pair(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}
/// <summary>
/// 
/// </summary>
public partial class PairList<TKey, TValue>
{
    public int Count
    {
        get
        {
            return list.Count;
        }
    }
    public TKey this[TValue value]
    {
        get
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].value.Equals(value))
                {
                    return list[i].key;
                }
            }
            return default(TKey);
        }
    }
    public TValue this[TKey key]
    {
        get
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].key.Equals(key))
                {
                    return list[i].value;
                }
            }
            return default(TValue);
        }
    }
    public Pair<TKey, TValue> this[int index]
    {
        get
        {
            if (index < list.Count)
            {
                return list[index];
            }
            return null;
        }
    }
    public bool ContainsKey(TKey key)
    {
        if (list.Find(e => e.key.Equals(key)) != null)
        {
            return true;
        }
        return false;
    }
    public bool ContainsValue(TValue value)
    {
        if (list.Find(e => e.value.Equals(value)) != null)
        {
            return true;
        }
        return false;
    }
    public bool Add(TKey key, TValue value)
    {
        if (!ContainsKey(key))
        {
            list.Add(new Pair<TKey, TValue>(key, value));
            return true;
        }
        return false;
    }
    public bool Remove(TKey key)
    {
        Pair<TKey, TValue> pair = list.Find(e => e.key.Equals(key));
        if (pair != null)
        {
            return list.Remove(pair);
        }
        return false;
    }
    public bool RemoveAt(int index)
    {
        if (index < list.Count)
        {
            list.RemoveAt(index);
            return true;
        }
        return false;
    }
}
/// <summary>
/// 
/// </summary>
partial class PairList<TKey, TValue>
{
    List<Pair<TKey, TValue>> list = new List<Pair<TKey, TValue>>();
}
