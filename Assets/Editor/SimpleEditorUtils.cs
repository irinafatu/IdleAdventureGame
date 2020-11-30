// IN YOUR EDITOR FOLDER, have SimpleEditorUtils.cs.
// paste in this text.
// to play, HIT COMMAND-q rather than command-P
// (the zero key, is near the P key, so it's easy to remember)
// simply insert the actual name of your opening scene
// "__preEverythingScene" on the second last line of code below.
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;

[InitializeOnLoad]
public static class SimpleEditorUtils
{
    // click command-q to go to the prelaunch scene and then play

    [MenuItem("Edit/Play-Unplay, But From Prelaunch Scene %q")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }
        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene(
                    "Assets/Scenes/LoadingScene.unity");
        EditorApplication.isPlaying = true;
    }
}
#endif