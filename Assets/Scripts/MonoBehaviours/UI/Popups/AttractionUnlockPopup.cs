using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class AttractionUnlockPopupData : BasePopupData
{
    public Guid guid;
}

public class AttractionUnlockPopup : BasePopup
{
    public Text titleText;
    public Image icon;

    [HideInInspector]
    public List<UnlockConditionComponent> _unlockConditionsList = new List<UnlockConditionComponent>();
    public Transform unlockConditionsListParent;
    public UnlockConditionComponent unlockConditionComponentPrefab;

    public override void Init(BasePopupData data)
    {
        base.Init(data);
        _unlockConditionsList.Clear();
        AttractionsDataAsset attractionsDataAsset = GameManager.Instance.GetAttractionsData();
        AttractionDataAssetItem assetDataItem = attractionsDataAsset.attractionsDataAsset.Find
          (a => a.gameEventType == attractionsDataAsset.currentEvent);
        BasicAtractionData attrationData = assetDataItem.GetBasicAttractionForGUID(((AttractionUnlockPopupData)data).guid);

        titleText.GetComponent<LocalizationComponent>().SetTheLocalizedText(attrationData.name);
        icon.sprite = attrationData.icon;
        foreach(UnlockCondition condition in attrationData.unlockConditionList)
        {
            if (condition.unlockConditionType != UnlockConditionType.FREE)
            {
                UnlockConditionComponent newCondition = Instantiate<UnlockConditionComponent>(
                            unlockConditionComponentPrefab, transform.position, Quaternion.identity);
                newCondition.transform.SetParent(unlockConditionsListParent, false);
                newCondition.Init(condition);
                _unlockConditionsList.Add(newCondition);
            }
        }

    }

    public override void OnCloseBtnPressed()
    {
        //it may need to inform other pages that the popup was closed -> event
        transform.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        Destroy(gameObject);
    }
}
