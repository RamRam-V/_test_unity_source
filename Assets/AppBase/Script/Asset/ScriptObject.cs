//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
///// <summary>
///// 
///// </summary>
//public abstract class ScriptObject : ScriptableObject
//{
//    static public TScript Create<TScript>() where TScript : ScriptableObject
//    {
//        return ScriptableObject.CreateInstance<TScript>();
//    }
//    static public void Release(ScriptObject script)
//    {
//        ScriptableObject.Destroy(script);
//    }
//}
///// <summary>
///// 
///// </summary>
//public abstract partial class SingletonScript<TScript> : ScriptableObject where TScript : ScriptableObject
//{
//    static TScript instance = null;

//    static public TScript Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = Find();
//                if (instance == null)
//                {
//                    instance = Create();
//                }
//            }
//            return instance;
//        }
//    }
//    static public bool Verify()
//    {
//        return (instance != null);
//    }
//    static protected void Release()
//    {
//        if (instance != null)
//        {
//            if (scripts.ContainsKey(typeof(TScript)))
//            {
//                scripts.Remove(typeof(TScript));
//            }
//            ScriptableObject.Destroy(instance);
//            instance = null;
//        }
//    }
//}
///// <summary>
///// 
///// </summary>
//partial class SingletonScript<TScript>
//{
//    static Dictionary<System.Type, ScriptableObject> scripts = new Dictionary<System.Type, ScriptableObject>();

//    static TScript Find()
//    {
//        if (scripts.ContainsKey(typeof(TScript)))
//        {
//            return scripts[typeof(TScript)] as TScript;
//        }
//        return ScriptableObject.FindObjectOfType<TScript>();
//    }
//    /*static TScript[] FindArray()
//    {
//        TScript[] scriptArray = ScriptableObject.FindObjectsOfType<TScript>();
//        return scriptArray;
//    }*/
//    static TScript Create()
//    {
//        if (!scripts.ContainsKey(typeof(TScript)))
//        {
//            TScript script = ScriptableObject.CreateInstance<TScript>() as TScript;
//            if (script != null)
//            {
//                scripts.Add(typeof(TScript), script);
//            }
//            return script;
//        }
//        return scripts[typeof(TScript)] as TScript;
//    }
//}
