using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizationComponent : MonoBehaviour
{
    public string localizationKey;
    public bool setOnceOnAwake = false; // dnamic text that changes

    public void Awake()
    {
        if (setOnceOnAwake)
        {
            SetTheLocalizedText();
        }
    }

    public void SetTheLocalizedText(string newLocalizationKey = null)
    {
        Text textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            return;
        }
        string localizedText = LocalizationManager.Instance.GetTextForKey(
            newLocalizationKey != null ? newLocalizationKey : localizationKey);
        if (localizedText == null)
        {
            return;
        }
        textComponent.text = localizedText;
    }

    public void OnEnable()
    {
        if (!setOnceOnAwake)
        {
            SetTheLocalizedText();
        }
    }
}
