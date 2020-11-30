using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AttractionItem : MonoBehaviour
{
    AttractionsTab attractionsTab;
    AttractionsList attractionsList;
    BasicAtractionData attractionData;//ifatu TODO? is this used? Necesary???

    //public delegate void ActionClick(AttractionType type);
    //public event ActionClick onClick;


    public AttractionType type;

    void OnEnable()
    {
        //if (attractionsTab != null)
        //    attractionsTab.onClick += OnTabClicked;
    }

    //public void OnTabClicked()
    //{
    //    //attractionsList.gameObject.SetActive(true);
    //    onClick(type);
    //}



    private void OnDisable()
    {
        //attractionsTab.onClick -= OnTabClicked;
    }

    public void Init(
        AttractionType typeParam,
        TabItemState state,
        BasicAtractionData attractionDataParam,
        Transform tabParent,
        Transform listParent,
        AttractionsTab tabObj,
        AttractionsList listObj,
        List<UserProductData> userProductDataList)
    {
        type = typeParam;
        attractionData = attractionDataParam;

        attractionsTab = Instantiate<AttractionsTab>(tabObj, Vector3.zero, Quaternion.identity);
        attractionsTab.transform.SetParent(tabParent, false);
        attractionsTab.Init(type);
        //attractionsTab.onClick += OnTabClicked;

        attractionsList = Instantiate<AttractionsList>(listObj, Vector3.zero, Quaternion.identity);
        attractionsList.transform.SetParent(listParent, false);
        attractionsList.Init(type, userProductDataList);
        attractionsList.gameObject.SetActive(false);
        SetUIState(state);

    }

    public void SetUIState(TabItemState state)
    {
        attractionsTab.ChangeCurrentState(state);
        attractionsList.gameObject.SetActive(state == TabItemState.HIGHLIGHT);
    }

    public void OnTabChanged()
    {
        bool isVisible = attractionsTab.ChangeCurrentState();
        attractionsList.gameObject.SetActive(isVisible);
    }
}


public class AttractionsUI : MonoBehaviour
{
    public Transform tabParent;
    public Transform listParent;
    public AttractionsTab attractionsTabObj;
    public AttractionsList attractionsListObj;
    BasicAtractionData attractionData;
    UserInventory userInventory;


    public List<AttractionItem> attractionItemList = new List<AttractionItem>();

    public void Start()
    {
        Init();
    }

    void OnEnable()
    {
        //if (attractionItemList != null && attractionItemList.Count > 0)
        //{
        //    foreach (AttractionItem item in attractionItemList)
        //    {
        //        item.onClick += OnTabCliked;
        //    }
        //}
    }

    void OnDisable()
    {
        //if (attractionItemList != null && attractionItemList.Count > 0)
        //{
        //    foreach (AttractionItem item in attractionItemList)
        //    {
        //        item.onClick -= OnTabCliked;
        //    }
        //}
    }

    public void Init()
    {
        userInventory = GameManager.Instance.GetUserInventory();
        foreach (AttractionType type in Enum.GetValues(typeof(AttractionType)))
        {
            List<UserProductData> attractionsList = userInventory.GetAllAttractionsOfType(type);
            AttractionsDataAsset attractionsDataAsset = GameManager.Instance.GetAttractionsData();

            BasicAtractionData nextRide = null;
            TabItemState tabState = TabItemState.IDLE;
            if (type == AttractionType.RIDE)
            {
                tabState = TabItemState.HIGHLIGHT;
            }
            if (attractionsList.Count == 0)
            {
                tabState = TabItemState.LOCKED; //IFATUtodo - this will probably be changed
            }

            List<UserProductData> rideUserItems = attractionsList.FindAll(a => a.type == type);
            if (rideUserItems != null && rideUserItems.Count > 0) // daca ai cel putin un element
            {
                Guid nextElementGuid;
                if (rideUserItems.Count > 0)
                {
                    nextElementGuid = rideUserItems[rideUserItems.Count - 1].guid;
                }
                switch (type)
                {
                    case AttractionType.RIDE:
                        {
                            List<RideData> assetData = attractionsDataAsset.GetCurrentEventAssets().rideData;
                            int lastElementIndex = 0;
                            if (rideUserItems.Count > 0)
                            {
                                 lastElementIndex = assetData.FindIndex(a => a.guid == nextElementGuid);
                                if (assetData.Count > lastElementIndex + 1)
                                {
                                    nextRide = assetData[lastElementIndex + 1];
                                }
                            }
                            else
                            {
                                nextRide = assetData[0];
                            }
                        }
                        break;
                    case AttractionType.FOOD_AND_BEVERAGE:
                        {
                            List<FoodAndBeverageData> assetData = attractionsDataAsset.GetCurrentEventAssets().foodAndBeverageData;
                            int lastElementIndex = 0;
                            if (rideUserItems.Count > 0)
                            {
                                lastElementIndex = assetData.FindIndex(a => a.guid == nextElementGuid);
                                if (assetData.Count > lastElementIndex + 1)
                                {
                                    nextRide = assetData[lastElementIndex + 1];
                                }
                            }
                            else
                            {
                                nextRide = assetData[0];
                            }
                        }
                        break;
                    case AttractionType.RECREATION_AREA:
                        {
                            List<RecreationAreaData> assetData = attractionsDataAsset.GetCurrentEventAssets().recreationAreaData;
                            int lastElementIndex = 0;
                            if (rideUserItems.Count > 0)
                            {
                                lastElementIndex = assetData.FindIndex(a => a.guid == nextElementGuid);
                                if (assetData.Count > lastElementIndex + 1)
                                {
                                    nextRide = assetData[lastElementIndex + 1];
                                }
                            }
                            else
                            {
                                nextRide = assetData[0];
                            }
                        }
                        break;
                    default:
                        break;
                }
                if (nextRide != null)
                {
                    userInventory.OnItemBought(nextRide.guid, type, 0);

                    attractionsList.Add(new UserProductData
                    {
                        count = 0,
                        guid = nextRide.guid,
                        guidString = nextRide.guid.ToString(),
                        level = 0,
                        type = type
                    });
                }
            }

            if (tabState == TabItemState.LOCKED && nextRide != null)
            {
                bool checkNextRideUnlockConditions = UnlockManager.Instance.CheckUnlockConditions(nextRide.unlockConditionList);
                if (checkNextRideUnlockConditions)
                {
                    tabState = TabItemState.UNLOCKED;
                }
            }

            AttractionItem newItem = new AttractionItem();
            newItem.Init(type, tabState, attractionData, tabParent, listParent,
                attractionsTabObj, attractionsListObj, attractionsList);
            //  newItem.onClick += OnTabCliked;
            attractionItemList.Add(newItem);
        }

    }

    public void OnTabChanged()
    {
        foreach (AttractionItem item in attractionItemList)
        {
            item.OnTabChanged();
        }
    }

}
