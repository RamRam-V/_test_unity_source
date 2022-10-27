using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Asset = UnityEngine.Object;
/// <summary>
/// 
/// </summary>
static public partial class AssetFiles
{
    static public void SetResourcesPath(string path)
    {
        resourcesPath = path;
    }
    static public void SetAssetBundlePath(string path)
    {
        assetBundlePath = path;
    }
    static public string GetResourcesPath(string path)
    {
        if (!string.IsNullOrEmpty(resourcesPath))
        {
            return string.Format("{0}/{1}", resourcesPath, path);
        }
        return path;
    }
    static public string GetAssetBundlePath(string path)
    {
        if (!string.IsNullOrEmpty(assetBundlePath))
        {
            return string.Format("{0}/{1}", assetBundlePath, path);
        }
        return path;
    }
    static public Asset LoadAsset(string path)
    {
        AssetFile assetFile = LoadAssetFile(path, null);
        if (assetFile != null)
        {
            return assetFile.LoadAsset(path);
        }
        return null;
    }
    static public Asset LoadAsset(string path, string tag)
    {
        AssetFile assetFile = LoadAssetFile(path, tag);
        if (assetFile != null)
        {
            return assetFile.LoadAsset(path);
        }
        return null;
    }
    static public Asset LoadAsset(string path, System.Type type)
    {
        AssetFile assetFile = LoadAssetFile(path, null);
        if (assetFile != null)
        {
            return assetFile.LoadAsset(path, type);
        }
        return null;
    }
    static public Asset LoadAsset(string path, string tag, System.Type type)
    {
        AssetFile assetFile = LoadAssetFile(path, tag);
        if (assetFile != null)
        {
            return assetFile.LoadAsset(path, type);
        }
        return null;
    }

    static public TAsset LoadAsset<TAsset>(string path) where TAsset : Asset
    {
        return LoadAsset<TAsset>(path, null);
    }


    static public TAsset LoadAsset<TAsset>(string path, string tag) where TAsset : Asset
    {
       // Debug.Log("TAsset LoadAsset path :: " + path);

        AssetFile assetFile = LoadAssetFile(path, tag);
        if (assetFile != null)
        {
            return assetFile.LoadAsset<TAsset>(path);
        }
        return null;

    }
    static public void UnloadAsset(string path, bool unloadAllLoadedObjects)
    {
        AssetFile assetFile = assetFileList.Find(e => e.Path.Equals(path));
        if (assetFile != null)
        {
            if (assetFile.UnloadAsset(unloadAllLoadedObjects))
            {
                assetFileList.Remove(assetFile);
            }
            else
            {
                assetFileList.Remove(assetFile);
            }
        }
    }
    static public void UnloadAssetAtTag(string tag, bool unloadAllLoadedObjects)
    {
        for (int i = assetFileList.Count - 1; i >= 0; i--)
        {
            if (((assetFileList[i].Tag == null) && (tag == null)) || (assetFileList[i].Tag.Equals(tag)))
            {
                if (assetFileList[i].UnloadAsset(unloadAllLoadedObjects))
                {
                    assetFileList.RemoveAt(i);
                }
                else
                {
                    assetFileList.RemoveAt(i);
                }
            }
        }
    }
    static public void UnloadAssetAll(bool unloadAllLoadedObjects)
    {
        for (int i = assetFileList.Count - 1; i >= 0; i--)
        {
            if (assetFileList[i].UnloadAsset(unloadAllLoadedObjects))
            {
                assetFileList.RemoveAt(i);
            }
            else
            {
                assetFileList.RemoveAt(i);
            }
        }
    }
}
/// <summary>
/// 
/// </summary>
partial class AssetFiles
{
    static string resourcesPath = null;
    static string assetBundlePath = null;
    static List<AssetFile> assetFileList = new List<AssetFile>();

    static AssetFile LoadAssetFile(string path, string tag)
    {
        //Debug.Log("AssetFiles :: LoadAssetFile :: " + path);

        path = BundleFiles.PrepareAsset(path);
        AssetFile assetFile = assetFileList.Find(e => e.Path.Equals(path));
        if (assetFile == null)
        {
            BundleFile bundleFile = BundleFiles.ContainsBundleFile(path);
            if (bundleFile != null)
            {
                assetFile = new AssetExternalFile(bundleFile, path, tag);
                assetFileList.Add(assetFile);
            }
            else
            {
                assetFile = new AssetInternalFile(path, tag);
                assetFileList.Add(assetFile);
            }
        }
        return assetFile;
    }
}
