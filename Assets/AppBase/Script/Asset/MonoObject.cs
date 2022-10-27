#if UNITY_EDITOR
#define HIDE_IN_HIERARCHY
#endif
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>
public abstract partial class MonoObject : MonoBehaviour
{
    Transform _transform = null;
    public Transform Transform
    {
        get
        {
            if (_transform == null)
            {
                _transform = this.transform;
            }
            return _transform;
        }
    }
    bool _destroying = false;
    public bool Destroying
    {
        get
        {
            return _destroying;
        }
        protected set
        {
            _destroying = value;
        }
    }
    public virtual void Release()
    {
    }
}
/// <summary>
/// 
/// </summary>
partial class MonoObject
{
    Rigidbody _rigidbody = null;
    public Rigidbody Rigidbody
    {
        get
        {
            if (_rigidbody == null)
            {
                _rigidbody = AssetObject.LoadComponent<Rigidbody>(gameObject);
            }
            return _rigidbody;
        }
    }
}
/// <summary>
/// temp code...
/// </summary>
partial class MonoObject
{
    List<string> fullList = new List<string>();
    List<string> instantFileList = new List<string>();

    protected int InstantFileCount
    {
        get
        {
            return instantFileList.Count;
        }
    }
    protected void PushInstantFile(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            if (!instantFileList.Contains(path))
            {
                if (!fullList.Contains(path))
                {
                    fullList.Add(path);
                    instantFileList.Add(path);
                }
            }
        }
    }
    protected string PopInstantFile(int index)
    {
        if (index < instantFileList.Count)
        {
            string path = instantFileList[index];
            instantFileList.RemoveAt(index);
            return path;
        }
        return null;
    }
}
/// <summary>
/// 
/// </summary>
partial class MonoObject
{
    static bool clearing = false;
    static List<MonoObject> singletonList = new List<MonoObject>();

    static public void ClearSingleton()
    {
        clearing = true;
        {
            for (int i=0; i<singletonList.Count;i++)
            {
                singletonList[i].Release();
            }
            singletonList.Clear();
        }
        clearing = false;
    }
    static public void ClearHierarchy(MonoBehaviour exception)
    {
        MonoBehaviour[] monoBehaviourArray = GameObject.FindObjectsOfType<MonoBehaviour>();
        if (monoBehaviourArray != null)
        {
            for (int i = 0; i < monoBehaviourArray.Length;i++ )
            {
                if (!monoBehaviourArray[i].Equals(exception))
                {
                    AssetObject.Destroy(monoBehaviourArray[i].gameObject);
                }
            }
        }
    }
    static protected bool InsertSingleton(MonoObject monoObject)
    {
        if (!clearing)
        {
            if (!singletonList.Contains(monoObject))
            {
                singletonList.Add(monoObject);
                return true;
            }
        }
        return false;
    }
    static protected bool RemoveSingleton(MonoObject monoObject)
    {
        if (!clearing)
        {
            MonoObject foundObject = singletonList.Find(e => e.Equals(monoObject));
            if (foundObject != null)
            {
                return singletonList.Remove(foundObject);
            }
        }
        return false;
    }
}
/// <summary>
/// 
/// </summary>
public abstract class SingletonMono<TComponent> : MonoObject where TComponent : MonoObject
{
    static TComponent instance = null;

    static public string GetName()
    {
        return typeof(TComponent).ToString();
    }
    static public TComponent Instance
    {
        get
        {
            if (instance == null)
            {
                instance = AssetObject.FindComponent<TComponent>(GetName());
                if (instance == null)
                {
                    instance = AssetObject.NewComponent<TComponent>(GetName());
                    if (instance != null)
                    {
                        InsertSingleton(instance);
                        AssetObject.DontDestroyOnLoad(instance.gameObject);
                    }
                }
            }
            return instance;
        }
    }
    static public bool Verify()
    {
        return (instance != null);
    }
    public override void Release()
    {
        if (instance != null)
        {
            RemoveSingleton(instance);
            AssetObject.Destroy(instance.gameObject);
            instance = null;
        }
    }
    [Conditional("HIDE_IN_HIERARCHY")]
    protected void HideInHierarchy()
    {
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }
}
