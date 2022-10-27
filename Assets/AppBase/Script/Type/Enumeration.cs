using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
static public partial class Enumeration<TEnum, TIndex>
{
    static public System.Array GetValues()
    {
        return System.Enum.GetValues(typeof(TEnum));
    }
    static public bool Add(TEnum type, TIndex index)
    {
        return list.Add(type, index);
    }
    static public TIndex GetIndex(TEnum type)
    {
        return list[type];
    }
    static public TEnum GetType(TIndex index)
    {
        return list[index];
    }
    static public bool ContainsIndex(TIndex index)
    {
        return list.ContainsValue(index);
    }
    static public bool ContainsType(TEnum type)
    {
        return list.ContainsKey(type);
    }
}
/// <summary>
/// 
/// </summary>
static public partial class Enumeration<TEnum, TIndex>
{
    static PairList<TEnum, TIndex> list = new PairList<TEnum, TIndex>();
}
