using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
/// <summary>
/// 
/// </summary>
static public partial class ScreenDevice
{
    public enum RatioTypes
    {
        unknown,
        r3x2,
        r4x3,
        r16x9,
        r16x10,
    }
    static public RatioTypes RatioType
    {
        get
        {
            return GetRatioType();
        }
    }
    static public Vector2 Ratio
    {
        get
        {
            return GetRatio();
        }
    }
    static public int Width
    {
        get
        {
            return Screen.width;
        }
    }
    static public int Height
    {
        get
        {
            return Screen.height;
        }
    }
    static public int DeviceWidth
    {
        get
        {
            return GetDeviceSize().x;
        }
    }
    static public int DeviceHeight
    {
        get
        {
            return GetDeviceSize().y;
        }
    }
    static public Texture2D CaptureScreenShot()
    {
        return CaptureScreenShotEx();
    }
    static public Point GetDeviceSize()
    {
        if (deviceSize.IsZero())
        {
#if UNITY_EDITOR
            deviceSize = GetGameViewSize();
#else
            deviceSize = new Point(Screen.width, Screen.height);
#endif
        }
        return deviceSize;
    }
}
/// <summary>
/// screenshot.
/// </summary>
partial class ScreenDevice
{
    static Texture2D screenShot = null;
    static RenderTexture renderTexture = null;

    static Texture2D CaptureScreenShotEx()
    {
        // after ... yield return new WaitForEndOfFrame();
        ClearScreenShot();
        if (screenShot == null)
        {
            screenShot = new Texture2D(Width, Height, TextureFormat.RGB24, true);
        }
        ClearRenderTexture();
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(Width, Height, 24);
            renderTexture.Create();
        }
        if (screenShot != null)
        {
            if (Camera.allCameras != null)
            {
                for (int i = 0; i < Camera.allCameras.Length; i++)
                {
                    RenderTexture targetTexture = Camera.allCameras[i].targetTexture;
                    Camera.allCameras[i].targetTexture = renderTexture;
                    Camera.allCameras[i].Render();
                    Camera.allCameras[i].targetTexture = targetTexture;
                }
            }
            //RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(new Rect(0, 0, Width, Height), 0, 0, true);
            screenShot.Apply();
            RenderTexture.active = null;
            return screenShot;
        }
        return null;
    }
    [Conditional("UNITY_EDITOR")]
    static public void ClearScreenShot()
    {
        if (screenShot != null)
        {
            if ((screenShot.width != Width) || (screenShot.height != Height))
            {
                AssetObject.Destroy(screenShot);
                screenShot = null;
            }
        }
    }
    [Conditional("UNITY_EDITOR")]
    static void ClearRenderTexture()
    {
        if (renderTexture != null)
        {
            if ((screenShot.width != renderTexture.width) || (screenShot.height != renderTexture.height))
            {
                RenderTexture.Destroy(renderTexture);
                renderTexture = null;
            }
        }
    }
}
/// <summary>
/// 
/// </summary>
partial class ScreenDevice
{
    static Point deviceSize = Point.zero;
    static Vector2 ratioValue = Vector2.zero;
    static RatioTypes ratioType = RatioTypes.unknown;

