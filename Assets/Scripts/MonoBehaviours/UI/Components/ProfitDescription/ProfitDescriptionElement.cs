using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfitDescriptionElement : MonoBehaviour
{
    public Image currencyIcon;
    public Text currencyAmountText;
    public GameDataAsset gameDataAsset;

    public void Init(CurrencyAmount currency)
    {
        currencyAmountText.text = "+" + currency.amount.ToString();
        currencyIcon.sprite = gameDataAsset.GetIconForCurrencyType(currency.type);
    }
}
