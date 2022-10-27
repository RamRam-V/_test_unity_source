using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Asset = UnityEngine.Object;
/// <summary>
/// 
/// </summary>
public interface BundleManager
{
    void Initialize();
    void SetBundleKey(string key);
    int GetBundleCountByKey();
    string GetBundleNameByKey(int index);
    string PrepareAsset(string path);
    void LoadFromDownload(BundleFiles.CallBack bundleCallback, BundleFile bundleFile);
    void LoadFromCacheOrDownload(BundleFiles.CallBack bundleCallback, BundleFile bundleFile);
    
}
/// <summary>
/// 
/// </summary>
static public partial class BundleFiles
{
    static public bool Output
    {
        get;
        set;
    }
    static public bool Overwrite
    {
        get;
        set;
    }
    public delegate void CallBack(BundleFile bundleFile);

    static public void Clear()
    {
        bundleManager = null;
        //bundleCallback = null;
        bundleManifest = null;
        bundleNameList.Clear();
    }
    static public void SetBundleManager(BundleManager bundleManager)
    {
        BundleFiles.bundleManager = bundleManager;
    }
    static public void Initialize()
    {
        if (bundleManager != null)
        {
            bundleManager.Initialize();
        }
    }
    static public void SetBundleKey(string bundleKey)
    {
        if (bundleManager != null)
        {
            bundleManager.SetBundleKey(bundleKey);
        }
    }
    static public int GetBundleCountByKey()
    {
        if (bundleManager != null)
        {
            return bundleManager.GetBundleCountByKey();
        }
        return 0;
    }
    static public string GetBundleNameByKey(int index)
    {
        if (bundleManager != null)
        {
            string bundleName = bundleManager.GetBundleNameByKey(index);
            if (!bundleName.Contains(Configuration.AssetBundleExtension))
            {
                bundleName += Configuration.AssetBundleExtension;
            }
            return bundleName;
        }
        return null;
    }
    static public string PrepareAsset(string path)
    {
        if (bundleManager != null)
        {
            Debug.Log("BundleFiles :: PrepareAsset :: " + path);
            return bundleManager.PrepareAsset(path);
        }
        return path;
    }
    static public void LoadFromDownload(BundleFiles.CallBack callback, BundleFile bundleFile)
    {
        if (bundleManager != null)
        {
            Debug.Log("BundleFiles :: LoadFromDownload :: " + bundleFile.URL);
            bundleManager.LoadFromDownload(callback, bundleFile);
        }
    }
    static public void LoadFromCacheOrDownload(BundleFiles.CallBack callback, BundleFile bundleFile)
    {
        if (bundleManager != null)
        {
            bundleManager.LoadFromCacheOrDownload(callback, bundleFile);
        }
    }    

}
/// <summary>
/// 
/// </summary>
partial class BundleFiles
{
    static public int GetBundleCount()
    {
        return bundleNameList.Count;
    }
    static public string GetBundleName(int index)
    {
        if (index < bundleNameList.Count)
        {
            return bundleNameList[index].key;
        }
        return null;
    }
    static public string GetBundleName(string path)
    {
        path = path.ToLower();
        for (int i = 0; i < bundleNameList.Count; i++)
        {
            if (!string.IsNullOrEmpty(bundleNameList[i].list.Find(e => e.Contains(path))))
            {
                return bundleNameList[i].key;
            }
        }
        return null;
    }
    static public BundleFile ContainsBundleFile(string path)
    {
        BundleFile bundleFile = bundleFileList.Find(e => e.Contains(path));
        if (bundleFile != null)
        {
            return bundleFile;
        }
        return null;
    }
    static public BundleFile FindBundleFile(string bundleName)
    {
        BundleFile bundleFile = bundleFileList.Find(e => e.Name.Equals(bundleName));
        if (bundleFile != null)
        {
            return bundleFile;
        }
        return null;
    }
    static public BundleFile LoadBundleFile(string bundleName)
    {
        Debug.Log("BundleFiles :: LoadBundleFile :: " + bundleName);

        if (!bundleName.Contains(Configuration.AssetBundleExtension))
        {
            bundleName += Configuration.AssetBundleExtension;                
        }

        Debug.Log("BundleFile LoadBundleFile :: " + bundleName);

        BundleFile bundleFile = FindBundleFile(bundleName);

        Debug.Log("BundleFiles :: LoadBundleFile :: " + bundleFile);

        if (bundleFile == null)
        {
            if (bundleManifest != null)
            {
                bundleFile = new BundleFile(bundleName, bundleManifest.GetAssetBundleHash(bundleName));
            }
            else
            {
                bundleFile = new BundleFile(bundleName, new Hash128());
            }

            bundleFileList.Add(bundleFile);
        }
        return bundleFile;
    }
    static public bool UnloadBundleFile(BundleFile bundleFile, bool unloadAllLoadedObjects)
    {
        if (bundleFile != null)
        {
            if (bundleFile.UnloadAsset(unloadAllLoadedObjects))
            {
                bundleFileList.Remove(bundleFile);
                return true;
            }
        }
        return false;
    }
    static public void RegisterBundleFile(BundleFile bundleFile)
    {
        string[] assetNames = bundleFile.GetAllAssetNames();
        if ((assetNames != null) && (assetNames.Length > 0))
        {
            TextKeyList bundleName = bundleNameList.Find(e => e.key.Equals(bundleFile.Name));
            if ((bundleName != null) && (bundleName.list.Count == 0))
            {
                bundleName.list = new List<string>(assetNames);
            }
        }
    }
}
/// <summary>
/// 
/// </summary>
partial class BundleFiles
{
    static public bool LoadFromDownloadList()//global::CallBack callback)
    {
        Debug.Log("BundleFiles :: BundleFiles");

        if (bundleManifest == null)
        {            
            if (bundleManager != null)
            {
                //bundleCallback = callback;
                BundleFile bundleFile = LoadBundleFile(Configuration.GetPlatformFolder());

                Debug.Log("BundleFiles :: LoadFromDownloadList :: " + bundleFile.URL);

                if (bundleFile != null)
                {
                    bundleManager.LoadFromDownload(LoadFromDownloadListComplete, bundleFile);
                    return true;
                }
            }
            //if (callback != null)
            //{
            //    callback();
            //}
        }
        return false;
    }
    static public void UnloadBundleFiles(bool unloadAllLoadedObjects)
    {
        for (int i = bundleFileList.Count - 1; i >= 0; i--)
        {
            if (bundleFileList[i] != null)
            {
                if (bundleFileList[i].UnloadAsset(unloadAllLoadedObjects))
                {
                    bundleFileList.RemoveAt(i);
                }
                else if (!bundleFileList[i].isDownload)
                {
                    if (!bundleFileList[i].isEmpty)
                    {
                        bundleFileList[i].Stop();
                    }
                    bundleFileList.RemoveAt(i);
                }
            }
        }
    }
}
/// <summary>
/// 
/// </summary>
partial class BundleFiles
{
    static BundleManager bundleManager = null;
    //static global::CallBack bundleCallback = null;
    static AssetBundleManifest bundleManifest = null;
    static List<TextKeyList> bundleNameList = new List<TextKeyList>();
    static List<BundleFile> bundleFileList = new List<BundleFile>();

