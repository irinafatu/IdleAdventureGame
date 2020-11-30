using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

//TODO Create a scriptable Object for User data relative stuff - icon, per business level...!
//TODO - Add custom drawer 
[Serializable]
public class BusinessLevel
{
    public string Name;
    public enum LevelType { NOUBIE, ROCKET, MOGUL};
    public LevelType currentLevel;
    public Image icon;

    
    public BusinessLevel()
    {
        Name = currentLevel.ToString();
    }

    //TODO o lista de perks pentru fiecare level 
}


public class User : MonoSingleton<User>
{
    public string username = "BusinessMogul";
    public BusinessLevel businessLevel;
    
    //TODO se verifca existenta fisierului de save -> daca exista se face load/ daca nu exista se creeaza un user nou
    public void CreateNewUser()
    {
        username = "BusinessMogul";
        businessLevel = new BusinessLevel { currentLevel = BusinessLevel.LevelType.NOUBIE };

    }

}
