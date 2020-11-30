using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserPopupData : BasePopupData
{
    public string title;
    public int fameLevel;
    public float fameLevelPercentage;
    public Sprite iconSprite;
    public double currentFamePoints;
    public double nextLevelFamePoints;
}


public class UserPopup : BasePopup
{
    public Text titleText;
    public Text fameLevelText;

    public Image icon;
    public Image fillImage;
    UserPopupData userPopupData;
    public Text famePointsText;

    public override void OnCloseBtnPressed()
    {
        //it may need to inform other pages that the popup was closed -> event
        transform.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        Destroy(gameObject);
    }

    public override void Init(BasePopupData data)
    {
        base.Init(data);
        userPopupData = (UserPopupData)data;
/*            inventoryData = userInventory.productDataList.Find(x => x.guid == ((AtractionPopupData)data).guid);

        AttractionDataAssetItem assetDataItem = attractionsDataAsset.attractionsDataAsset.Find
          (a => a.gameEventType == attractionsDataAsset.currentEvent);
        RideData rideData = assetDataItem.rideData.Find(a => a.guid == ((AtractionPopupData)data).guid);
        if (rideData != null)
*/        {
            InitUserPopup(userPopupData);
        }
    }

    public void InitUserPopup(UserPopupData data)
    {
        titleText.text = data.title;
        fameLevelText.text = data.fameLevel.ToString();
        famePointsText.text = data.currentFamePoints.ToString() + "/" + data.nextLevelFamePoints.ToString();


        //titleText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.title);
        //fameLevelText.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.fameLevel.ToString());
        icon.sprite = data.iconSprite;
        fillImage.fillAmount = data.fameLevelPercentage;
    }
}
