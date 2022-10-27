using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Asset = UnityEngine.Object;
/// <summary>
/// 
/// </summary>
public interface AssetManager
{
    void DontDestroyOnLoadAsset(Asset asset);
    void DestroyAsset(Asset asset);
    void DestroyAsset(Asset asset, float time);
    void DestroyAssetImmediate(Asset asset);
    void DestroyAssetImmediate(Asset asset, bool allowDestroyingAssets);
    GameObject NewGameObject();
    GameObject NewGameObject(string name);
    GameObject FindGameObject(string name);
    Asset InstantiateAsset(Asset asset);
    Asset InstantiateAsset(Asset asset, Vector3 position, Quaternion rotation, Transform parent);
}
/// <summary>
/// 
/// </summary>
static public partial class AssetObject
{
    static public void SetAssetManager(AssetManager assetManager)
    {
        AssetObject.assetManager = assetManager;
    }
}
/// <summary>
/// for asset.
/// </summary>
partial class AssetObject
{
    static public Asset CloneAsset(Asset asset)
    {
        Asset clone = InstantiateAsset(asset);
        if (clone != null)
        {
            clone.name = asset.name;
            return clone;
        }
        return null;
    }
    static public Asset CloneAsset(Asset asset, Vector3 position, Quaternion rotation, Transform parent)
    {
        Asset clone = InstantiateAsset(asset, position, rotation, parent);
        if (clone != null)
        {
            clone.name = asset.name;
            return clone;
        }
        return null;
    }
    static public void DontDestroyOnLoad(Asset asset)
    {
        if (assetManager != null)
        {
            assetManager.DontDestroyOnLoadAsset(asset);
        }
        else
        {
            GameObject.DontDestroyOnLoad(asset);
        }
    }
    static public void Destroy(Asset asset)
    {
        if (assetManager != null)
        {
            assetManager.DestroyAsset(asset);
        }
        else
        {
            GameObject.Destroy(asset);
        }
    }
    static public void Destroy(Asset asset, float time)
    {
        if (assetManager != null)
        {
            assetManager.DestroyAsset(asset, time);
        }
        else
        {
            GameObject.Destroy(asset, time);
        }
    }
    static public void DestroyImmediate(Asset asset)
    {
        if (assetManager != null)
        {
            assetManager.DestroyAssetImmediate(asset);
        }
        else
        {
            GameObject.DestroyImmediate(asset);
        }
    }
    static public void DestroyImmediate(Asset asset, bool allowDestroyingAssets)
    {
        if (assetManager != null)
        {
            assetManager.DestroyAssetImmediate(asset, allowDestroyingAssets);
        }
        else
        {
            GameObject.DestroyImmediate(asset, allowDestroyingAssets);
        }
    }
}
/// <summary>
/// for gameobject.
/// </summary>
partial class AssetObject
{
    /// <summary>
    /// 1. new empty GameObject.
    /// </summary>
    static public GameObject NewGameObject()
    {
        return GenerateGameObject();
    }
    static public GameObject NewGameObject(string name)
    {
        return NewGameObject(name, null);
    }
    static public GameObject NewGameObject(string name, Vector3 size)
    {
        return NewGameObject(name, null, size);
    }
    static public GameObject NewGameObject(string name, Vector3 position, Quaternion rotation)
    {
        return NewGameObject(name, null, position, rotation);
    }
    static public GameObject NewGameObject(string name, Vector3 position, Quaternion rotation, Vector3 size)
    {
        return NewGameObject(name, null, position, rotation, size);
    }
    static public GameObject NewGameObject(Transform parent)
    {
        GameObject gameObject = GenerateGameObject();
        if (gameObject != null)
        {
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            return gameObject;
        }
        return null;
    }
    static public GameObject NewGameObject(string name, Transform parent)
    {
        GameObject gameObject = GenerateGameObject(name) as GameObject;
        if (gameObject != null)
        {
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            return gameObject;
        }
        return null;
    }
    static public GameObject NewGameObject(string name, Transform parent, Vector3 size)
    {
        GameObject gameObject = GenerateGameObject(name) as GameObject;
        if (gameObject != null)
        {
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            gameObject.transform.localScale = size;
            return gameObject;
        }
        return null;
    }
    static public GameObject NewGameObject(string name, Transform parent, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = GenerateGameObject(name) as GameObject;
        if (gameObject != null)
        {
            gameObject.transform.localPosition = position;
            gameObject.transform.localRotation = rotation;
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            return gameObject;
        }
        return null;
    }
    static public GameObject NewGameObject(string name, Transform parent, Vector3 position, Quaternion rotation, Vector3 size)
    {
        GameObject gameObject = GenerateGameObject(name) as GameObject;
        if (gameObject != null)
        {
            gameObject.transform.localPosition = position;
            gameObject.transform.localRotation = rotation;
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            gameObject.transform.localScale = size;
            return gameObject;
        }
        return null;
    }
    /// <summary>
    /// 2. Instantiate GameObject.
    /// </summary>
    static public GameObject CreateGameObject(string path)
    {
        return CreateGameObject(path, null);
    }
    static public GameObject CreateGameObject(string path, Vector3 size)
    {
        return CreateGameObject(path, null, size);
    }
    static public GameObject CreateGameObject(string path, Vector3 position, Quaternion quaternion)
    {
        return CreateGameObject(path, null, position, quaternion);
    }
    static public GameObject CreateGameObject(string path, Vector3 position, Quaternion quaternion, Vector3 size)
    {
        return CreateGameObject(path, null, position, quaternion, size);
    }
    static public GameObject CreateGameObject(string path, Transform parent)
    {
        Asset asset = AssetFiles.LoadAsset<GameObject>(path);
        if (asset != null)
        {
            return CreateGameObject(asset, parent);
        }
        return null;
    }
    static public GameObject CreateGameObject(string path, Transform parent, Vector3 size)
    {
        Asset asset = AssetFiles.LoadAsset<GameObject>(path);
        if (asset != null)
        {
            return CreateGameObject(asset, parent, size);
        }
        return null;
    }
    static public GameObject CreateGameObject(string path, Transform parent, Vector3 position, Quaternion rotation)
    {
        Asset asset = AssetFiles.LoadAsset<GameObject>(path);
        if (asset != null)
        {
            return CreateGameObject(asset, parent, position, rotation);
        }
        return null;
    }
    static public GameObject CreateGameObject(string path, Transform parent, Vector3 position, Quaternion rotation, Vector3 size)
    {
        Asset asset = AssetFiles.LoadAsset<GameObject>(path);
        if (asset != null)
        {
            return CreateGameObject(asset, parent, position, rotation, size);
        }
        return null;
    }
    /// <summary>
    /// 3. Instantiate GameObject at asset.
    /// </summary>
    static public GameObject CreateGameObject(Asset asset)
    {
        return CreateGameObject(asset, null);
    }
    static public GameObject CreateGameObject(Asset asset, Vector3 size)
    {
        return CreateGameObject(asset, null, size);
    }
    static public GameObject CreateGameObject(Asset asset, Vector3 position, Quaternion rotation)
    {
        return CreateGameObject(asset, null, position, rotation);
    }
    static public GameObject CreateGameObject(Asset asset, Vector3 position, Quaternion rotation, Vector3 size)
    {
        return CreateGameObject(asset, null, position, rotation, size);
    }
    static public GameObject CreateGameObject(Asset asset, Transform parent)
    {
        GameObject gameObject = InstantiateAsset(asset) as GameObject;
        if (gameObject != null)
        {
            gameObject.name = asset.name;
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            return gameObject;
        }
        return null;
    }
    static public GameObject CreateGameObject(Asset asset, Transform parent, Vector3 size)
    {
        GameObject gameObject = InstantiateAsset(asset) as GameObject;
        if (gameObject != null)
        {
            gameObject.name = asset.name;
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            gameObject.transform.localScale = size;
            return gameObject;
        }
        return null;
    }
    static public GameObject CreateGameObject(Asset asset, Transform parent, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = InstantiateAsset(asset, position, rotation, parent) as GameObject;
        if (gameObject != null)
        {
            gameObject.name = asset.name;
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            {
                gameObject.transform.localPosition = position;
                gameObject.transform.localRotation = rotation;
            }
            return gameObject;
        }
        return null;
    }
    static public GameObject CreateGameObject(Asset asset, Transform parent, Vector3 position, Quaternion rotation, Vector3 size)
    {
        GameObject gameObject = InstantiateAsset(asset, position, rotation, parent) as GameObject;
        if (gameObject != null)
        {
            gameObject.name = asset.name;
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            {
                gameObject.transform.localPosition = position;
                gameObject.transform.rotation = rotation;
                gameObject.transform.localScale = size;
            }
            return gameObject;
        }
        return null;
    }
    /// <summary>
    /// 4. GameObject support.
    /// </summary>
    static public GameObject FindGameObject(string name)
    {
        if (assetManager != null)
        {
            return assetManager.FindGameObject(name);
        }
        return GameObject.Find(name);
    }
    static public GameObject FindGameObject(GameObject gameObject, string name)
    {
        //if (gameObject == null) return null;

        //if (gameObject.name.Equals(name))
        //{
        //    return gameObject;
        //}
        //for (int i = 0; i < gameObject.transform.childCount; i++)
        //{
        //    GameObject childObject = FindGameObject(gameObject.transform.GetChild(i).gameObject, name);
        //    if (childObject != null)
        //    {
        //        return childObject;
        //    }
        //}

        //Debug.Log(string.Format("{0} {1}", gameObject.name, name));
        //return null;
        GameObject peekObject = FindGameObject2(gameObject, name);
        if (peekObject == null)
        {
            Debug.LogError(string.Format("{0} {1}", gameObject.name, name));
        }
        return peekObject;
    }