    static RatioTypes GetRatioType()
    {
        if (ratioType == RatioTypes.unknown)
        {
            Vector2 ratio = GetRatio();
            if (ratio.x == 3 && ratio.y == 2)
            {
                ratioType = RatioTypes.r3x2;
            }
            else if (ratio.x == 4 && ratio.y == 3)
            {
                ratioType = RatioTypes.r4x3;
            }
            else if (ratio.x == 16 && ratio.y == 9)
            {
                ratioType = RatioTypes.r16x9;
            }
            else if (ratio.x == 16 && ratio.y == 10)
            {
                ratioType = RatioTypes.r16x10;
            }
            //exceptional
            else if (ratio.x == 40 && ratio.y == 23)
            {
                //Nuxus 1280x736
                ratioType = RatioTypes.r16x9;
            }
            else if (ratio.x == 299 && ratio.y == 180)
            {
                //Vega Iron 1196x720
                ratioType = RatioTypes.r16x9;
            }
            else
            {
                ratioType = RatioTypes.unknown;
            }
        }
        return ratioType;
    }
    static Vector2 GetRatio()
    {
        if (ratioValue == Vector2.zero)
        {
            float width = GetDeviceSize().x;
            float height = GetDeviceSize().y;
            if ((width != 0.0f) && (height != 0.0f))
            {
                int calc = Mathf.FloorToInt((width / height) * 100);
                //+- 0.01
                if (132 <= calc && calc <= 134)
                {
                    //1.33
                    ratioValue = new Vector2(4, 3);
                }
                else if (149 <= calc && calc <= 151)
                {
                    //1.50
                    ratioValue = new Vector2(3, 2);
                }
                else if (159 <= calc && calc <= 161)
                {
                    //1.60
                    ratioValue = new Vector2(16, 10);
                }
                else if (176 <= calc && calc <= 178)
                {
                    //1.77
                    ratioValue = new Vector2(16, 9);
                }
                else
                {
                    ratioValue = GetRatioEx();
                }
            }
        }
        return ratioValue;
    }
    static Vector2 GetRatioEx()
    {
        int width = (int)GetDeviceSize().x;
        int height = (int)GetDeviceSize().y;
        if ((width != 0) && (height != 0))
        {
            int temp, min, max;
            if (width < height)
            {
                min = height;
                max = width;
            }
            else
            {
                min = width;
                max = height;
            }
            while ((max % min) != 0)
            {
                temp = max % min;
                max = min;
                min = temp;
            }
            return new Vector2(width / min, height / min);
        }
        return Vector2.one;
    }
    static Point GetGameViewSize()
    {
        System.Type gameViewType = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        BindingFlags mFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        MethodInfo GetMainGameView = gameViewType.GetMethod("GetMainGameView", mFlags);
        System.Object mainGameView = GetMainGameView.Invoke(null, null);
        if (mainGameView != null)
        {
            BindingFlags pFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            PropertyInfo currentGameViewSize = gameViewType.GetProperty("currentGameViewSize", pFlags);
            System.Object gameViewSize = currentGameViewSize.GetValue(mainGameView, null);
            if (gameViewSize != null)
            {
                System.Type gameViewSizeType = gameViewSize.GetType();
                PropertyInfo width = gameViewSizeType.GetProperty("width");
                PropertyInfo height = gameViewSizeType.GetProperty("height");
                if ((width != null) && (height != null))
                {
                    int x = (int)width.GetValue(gameViewSize, null);
                    int y = (int)height.GetValue(gameViewSize, null);
                    return new Point(x, y);
                }
            }
        }
        return Point.zero;
    }
}
/* original code.
#if UNITY_EDITOR
	static bool Editor__getGameViewSizeError = false;
	static public bool Editor__gameViewReflectionError = false;
	// Try and get game view size
	// Will return true if it is able to work this out
	// If width / height == 0, it means the user has selected an aspect ratio "Resolution"
	static public bool Editor__GetGameViewSize(out float width, out float height, out float aspect) {
		try {
			Editor__gameViewReflectionError = false;
			
			System.Type gameViewType = System.Type.GetType("UnityEditor.GameView,UnityEditor");
			System.Reflection.MethodInfo GetMainGameView = gameViewType.GetMethod("GetMainGameView", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
			object mainGameViewInst = GetMainGameView.Invoke(null, null);
			if (mainGameViewInst == null) {
				width = height = aspect = 0;
				return false;
			}
			System.Reflection.FieldInfo s_viewModeResolutions = gameViewType.GetField("s_viewModeResolutions", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
			if (s_viewModeResolutions == null) {
				System.Reflection.PropertyInfo currentGameViewSize = gameViewType.GetProperty("currentGameViewSize", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
				object gameViewSize = currentGameViewSize.GetValue(mainGameViewInst, null);
				System.Type gameViewSizeType = gameViewSize.GetType();
				int gvWidth = (int)gameViewSizeType.GetProperty("width").GetValue(gameViewSize, null);
				int gvHeight = (int)gameViewSizeType.GetProperty("height").GetValue(gameViewSize, null);
				int gvSizeType = (int)gameViewSizeType.GetProperty("sizeType").GetValue(gameViewSize, null);
				if (gvWidth == 0 || gvHeight == 0) {
					width = height = aspect = 0;
					return false;
				}
				else if (gvSizeType == 0) {
					width = height = 0;
					aspect = (float)gvWidth / (float)gvHeight;
					return true;
				}
				else {
					width = gvWidth; height = gvHeight;
					aspect = (float)gvWidth / (float)gvHeight;
					return true;
				}
			}
			else {
				Vector2[] viewModeResolutions = (Vector2[])s_viewModeResolutions.GetValue(null);
				float[] viewModeAspects = (float[])gameViewType.GetField("s_viewModeAspects", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).GetValue(null);
				string[] viewModeStrings = (string[])gameViewType.GetField("s_viewModeAspectStrings", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).GetValue(null);
				if (mainGameViewInst != null 
				    && viewModeStrings != null
				    && viewModeResolutions != null && viewModeAspects != null) {
					int aspectRatio = (int)gameViewType.GetField("m_AspectRatio", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).GetValue(mainGameViewInst);
					string thisViewModeString = viewModeStrings[aspectRatio];
					if (thisViewModeString.Contains("Standalone")) {
						width = UnityEditor.PlayerSettings.defaultScreenWidth; height = UnityEditor.PlayerSettings.defaultScreenHeight;
						aspect = width / height;
					}
					else if (thisViewModeString.Contains("Web")) {
						width = UnityEditor.PlayerSettings.defaultWebScreenWidth; height = UnityEditor.PlayerSettings.defaultWebScreenHeight;
						aspect = width / height;
					}
					else {
						width = viewModeResolutions[ aspectRatio ].x; height = viewModeResolutions[ aspectRatio ].y;
						aspect = viewModeAspects[ aspectRatio ];
						// this is an error state
						if (width == 0 && height == 0 && aspect == 0) {
							return false;
						}
					}
					return true;
				}
			}
		}
		catch (System.Exception e) {
			if (Editor__getGameViewSizeError == false) {
				Debug.LogError("tk2dCamera.GetGameViewSize - has a Unity update broken this?\nThis is not a fatal error, but a warning that you've probably not got the latest 2D Toolkit update.\n\n" + e.ToString());
				Editor__getGameViewSizeError = true;
			}
			Editor__gameViewReflectionError = true;
		}
		width = height = aspect = 0;
		return false;
	}
#endif
*/
