using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Asset = UnityEngine.Object;
using Scene = System.Enum;
/// <summary>
///  GUI 기본 관리
/// </summary>
public partial class GUIRoot<TGUI> : SingletonMono<TGUI> where TGUI : MonoObject
{
    public Camera UICamera
    {
        get;
        private set;
    }
    public Transform UIRoot
    {
        get;
        private set;
    }
    public Transform TemporaryRoot
    {
        get;
        private set;
    }
    public int GUIAssetCount
    {
        get
        {
            return GUIAssets.Count;
        }
    }
    public GUILeaf TopmostGUI
    {
        get
        {
            GUILeaf topmost = null;
            int index = -1;
            for (int i = 0; i < ActiveGUIAssets.Count; i++)
            {
                if (ActiveGUIAssets[i].Active)
                {
                    if (ActiveGUIAssets[i].Index > index)
                    {
                        index = ActiveGUIAssets[i].Index;
                        topmost = ActiveGUIAssets[i];
                    }
                }
            }
            return topmost;
        }
    }
    public GUILeaf FindTopmostGUI<TGUILeaf>() where TGUILeaf : GUILeaf
    {
        GUILeaf topmost = null;
        int index = int.MinValue;
        for (int i = 0; i < ActiveGUIAssets.Count; i++)
        {
            if (ActiveGUIAssets[i].Active)
            {
                if (ActiveGUIAssets[i] is TGUILeaf)
                {
                    if (ActiveGUIAssets[i].Index > index)
                    {
                        index = ActiveGUIAssets[i].Index;
                        topmost = ActiveGUIAssets[i];
                    }
                }
            }
        }
        return topmost;
    }
    public virtual bool Initialize(string path)
    {
        if (objUIRoot == null)
        {
            objUIRoot = AssetObject.CreateGameObject(path, this.transform);
            if (objUIRoot != null)
            {
                UIRoot = objUIRoot.transform;
                UIRoot.localPosition = Vector3.zero;
                UICamera = AssetObject.FindComponent<Camera>(objUIRoot, "Camera");
                TemporaryRoot = AssetObject.FindComponent<Transform>(objUIRoot, "TemporaryUIRoot");
                //if (TemporaryRoot != null)
                //{
                //    AssetObject.InsertComponent<UIPanel>(TemporaryRoot.gameObject);
                //}
                return true;
            }
        }
        //Console.Write(this, "already created.");
        return false;
    }
    /// <summary>
    /// 해당 경로에 있는 GUI를 가져옵니다. (create, load, find기능이 포함되어있습니다.)
    /// </summary>
    /// <typeparam name="T">가져올 GUI에 매핑해줄 스크립트입니다. controllers를 상속받아야합니다. </typeparam>
    /// <param name="path">가져올 경로</param>
    /// <param name="parent">생성후 종속될 부모 오브젝트</param>
    public T CreateGUI<T>() where T : GUILeaf
    {
        return CreateGUI<T>("EmptyGUI");
    }
    public T CreateGUI<T>(string name) where T : GUILeaf
    {
        //UIPanel guiPanel = AssetObject.NewComponent<UIPanel>(name, UIRoot);
        //if (guiPanel != null)
        //{
        //    GameObject guiObject = guiPanel.gameObject;
        //    if (guiObject != null)
        //    {
        //        T controller = MappingOnGUI<T>(guiObject);
        //        if (controller != null)
        //        {
        //            controller.transform.localPosition = Vector3.zero;
        //            controller.Index = GUIAssets.Count - 1;
        //            controller.Initialize();
        //            SetPanelDepth(controller);
        //            return controller;
        //        }
        //    }
        //}
        return null;
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
                controller.Index = GUIAssets.Count - 1;
                //controller.Initialize();
                //SetPanelDepth(controller);
                return controller;
            }
        }
        return null;
    }

    public T LoadGUI<T>(string path, Transform parent, int depth) where T : GUILeaf
    {
        GameObject guiObject = AssetObject.CreateGameObject(path, parent, Vector3.one);
        if (guiObject != null)
        {
            T controller = MappingOnGUI<T>(guiObject);
            if (controller != null)
            {
                controller.transform.localPosition = Vector3.zero;
                controller.Index = GUIAssets.Count - 1;
                //controller.Initialize();
                SetPanelDepth(controller, depth);
                return controller;
            }
        }
        return null;
    }

    public T LoadGUI<T>(string path) where T : GUILeaf
    {
        return LoadGUI<T>(path, UIRoot);
    }
    public T InsertGUI<T>(GameObject guiObject) where T : GUILeaf
    {
        if (guiObject != null)
        {
            T controller = MappingOnGUI<T>(guiObject);
            if (controller != null)
            {
                controller.Index = GUIAssets.Count - 1;
                //controller.Initialize();
                SetPanelDepth(controller);
                return controller;
            }
        }
        return null;
    }
    public T GetGUI<T>() where T : GUILeaf
    {
        for (int i = 0; i < GUIAssets.Count; i++)
        {
            if (GUIAssets[i] is T)
            {
                return GUIAssets[i] as T;
            }
        }
        return null;
    }
    public void ReturnMemory(GUILeaf asset)
    {
        if (asset != null)
        {
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }
    }
    /// <summary>
    /// GUILeaf를 가지는 object 삭제
    /// </summary>
    public bool RemoveGUI(GUILeaf asset)
    {
        if (CheckForIncludeGUIAssets(asset) && RemoveInGUIAssets(asset))
        {
            SortPanelGUIAsset();
            AssetObject.Destroy(asset);
            asset = null;
            return true;
        }
        return false;
    }
    public bool DestroyGUI(GUILeaf asset)
    {
        if (CheckForIncludeGUIAssets(asset) && RemoveInGUIAssets(asset))
        {
            SortPanelGUIAsset();
            AssetObject.Destroy(asset.gameObject);
            AssetObject.Destroy(asset);
            asset = null;
            return true;
        }
        return false;
    }
    public bool DestroyImmediateGUI(GUILeaf asset, bool allowDestroyingAssets = false)
    {
        if (CheckForIncludeGUIAssets(asset) && RemoveInGUIAssets(asset))
        {
            SortPanelGUIAsset();
            AssetObject.DestroyImmediate(asset.gameObject, allowDestroyingAssets);
            asset = null;
            return true;
        }
        return false;
    }
    /// <summary>
    /// 지정GUI를 최상단 GUI로 변경해주는 함수이다.
    /// </summary>
    /// <param name="target">변경 대상</param>
    /// <returns>성공 true, 실패 false</returns>
    public virtual bool ActiveGUI(GUILeaf target)
    {
        if (!(ActiveGUIAssets.Contains(target)))
        {
            ActiveGUIAssets.Add(target);
        }
        if (CheckForIncludeGUIAssets(target) && MoveForwardInGUIAssets(target))
        {
            SortPanelGUIAsset();
            return true;
        }
        return false;
    }
    public bool InactiveGUI(GUILeaf target)
    {
        if (ActiveGUIAssets.Contains(target))
        {
            ActiveGUIAssets.Remove(target);
        }
        if (CheckForIncludeGUIAssets(target) && MoveBackwardInGUIAssets(target))
        {
            SortPanelGUIAsset();
            return true;
        }
        return false;
    }
}
/// <summary>
/// 
/// </summary>
partial class GUIRoot<TGUI>
{
    protected List<GUILeaf> ActiveGUIAssets = new List<GUILeaf>();

