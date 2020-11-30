using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums
public enum AttractionType
{
    RIDE = 0,
    FOOD_AND_BEVERAGE = 1,
    RECREATION_AREA = 2
}


public enum ItemState
{
    LOCKED = 0,
    TO_BE_UNLOCKED = 1,
    IDLE = 2,//Unlocked ut not giving money at the moment(no tap or no card)
    TAPPED = 3,//No card to give it Auto so the user just tapped ; necxt tap will work only after the filling is completed
    AUTO = 4, // if the state is auto then the tapping doesn`t work anymore
}

public enum PerkType
{
    MORE_PROFIT,
    MORE_PROFIT_PERCENTAGE,
    SMALLER_PRICE,
    SMALLER_PRICE_PERCENTAGE,
}



[Serializable]
public struct UpgradeStep
{
    public float ticketsNeeded;
    public PerkType perkType;
    public float perkValue;
}

#endregion
public class BasicAtractionData : ScriptableObject
{
    public Guid guid;
    private string guidString;
    public string name;
    public Sprite icon;
    public AttractionType atractionType;
    [HideInInspector]
    public ItemState state;
    public List<UpgradeStep> upgradeStepList;
    [HideInInspector]
    public float count;
    public string description;
    public List<UnlockCondition> unlockConditionList;


    public BasicAtractionData()
    {
        if (guid == Guid.Empty)
        {
            guid = Guid.NewGuid();
        }
    }
  public string GetGUIDString()
    {
        guidString = guid.ToString();
        return guidString;
    }
    public Guid GetGUID()
    {
       // if (guid == null)
     //   {
      //      guid = Guid.NewGuid();
      //  }
        guidString = guid.ToString();
        return guid;
    }
}
