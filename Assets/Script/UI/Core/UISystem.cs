using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Asset = UnityEngine.Object;
using Scene = System.Enum;
using UnityEngine.UI;
//using NetworkPacket = CSCommon.BASE_DATA;

/// <summary>
/// 
/// </summary>
partial class UISystem : CSingleton<UISystem>
{
    private Canvas canvas;
    private GameObject uiRoot;

    private GUILeaf uiLoading;

    public bool Initialize()
    {
        if (uiRoot == null)
        {
            uiRoot = new GameObject();
            uiRoot.name = "Canvas";
            canvas = uiRoot.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler canvasScaler = uiRoot.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(960, 540);
            uiRoot.AddComponent<GraphicRaycaster>();
            uiRoot.layer = LayerMask.NameToLayer("UI");
        }

        return true;
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }

    public T LoadGUI<T>(string path) where T : GUILeaf
    {
        return LoadGUI<T>(path, uiRoot.transform);
    }

    public T LoadGUI<T>(string path, Transform parent) where T : GUILeaf
    {
        GameObject guiObject = AssetObject.CreateGameObject(path, parent, Vector3.one);
        if (guiObject != null)
        {
            T controller = MappingOnGUI<T>(guiObject);
            if (controller != null)
            {
                controller.transform.localPosition = Vector3.zero;
                return controller;
            }
        }
        return null;
    }

    T MappingOnGUI<T>(GameObject target) where T : GUILeaf
    {
        T scriptBuffer = AssetObject.InsertComponent<T>(target);

        GUILeaf guiScript = scriptBuffer as GUILeaf;

        if (guiScript == null)
        {
            //Console.Write(this, "GUICtrl is null. <unusual>");
            return null;
        }
        return scriptBuffer;
    }

    protected virtual void OnDestroy()
    {
        //UnregisterNetworkHandler();
    }

    public void SetLoading(bool isLoading, Vector3 pos)
    {
        if (isLoading)
        {
            if (uiLoading == null)
            {
                uiLoading = LoadGUI<GUILeaf>("Prefab/loading");
                uiLoading.transform.position = pos;
                uiLoading.transform.localScale = Vector3.one * 3;
            }
        }
        else
        {
            if (uiLoading != null)
            {
                uiLoading.Exit();
            }
        }
    }
}

/// <summary>
/// ???�환??ui?�태 변경작?? open기�??�로 ?�업?�다.
/// </summary>
partial class UISystem : SceneListener
{
    public void SceneOpen(Scene scene)
    {
    }
    public void SceneClose(Scene currentScene, Scene nextScene)
    {
        //    if (GUIAssetCount > 0)
        //    {
        //        for (int i = GUIAssetCount - 1; i >= 0; i--)
        //        {
        //            GUIController ctrl = GetGUIAsset<GUIController>(i);
        //            if (ctrl != null && ctrl.destroy)
        //            {
        //                ctrl.ProcessForChangeScene(currentScene);
        //            }
        //        }
        //        System.GC.Collect();
        //    }
    }
}