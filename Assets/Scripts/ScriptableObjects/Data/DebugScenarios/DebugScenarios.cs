using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebugScenarioType
{
    NEW_USER = 0,
    ALL_UNLOCKED = 1,
    NEW_USER_RICH = 2,

    CUSTOM = 100,
    NONE = 999,
}

[Serializable]
public class DebugScenario
{
    public string name;
    public DebugScenarioType scenarioType;
    public List<CurrencyAmount> initialCurrencies;

}


[CreateAssetMenu(fileName ="DebugScenarios", menuName = "DEBUG/DebugScenarios")]
public class DebugScenarios : ScriptableObject
{
    public DebugScenarioType currentScenario = DebugScenarioType.NONE;
    public List<DebugScenario> scenarios = new List<DebugScenario>();

    public void SetupScenario()
    {
        switch (currentScenario)
        {
            case DebugScenarioType.NEW_USER: //all items locked but one
            case DebugScenarioType.NEW_USER_RICH:  //all items unlocked but one
                {
                    DebugScenario scenario = scenarios.Find(a => a.scenarioType == currentScenario);
                    GameManager.Instance.Debug_NewUserScenario();
                    GameManager.Instance.Debug_AddCurrency(scenario.initialCurrencies);
                }
                break;
            case DebugScenarioType.ALL_UNLOCKED:  //all items unlocked but one
                {
                    DebugScenario scenario = scenarios.Find(a => a.scenarioType == currentScenario);
                    GameManager.Instance.Debug_UnlockAllItems();
                    GameManager.Instance.Debug_AddCurrency(scenario.initialCurrencies);
                }
                break;
            case DebugScenarioType.CUSTOM:
                break;
            case DebugScenarioType.NONE:
                break;
            default:
                break;
        }
    }

}
