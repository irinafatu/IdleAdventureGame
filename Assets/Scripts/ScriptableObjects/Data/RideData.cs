using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RideCategory
{
    CAROUSEL = 0,
    WHEEL = 1,
    SLIDE = 2,
    HOUSE = 3,
    ROLLER_COASTER = 4
}


[CreateAssetMenu(fileName = "RideData", menuName = "Data/Ride/Item")]
public class RideData : BasicAtractionData
{
    public RideCategory category;

    public CurrencyAmount initialCost;
    public List<CurrencyAmount> initialProfits;
    public List<CurrencyAmount> profits; //current profits
    public double fillTimeSeconds;

    //ifatu TODO - add employer card

}
