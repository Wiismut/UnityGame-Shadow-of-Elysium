using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PatienceLevel_Lvl3 : MonoBehaviour {
    public float Patience_Level_Lvl3 = 1;
    public Slider patienceSlider_Lvl3;
    public Image patienceImage_Lvl3;
    private Squid_Dialog dialog_Squid;
    private Pele_Dialog dialog_Pele;
    private void Start()
    {

        patienceSlider_Lvl3.value = Patience_Level_Lvl3;
        UpdatePatienceImageColor();
        dialog_Squid = FindObjectOfType<Squid_Dialog>();

        if (dialog_Squid == null)
        {
            Debug.LogError("Squid_Dialog не определен");
        }

        dialog_Pele = FindObjectOfType<Pele_Dialog>();

        if (dialog_Pele == null)
        {
            Debug.LogError("Pele_Dialog не определен");
        }


    }


    private void IncreasePatience()
    {
        if (Patience_Level_Lvl3 < 1)
        {
            Patience_Level_Lvl3 += 0.25f;
            Patience_Level_Lvl3 = Mathf.Min(Patience_Level_Lvl3, 1);
            Debug.Log("Уровень терпимости увеличен до: " + Patience_Level_Lvl3);
            UpdateSliderAndImage();
        }
        else
        {
            Debug.Log("Уровень терпимости уже максимальный: " + Patience_Level_Lvl3);
        }
    }

    private void DecreasePatience()
    {
        if (Patience_Level_Lvl3 > 0)
        {
            Patience_Level_Lvl3 -= 0.25f;
            Patience_Level_Lvl3 = Mathf.Max(Patience_Level_Lvl3, 0);
            Debug.Log("Уровень терпимости уменьшен до: " + Patience_Level_Lvl3);
            UpdateSliderAndImage();
        }
        else
        {
            Debug.Log("Уровень терпимости уже минимальный: " + Patience_Level_Lvl3);
        }

        if (Patience_Level_Lvl3 <= 0)
        {
            Debug.Log("Переход к сцене поражения.");
            LoadLossScene();
        }
    }


    private void LoadLossScene()
    {


        int selectedPlayer = PlayerPrefs.GetInt("SelectPlayer", 0);
        string lossSceneName = selectedPlayer == 0 ? "LossPatienceMale" : "LossPatienceFemale";
        SceneManager.LoadScene(lossSceneName);

    }


    private void UpdateSliderAndImage()
    {
        if (patienceSlider_Lvl3 == null)
        {
            Debug.LogError("patienceSlider_Lvl3 не привязан!");
            return;
        }
        patienceSlider_Lvl3.value = Patience_Level_Lvl3;

        if (patienceImage_Lvl3 == null)
        {
            Debug.LogError("patienceImage_Lvl3 не привязан!");
            return;
        }
        UpdatePatienceImageColor();
    }


    private void UpdatePatienceImageColor()
    {
        if (patienceImage_Lvl3 == null) return;

        Color newColor;
        if (Patience_Level_Lvl3 == 1)
            newColor = new Color32(50, 205, 50, 255);
        else if (Patience_Level_Lvl3 >= 0.75f)
            newColor = new Color32(255, 215, 0, 255);
        else if (Patience_Level_Lvl3 >= 0.5f)
            newColor = new Color32(255, 99, 71, 255);
        else
            newColor = new Color32(178, 34, 34, 255);

        patienceImage_Lvl3.color = newColor;
    }

    public void OnButtonClick(TMP_Text buttonText)
    {


        if ((buttonText.text == "Благодарю Вас!") || (buttonText.text == "Thank you!"))
        {
            Debug.Log("Переход к функции по тексту: " + buttonText.text);
            Debug.Log("терпимость повышена");
            IncreasePatience();
        }
        else if ((buttonText.text == "Не очень то Вы храбры") || (buttonText.text == "You're not very brave."))
        {
            Debug.Log("терпимость понижена");

           DecreasePatience();
        }
        else if( (buttonText.text == "/взглянуть на карту/") || (buttonText.text == "/take a look at the map/"))
        {
            if (dialog_Squid == null)
            {
                Debug.LogError("dialog_Squid не найден!");
            }
            else
            {

                dialog_Squid.HideDialog();
            }

        }




        if ((buttonText.text == "Здравствуйте!") || (buttonText.text == "Hello!"))
        {
            Debug.Log("Переход к функции по тексту: " + buttonText.text);
            Debug.Log("терпимость повышена");
            IncreasePatience();
        }
        else if ((buttonText.text == "Извините...") || (buttonText.text == "Excuse me..."))
        {
            Debug.Log("терпимость понижена");

            DecreasePatience();
        }
        else if ((buttonText.text == "/уйти/") || (buttonText.text == "Кокосы? Нужно проверить") || (buttonText.text == "/leave/") || (buttonText.text == "Coconuts? I need to check it out"))
        {
            if (dialog_Pele == null)
            {
                Debug.LogError("dialog_Pele не найден!");
            }
            else
            {

                dialog_Pele.HideDialog();
            }

        }








    }

}

