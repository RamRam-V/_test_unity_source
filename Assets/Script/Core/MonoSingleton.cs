using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    obj.AddComponent<T>();
                }

                if (_instance == null)
                    return null;

                if (_instance.gameObject == null)
                    return null;

                if (_instance.gameObject.transform.parent != null)
                    DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }
}
