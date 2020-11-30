using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MenuData
{
    public MenuType menuType;
    public BaseMenu menuPrefab;
}



[CreateAssetMenu(fileName ="MenuDataList", menuName ="Data/UI/MenuDataList")]
public class MenuDataList : ScriptableObject
{
    public List<MenuData> menuDataList;
}
