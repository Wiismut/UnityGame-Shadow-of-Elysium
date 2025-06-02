using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PatienceLevel_Lvl1 : MonoBehaviour {
    public float Patience_Level = 1;
    public Slider patienceSlider;
    public Image patienceImage;
    public GameObject objectToToggleGeisha;
    public GameObject objectToToggKitsune;
    private Dialog dialog;
    private Tanuki_Dialog dialog_Tanuki;
    private Kitsune_Dialog dialog_Kitsune;
    private Geisha_Dialog dialog_Geisha;
    private void Start()
    {
        patienceSlider.value = Patience_Level;
        UpdatePatienceImageColor();
        objectToToggleGeisha.SetActive(false);
        objectToToggKitsune.SetActive(false);
        dialog = FindObjectOfType<Dialog>();
        dialog_Tanuki = FindObjectOfType<Tanuki_Dialog>();
        dialog_Kitsune = FindObjectOfType<Kitsune_Dialog>();
        dialog_Geisha = FindObjectOfType<Geisha_Dialog>();
        if (dialog == null)
        {
            Debug.LogError("Dialog не найден");
        }
        if (dialog_Tanuki == null)
        {
            Debug.LogError("диалог не найден");
        }
    }

    public void PatienceMAX() {
        Patience_Level = 1;
        UpdateSliderAndImage(); 
        Debug.Log("Терпимость восстановлена до максимума: " + Patience_Level);
    }

    private void IncreasePatience()
    {
        if (Patience_Level < 1)
        {
            Patience_Level += 0.25f;
            Patience_Level = Mathf.Min(Patience_Level, 1);
            UpdateSliderAndImage();
            Debug.Log("Уровень терпимости повышен: " + Patience_Level);
        }
    }

    private void DecreasePatience()
    {
        if (Patience_Level > 0)
        {
            Patience_Level -= 0.25f;
            Patience_Level = Mathf.Max(Patience_Level, 0);
            UpdateSliderAndImage();
            Debug.Log("Уровень терпимости понижен: " + Patience_Level);
        }
        if (Patience_Level <= 0)
        {
            LoadLossScene();
        }
    }

    private void LoadLossScene()
    {
        int selectedPlayer = PlayerPrefs.GetInt("SelectPlayer", 0);
        string lossSceneName = selectedPlayer == 0 ? "LossPatienceMale" : "LossPatienceFemale";
        SceneManager.LoadScene(lossSceneName);
    }

    public void ToggleObjectVisibilityGeisha()
    {
        if (objectToToggleGeisha != null)
        {
            objectToToggleGeisha.SetActive(!objectToToggleGeisha.activeSelf);
        }
        else
        {
            Debug.LogWarning("Object to toggle is not assigned!");
        }
    }

    public void ToggleObjectVisibilityKitsune()
    {
        if (objectToToggKitsune != null)
        {
            objectToToggKitsune.SetActive(!objectToToggKitsune.activeSelf);
        }
        else
        {
            Debug.LogWarning("Object to toggle is not assigned!");
        }
    }

    private void UpdateSliderAndImage()
    {
        if (patienceSlider != null)
        {
            patienceSlider.value = Patience_Level;
        }
        UpdatePatienceImageColor();
    }

    private void UpdatePatienceImageColor()
    {
        if (patienceImage == null) return;

        Color newColor;
        if (Patience_Level == 1)
            newColor = new Color32(50, 205, 50, 255);
        else if (Patience_Level >= 0.75f)
            newColor = new Color32(255, 215, 0, 255);
        else if (Patience_Level >= 0.5f)
            newColor = new Color32(255, 99, 71, 255);
        else
            newColor = new Color32(178, 34, 34, 255);

        patienceImage.color = newColor;
    }

    public void OnButtonClick(TMP_Text buttonText)
    {
        if ((buttonText.text == "Хэй!") || (buttonText.text == "Привет?") || (buttonText.text == "Hi?") || (buttonText.text == "Hey!"))
        {
            DecreasePatience();
        }
        else if ((buttonText.text == "Ок..") || (buttonText.text == "Ok.."))
        {
            if (dialog != null)
            {
                dialog.HideDialog();
            }
        }







        else if ((buttonText.text == "Ну и ну, дух-енот") || (buttonText.text == "У меня нет на это времени") || (buttonText.text == "Lol spirit-raccoon") || (buttonText.text == "I don't have time for this"))
        {
            DecreasePatience();
        }
        else if ((buttonText.text == "Конечно, давай!") || (buttonText.text == "Sure!"))
        {
            IncreasePatience();
        }
        else if ((buttonText.text == "Ух ты!") || (buttonText.text == "Подумаешь...") || (buttonText.text == "WOW") || (buttonText.text == "Never mind.."))
        {
            if (dialog_Tanuki != null)
            {
                dialog_Tanuki.HideDialog();
            }
        }
        else if ((buttonText.text == "Можно хвосты потрогать??") || (buttonText.text == "Can I touch the tails??"))
        {
            DecreasePatience();
        }
        else if ((buttonText.text == "...") || (buttonText.text == "Ух ты! Спасибо!") || (buttonText.text == "Wow! Thanks!"))
        {
            if (dialog_Kitsune != null)
            {
                ToggleObjectVisibilityKitsune();
                dialog_Kitsune.HideDialog();
            }
        }
        else if ((buttonText.text == "Не в моем вкусе, извините...") || (buttonText.text == "Not my jam, sorry..."))
        {
            DecreasePatience();
        }
        else if ((buttonText.text == "Ух ты! Она замечательна.") || (buttonText.text == "Wow! It's wonderful."))
        {
            IncreasePatience();
            ToggleObjectVisibilityGeisha();
        }
        else if ((buttonText.text == "Спасибо!") || (buttonText.text == "Ну..удачи") || (buttonText.text == "Thanks!") || (buttonText.text == "Well.."))
        {
            if (dialog_Geisha != null)
            {
                dialog_Geisha.HideDialog();
            }
        }
    }
}

