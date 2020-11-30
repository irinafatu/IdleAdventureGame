using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoSingleton<UnlockManager>
{
    UserInventory userInventory;
    public void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        userInventory = GameManager.Instance.GetUserInventory();
    }

    public bool CheckUnlockConditions(List<UnlockCondition> conditions)
    {
        foreach(UnlockCondition condition in conditions)
        {
            bool conditionMet = true;
            switch (condition.unlockConditionType)
            {
                case UnlockConditionType.FREE:
                    {
                        conditionMet = true;
                    }
                    break;
                case UnlockConditionType.PAY_CURRENCY:
                    {
                        conditionMet = CheckCurrency(condition);  
                    }
                    break;
                case UnlockConditionType.ATTRACTION_COUNT:
                    {
                        conditionMet = CheckAttractionCount(condition);
                    }
                    break;
                case UnlockConditionType.FAME_LEVEL:
                    {
                        conditionMet = CheckFameLevel(condition);
                    }
                    break;
            }
            if (conditionMet == false)
                return false;
        }
        return true;
    }

    bool CheckCurrency(UnlockCondition condition)
    {
        UnlockConditionValue value = condition.unlockConditionValue;
        return userInventory.HasEnoughCurrency(value.currencyAmount);
    }

    bool CheckAttractionCount(UnlockCondition condition)
    {
        UnlockConditionValue value = condition.unlockConditionValue;
        return userInventory.HasEnoughAttractionOfGUID(value.guidData.guid, value.guidCount);
    }

    bool CheckFameLevel(UnlockCondition condition)
    {
        UnlockConditionValue value = condition.unlockConditionValue;
        return userInventory.HasMinFameLevel(value.fameLevel);
    }
}