    protected T GetGUIAsset<T>(int index) where T : GUILeaf
    {
        if ((index >= 0) && (index < GUIAssetCount))
        {
            return GUIAssets[index] as T;
        }
        return null;
    }
}
/// <summary>
/// GUI 매핑관련
/// </summary>
partial class GUIRoot<TGUI>
{
    int ipanelDepthWeigth = 10;
    GameObject objUIRoot = null;
    List<GUILeaf> GUIAssets = new List<GUILeaf>();

    //GameObject Mapping on GUI
    T MappingOnGUI<T>(GameObject target) where T : GUILeaf
    {
        T scriptBuffer = AssetObject.InsertComponent<T>(target);

        GUILeaf guiScript = scriptBuffer as GUILeaf;

        if (guiScript == null)
        {
            //Console.Write(this, "GUICtrl is null. <unusual>");
            return null;
        }
        AddedInGUIAssets(guiScript);
        return scriptBuffer;
    }
    bool AddedInGUIAssets(GUILeaf target)
    {
        GUIAssets.Add(target);
        return true;
    }
    bool RemoveInGUIAssets(GUILeaf target)
    {
        //삭제하려는 인덱스 검출
        int iRemoveIndex = 0;
        for (int i = 0; i < GUIAssets.Count; i++)
        {
            if (GUIAssets[i] == target)
            {
                iRemoveIndex = i;
                break;
            }
        }
        //삭제 하려는 인덱스 보다 큰 인덱스들 <<
        for (int i = 0; i < GUIAssets.Count; i++)
        {
            if (GUIAssets[i].Index > GUIAssets[iRemoveIndex].Index)
            {
                GUIAssets[i].Index--;
            }
        }
        //삭제하려는 인덱스 제거
        if (ActiveGUIAssets.Contains(GUIAssets[iRemoveIndex]))
        {
            ActiveGUIAssets.Remove(GUIAssets[iRemoveIndex]);
        }
        GUIAssets[iRemoveIndex] = null;
        GUIAssets.RemoveAt(iRemoveIndex);
        return true;
    }
    public bool MoveForwardInGUIAssets(GUILeaf target)
    {
        int iMoveIndex = 0;
        int iFrontIndex = 0;
        //최상단으로 올리려는 에섯검출 및 최상단 에셋검출
        for (int i = 0; i < GUIAssets.Count; i++)
        {
            if (GUIAssets[i] == target)
            {
                iMoveIndex = GUIAssets[i].Index;
            }
            if (GUIAssets[i].Index > iFrontIndex)
            {
                iFrontIndex = GUIAssets[i].Index;
            }
        }
        //최상단으로 옮기려는 에셋에 최상단 에셋의 인덱스 할당 및 나머지 들 인덱스 <<
        for (int i = 0; i < GUIAssets.Count; i++)
        {
            if (GUIAssets[i].Index == iMoveIndex)
            {
                GUIAssets[i].Index = iFrontIndex;
            }
            else if (GUIAssets[i].Index > iMoveIndex)
            {
                GUIAssets[i].Index--;
            }
        }
        return true;
    }
    public bool MoveBackwardInGUIAssets(GUILeaf target)
    {
        int iMoveIndex = 0;
        int iBackIndex = 0;
        //최상단으로 올리려는 에섯검출 및 최상단 에셋검출
        for (int i = 0; i < GUIAssets.Count; i++)
        {
            if (GUIAssets[i] == target)
            {
                iMoveIndex = GUIAssets[i].Index;
            }
            if (GUIAssets[i].Index < iBackIndex)
            {
                iBackIndex = GUIAssets[i].Index;
            }
        }
        //최상단으로 옮기려는 에셋에 최상단 에셋의 인덱스 할당 및 나머지 들 인덱스 <<
        for (int i = 0; i < GUIAssets.Count; i++)
        {
            if (GUIAssets[i].Index == iMoveIndex)
            {
                GUIAssets[i].Index = iBackIndex;
            }
            else if (GUIAssets[i].Index < iMoveIndex)
            {
                GUIAssets[i].Index++;
            }
        }
        return true;
    }
    void SetPanelDepth(GUILeaf target, int depth)
    {
        //target.SetPanelDepth(depth);

        MoveForwardInGUIAssets(target);
    }

