using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSChecker : MonoBehaviour
{
    [Range(1, 100)]
    public int fFontSize;

    float deltaTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        fFontSize = fFontSize == 0 ? 50 : fFontSize;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / fFontSize;
        style.normal.textColor = new Color(1, 1, 1, 1);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0}ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
