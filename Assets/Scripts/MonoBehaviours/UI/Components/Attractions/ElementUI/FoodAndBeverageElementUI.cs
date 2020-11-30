using System;
using UnityEngine;
using UnityEngine.UI;

public class FoodAndBeverageElementUI : BaseElementUI
{
    Guid guid;
    FoodAndBeverageData _foodAndBeverageData;
    UserProductData _userProductData;
    [Header("Idle State")]
    public GameObject idleBox;
    public Image fillImage;
    public FillAnimation fillAnimation;
    UserInventory inventory;
    GameDataAsset gameAsset;
    public ProfitDescriptionBox profitDescriptionBox;

    [Header("Locked State")]
    public GameObject lockedBox;
    public Image lockedFillImage;
    public Text lockedNameText;
    public Text lockedInfoText;
    public GameObject locketAnim;
    public Text tapToUnlockText;
    public Image currencyCostIcon;


    public override void Init(BasicAtractionData data)
    {
        inventory = GameManager.Instance.GetUserInventory();
        gameAsset = GameManager.Instance.GetGameData();
        _foodAndBeverageData = (FoodAndBeverageData)data;
        guid = data.guid;
        _userProductData = inventory.GetUserProductData(guid);

        if (_userProductData == null || _userProductData.count == 0)
        {
            _foodAndBeverageData.state = ItemState.LOCKED; //TODO - remove hardcodation
        }
        else
            _foodAndBeverageData.state = ItemState.IDLE; //TODO - remove hardcodation
        InitUIElements();
        CheckBuyBtnState();

        fillAnimation.Init(fillImage, _foodAndBeverageData.fillTimeSeconds, OnAnimComplete);

    }

    public void InitUIElements()
    {
        currencyCostIcon.gameObject.SetActive(false);
        if (_foodAndBeverageData.state == ItemState.LOCKED || _foodAndBeverageData.state == ItemState.TO_BE_UNLOCKED)
        {
            lockedBox.SetActive(true);
            idleBox.SetActive(false);
            lockedFillImage.sprite = _foodAndBeverageData.icon;
            lockedNameText.gameObject.GetComponent<LocalizationComponent>().SetTheLocalizedText(_foodAndBeverageData.name);
            tapToUnlockText.gameObject.SetActive(_foodAndBeverageData.state == ItemState.TO_BE_UNLOCKED);
            locketAnim.gameObject.SetActive(_foodAndBeverageData.state == ItemState.LOCKED);
            //lockedInfoText.gameObject.SetActive(_rideData.state == ItemState.LOCKED);
            if (_foodAndBeverageData.state == ItemState.LOCKED)
            {
                lockedInfoText.gameObject.GetComponent<LocalizationComponent>().SetTheLocalizedText();
            }
            else
            {
                AttractionsDataAsset attractionsDataAsset = GameManager.Instance.GetAttractionsData();
                AttractionDataAssetItem assetDataItem = attractionsDataAsset.attractionsDataAsset.Find
                  (a => a.gameEventType == attractionsDataAsset.currentEvent);
                BasicAtractionData attrationData = assetDataItem.GetBasicAttractionForGUID(guid);
                string costText = LocalizationManager.Instance.GetTextForKey("UNLOCK_COST");
                UnlockCondition currencyCondition = attrationData.unlockConditionList.Find(a => a.unlockConditionType == UnlockConditionType.PAY_CURRENCY);
                if (currencyCondition != null)
                {
                    lockedInfoText.text = costText + currencyCondition.unlockConditionValue.currencyAmount.amount.ToString();
                    CurrencyData currencyData = GameManager.Instance.GetGameData().currencies.Find(
                      a => a.type == currencyCondition.unlockConditionValue.currencyAmount.type);
                    currencyCostIcon.gameObject.SetActive(true);
                    currencyCostIcon.sprite = currencyData.icon;
                }
            }
        }

        if (_foodAndBeverageData.state == ItemState.IDLE)
        {
            lockedBox.SetActive(false);
            idleBox.SetActive(true);

            icon.sprite = _foodAndBeverageData.icon;
            nameText.gameObject.GetComponent<LocalizationComponent>().SetTheLocalizedText(_foodAndBeverageData.name);
            descriptionText.gameObject.GetComponent<LocalizationComponent>().SetTheLocalizedText(_foodAndBeverageData.description);
            amountText.text = _userProductData.count.ToString();

            buyBtnText.text = LocalizationManager.Instance.GetTextForKey(buyBtnText.GetComponent<LocalizationComponent>().localizationKey)
                + " " + _foodAndBeverageData.initialCost.amount.ToString();
            buyBtnCurrencyIcon.sprite = gameAsset.currencies.Find(x => x.type == _foodAndBeverageData.initialCost.type).icon;
            profitDescriptionBox.Init(_foodAndBeverageData.initialProfits);
        }
        //check state
    }

