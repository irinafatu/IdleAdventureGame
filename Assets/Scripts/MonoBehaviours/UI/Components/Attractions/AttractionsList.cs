using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttractionsList : MonoBehaviour
{
    AttractionsDataAsset attractionsDataAsset;


    public AttractionType type;
    public AttractionsListSO dataSO;
    public Transform listParent;
    public void Init(AttractionType typeParam, List<UserProductData> userProductDataList)
    {
        type = typeParam;
        attractionsDataAsset = GameManager.Instance.GetAttractionsData();
        AttractionsListData data = dataSO.listData.Find(x => x.type == type);
        if (data == null)
        {
            return; // todo add error?
        }

        switch (type)
        {
            case AttractionType.RIDE:
                {
                    InitRideList(data, userProductDataList);
                }
                break;
            case AttractionType.FOOD_AND_BEVERAGE:
                {
                    InitFoodAndBeverageList(data, userProductDataList);
                }
                break;
            case AttractionType.RECREATION_AREA:
                {
                    InitRecreationAreaList(data, userProductDataList);
                }
                break;
            default:
                break;
        }

    }


    private void InitRideList(AttractionsListData data, List<UserProductData> userProductDataList)
    {
        transform.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        AttractionDataAssetItem assetDataItem = attractionsDataAsset.attractionsDataAsset.Find
(a => a.gameEventType == attractionsDataAsset.currentEvent);
        List<RideData> rideDataList = assetDataItem.rideData;
        foreach (UserProductData userData in userProductDataList)
        {
            GameObject gameObj = Instantiate(data.prefab);
            RideElementUI rideElementUI = gameObj.GetComponent<RideElementUI>();
            RideData rideDta = rideDataList.Find(a => a.GetGUID() == userData.guid);
            rideElementUI.Init(rideDta);

            gameObj.transform.SetParent(listParent, false);
        }
    }

    private void InitFoodAndBeverageList(AttractionsListData data, List<UserProductData> userProductDataList)
    {
        transform.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        AttractionDataAssetItem assetDataItem = attractionsDataAsset.attractionsDataAsset.Find
(a => a.gameEventType == attractionsDataAsset.currentEvent);
        List<FoodAndBeverageData> foodAndBeverageDataList = assetDataItem.foodAndBeverageData;
        foreach (UserProductData userData in userProductDataList)
        {
            GameObject gameObj = Instantiate(data.prefab);
            FoodAndBeverageElementUI foodAndBeverageElementUI = gameObj.GetComponent<FoodAndBeverageElementUI>();
            foodAndBeverageElementUI.Init(foodAndBeverageDataList.Find(a => a.guid == userData.guid));

            gameObj.transform.SetParent(listParent, false);
        }
    }

    private void InitRecreationAreaList(AttractionsListData data, List<UserProductData> userProductDataList)
    {
        transform.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        AttractionDataAssetItem assetDataItem = attractionsDataAsset.attractionsDataAsset.Find
(a => a.gameEventType == attractionsDataAsset.currentEvent);
        List<RecreationAreaData> recreationAreaDataList = assetDataItem.recreationAreaData;
        foreach (UserProductData userData in userProductDataList)
        {
            GameObject gameObj = Instantiate(data.prefab);
            RecreationAreaElementUI recreationAreaElementUI = gameObj.GetComponent<RecreationAreaElementUI>();
            recreationAreaElementUI.Init(recreationAreaDataList.Find(a => a.guid == userData.guid));

            gameObj.transform.SetParent(listParent, false);
        }
    }

}
