using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyButton : MonoBehaviour
{
    private CurrencyData currencyData;
    public UnityEngine.UI.Image currencyIcon;
    public Text currencyAmountText;
    public Animator fillAnimator;
    UserInventory userInventory;
    CurrencyAmount inventoryData;

    public void Start()
    {
        userInventory = GameManager.Instance.GetUserInventory();
        userInventory.addCurrencyEvent.AddListener(OnCurrencyIncresed);
        userInventory.spendCurrencyEvent.AddListener(OnCurrencyDecresed);
    }

    public void OnDestroy()
    {
        userInventory.addCurrencyEvent.RemoveListener(OnCurrencyIncresed);
        userInventory.spendCurrencyEvent.RemoveListener(OnCurrencyDecresed);
    }

    public void Init(CurrencyData data)
    {
        currencyData = data;
        currencyIcon.sprite = data.icon;
        if (userInventory == null)
            userInventory = GameManager.Instance.GetUserInventory();
        inventoryData = userInventory.currenciesDataList.Find(x => x.type == currencyData.type);
        currencyAmountText.text = inventoryData.amount.ToString();

        fillAnimator.gameObject.SetActive(currencyData.hasFillAnimation);

    }

    public void OnBtnPressed()
    {
        //TODO - open shop page with the specific currency packages on screen
    }

    public void PlayAnimation()
    {

    }

    public void OnCurrencyIncresed(CurrencyAmount amount)
    {

        if (currencyData.type == amount.type)
        {
            currencyAmountText.text = inventoryData.amount.ToString();
        }
    }


    public void OnCurrencyDecresed(CurrencyAmount amount)
    {
        if (currencyData.type == amount.type)
        {
            currencyAmountText.text = inventoryData.amount.ToString();
        }
    }

    public void Update()
    {

    }
}
