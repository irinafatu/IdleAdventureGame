using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataAsset", menuName = "Data/Asset/GameDataAsset")]
public class GameDataAsset : ScriptableObject
{
    [Header("Currencies")]
    public List<CurrencyData> currencies = new List<CurrencyData>();


    #region Currency
    public Sprite GetIconForCurrencyType(CurrencyType type)
    {
        CurrencyData currencyData = currencies.Find(a => a.type == type);
        if (currencyData != null)
        {
           return currencyData.icon;
        }
        return null;
    }
    #endregion Currency
}
