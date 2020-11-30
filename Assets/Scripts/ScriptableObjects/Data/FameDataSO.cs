using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Fame
{
    public int fameLevel = 1;
    public double experiencePoints = 0;
}

[CreateAssetMenu(fileName ="FameDataSO", menuName = "Data/FameData")]
public class FameDataSO : ScriptableObject
{
    private Fame currentFame;
    public List<Fame> fameLevels = new List<Fame>();
    public Fame CurrentFame { get { return currentFame; } }

    public void IncreaseFameLevel()
    {
        CurrentFame.fameLevel++;
    }

    public void IncreaseExperiencePoint(double value)
    {
        CurrentFame.experiencePoints += value;
    }

    public double GetExperiencePointsForNextLevel()
    {
        Fame nextFame = fameLevels.Find(a => a.fameLevel == CurrentFame.fameLevel + 1);
        if (nextFame != null)
        {
            return nextFame.experiencePoints;
        }
        Debug.LogError("FameDataSO - no next fame level found. Please investigate.");
        return -1;
    }

    public double GetExperiencePointsNeededToLevelUp()
    {
        Fame nextFame = fameLevels.Find(a => a.fameLevel == CurrentFame.fameLevel + 1);
        if (nextFame != null)
        {
            return nextFame.experiencePoints - CurrentFame.experiencePoints;
        }
        Debug.LogError("FameDataSO - no next fame level found. Please investigate.");
        return -1;
    }

    public double GetCurrentFameExperiencePoints()
    {
        return CurrentFame.experiencePoints;
    }

    public int GetCurrentFameLavel()
    {
        return CurrentFame.fameLevel;
    }
}
