using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class RecreationAreaPopup : BasePopup
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
        RecreationAreaData recreationAreaData = assetDataItem.recreationAreaData.Find(a => a.guid == ((AtractionPopupData)data).guid);

        if (recreationAreaData != null)
        {
            InitRecreationAreaPopup(recreationAreaData, inventoryData);
        }
    }

    public void InitRecreationAreaPopup(RecreationAreaData data, UserProductData inventoryData)
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
}
