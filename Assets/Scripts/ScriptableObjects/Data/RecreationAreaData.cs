using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ifatuTODO - add more perks 
[CreateAssetMenu(fileName = "RecreationAreaData", menuName = "Data/RecreationArea/Item")]
public class RecreationAreaData : BasicAtractionData
{
    public CurrencyAmount initialCost;
    public PerkType perkType;
}
