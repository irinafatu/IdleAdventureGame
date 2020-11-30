using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuType
{
    NONE = -1,
    LOADING_MENU = 0,
    MAIN_MENU = 10,
    SHOP_MENU = 20,
    OPTIONS_MENU = 30,
    HELP_MENU = 40,
    CARDS_MENU = 50,
}



public class BaseMenu : MonoBehaviour
{
    public virtual void Init()
    {

    }
}
