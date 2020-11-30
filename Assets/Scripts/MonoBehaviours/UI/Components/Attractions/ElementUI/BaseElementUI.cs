using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseElementUI : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text descriptionText;
    public Text amountText;
    //todo - locked state
    //todo put tap on the entire button
    public Button upgradeButton;
    public Button buyButton;
    public Text buyBtnText;
    public Image buyBtnCurrencyIcon;


    public abstract void Init(BasicAtractionData dataAsset);
    public abstract void OnBuyBtnPressed();

    public abstract void OnUpgradeBtnPressed();

    public virtual void OnInfoBtnPressed()
    {

    }

    //no automatization (no emplyer) we do things manual by tapping the entire element (minus the other buttons :)))
    public virtual void OnTap() { }
    
}
