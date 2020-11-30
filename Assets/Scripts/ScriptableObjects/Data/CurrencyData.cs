using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    MONEY = 0,
    TICKETS = 10,
}
[Serializable]
public class CurrencyAmount
{
    public CurrencyType type;
    public long amount;
}


[CreateAssetMenu(fileName ="NewCurrency", menuName ="Data/Currency")]
public class CurrencyData : ScriptableObject
{
    public CurrencyType type;
    public Sprite icon;
    public bool hasFillAnimation = false;
    //ifatu TODO - add animation?
}