    static public GameObject FindGameObject2(GameObject gameObject, string name)
    {
        if (gameObject == null) return null;

        if (gameObject.name.Equals(name))
        {
            return gameObject;
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject childObject = FindGameObject2(gameObject.transform.GetChild(i).gameObject, name);
            if (childObject != null)
            {
                return childObject;
            }
        }

        //Debug.Log(string.Format("{0} {1}", gameObject.name, name));
        return null;
    }

    static public GameObject FindGameObject(GameObject gameObject, string parent, string child)
    {
        GameObject parentObject = AssetObject.FindGameObject(gameObject, parent);
        if (parentObject != null)
        {
            GameObject childObject = AssetObject.FindGameObject(parentObject, child);
            if (childObject != null)
            {
                return childObject;
            }
        }
        return null;
    }
    static public GameObject ContainsGameObject(GameObject gameObject, string name)
    {
        if (gameObject.name.Contains(name))
        {
            return gameObject;
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject childObject = ContainsGameObject(gameObject.transform.GetChild(i).gameObject, name);
            if (childObject != null)
            {
                return childObject;
            }
        }
        return null;
    }
    static public GameObject ContainsGameObject(GameObject gameObject, string parent, string child)
    {
        GameObject parentObject = AssetObject.ContainsGameObject(gameObject, parent);
        if (parentObject != null)
        {
            GameObject childObject = AssetObject.ContainsGameObject(parentObject, child);
            if (childObject != null)
            {
                return childObject;
            }
        }
        return null;
    }
    static public bool ContainsGameObject(GameObject gameObject, GameObject targetObject)
    {
        if (gameObject.Equals(targetObject))
        {
            return true;
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (ContainsGameObject(gameObject.transform.GetChild(i).gameObject, targetObject))
            {
                return true;
            }
        }
        return false;
    }
    static public GameObject[] FindGameObjectArray(GameObject gameObject, string name)
    {
        List<GameObject> list = new List<GameObject>();
        FindGameObjectArray(ref list, gameObject, name);
        return list.ToArray();
    }
    static void FindGameObjectArray(ref List<GameObject> list, GameObject gameObject, string name)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            FindGameObjectArray(ref list, gameObject.transform.GetChild(i).gameObject, name);
        }
        if (gameObject.name.Equals(name))
        {
            list.Add(gameObject);
        }
    }

    static public GameObject[] FindGameObjectArrayIncludeName(GameObject gameObject, string name)
    {
        List<GameObject> list = new List<GameObject>();
        FindGameObjectArrayIncludeName(ref list, gameObject, name);
        return list.ToArray();
    }
    static void FindGameObjectArrayIncludeName(ref List<GameObject> list, GameObject gameObject, string name)
    {
        for(int i=0; i<gameObject.transform.childCount; i++)
        {
            FindGameObjectArrayIncludeName(ref list, gameObject.transform.GetChild(i).gameObject, name);
        }
        if(gameObject.name.Contains(name))
        {
            list.Add(gameObject);
        }
    }

    static public GameObject FindRootGameObject(GameObject gameObject)
    {
        if (gameObject.transform.parent != null)
        {
            return FindRootGameObject(gameObject.transform.parent.gameObject);
        }
        return gameObject;
    }
}
/// <summary>
/// for component.
/// </summary>
partial class AssetObject
{
    /// <summary>
    /// [1] NewGameObject() and LoadComponent().
    /// </summary>
    static string GetName<TComponent>() where TComponent : Component
    {
        return typeof(TComponent).ToString();
    }
    static public TComponent NewComponent<TComponent>() where TComponent : Component
    {
        return NewComponent<TComponent>(GetName<TComponent>(), null);
    }
    static public TComponent NewComponent<TComponent>(string name) where TComponent : Component
    {
        return NewComponent<TComponent>(name, null);
    }
    static public TComponent NewComponent<TComponent>(Transform parent) where TComponent : Component
    {
        return NewComponent<TComponent>(GetName<TComponent>(), parent);
    }
    static public TComponent NewComponent<TComponent>(string name, Transform parent) where TComponent : Component
    {
        GameObject gameObject = GenerateGameObject(name);
        if (gameObject != null)
        {
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            return LoadComponent<TComponent>(gameObject);
        }
        return null;
    }
    /// <summary>
    /// [2] Instantiate GameObject at path and LoadComponent().
    /// </summary>
    static public TComponent CreateComponent<TComponent>(string path) where TComponent : Component
    {
        return CreateComponent<TComponent>(path, null);
    }
    static public TComponent CreateComponent<TComponent>(string path, Transform parent) where TComponent : Component
    {
        GameObject gameObject = CreateGameObject(path, parent);
        if (gameObject != null)
        {
            if (parent != null)
            {
                gameObject.transform.SetParent(parent, isWorldPositionStays);
            }
            return LoadComponent<TComponent>(gameObject);
        }
        return null;
    }
    /// <summary>
    /// [3] find or insert Component.
    /// </summary>
    static public TComponent LoadComponent<TComponent>(GameObject target) where TComponent : Component
    {
        TComponent component = FindComponent<TComponent>(target);
        if (component == null)
        {
            component = InsertComponent<TComponent>(target);
        }
        return component;
    }
    static public Component LoadComponent(GameObject target, string className)
    {
        Component component = FindComponent(target, className);
        if (component == null)
        {
            component = InsertComponent(target, className);
        }
        return component;
    }
    static public Component LoadComponent(GameObject target, System.Type type)
    {
        Component component = FindComponent(target, type);
        if (component == null)
        {
            component = InsertComponent(target, type);
        }
        return component;
    }
    /// <summary>
    /// [4] insert Component.
    /// </summary>
    static public TComponent InsertComponent<TComponent>(GameObject target) where TComponent : Component
    {
        //Debug.Log("target :: " + target);
        TComponent component = target.AddComponent<TComponent>();
        return component;
    }
    static public Component InsertComponent(GameObject target, string className)
    {
        System.Type type = System.Type.GetType(className);
        if (type != null)
        {
            return InsertComponent(target, type);
        }
        return null;
    }
    static public Component InsertComponent(GameObject target, System.Type type)
    {
        Component component = target.AddComponent(type);
        return component;
    }
    /// <summary>
    /// [5] remove Component.
    /// </summary>
    static public bool RemoveComponent<TComponent>() where TComponent : Component
    {
        TComponent component = FindComponent<TComponent>();
        if (component != null)
        {
            GameObject.Destroy(component);
            return true;
        }
        return false;
    }
    static public bool RemoveComponent<TComponent>(GameObject target) where TComponent : Component
    {
        TComponent component = target.GetComponent<TComponent>();
        if (component != null)
        {
            GameObject.DestroyImmediate(component);
            return true;
        }
        return false;
    }
    static public bool RemoveComponent(GameObject target, string className)
    {
        Component component = target.GetComponent(className);
        if (component != null)
        {
            GameObject.Destroy(component);
            return true;
        }
        return false;
    }
    static public bool RemoveComponent(GameObject target, System.Type type)
    {
        Component component = target.GetComponent(type);
        if (component != null)
        {
            GameObject.Destroy(component);
            return true;
        }
        return false;
    }
    /// <summary>
    /// [6-1] Component support.
    /// </summary>
    static public Component FindComponent(GameObject target, string className)
    {
        Component component = target.GetComponent(className);
        return component;
    }
    static public Component FindComponent(GameObject target, System.Type type)
    {
        Component component = target.GetComponent(type);
        return component;
    }
    static public TComponent FindComponent<TComponent>() where TComponent : Component
    {
        return FindComponent<TComponent>(GetName<TComponent>());
    }
    static public TComponent FindComponent<TComponent>(bool inactive) where TComponent : Component
    {
        return FindComponent<TComponent>(GetName<TComponent>(), inactive);
    }
    static public TComponent FindComponent<TComponent>(string name) where TComponent : Component
    {
        return FindComponent<TComponent>(name, true);
    }
    static public TComponent FindComponent<TComponent>(string name, bool inactive) where TComponent : Component
    {
        TComponent component = null;
        {
            GameObject gameObject = FindGameObject(name);
            if (gameObject != null)
            {
                component = gameObject.GetComponent<TComponent>();
                if (component != null)
                {
                    if (!component.gameObject.name.Equals(name))
                    {
                        component = null;
                    }
                }
                if (component == null)
                {
                    component = FindComponent<TComponent>(gameObject, name, inactive);
                }
            }
        }
        return component;
    }
    static public TComponent FindComponent<TComponent>(GameObject target) where TComponent : Component
    {
        return FindComponent<TComponent>(target, true);
    }
    static public TComponent FindComponent<TComponent>(GameObject target, bool inactive) where TComponent : Component
    {
        TComponent component = null;
        {
            if (target != null)
            {
                component = target.GetComponent<TComponent>();
                if (component == null)
                {
                    component = FindComponent<TComponent>(target, null, inactive);
                }
            }
        }
        return component;
    }
    static public TComponent FindComponent<TComponent>(GameObject target, string name) where TComponent : Component
    {
        // 최진 2017-10-21 pivot, overflowMethod 강제변경 막음
        //TComponent obj = FindComponent<TComponent>(target, name, true);

        //if (obj != null && (typeof(TComponent) == typeof(UILabel)))
        //{
        //    UILabel label = obj as UILabel;

        //    if (label != null && label.text.Length > 0)
        //    {
        //        label.pivot = UIWidget.Pivot.TopLeft;
        //        label.overflowMethod = UILabel.Overflow.ResizeFreely;
        //        //label.spacingY = ConfigData.LABEL_SPACING_Y;
        //    }
        //}

        return FindComponent<TComponent>(target, name, true);
    }

