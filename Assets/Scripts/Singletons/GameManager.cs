using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
	public AllDataAssets dataAsset;
	public GameObject[] SystemPrefabs;
	private List<GameObject> _instancedSystemPrefabs;

	[Header("DEBUG")]
	public DebugScenarios debugScenarios;

	private MenuType menuTypeToOpenOnLoadComplete = MenuType.NONE;
	private void Start()
	{
		DontDestroyOnLoad(gameObject);
		_instancedSystemPrefabs = new List<GameObject>();
		InstantiateSystemPrefabs();

		OnApplicationStart();
		debugScenarios.SetupScenario();
	}


	private void InstantiateSystemPrefabs()
	{
		GameObject prefabInstance;
		for (int i = 0; i < SystemPrefabs.Length; i++)
		{
			prefabInstance = Instantiate(SystemPrefabs[i]);
			_instancedSystemPrefabs.Add(prefabInstance);
		}
	}

	//Distrugerea unei liste de prefaburi instantiate
	private void OnDestroy()
	{
		
		for (int i = 0; i < _instancedSystemPrefabs.Count; i++)
		{
			Destroy(_instancedSystemPrefabs[i]);
		}
		_instancedSystemPrefabs.Clear();
	}
	//todo - make it private and set an event
	//todo - rmove old scene
	public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Additive, MenuType pMenuTypeToOpenOnLoadComplete = MenuType.NONE)
	{
		AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, mode);
		ao.completed += OnLoadOperationComplete;
		menuTypeToOpenOnLoadComplete = pMenuTypeToOpenOnLoadComplete;
	}

	private void OnLoadOperationComplete(AsyncOperation op)
	{
		if (menuTypeToOpenOnLoadComplete != MenuType.NONE)
		{
			MenuManager.Instance.OpenMenu(menuTypeToOpenOnLoadComplete, true);
		}
	}

    #region STARTUP_APPLICATION

	public void OnApplicationStart()
	{
		//load the LoadingScreenMenu
		MenuManager.Instance.OpenMenu(MenuType.LOADING_MENU, true);
	}


    #endregion

    #region DEBUG
	
	public void Debug_NewUserScenario()
	{
		AttractionDataAssetItem assetItem = GetAttractionsData().GetCurrentEventAssets();
		GetUserInventory().productDataList.Clear();
		List<RideData> freeRideUnlockedItems = assetItem.rideData.FindAll(a => a.unlockConditionList.Find(x => x.unlockConditionType == UnlockConditionType.FREE) != null);
		foreach (RideData ride in freeRideUnlockedItems)
		{
			bool isUnlocked = UnlockManager.Instance.CheckUnlockConditions(ride.unlockConditionList);
			if (isUnlocked)
				GetUserInventory().OnItemBought(ride.guid, AttractionType.RIDE, 1);
		}

		List<FoodAndBeverageData> freeFABUnlockedItems = assetItem.foodAndBeverageData.FindAll(a => a.unlockConditionList.Find(x => x.unlockConditionType == UnlockConditionType.FREE) != null);
		foreach (FoodAndBeverageData ride in freeFABUnlockedItems)
		{
			bool isUnlocked = UnlockManager.Instance.CheckUnlockConditions(ride.unlockConditionList);
			if (isUnlocked)
				GetUserInventory().OnItemBought(ride.guid, AttractionType.FOOD_AND_BEVERAGE, 1);
		}

		List<RecreationAreaData> freeRAUnlockedItems = assetItem.recreationAreaData.FindAll(a => a.unlockConditionList.Find(x => x.unlockConditionType == UnlockConditionType.FREE) != null);
		foreach (RecreationAreaData ride in freeRAUnlockedItems)
		{
			bool isUnlocked = UnlockManager.Instance.CheckUnlockConditions(ride.unlockConditionList);
			if (isUnlocked)
				GetUserInventory().OnItemBought(ride.guid, AttractionType.RECREATION_AREA, 1);
		}
	}

	public void Debug_UnlockAllItems()
	{
		AttractionDataAssetItem assetItem = GetAttractionsData().GetCurrentEventAssets();
		GetUserInventory().productDataList.Clear();
		foreach (RideData ride in assetItem.rideData)
		{
			GetUserInventory().OnItemBought(ride.guid, AttractionType.RIDE, 15001);
		}
		foreach (FoodAndBeverageData foodAndBeverage in assetItem.foodAndBeverageData)
		{
			GetUserInventory().OnItemBought(foodAndBeverage.guid, AttractionType.FOOD_AND_BEVERAGE, 22009);
		}
		foreach (RecreationAreaData recreationArea in assetItem.recreationAreaData)
		{
			GetUserInventory().OnItemBought(recreationArea.guid, AttractionType.RECREATION_AREA, 3500);
		}
	}

	public void Debug_AddCurrency(List<CurrencyAmount> currencies)
	{
		foreach(CurrencyAmount currency in currencies)
		{
			GetUserInventory().DEBUG_SetCurrency(currency);
		}
	}
	#endregion

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PopupManager.Instance.OpenPopup(PopupType.EXIT_GAME, null);
		}
	}

	public AttractionsDataAsset GetAttractionsData()
	{
		return dataAsset.attractionsSO;
	}

	public UserInventory GetUserInventory()
	{
		return dataAsset.userInventorySO;
	}

	public FameDataSO GetFameData()
	{
		return dataAsset.fameSO;
	}

	public GameDataAsset GetGameData()
	{
		return dataAsset.gameSO;
	}

	public UserFame GetUserFame()
	{
		return dataAsset.userFameSO;
	}
}
