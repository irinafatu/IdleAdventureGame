using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenu : BaseMenu
{
    public void OnTapToContinueBtnPressed()
    {
        GameManager.Instance.LoadScene("MainScene", LoadSceneMode.Single, MenuType.MAIN_MENU);
        MenuManager.Instance.OpenMenu(MenuType.MAIN_MENU, true);
    } 
}
