using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserFame", menuName = "Data/UserFame")]
public class UserFame : ScriptableObject
{
    public Fame currentFame;

    public void IncreaseFameLevel()
    {
        currentFame.fameLevel++;
    }

    public void IncreaseExperiencePoint(double value)
    {
        currentFame.experiencePoints += value;
    }

    public double GetExperiencePointsForNextLevel()
    {
        Fame nextFame = GameManager.Instance.GetFameData().fameLevels.Find(a => a.fameLevel == currentFame.fameLevel + 1);
        if (nextFame != null)
        {
            return nextFame.experiencePoints;
        }
        Debug.LogError("FameDataSO - no next fame level found. Please investigate.");
        return -1;
    }

    public double GetExperiencePointsNeededToLevelUp()
    {
        Fame nextFame = GameManager.Instance.GetFameData().fameLevels.Find(a => a.fameLevel == currentFame.fameLevel + 1);
        if (nextFame != null)
        {
            return nextFame.experiencePoints - currentFame.experiencePoints;
        }
        Debug.LogError("FameDataSO - no next fame level found. Please investigate.");
        return -1;
    }

    public float GetPercentageOfCurrentLevel()
    {
        Fame nextFame = GameManager.Instance.GetFameData().fameLevels.Find(a => a.fameLevel == currentFame.fameLevel + 1);
        if (nextFame != null)
        {
            return (float)(currentFame.experiencePoints / nextFame.experiencePoints);
        }
        Debug.LogError("FameDataSO - no next fame level found. Please investigate.");
        return -1;
    }

    public double GetCurrentFameExperiencePoints()
    {
        return currentFame.experiencePoints;
    }

    public int GetCurrentFameLavel()
    {
        return currentFame.fameLevel;
    }
}
