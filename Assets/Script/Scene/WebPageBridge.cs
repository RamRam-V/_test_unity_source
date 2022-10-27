using System.Runtime.InteropServices;

public class WebPageBridge : CSingleton<WebPageBridge>
{
    [DllImport("__Internal")]
    public static extern void AvatarLoadingCompleted();

    [DllImport("__Internal")]
    public static extern void AvatarLoadFail();

    public void AvatarLoadingCompletedWebPage()
    {
#if !UNITY_EDITOR
        AvatarLoadingCompleted();
#endif
    }

    public void AvatarLoadFailWebPage()
    {
#if !UNITY_EDITOR
        AvatarLoadFail();
#endif
    }
}