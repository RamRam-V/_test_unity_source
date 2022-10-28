using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

public class BuildEditor
{
    //메뉴에서 빌드 옵션이 보이게 함
    [MenuItem("Build/WebGL")]
    public static void BuildWebGL()
    {
        SplashScreen.showUnityLogo = false;
        SplashScreen.overlayOpacity = 0f;

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
        BuildPlayerOptions options = GetBuildPlayerOptions("/Build");
        options.target = BuildTarget.WebGL;
        options.options = BuildOptions.Development;

        PlayerSettings.WebGL.emscriptenArgs = "-s ALLOW_MEMORY_GROWTH=1";
        PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.ExplicitlyThrownExceptionsOnly;
        PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Wasm;
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
        PlayerSettings.WebGL.wasmStreaming = true;


        Build(options);
    }

    //빌드 시작
    private static void Build(BuildPlayerOptions options)
    {
        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        //빌드 결과
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    //공통된 PlayerOption을 가져옴
    public static BuildPlayerOptions GetBuildPlayerOptions(string buildPath)
    {
        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = BuildEditor.GetScenes();
        options.locationPathName = absPath + buildPath;
        return options;
    }

    //절대 경로를 돌려줌
    public static string absPath
    {
        get
        {
#if UNITY_EDITOR
            string absPath = Application.dataPath;
            return absPath.Substring(0, absPath.LastIndexOf("/"));
#endif
            return Application.dataPath;
        }
    }

    //Build 세팅으로부터 Scene을 가져옴
    public static string[] GetScenes()
    {
        var scenes = EditorBuildSettings.scenes;
        var sceneList = new List<string>();
        foreach (var scene in scenes)
        {
            if (scene.enabled)
            {
                sceneList.Add(scene.path);
            }
        }
        return sceneList.ToArray();
    }
}
