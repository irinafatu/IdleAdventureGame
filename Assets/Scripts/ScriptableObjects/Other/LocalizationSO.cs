using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Language
{
    ENGLISH = 0,
    ROMANIAN = 1,
}



[Serializable]
public class LocalizationValue
{
    public Language language;
    public string text;
}

[Serializable]
public class LocalizationList
{
    public string key;
    //public LocalizationValue[] value = new LocalizationValue[System.Enum.GetValues(typeof(Language)).Length];
    public List<LocalizationValue> value = new List<LocalizationValue>();
   // public LocalizationValue[] value;//= new LocalizationValue[2];
}
[CreateAssetMenu(fileName ="LocalizationSO", menuName = "Localization")]
public class LocalizationSO : ScriptableObject
{
    [SerializeField]
    public List<LocalizationList> listData = new List<LocalizationList>();

}
