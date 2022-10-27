using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Scene = System.Enum;

/// <summary>
/// 
/// </summary>
public interface SceneListener
{
    void SceneOpen(Scene scene);
    void SceneClose(Scene currentScene, Scene nextScene);
}
/// <summary>
/// 
/// </summary>
static public partial class SceneObject
{
    //static public AppProject.Scene CurScene
    //{
    //    get
    //    {
    //        try
    //        {
    //            return (AppProject.Scene)Enum.Parse(typeof(AppProject.Scene), UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    //        }
    //        catch(Exception e)
    //        {
    //            Debug.LogRed("Error : " + e.Message);
    //            return AppProject.Scene.None;
    //        }
    //    }
    //}

    static public void Clear()
    {
        SceneObject.listenerList.Clear();
    }
    static public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    static public void SetHierarchyNameForm(string form)
    {
        hierarchyNameForm = form;
    }
    static public void SetUseLoadLevel(bool useLoadLevel)
    {
        SceneObject.useLoadLevel = useLoadLevel;
    }
    static public void SetUnloadScene(Scene unloadScene)
    {
        SceneObject.unloadScene = unloadScene;
    }
    static public void InsertListener(SceneListener listener)
    {
        listenerList.Add(listener);
    }
    static public void RemoveListener(SceneListener listener)
    {
        listenerList.Remove(listener);
    }
    static public bool IsNextScene()
    {
        return (nextScene != null);
    }
    static public bool ChangeScene()
    {
        if (nextScene != null)
        {
            Scene scene = nextScene;
            nextScene = null;
            MoveScene(scene);
            return true;
        }
        return false;
    }
    static public void ChangeScene(Scene scene)
    {
        SceneObject.nextScene = scene;
        //MapViewManager.Instance.RemoveData();

        if (unloadScene != null)
        {
            MoveScene(unloadScene);
        }
        else
        {
            MoveScene(scene);
        }
    }
    static public void ChangeScene(Scene scene, Scene nextScene)
    {
        SceneObject.nextScene = nextScene;
        MoveScene(scene);
    }
    
    static public Scene GetCurrentScene()
    {
        return currentScene;
    }
    
}
/// <summary>
/// 
/// </summary>
partial class SceneObject
{
    static string hierarchyNameForm = "({0})";
    static bool useLoadLevel = true;
    static Scene unloadScene = null;
    static Scene currentScene = null;
    static Scene nextScene = null;
    static List<SceneListener> listenerList = new List<SceneListener>();

    static void MoveScene(Scene scene)
    {
        currentScene = scene;
        if (useLoadLevel)
        {
            NotifySceneClose();
            LoadScene(scene.ToString());
            NotifySceneOpen();
        }
        else
        {
            DestroyScene();
            NotifySceneClose();
            ApplicationLoadLevel(scene);
            NotifySceneOpen();
        }
    }
    static void NotifySceneClose()
    {
        for (int i = 0; i < listenerList.Count;i++ )
        {
            listenerList[i].SceneClose(currentScene, nextScene);
        }
    }
    static void NotifySceneOpen()
    {
        for (int i = 0; i < listenerList.Count; i++)
        {
            listenerList[i].SceneOpen(currentScene);
        }
    }
}
/// <summary>
/// 
/// </summary>
partial class SceneObject
{
    static GameObject sceneObject = null;

    static void DestroyScene()
    {
        if (sceneObject != null)
        {
            AssetObject.Destroy(sceneObject);
            sceneObject = null;
        }
    }
    static bool ApplicationLoadLevel(Scene scene)
    {
        sceneObject = AssetObject.NewGameObject(string.Format(hierarchyNameForm, scene));
        if (sceneObject != null)
        {
            if (AssetObject.InsertComponent(sceneObject, scene.ToString()) != null)
            {
                return true;
            }
            else
            {
                AssetObject.Destroy(sceneObject);
                sceneObject = null;
            }
        }
        return false;
    }
}