    static void LoadFromDownloadListComplete(BundleFile bundleFile)
    {
        if (bundleFile != null)
        {
            if (bundleFile.IsError())
            {
                //Console.Write(bundleFile.GetErrorMessage());
                UnloadBundleFile(bundleFile, true);
            }
            else
            {
                bundleManifest = bundleFile.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                if (bundleManifest != null)
                {
                    LoadBundleNameList(bundleManifest);
                }
                else
                {
                    UnloadBundleFile(bundleFile, true);
                    LoadBundleNameList();
                }
            }
        }
        //if (bundleCallback != null)
        //{
        //    bundleCallback();
        //}
    }
    static void LoadBundleNameList(AssetBundleManifest assetBundleManifest)
    {
        string[] assetBundleNames = assetBundleManifest.GetAllAssetBundles();
        for (int i = 0; i < assetBundleNames.Length; i++)
        {
            //string[] assetFiles = assetBundleManifest.GetAllDependencies(assetBundleNames[i]);
            //bundleNameList.Add(new TextKeyList(assetBundleNames[i], assetFiles));
            bundleNameList.Add(new TextKeyList(assetBundleNames[i]));
        }
    }
    static void LoadBundleNameList()
    {
#if UNITY_EDITOR
        string[] assetBundleNames = UnityEditor.AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < assetBundleNames.Length; i++)
        {
            string[] assetFiles = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleNames[i]);
            for (int j = 0; j < assetFiles.Length; j++)
            {
                assetFiles[j] = assetFiles[j].ToLower();
            }
            bundleNameList.Add(new TextKeyList(assetBundleNames[i], assetFiles));
        }
#endif
    }
}
