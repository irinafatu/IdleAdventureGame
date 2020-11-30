using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PopupData
{
    public PopupType popupType;
    public BasePopup popupPrefab;
}



[CreateAssetMenu(fileName = "PopupDataList", menuName = "Data/UI/PopupDataList")]
public class PopupDataList : ScriptableObject
{
    public List<PopupData> popupDataList;
}
