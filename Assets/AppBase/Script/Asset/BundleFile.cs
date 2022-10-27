using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Asset = UnityEngine.Object;
/// <summary>
/// 
/// </summary>
class TextKeyList
{
    public string key = null;
    public List<string> list = null;

    public TextKeyList(string key)
    {
        this.key = key;
        this.list = new List<string>();
    }
    public TextKeyList(string key, string[] array)
    {
        this.key = key;
        this.list = new List<string>(array);
    }
    public TextKeyList(string key, List<string> list)
    {
        this.key = key;
        this.list = new List<string>(list);
    }
}
/// <summary>
/// 
/// </summary>
public partial class BundleFile
{
    public string URL
    {
        get;
        private set;
    }
    public string Name
    {
        get;
        private set;
    }
    public bool isDownload
    {
        get;
        private set;
    }
    public bool isEmpty
    {
        get
        {
            return (www == null);
        }
    }
    public void Stop()
    {
        if (www != null)
        {
            www.Dispose();
            www = null;
        }
        isDownload = false;
    }
    public bool IsError()
    {
        if (www != null)
        {
            return !string.IsNullOrEmpty(www.error);
        }
        return false;
    }
    public string GetErrorMessage()
    {
        if (www != null)
        {
            return www.error;
        }
        return null;
    }
    public IEnumerator LoadFromDownload()
    {
        Stop();
        //isDownload = true;
        //www = new WWW(URL);
        //yield return www;
        //isDownload = false;

        isDownload = true;

        //if (URL == AppProject.PatchPath)
        //{
        //    Debug.Log("LoadFromDownload URL Skip :: " + URL);
        //    isDownload = false;
        //    www = new WWW("");
        //    yield return www;
        //}
        //else
        {
            www = new WWW(URL);
            yield return www;
        }

        isDownload = false;

    }
    public IEnumerator LoadFromCacheOrDownload()
    {
        Stop();
        isDownload = true;
        www = WWW.LoadFromCacheOrDownload(URL, hash);
        yield return www;
        isDownload = false;
    }
    public Asset LoadAsset(string path)
    {
        if (IsCached())
        {
            string name = FindName(path);
            if (!string.IsNullOrEmpty(name))
            {
                return www.assetBundle.LoadAsset(name);
            }
        }
        return null;
    }
    public Asset LoadAsset(string path, System.Type type)
    {
        if (IsCached())
        {
            string name = FindName(path);
            if (!string.IsNullOrEmpty(name))
            {
                return www.assetBundle.LoadAsset(name, type);
            }
        }
        return null;
    }
    public TAsset LoadAsset<TAsset>(string path) where TAsset : Asset
    {
        if (IsCached())
        {
            string name = FindName(path);
            if (!string.IsNullOrEmpty(name))
            {
                return www.assetBundle.LoadAsset<TAsset>(name);
            }
        }
        return null;
    }
    public bool UnloadAsset(bool unloadAllLoadedObjects)
    {
        if (IsCached())
        {
            www.assetBundle.Unload(unloadAllLoadedObjects);
            www.Dispose();
            www = null;
            return true;
        }
        return false;
    }
    public bool Contains(string path)
    {
        if (IsCached())
        {
            path = path.ToLower();
            string[] names = www.assetBundle.GetAllAssetNames();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Contains(path))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public string[] GetAllAssetNames()
    {
        if (IsCached())
        {
            return www.assetBundle.GetAllAssetNames();
        }
        return null;
    }
    string FindName(string path)
    {
        if (IsCached())
        {
            path = path.ToLower();
            string[] names = www.assetBundle.GetAllAssetNames();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Contains(path))
                {
                    return names[i];
                }
            }
        }
        return null;
    }
}
/// <summary>
/// 
/// </summary>
partial class BundleFile
{
    WWW www = null;
    Hash128 hash;

    public BundleFile(string name, Hash128 hash)
    {
        this.URL = GetBundleURL(name);
        this.Name = name;
        this.hash = hash;
        this.isDownload = false;
    }
    string GetBundleURL(string name)
    {
        if (!name.Contains(Configuration.AssetBundleExtension))
        {
           name += Configuration.AssetBundleExtension;
        }

        return string.Format("{0}/{1}", Configuration.GetAssetBundlePath(), name);
    }
    bool IsCached()
    {
        return (!isDownload) && (www != null) && (string.IsNullOrEmpty(www.error)) && (www.assetBundle != null);
    }
}
