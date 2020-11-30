using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockConditionComponent : MonoBehaviour
{
    public GameObject checkIcon;
    public GameObject uncheckIcon;
    public UnityEngine.UI.Image conditionIcon;
    public Text conditionText;

    public void Init(UnlockCondition condition)
    {
        bool isUnlocked = CheckCondition(condition);
        checkIcon.SetActive(isUnlocked);
        uncheckIcon.SetActive(!isUnlocked);

        switch (condition.unlockConditionType)
        {
            case UnlockConditionType.PAY_CURRENCY:
                {
                    conditionText.text = "Cost " + condition.unlockConditionValue.currencyAmount.amount.ToString();
                    CurrencyData currencyData = GameManager.Instance.GetGameData().currencies.Find(
                        a => a.type == condition.unlockConditionValue.currencyAmount.type);
                    conditionIcon.sprite = currencyData.icon;
                }
                break;
            case UnlockConditionType.ATTRACTION_COUNT:
                {
                    conditionText.text = "Min " + condition.unlockConditionValue.guidCount.ToString();
                    BasicAtractionData attractionData =  GameManager.Instance.GetAttractionsData().GetCurrentEventAssets().GetBasicAttractionForGUID(
                        condition.unlockConditionValue.guidData.guid);
                    if (attractionData != null)
                    {
                        conditionIcon.sprite = attractionData.icon;
                    }
                }
                break;
            case UnlockConditionType.FAME_LEVEL:
                {
                    conditionText.text = "Min Fame Level: " + condition.unlockConditionValue.fameLevel;
                }
                break;
        }


    }

    public bool CheckCondition(UnlockCondition condition)
    {
        List<UnlockCondition> list = new List<UnlockCondition> { condition };
        return UnlockManager.Instance.CheckUnlockConditions(list);
    }
}