    static public TComponent FindComponent<TComponent>(GameObject target, string name, bool inactive) where TComponent : Component
    {
        TComponent result = null;
        {
            if (target != null)
            {
                // bug inactive value
                TComponent[] componentArray = target.GetComponentsInChildren<TComponent>(inactive);
                if (string.IsNullOrEmpty(name))
                {
                    if (componentArray.Length > 0)
                    {
                        result = componentArray[0];
                    }
                }
                else
                {
                    for (int i = 0; i < componentArray.Length;i++ )
                    {
                        if (componentArray[i].gameObject.name.Equals(name))
                        {
                            result = componentArray[i];
                            break;
                        }
                    }
                }
            }
        }
        return result;
    }
    static public TComponent FindComponent<TComponent>(GameObject target, string parent, params string[] children) where TComponent : Component
    {
        return FindComponent<TComponent>(target, parent, true, children);
    }
    static public TComponent FindComponent<TComponent>(GameObject target, string parent, bool inactive, params string[] children) where TComponent : Component
    {
        GameObject nextObject = AssetObject.FindGameObject(target, parent);
        if (nextObject != null)
        {
            for (int i = 0; i < children.Length;i++ )
            {
                nextObject = AssetObject.FindGameObject(nextObject, children[i]);
                if (nextObject == null)
                {
                    return null;
                }
            }
            return FindComponent<TComponent>(nextObject, inactive);
        }
        return null;
    }
    static public TComponent[] FindComponents<TComponent>(GameObject target) where TComponent : Component
    {
        TComponent[] component = null;
        {
            if (target != null)
            {
                component = target.GetComponents<TComponent>();
                if (component == null)
                {
                    component = FindComponents<TComponent>(target, null);
                }
                else if (component.Length == 0)
                {
                    component = FindComponents<TComponent>(target, null);
                }
            }
        }
        return component;
    }
    static public TComponent[] FindComponents<TComponent>(GameObject target, string name) where TComponent : Component
    {
        TComponent[] result = null;
        {
            if (target != null)
            {
                TComponent[] componentArray = target.GetComponentsInChildren<TComponent>(true);
                if (string.IsNullOrEmpty(name))
                {
                    result = componentArray;
                }
                else
                {
                    List<TComponent> list = new List<TComponent>();
                    for (int i = 0; i < componentArray.Length;i++ )
                    {
                        if (componentArray[i].gameObject.name.Equals(name))
                        {
                            list.Add(componentArray[i]);
                        }
                    }
                    if (list.Count > 0)
                    {
                        result = list.ToArray();
                    }
                }
            }
        }
        return result;
    }
    static public List<TComponent> FindComponentsAll<TComponent>(GameObject target) where TComponent : Component
    {
        List<TComponent> componentList = new List<TComponent>();
        {
            if (target != null)
            {
                {
                    TComponent[] componentArray = target.GetComponents<TComponent>();
                    for (int i = 0; i < componentArray.Length; i++)
                    {
                        componentList.Add(componentArray[i]);
                    }
                }
                {
                    TComponent[] componentArray = FindComponents<TComponent>(target, null);
                    for (int i = 0; i < componentArray.Length; i++)
                    {
                        componentList.Add(componentArray[i]);
                    }
                }
            }
        }
        return componentList;
    }
    /// <summary>
    /// [6-2] Component support.
    /// </summary>
    static public TComponent FindParentComponent<TComponent>(GameObject gameObject) where TComponent : Component
    {
        return FindParentComponent<TComponent>(gameObject.transform);
    }
    static public TComponent FindParentComponent<TComponent>(Transform transform) where TComponent : Component
    {
        if ((transform != null) && (transform.gameObject != null))
        {
            TComponent component = transform.gameObject.GetComponent<TComponent>();
            if (component == null)
            {
                return FindParentComponent<TComponent>(transform.parent);
            }
            return component;
        }
        return null;
    }

