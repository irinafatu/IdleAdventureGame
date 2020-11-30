using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AttractionsListData
{
    public AttractionType type;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "AttractionsListSO", menuName = "Data/UI/AttractionsList")]
public class AttractionsListSO : ScriptableObject
{
    public List<AttractionsListData> listData = new List<AttractionsListData>();
}
