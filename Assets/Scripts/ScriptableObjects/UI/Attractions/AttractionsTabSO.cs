using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttractionTabData
{
    public AttractionType type;
    public Sprite icon;
    public string Name;   
}


[CreateAssetMenu(fileName = "AttractionsTabSO", menuName = "Data/UI/AttractionsTab")]
public class AttractionsTabSO : ScriptableObject
{
    public List<AttractionTabData> dataList = new List<AttractionTabData>();
}
