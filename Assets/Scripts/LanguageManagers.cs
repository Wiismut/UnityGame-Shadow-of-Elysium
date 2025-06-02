using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageManagers : MonoBehaviour
{
    private bool _isLoading = false;

    void Start()
    {
        string savedLang = PlayerPrefs.GetString("lang", "en");
        //string savedLang = PlayerPrefs.GetString("lang", "ru");
        SetLanguage(savedLang);
    }

    public void SetLanguage(string langCode)
    {
        if (_isLoading) return;
        _isLoading = true;
        var locales = LocalizationSettings.AvailableLocales.Locales;
        var locale = locales.Find(l => l.Identifier.Code == langCode);
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
            PlayerPrefs.SetString("lang", langCode);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning($"Locale with code '{langCode}' not found!");
        }

        _isLoading = false;
    }
}
