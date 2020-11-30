using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LocalizationManager : MonoSingleton<LocalizationManager>
{
    public LocalizationSO localizationSO;
    //TODO - make a way for the currentLAnguage to change either by geting the device language or by 
    //selecting a diferent one from the game
    //make currentLanguage private
    public Language currentLanguage = Language.ENGLISH;

    public string GetTextForKey(string key)
    {
        LocalizationList item = localizationSO.listData.Find(a => a.key == key);
        if (item == null) return null;
        LocalizationValue itemValue = item.value.Find(a => a.language == currentLanguage);
        if (itemValue == null) return null;
        return itemValue.text;
    }
}
