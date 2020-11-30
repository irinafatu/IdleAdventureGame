using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoSingleton<MenuManager>
{
    //Dont destroy on load
    public MenuDataList _menuDataList;
    [HideInInspector]
    public BaseMenu _currentMenu;
    private Canvas _canvas; //todo clean this part later when everything works fine

    [HideInInspector]
    private List<BaseMenu> _menuStack = new List<BaseMenu>();


    public void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    public void OpenMenu(MenuType menuType, bool isSingleOnScreen = false)
    {
        _canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        MenuData menuData = _menuDataList.menuDataList.Find(x => x.menuType == menuType);
        if (menuData == null)
        {
            Debug.LogError("MenuManager:OpenMenu The menu " + menuType.ToString() + "you are trying to open doesn`t exist.");
            return;
        }

        if (isSingleOnScreen)
        {
            CloseAllMenus();
        }
        //ifatuTODO - check if this is needed or corect
        //Destroy(_currentMenu);
        //_currentMenu = null;
        _currentMenu = Instantiate(menuData.menuPrefab, transform.position, Quaternion.identity);
        _currentMenu.transform.SetParent(_canvas.transform, false);

 

        //   _currentMenu.transform.position = new Vector3(0, 0, 0);
        // _currentMenu.transform.localScale = new Vector3(1, 1, 1);
        _menuStack.Add(_currentMenu);

    }

    public void CloseAllMenus()
    {

        for (int i = 0; i < _menuStack.Count; i++)
        {
            Destroy(_menuStack[i]);
        }
        _menuStack.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
