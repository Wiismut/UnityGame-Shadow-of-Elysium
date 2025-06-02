using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageRUEN : MonoBehaviour {
    public void SetEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales
            .Find(locale => locale.Identifier.Code == "en");
    }

    public void SetRussian()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales
            .Find(locale => locale.Identifier.Code == "ru");
    }
}
