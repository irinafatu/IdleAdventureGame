using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[Serializable]
public class UserProductData
{
    public Guid guid;
    public string guidString;
    public AttractionType type;
    public double count;
    public int level;

    public UserProductData()
    {
        guidString = guid.ToString();
    }
}
[System.Serializable]
public class CurrencyEvent : UnityEvent<CurrencyAmount>
{
}

[Serializable]
public class UserInventorySaveData
{
    public List<CurrencyAmount> currenciesDataList = new List<CurrencyAmount>();
    public List<UserProductData> productDataList = new List<UserProductData>();
}


[CreateAssetMenu(fileName = "UserDataSO", menuName = "Data/User")]
public class UserInventory : ScriptableObject, ISaveable
{
    public CurrencyEvent spendCurrencyEvent = new CurrencyEvent();
    public CurrencyEvent addCurrencyEvent = new CurrencyEvent();
    public List<CurrencyAmount> currenciesDataList = new List<CurrencyAmount>();
    public List<UserProductData> productDataList = new List<UserProductData>();

    string filename = "UserInventory.txt";

    public void AddCurrency(CurrencyAmount currencyAmount)
    {
        CurrencyAmount currency = currenciesDataList.Find(a => a.type == currencyAmount.type);
        currency.amount += currencyAmount.amount;
        addCurrencyEvent.Invoke(currencyAmount);
        Save();
    }

    public void SpendCurrency(CurrencyAmount currencyAmount)
    {
        CurrencyAmount currency = currenciesDataList.Find(a => a.type == currencyAmount.type);
        currency.amount -= currencyAmount.amount;
        spendCurrencyEvent.Invoke(currencyAmount);
        Save();
    }

    public void DEBUG_SetCurrency(CurrencyAmount currencyAmount)
    {
        CurrencyAmount currency = currenciesDataList.Find(a => a.type == currencyAmount.type);
        currency.amount = currencyAmount.amount;
        addCurrencyEvent.Invoke(currencyAmount);
        Save();
    }

    public bool HasEnoughCurrency(CurrencyAmount currencyAmount)
    {
        CurrencyAmount currency = currenciesDataList.Find(a => a.type == currencyAmount.type);
        if (currency != null && currency.amount >= currencyAmount.amount)
            return true;
        return false;
    }

    public bool HasEnoughAttractionOfGUID(Guid attractionGuid, double attractionValue)
    {
        UserProductData product = productDataList.Find(a => a.guid == attractionGuid);
        if (product != null && product.count >= attractionValue)
            return true;
        return false;
    }

    public bool HasMinFameLevel(int fameLevelDesired)
    {
        return GameManager.Instance.GetUserFame().GetCurrentFameLavel() >= fameLevelDesired;
    }

    public void OnItemBought(Guid guid, AttractionType type, double amount)
    {
        UserProductData product = productDataList.Find(a => a.guid == guid);
        if (product == null) //new item -> add it to the list
        {
            product = new UserProductData
            {
                guid = guid,
                guidString = guid.ToString(),
                count = amount,
                level = 1,
                type = type
            };
            productDataList.Add(product);
        }
        else
        {
            product.count += amount;
        }
        Save();
    }

    public void OnItemLevelUp(Guid guid, int level)
    {
        UserProductData product = productDataList.Find(a => a.guid == guid);
        product.level = level;
        Save();
    }

    public List<UserProductData> GetAllAttractionsOfType(AttractionType pType)
    {
        List<UserProductData> newData = new List<UserProductData>();
        newData = productDataList.FindAll(a => a.type == pType);
        return newData;
    }

    public UserProductData GetUserProductData(Guid guid)
    {
        return productDataList.Find(a => a.guid == guid);
    }

    public void Save()
    {
        UserInventorySaveData dataToSave = new UserInventorySaveData();
        dataToSave.currenciesDataList = currenciesDataList;
        dataToSave.productDataList = productDataList;

        string userInventoryString = JsonUtility.ToJson(dataToSave, true);
        SaveDataManager.Instance.WriteToFile(filename, userInventoryString);
    }

    public void Load()
    {
        UserInventorySaveData dataToSave = new UserInventorySaveData();
        string json = SaveDataManager.Instance.ReadFromFile(filename);
        if (json == "")
        {
            Save();
            return;
        }
        JsonUtility.FromJsonOverwrite(json, dataToSave);
        if (dataToSave == null)
            return;
        if (dataToSave.currenciesDataList != null)
            currenciesDataList = dataToSave.currenciesDataList;
        if (dataToSave.productDataList != null)
            productDataList = dataToSave.productDataList;
    }

    //o lista de produse cu starea lor
    //money earned - per currency - o list ade urrency sic ae are


}
