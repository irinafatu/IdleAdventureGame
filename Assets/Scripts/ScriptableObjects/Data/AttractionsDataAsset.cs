using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public enum GameEventType
{
    DEFAULT,
    CHRISTMAS,
    EASTER
}
[Serializable]
public class AttractionDataAssetItem
{
    public GameEventType gameEventType;
    public List<RideData> rideData;
    public List<FoodAndBeverageData> foodAndBeverageData;
    public List<RecreationAreaData> recreationAreaData;

    public BasicAtractionData GetBasicAttractionForGUID(Guid guid)
    {
        BasicAtractionData attraction = rideData.Find(a => a.guid == guid);
        if (attraction == null)
            attraction = foodAndBeverageData.Find(a => a.guid == guid);
        if (attraction == null)
            attraction = recreationAreaData.Find(a => a.guid == guid);
        return attraction;
    }

}

[CreateAssetMenu(fileName = "AttractionDataAsset", menuName = "Data/Asset/AttractionDataAsset")]
public class AttractionsDataAsset : ScriptableObject
{

    public GameEventType currentEvent = GameEventType.DEFAULT;
    public List<AttractionDataAssetItem> attractionsDataAsset = new List<AttractionDataAssetItem>();

    public AttractionDataAssetItem GetCurrentEventAssets()
    {
        return attractionsDataAsset.Find(x => x.gameEventType == currentEvent);
    }
}
