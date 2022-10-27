using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// </summary>
public static class Configuration
{
    static Configuration()
    {
        TextExtension = ".txt";
        MaterialExtension = ".mat";
        MetaFileExtension = ".meta";
        JsonFileExtension = ".json";
        PrefabFileExtension = ".prefab";
        AssetBundleExtension = ".bin";
        AssetBundleFolder = "AssetBundles";
        BuildFolder = "Build";
    }
    public static string TextExtension
    {
        get;
        private set;
    }
    public static string MaterialExtension
    {
        get;
        private set;
    }
    public static string JsonFileExtension
    {
        get;
        private set;
    }
    public static string MetaFileExtension
    {
        get;
        private set;
    }
    public static string PrefabFileExtension
    {
        get;
        private set;
    }
    public static string AssetBundleExtension
    {
        get;
        private set;
    }
    public static string AssetBundleFolder
    {
        get;
        private set;
    }
    public static string BuildFolder
    {
        get;
        private set;
    }
    public static string GetWritablePath()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return "/mnt/sdcard";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return Application.persistentDataPath;
        }
        return Application.dataPath.Replace("/Assets", "");
    }
    public static string GetRelativePath()
    {
        return GetRelativePath(Application.platform);
    }
    public static string GetRelativePath(RuntimePlatform platform)
    {
        /*if (Application.isEditor)
        {
            // Use the build output folder directly.
            return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/") + "/" + Configuration.BuildFolder;
        }
        else if (Application.isWebPlayer)
        {
            return System.IO.Path.GetDirectoryName(Application.absoluteURL).Replace("\\", "/") + "/StreamingAssets";
        }
        else if (Application.isMobilePlatform || Application.isConsolePlatform)
        {
            return Application.streamingAssetsPath;
        }*/
        // For standalone player.
        if (platform == RuntimePlatform.Android)
        {
            return "http://d3qdbhlem9urva.cloudfront.net/patch";
        }
        //return "http://52.193.144.142:10382";
        //return "http://172.20.10.201:8888/space";
        return "file:///E:/TestMovie";
        //return AppProject.ServerPath;
    }
    public static string GetAssetBundlePath()
    {
        string platformFolder = GetPlatformFolder();
        return string.Format("{0}/{1}/{2}", GetRelativePath(), AssetBundleFolder, platformFolder);
    }
#if UNITY_EDITOR
    public static string GetPlatformFolder(UnityEditor.BuildTarget target)
    {
        switch (target)
        {
            case UnityEditor.BuildTarget.Android:
                return "Android";
            case UnityEditor.BuildTarget.iOS:
                return "iOS";
            //case UnityEditor.BuildTarget.WebPlayer:
            //    return "WebPlayer";
            case UnityEditor.BuildTarget.StandaloneWindows:
            case UnityEditor.BuildTarget.StandaloneWindows64:
                return "Windows";
            case UnityEditor.BuildTarget.StandaloneOSXIntel:
            case UnityEditor.BuildTarget.StandaloneOSXIntel64:
            case UnityEditor.BuildTarget.StandaloneOSX:
                return "OSX";
            case UnityEditor.BuildTarget.WSAPlayer:
                return "HoloLens";
            default:
                return "";
        }
    }
#endif
    public static string GetPlatformFolder()
    {
        return GetPlatformFolder(Application.platform);
    }
    public static string GetPlatformFolder(RuntimePlatform platform)
    {
        switch (platform)
        {
            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.IPhonePlayer:
                return "iOS";
            //case RuntimePlatform.WindowsWebPlayer:
            //case RuntimePlatform.OSXWebPlayer:
            //    return "WebPlayer";
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                return "Windows";
            case RuntimePlatform.OSXPlayer:
                return "OSX";
            case RuntimePlatform.WSAPlayerARM:
            case RuntimePlatform.WSAPlayerX64:
            case RuntimePlatform.WSAPlayerX86:
                return "HoloLens";
            default:
                return "";
        }
    }
}
