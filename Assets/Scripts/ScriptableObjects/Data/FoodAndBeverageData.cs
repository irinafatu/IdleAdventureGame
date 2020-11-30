using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodAndBeverageType
{
    FOOD = 0, 
    BEVERAGE = 1
}


[CreateAssetMenu(fileName = "FoodAndBeverageData", menuName = "Data/FoodAndBeverage/Item")]
public class FoodAndBeverageData : BasicAtractionData
{
    public FoodAndBeverageType category;

    public CurrencyAmount initialCost;
    public List<CurrencyAmount> initialProfits;
    public List<CurrencyAmount> profits; //current profits
    public float fillTimeSeconds;
    //TODO add card employer

  
}