    void SetPanelDepth(GUILeaf target)
    {
        //int Length = target.GetPanelCount;
        //if (Length >= ipanelDepthWeigth)
        //{
        //    int iTemp = ((Length / 10) + 1) * 10;
        //    ipanelDepthWeigth = iTemp;
        //}
        //int iWeight = ipanelDepthWeigth * target.Index;
        //target.SetPanelDepth(iWeight);
    }
    void SortPanelGUIAsset()
    {
        //for (int i = 0; i < GUIAssets.Count; i++)
        //{
        //    int iWeight = ipanelDepthWeigth * GUIAssets[i].Index;
        //    GUIAssets[i].SetPanelDepth(iWeight);
        //}
    }
    /// <summary>
    /// 지정된 GUI가 관리되는 GUI인지 판별한다. 
    /// </summary>
    /// <param name="target">판별 대상</param>
    /// <returns>관리될 경우 true, 아닐 경우 false</returns>
    bool CheckForIncludeGUIAssets(GUILeaf target)
    {
        if (target == null)
        {
            //Console.Write(this, "havn't guictrl. this is not guiAsset <unusual>");
            return false;
        }
        if (GUIAssets.Contains(target) == false)
        {
            //Console.Write(this, string.Format("not found asset in GUIAssetList : {0}", target.name));
            return false;
        }
        return true;
    }
}
