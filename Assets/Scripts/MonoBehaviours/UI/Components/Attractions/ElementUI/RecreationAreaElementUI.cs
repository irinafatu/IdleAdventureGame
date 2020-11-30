using System;
using UnityEngine;
using UnityEngine.UI;

public class RecreationAreaElementUI : BaseElementUI
{
    Guid guid;
    RecreationAreaData _recreationAreaData;
    UserProductData _userProductData;
    [Header("Idle State")]
    public GameObject idleBox;
    UserInventory inventory;
    GameDataAsset gameAsset;

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
        _recreationAreaData = (RecreationAreaData)data;
        guid = data.guid;
        _userProductData = inventory.GetUserProductData(guid);

        if (_userProductData == null || _userProductData.count == 0)
        {
            _recreationAreaData.state = ItemState.LOCKED; //TODO - remove hardcodation
        }
        else
            _recreationAreaData.state = ItemState.IDLE; //TODO - remove hardcodation
        InitUIElements();
        CheckBuyBtnState();


    }

    public void InitUIElements()
    {
        currencyCostIcon.gameObject.SetActive(false);
        if (_recreationAreaData.state == ItemState.LOCKED || _recreationAreaData.state == ItemState.TO_BE_UNLOCKED)
        {
            lockedBox.SetActive(true);
            idleBox.SetActive(false);
            lockedFillImage.sprite = _recreationAreaData.icon;
            lockedNameText.gameObject.GetComponent<LocalizationComponent>().SetTheLocalizedText(_recreationAreaData.name);
            tapToUnlockText.gameObject.SetActive(_recreationAreaData.state == ItemState.TO_BE_UNLOCKED);
            locketAnim.gameObject.SetActive(_recreationAreaData.state == ItemState.LOCKED);
            //lockedInfoText.gameObject.SetActive(_rideData.state == ItemState.LOCKED);
            if (_recreationAreaData.state == ItemState.LOCKED)
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

        if (_recreationAreaData.state == ItemState.IDLE)
        {
            lockedBox.SetActive(false);
            idleBox.SetActive(true);

            icon.sprite = _recreationAreaData.icon;
            nameText.gameObject.GetComponent<LocalizationComponent>().SetTheLocalizedText(_recreationAreaData.name);
            descriptionText.gameObject.GetComponent<LocalizationComponent>().SetTheLocalizedText(_recreationAreaData.description);
            amountText.text = _userProductData.count.ToString();

            buyBtnText.text = LocalizationManager.Instance.GetTextForKey(buyBtnText.GetComponent<LocalizationComponent>().localizationKey)
                + " " + _recreationAreaData.initialCost.amount.ToString();
            buyBtnCurrencyIcon.sprite = gameAsset.currencies.Find(x => x.type == _recreationAreaData.initialCost.type).icon;
        }
        //check state
    }

    public void Update()
    {
        CheckBuyBtnState();
        if (_recreationAreaData.state == ItemState.LOCKED ||
           _recreationAreaData.state == ItemState.TO_BE_UNLOCKED)
        {
            CheckUnlock();
        }
    }

    private void CheckUnlock()
    {
        bool couldBeUnlocked = UnlockManager.Instance.CheckUnlockConditions(_recreationAreaData.unlockConditionList);
        if (couldBeUnlocked)
        {
            if (_recreationAreaData.state == ItemState.LOCKED)
            {
                _recreationAreaData.state = ItemState.TO_BE_UNLOCKED;
                InitUIElements();
            }
        }
        else if (_recreationAreaData.state == ItemState.TO_BE_UNLOCKED)
        {
            _recreationAreaData.state = ItemState.LOCKED;
            InitUIElements();
        }
    }

    private void CheckBuyBtnState()
    {
        long currentAmount = inventory.currenciesDataList.Find(x => x.type == _recreationAreaData.initialCost.type).amount;
        if (currentAmount >= _recreationAreaData.initialCost.amount)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }


    public override void OnBuyBtnPressed()
    {
        inventory.OnItemBought(_recreationAreaData.guid, AttractionType.RECREATION_AREA, 1);
        inventory.SpendCurrency(_recreationAreaData.initialCost);
        amountText.text = inventory.productDataList.Find(x => x.guid == _recreationAreaData.guid).count.ToString();
    }

    public override void OnUpgradeBtnPressed()
    {

    }

    public override void OnInfoBtnPressed()
    {
        AtractionPopupData popupData = new AtractionPopupData { guid = guid };
        PopupManager.Instance.OpenPopup(PopupType.RECREATION_AREA_INFO, popupData);
    }

    //no automatization (no emplyer) we do things manual by tapping the entire element (minus the other buttons :)))
    public override void OnTap()
    {
        switch (_recreationAreaData.state)
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
                    _recreationAreaData.state = ItemState.IDLE;
                    OnBuyBtnPressed();
                    InitUIElements();

                    //ifatuTODO - unlock item -> buy one - the initial price or the unlocke price condition must be withdraw
                }
                break;
            case ItemState.IDLE:// do nothing
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
