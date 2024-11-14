using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class PlayModeStartSceneMenu
{
    private const string SetMenuPath = "Assets/PlayModeStartScene/Set";
    private const string ClearMenuPath = "Assets/PlayModeStartScene/Clear";

    [MenuItem(SetMenuPath, priority = 0)]
    public static void SetPlayModeStartScene()
    {
        EditorSceneManager.playModeStartScene = Selection.activeObject as SceneAsset;
    }

    [MenuItem(SetMenuPath, true)]
    public static bool PlayModeStartSceneValidate()
    {
        Menu.SetChecked(SetMenuPath, EditorSceneManager.playModeStartScene != null);
        return Selection.count == 1 && Selection.activeObject is SceneAsset;
    }

    [MenuItem(ClearMenuPath, priority = 1)]
    public static void ClearPlayModeStartScene()
    {
        EditorSceneManager.playModeStartScene = null;
    }
}