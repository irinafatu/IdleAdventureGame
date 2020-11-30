using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class RidePopup : BasePopup
{
    public Text titleText;
    public Text descriptionText;

    public Image icon;
    public Text amountText;

    public AttractionType type;
    UserInventory userInventory;
    //TODO - add card!

    public override void Init(BasePopupData data)
    {
        base.Init(data);
        userInventory = GameManager.Instance.GetUserInventory();
        UserProductData inventoryData = userInventory.productDataList.Find(x => x.guid == ((AtractionPopupData)data).guid);
        AttractionsDataAsset attractionsDataAsset = GameManager.Instance.GetAttractionsData();
        AttractionDataAssetItem assetDataItem = attractionsDataAsset.attractionsDataAsset.Find
          (a => a.gameEventType == attractionsDataAsset.currentEvent);
        RideData rideData = assetDataItem.rideData.Find(a => a.guid == ((AtractionPopupData)data).guid);
        if (rideData != null)
        {
            InitRidePopup(rideData, inventoryData);
        }
    }

    public void InitRidePopup(RideData data, UserProductData inventoryData)
    {
        titleText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.name);
        descriptionText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.description);
        amountText.text = inventoryData.count.ToString();
        icon.sprite = data.icon;
    }
    public override void OnCloseBtnPressed()
    {
        //it may need to inform other pages that the popup was closed -> event
        transform.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        Destroy(gameObject);
    }

    /* public override void Init(BasePopupData data)
     {
         base.Init(data);
         switch (type)
         {
             case AttractionType.RIDE:
                 {
                     RideData rideData = rideListData.listData.Find(a => a.guid == ((AtractionPopupData)data).guid);
                     if (rideData != null)
                     {
                         InitRidePopup(rideData);
                     }
                 }
                 break;
             case AttractionType.FOOD_AND_BEVERAGE:
                 {
                     FoodAndBeverageData foodAndBeverageData = foodAndBeverageListData.listData.Find(a => a.guid == ((AtractionPopupData)data).guid);
                     if (foodAndBeverageData != null)
                     {
                         InitFoodAndBeveragePopup(foodAndBeverageData);
                     }
                 }
                 break;
             case AttractionType.RECREATION_AREA:
                 {
                     RecreationAreaData recreationAreaData = recreationAreaListData.listData.Find(a => a.guid == ((AtractionPopupData)data).guid);
                     if (recreationAreaData != null)
                     {
                         InitRecreationAreaPopup(recreationAreaData);
                     }
                 }
                 break;
             default:
                 break;
         }

     }

     public void InitRidePopup(RideData data)
     {
         titleText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.name);
         descriptionText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.description);
         amountText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.count.ToString());
         icon.sprite = data.icon;
     }

     public void InitFoodAndBeveragePopup(FoodAndBeverageData data)
     {
         titleText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.name);
         descriptionText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.description);
         amountText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.count.ToString());
         icon.sprite = data.icon;
     }

     public void InitRecreationAreaPopup(RecreationAreaData data)
     {
         titleText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.name);
         descriptionText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.description);
         amountText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.count.ToString());
         icon.sprite = data.icon;
     } */
}
