using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopupType
{
    NONE = 0,
    RIDE_INFO = 1,
    FOOD_AND_BEVERAGE_INFO = 2,
    RECREATION_AREA_INFO = 3,
    USER_INFO = 4,
    EXIT_GAME = 5,
    UNLOCK_INFO = 6
}

public class BasePopupData
{
}



public class BasePopup : MonoBehaviour
{
    public PopupType popupType;
    public bool hasCloseButton;

    public virtual void OnCloseBtnPressed()
    {
        //it may need to inform other pages that the popup was closed -> event
        transform.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        Destroy(gameObject);
    }

    public virtual void Init(BasePopupData data)
    {
    }
}
