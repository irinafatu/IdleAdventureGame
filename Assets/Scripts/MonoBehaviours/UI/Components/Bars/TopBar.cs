using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{
    public UserButton userButton;
    public Transform currencyButtonsParent;
    UserInventory userInventory;
    GameDataAsset gameDataAsset;
    List<CurrencyButton> _currencyButtonList = new List<CurrencyButton>();
    public CurrencyButton currencyButtonPrefab;

    public void Start()
    {
        userInventory = GameManager.Instance.GetUserInventory();
        gameDataAsset = GameManager.Instance.GetGameData();
        InitCurrencies();
    }

    public void OnDestroy()
    {
        for (int i = 0; i < _currencyButtonList.Count; i++)
        {
            Destroy(_currencyButtonList[i]);
        }
        _currencyButtonList.Clear();

    }
    private void InitCurrencies()
    {
        _currencyButtonList.Clear();
        foreach (CurrencyData currency in gameDataAsset.currencies)
        {
            CurrencyButton newButton = Instantiate<CurrencyButton>(currencyButtonPrefab, currencyButtonsParent.position, Quaternion.identity);
            newButton.transform.SetParent(currencyButtonsParent.transform, false);
            newButton.Init(currency);
            _currencyButtonList.Add(newButton);
        }
    }
}
