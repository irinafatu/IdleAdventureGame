using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public enum TabItemState
{
    LOCKED,
    UNLOCKED,
    IDLE,
    HIGHLIGHT,
    UPDATE_TO_HIGHLIGHT,
}



public class AttractionsTab : MonoBehaviour
{
    public AttractionsTabSO dataSO;
    public UnityEngine.UI.Image icon;
    public UnityEngine.UI.Image backgroundImage;
    public Text name;
    [HideInInspector]
    public AttractionType type;
    public Color idleColor;
    public Color highlightColor;
    public Color lockedColor;
    public GameObject lockObj;

    public GameEvent onTabChangedGameEvent;
    TabItemState currentState = TabItemState.IDLE;
    public TabItemState CurrentState { get { return currentState; } }


    //public delegate void ActionClick();
    //public event ActionClick onClick ;

    //public void ButtonClick()
    //{
    //    if (onClick != null)
    //    {
    //        onClick();
    //    }
    //}

    public void Init(AttractionType typeParam)
    {
        type = typeParam;
        AttractionTabData data = dataSO.dataList.Find(x => x.type == type);
        if (data == null)
        {
            return; // todo add error?
        }
        icon.sprite = data.icon;

        name.GetComponent<LocalizationComponent>().SetTheLocalizedText(data.Name);
       //ifatu - todo - remove hardcodation very ugly
        //if (type == AttractionType.RIDE)
        //{
        //    currentState = TabItemState.HIGHLIGHT;
        //    backgroundImage.color = highlightColor;
        //}

    }

    public void OnItemPressed()
    {
        switch (currentState)
        {
            case TabItemState.LOCKED:
                {
                    lockObj.GetComponent<Animator>().SetTrigger("Shake");
                    //show locked anim text and play shake lock animation
                    break;
                }
            case TabItemState.IDLE:
                {
                    currentState = TabItemState.UPDATE_TO_HIGHLIGHT;
                    onTabChangedGameEvent.Raise();
                }
                break;
            case TabItemState.HIGHLIGHT:
            case TabItemState.UPDATE_TO_HIGHLIGHT:
                break;
            default:
                break;
        }
      
    }
    public void ChangeCurrentState(TabItemState newState)
    {
        currentState = newState;
        lockObj.SetActive(currentState == TabItemState.LOCKED);
        switch (currentState)
        {
            case TabItemState.LOCKED:
                {
                    backgroundImage.color = lockedColor;
                }
                break;
            case TabItemState.IDLE:
                {
                    backgroundImage.color = idleColor;
                }
                break;
            case TabItemState.HIGHLIGHT:
                {
                    backgroundImage.color = highlightColor;
                }
                break;
            case TabItemState.UPDATE_TO_HIGHLIGHT:
                break;
            default:
                break;
        }
    }

    public bool ChangeCurrentState()
    {

        switch (currentState)
        {
            case TabItemState.LOCKED:
            case TabItemState.IDLE:
                return false;
            case TabItemState.HIGHLIGHT:
                {
                    currentState = TabItemState.IDLE;
                    backgroundImage.color = idleColor;
                    return false;
                }
            case TabItemState.UPDATE_TO_HIGHLIGHT:
                {
                    //todo  change ui
                    currentState = TabItemState.HIGHLIGHT;
                    backgroundImage.color = highlightColor;
                    return true;
                }
            default:
                break;
        }

        return false;
    }
    
}
