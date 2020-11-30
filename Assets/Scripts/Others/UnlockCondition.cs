using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UnlockConditionValue
{
    //use for ATTRACTION_COUNT type
    public BasicAtractionData guidData;
    public double guidCount;
    //use for PAY_CURRENCY type
    public CurrencyAmount currencyAmount;
    //use for FAME_LEVEL type
    public int fameLevel;
}

public enum UnlockConditionType
{
    FREE = 0,
    PAY_CURRENCY = 10,
    ATTRACTION_COUNT = 20,
    FAME_LEVEL = 30,
}

[Serializable]
public class UnlockCondition
{
    public UnlockConditionType unlockConditionType;
    public UnlockConditionValue unlockConditionValue;
}
