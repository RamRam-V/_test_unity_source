using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Asset = UnityEngine.Object;
/// <summary>
/// 
/// </summary>
public abstract class AssetFile
{
    public string Tag
    {
        get;
        protected set;
    }
    public string Path
    {
        get;
        protected set;
    }
    public abstract Asset LoadAsset(string path);
    public abstract Asset LoadAsset(string path, System.Type type);
    public abstract TAsset LoadAsset<TAsset>(string path) where TAsset : Asset;
    public abstract bool UnloadAsset(bool unloadAllLoadedObjects);
}
/// <summary>
/// 
/// </summary>
public partial class AssetExternalFile : AssetFile
{
    public override Asset LoadAsset(string path)
    {
        if (loadedAsset == null)
        {
            if (bundleFile != null)
            {
                loadedAsset = bundleFile.LoadAsset(path);
            }
        }
        return loadedAsset;
    }
    public override Asset LoadAsset(string path, System.Type type)
    {
        if (loadedAsset == null)
        {
            if (bundleFile != null)
            {
                loadedAsset = bundleFile.LoadAsset(path, type);
            }
        }
        return loadedAsset;
    }
    public override TAsset LoadAsset<TAsset>(string path)
    {
        if (loadedAsset == null)
        {
            if (bundleFile != null)
            {
                loadedAsset = bundleFile.LoadAsset<TAsset>(path);
            }
        }
        return loadedAsset as TAsset;
    }
    public override bool UnloadAsset(bool unloadAllLoadedObjects)
    {
        if (loadedAsset != null)
        {
            if (bundleFile != null)
            {
                if (bundleFile.UnloadAsset(unloadAllLoadedObjects))
                {
                    loadedAsset = null;
                    return true;
                }
                
            }
        }
        return false;
    }
}
/// <summary>
/// 
/// </summary>
partial class AssetExternalFile
{
    Asset loadedAsset = null;
    BundleFile bundleFile = null;

    public AssetExternalFile(BundleFile bundleFile, string path, string tag)
    {
        this.Tag = tag;
        this.Path = path;
        this.bundleFile = bundleFile;
    }
}
/// <summary>
/// 
/// </summary>
public partial class AssetInternalFile : AssetFile
{
    public override Asset LoadAsset(string path)
    {
        if (loadedAsset == null)
        {
            loadedAsset = ResourcesLoad(path);
        }
#if UNITY_EDITOR
        if (loadedAsset == null)
        {
            loadedAsset = AssetDataBaseLoad(path);
        }
        if (loadedAsset == null)
        {
            loadedAsset = AssetDataBaseLoadIntact(path);
        }
#endif
        return loadedAsset;
    }
    public override Asset LoadAsset(string path, System.Type type)
    {
        if (loadedAsset == null)
        {
            loadedAsset = ResourcesLoad(path, type);
        }
#if UNITY_EDITOR
        if (loadedAsset == null)
        {
            loadedAsset = AssetDataBaseLoad(path, type);
        }
        if (loadedAsset == null)
        {
            loadedAsset = AssetDataBaseLoadIntact(path, type);
        }
#endif
        return loadedAsset;
    }
    public override TAsset LoadAsset<TAsset>(string path)
    {
        if (loadedAsset == null)
        {
            loadedAsset = ResourcesLoad<TAsset>(path);
        }
#if UNITY_EDITOR
        if (loadedAsset == null)
        {
            loadedAsset = AssetDataBaseLoad<TAsset>(path);
        }
        if (loadedAsset == null)
        {
            loadedAsset = AssetDataBaseLoadIntact<TAsset>(path);
        }
#endif
        return loadedAsset as TAsset;
    }
    public override bool UnloadAsset(bool unloadAllLoadedObjects)
    {
        //Resources.UnloadAsset(loadedAsset);
        loadedAsset =  null;
        return true;
    }
}
/// <summary>
/// 
/// </summary>
partial class AssetInternalFile
{
    Asset loadedAsset = null;

    public AssetInternalFile(string path, string tag)
    {
        this.Tag = tag;
        this.Path = path;
    }
    string RemoveFileExtension(string filePath)
    {
        int index = filePath.LastIndexOf(".");
        if (index > 0)
        {
            filePath = filePath.Remove(index);
        }
        return filePath;
    }
    Asset ResourcesLoad(string path)
    {
        string assetPath = RemoveFileExtension(path);
        return Resources.Load(assetPath);
    }
    Asset ResourcesLoad(string path, System.Type type)
    {
        string assetPath = RemoveFileExtension(path);
        return Resources.Load(assetPath, type);
    }
    TAsset ResourcesLoad<TAsset>(string path) where TAsset : Asset
    {
        string assetPath = RemoveFileExtension(path);
        return Resources.Load<TAsset>(assetPath);
    }
#if UNITY_EDITOR
    static Asset AssetDataBaseLoad(string path)
    {
        string assetPath = AssetFiles.GetAssetBundlePath(path);
        return UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath, typeof(Asset));
    }
    static Asset AssetDataBaseLoad(string path, System.Type type)
    {
        string assetPath = AssetFiles.GetAssetBundlePath(path);
        return UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath, type);
    }
    static TAsset AssetDataBaseLoad<TAsset>(string path) where TAsset : Asset
    {
        string assetPath = AssetFiles.GetAssetBundlePath(path);
        return UnityEditor.AssetDatabase.LoadAssetAtPath<TAsset>(assetPath);
    }
    static Asset AssetDataBaseLoadIntact(string path)
    {
        return UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(Asset));
    }
    static Asset AssetDataBaseLoadIntact(string path, System.Type type)
    {
        return UnityEditor.AssetDatabase.LoadAssetAtPath(path, type);
    }
    static TAsset AssetDataBaseLoadIntact<TAsset>(string path) where TAsset : Asset
    {
        return UnityEditor.AssetDatabase.LoadAssetAtPath<TAsset>(path);
    }
#endif
}