    public void Update()
    {
        CheckBuyBtnState();
        if (_foodAndBeverageData.state == ItemState.LOCKED || 
            _foodAndBeverageData.state == ItemState.TO_BE_UNLOCKED)
        {
            CheckUnlock();
        }
    }

    private void CheckUnlock()
    {
        bool couldBeUnlocked = UnlockManager.Instance.CheckUnlockConditions(_foodAndBeverageData.unlockConditionList);
        if (couldBeUnlocked)
        {
            if (_foodAndBeverageData.state == ItemState.LOCKED)
            {
                _foodAndBeverageData.state = ItemState.TO_BE_UNLOCKED;
                InitUIElements();
            }
        }
        else if (_foodAndBeverageData.state == ItemState.TO_BE_UNLOCKED)
        {
            _foodAndBeverageData.state = ItemState.LOCKED;
            InitUIElements();
        }
    }

    private void CheckBuyBtnState()
    {
        long currentAmount = inventory.currenciesDataList.Find(x => x.type == _foodAndBeverageData.initialCost.type).amount;
        if (currentAmount >= _foodAndBeverageData.initialCost.amount)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    public void OnAnimComplete()
    {
        foreach (CurrencyAmount profit in _foodAndBeverageData.initialProfits)
        {
            inventory.AddCurrency(profit);//ifatu TODO - add currentProfit instead
        }
        //TODO test auto?
        _foodAndBeverageData.state = ItemState.IDLE;
    }

    public override void OnBuyBtnPressed()
    {
        inventory.OnItemBought(_foodAndBeverageData.guid, AttractionType.FOOD_AND_BEVERAGE, 1);
        inventory.SpendCurrency(_foodAndBeverageData.initialCost);
        amountText.text = inventory.productDataList.Find(x => x.guid == _foodAndBeverageData.guid).count.ToString();
    }

    public override void OnUpgradeBtnPressed()
    {

    }

    public override void OnInfoBtnPressed()
    {
        AtractionPopupData popupData = new AtractionPopupData { guid = guid };
        PopupManager.Instance.OpenPopup(PopupType.FOOD_AND_BEVERAGE_INFO, popupData);
    }

    //no automatization (no emplyer) we do things manual by tapping the entire element (minus the other buttons :)))
    public override void OnTap()
    {
        switch (_foodAndBeverageData.state)
        {
            case ItemState.LOCKED:
                {
                   // locketAnim.gameObject.GetComponent<Animator>().SetTrigger("Shake");
                    //ifatuTODO show locked popup
                    AttractionUnlockPopupData popupData = new AttractionUnlockPopupData { guid = guid };
                    PopupManager.Instance.OpenPopup(PopupType.UNLOCK_INFO, popupData);
                }
                break;
            case ItemState.TO_BE_UNLOCKED:
                {
                    _foodAndBeverageData.state = ItemState.IDLE;
                    OnBuyBtnPressed();
                    InitUIElements();

                    //ifatuTODO - unlock item -> buy one - the initial price or the unlocke price condition must be withdraw
                }
                break;
            case ItemState.IDLE:
                {
                    if (fillAnimation != null)
                    {
                        fillAnimation.StartAnim();
                        _foodAndBeverageData.state = ItemState.TAPPED;
                    }
                }
                break;
            case ItemState.TAPPED: // do nothing
                break;
            case ItemState.AUTO://do nothing
                break;
            default:
                break;
        }
    }

}
