using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoSingleton<PopupManager>
{
    //Dont destroy on load
    public PopupDataList _popupDataList;
    private Canvas _canvas; //todo clean this part later when everything works fine
    [HideInInspector]
    private List<BasePopup> _popupStack = new List<BasePopup>();
    [HideInInspector]
    public BasePopup _currentPopup;


    public void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    public void OpenPopup(PopupType popupType, BasePopupData data/*,bool isSingleOnScreen = false*/)
    {
        _canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        PopupData popupData = _popupDataList.popupDataList.Find(x => x.popupType == popupType);
        if (popupData == null)
        {
            Debug.LogError("PopupManager:OpenPopup The popup " + popupType.ToString() + "you are trying to open doesn`t exist.");
            return;
        }

        // if (isSingleOnScreen)
        //{
        //   CloseAllMenus();
        //}
        //ifatuTODO - check if this is needed or corect
        //Destroy(_currentMenu);
        //_currentMenu = null;
        _currentPopup = Instantiate(popupData.popupPrefab, transform.position, Quaternion.identity);
        _currentPopup.Init(data);
        _currentPopup.transform.SetParent(_canvas.transform, false);

        //   _currentMenu.transform.position = new Vector3(0, 0, 0);
        // _currentMenu.transform.localScale = new Vector3(1, 1, 1);
        _popupStack.Add(_currentPopup);

    }

   /* public void CloseAllMenus()
    {

        for (int i = 0; i < _menuStack.Count; i++)
        {
            Destroy(_menuStack[i]);
        }
        _menuStack.Clear();
    }
    */
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