    static public GameObject FindParentGameObject<TComponent>(TComponent target) where TComponent : Component
    {
        if((target != null) && (target.gameObject != null))
        {
            return target.transform.parent.gameObject;
        }
        return null;
    }
    /// <summary>
    /// [7-1] Transform support.
    /// </summary>
    static public Transform FindTransform(Transform transform, string name)
    {
        if (transform.name.Equals(name))
        {
            return transform;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform result = FindTransform(transform.GetChild(i), name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
    static public string[] FindAllTransformName(Transform transform)
    {
        List<string> transformNames = new List<string>();
        AssetObject.FindAllTransformName(transform, ref transformNames);
        return transformNames.ToArray();
    }
    static void FindAllTransformName(Transform transform, ref List<string> list)
    {
        list.Add(transform.name);
        for (int i = 0; i < transform.childCount; i++)
        {
            FindAllTransformName(transform.GetChild(i), ref list);
        }
    }
}
/// <summary>
/// 
/// </summary>
partial class AssetObject
{
    static bool isWorldPositionStays = false;
    static AssetManager assetManager = null;

    static GameObject GenerateGameObject()
    {
        if (assetManager != null)
        {
            return assetManager.NewGameObject();
        }
        return new GameObject();
    }
    static GameObject GenerateGameObject(string name)
    {
        if (assetManager != null)
        {
            return assetManager.NewGameObject(name);
        }
        return new GameObject(name);
    }
    static Asset InstantiateAsset(Asset asset)
    {
        if (assetManager != null)
        {
            return assetManager.InstantiateAsset(asset);
        }
        return GameObject.Instantiate(asset);
    }
    static Asset InstantiateAsset(Asset asset, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (assetManager != null)
        {
            return assetManager.InstantiateAsset(asset, position, rotation, parent);
        }
        return GameObject.Instantiate(asset, position, rotation);
    }

    static public void DeactiveGameObjectSpecificName(GameObject target, string name)
    {
        GameObject[] list = FindGameObjectArrayIncludeName(target, name);
        foreach(var obj in list)
        {
            obj.SetActive(false);
        }
    }
}
