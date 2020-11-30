﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGamePopup : BasePopup
{
    public void OnYesButtonPressed()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void OnNoButtonPressed()
    {
        Destroy(gameObject);
    }
}
