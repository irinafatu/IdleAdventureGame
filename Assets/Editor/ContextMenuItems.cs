using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ContextMenuItems
{
    [MenuItem("CONTEXT/Transform/Scale and Position")]
    static void ScaleAndPosition(MenuCommand command)
    {
        Transform t = (Transform)command.context;
        t.localScale = Vector3.one;
        t.position = Vector3.zero;
    }
}
