//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

public partial class GUILeaf : MonoObject//, INetworkScene
{
    public bool Active
    {
        set
        {
            this.gameObject.SetActive(value);
        }
        get
        {
            if (this == null)
                return false;

            if (this.gameObject == null)
                return false;

            return this.gameObject.activeInHierarchy;
        }
    }
    public int Index
    {
        get;
        set;
    }
    //    public int GetPanelCount
    //    {
    //        get
    //        {
    //            return childPanels.Count;
    //        }
    //    }
    //    public int GetPanelDepth
    //    {
    //        get;
    //        private set;
    //    }
    public virtual void Open()
    {
        this.Active = true;
    }
    public virtual void Close()
    {
        this.Active = false;
    }

    public virtual void Exit()
    {
        Destroy(gameObject);
    }

    public virtual void Parse(string eventName, string data)
    {
        
    }
    //public virtual void Initialize()
    //{
    //    //UIPanel[] panels = AssetObject.FindComponentsAll<UIPanel>(this.gameObject).ToArray();
    //    //for (int i = 0; i < panels.Length; i++)
    //    //{
    //    //    PanelInfo buffer = new PanelInfo();
    //    //    buffer.panel = panels[i];
    //    //    buffer.baseDepth = panels[i].depth;
    //    //    childPanels.Add(buffer);
    //    //}
    //}

    //    public virtual void Refresh()
    //    {

    //    }

    //    //public void RegistPanel(UIPanel panel)
    //    //{
    //    //    for (int i = 0; i < childPanels.Count; i++)
    //    //    {
    //    //        if (childPanels[i].panel == panel)
    //    //            return;
    //    //    }

    //    //    PanelInfo buffer = new PanelInfo();
    //    //    buffer.panel = panel;
    //    //    buffer.baseDepth = GetPanelCount + 5;
    //    //    panel.depth = GetPanelCount + 5;
    //    //    childPanels.Add(buffer);
    //    //}
    //    public void SetPanelDepth(int depth)
    //    {
    //        for (int i = 0; i < childPanels.Count; i++)
    //        {
    //            GetPanelDepth = childPanels[i].baseDepth + depth;
    //            childPanels[i].panel.depth = GetPanelDepth;
    //        }
    //    }

    //    //public List<UIPanel> GetChildPanels()
    //    //{
    //    //    List<UIPanel> panelList = new List<UIPanel>();
    //    //    for(int i = 0; i < childPanels.Count; i++)
    //    //    {
    //    //        panelList.Add(childPanels[i].panel);
    //    //    }

    //    //    return panelList;
    //    //}
}
///// <summary>
///// 
///// </summary>
partial class GUILeaf
{
    //    protected struct PanelInfo
    //    {
    //        //public UIPanel panel;
    //        public int baseDepth;
    //    }
    //    protected List<PanelInfo> childPanels = new List<PanelInfo>();

    //protected virtual void Awake()
    //{
    //    SocketIOManager.Instance.AddReceiver(this);   
    //}
    //protected virtual void OnDestroy()
    //{
    //    SocketIOManager.Instance.RemoveReceiver(this);
    //}
}
