using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AllDataAsset", menuName = "Data/AllDataAsset")]
public class AllDataAssets : ScriptableObject
{
    public AttractionsDataAsset attractionsSO;
    public FameDataSO fameSO;
    public GameDataAsset gameSO;
    public UserInventory userInventorySO;
    public UserFame userFameSO;
}
