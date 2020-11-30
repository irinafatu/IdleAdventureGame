using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UserButton : MonoBehaviour
{
    public UnityEngine.UI.Image icon;
    public Text fameLevel;
    public UnityEngine.UI.Image fillImage;
    UserFame userFame;

    // Start is called before the first frame update
    void Start()
    {
        userFame = GameManager.Instance.GetUserFame();
        fameLevel.text = userFame.GetCurrentFameLavel().ToString();
        fillImage.fillAmount = userFame.GetPercentageOfCurrentLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUserButtonPressed()
    {
        UserPopupData userPopupData = new UserPopupData
        {
            fameLevel = userFame.GetCurrentFameLavel(),
            fameLevelPercentage = userFame.GetPercentageOfCurrentLevel(),
            title = "Nu STIU",
            iconSprite = icon.sprite,
            currentFamePoints = userFame.GetCurrentFameExperiencePoints(),
            nextLevelFamePoints = userFame.GetExperiencePointsForNextLevel()
        };
        PopupManager.Instance.OpenPopup(PopupType.USER_INFO, userPopupData);
    }
}
