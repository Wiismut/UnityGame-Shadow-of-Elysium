using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PatienceLevel_Lvl2 : MonoBehaviour {
    public float Patience_Level_Lvl2 = 1;
    public Slider patienceSlider_Lvl2;
    public Image patienceImage_Lvl2;
    private Mermaid_Dialog dialog_Mermaid;
    private Sirin_Dialog dialog_Sirin;
    private Deer_Dialog dialog_Deer;
    private Leshii_Dialog dialog_Leshii;
    private Cat_Dialog dialog_Cat;
    public GameObject podskaska;
    private void Start()
    {

        podskaska.SetActive(false);
        patienceSlider_Lvl2.value = Patience_Level_Lvl2;
        UpdatePatienceImageColor();
        dialog_Sirin = FindObjectOfType<Sirin_Dialog>();

        if (dialog_Sirin == null)
        {
            Debug.LogError("Sirin_Dialog не определен");
        }
        dialog_Deer = FindObjectOfType<Deer_Dialog>();

        if (dialog_Deer == null)
        {
            Debug.LogError("Deer_Dialog не определен");
        }
        dialog_Mermaid = FindObjectOfType<Mermaid_Dialog>();

        if (dialog_Mermaid == null)
        {
            Debug.LogError("dialog_Mermaid не определен");
        }
        dialog_Leshii = FindObjectOfType<Leshii_Dialog>();

        if (dialog_Leshii == null)
        {
            Debug.LogError("Leshii_Dialog не определен");
        }
        dialog_Cat = FindObjectOfType<Cat_Dialog>();

        if (dialog_Cat == null)
        {
            Debug.LogError("Cat_Dialog не определен");
        }


    }

    public void PodskaskaKot()
    {
        if (podskaska != null)
        {
            podskaska.SetActive(!podskaska.activeSelf);
        }
        else
        {
            Debug.LogWarning("подсказки нет");
        }
    }

    private void IncreasePatience()
    {
        if (Patience_Level_Lvl2 < 1)
        {
            Patience_Level_Lvl2 += 0.25f;
            Patience_Level_Lvl2 = Mathf.Min(Patience_Level_Lvl2, 1);
            Debug.Log("Уровень терпимости увеличен до: " + Patience_Level_Lvl2);
            UpdateSliderAndImage();
        }
        else
        {
            Debug.Log("Уровень терпимости уже максимальный: " + Patience_Level_Lvl2);
        }
    }

    private void DecreasePatience()
    {
        if (Patience_Level_Lvl2 > 0)
        {
            Patience_Level_Lvl2 -= 0.25f;
            Patience_Level_Lvl2 = Mathf.Max(Patience_Level_Lvl2, 0);
            Debug.Log("Уровень терпимости уменьшен до: " + Patience_Level_Lvl2);
            UpdateSliderAndImage();
        }
        else
        {
            Debug.Log("Уровень терпимости уже минимальный: " + Patience_Level_Lvl2);
        }

        if (Patience_Level_Lvl2 <= 0)
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
        if (patienceSlider_Lvl2 == null)
        {
            Debug.LogError("patienceSlider_Lvl2 не привязан");
            return;
        }
        patienceSlider_Lvl2.value = Patience_Level_Lvl2;

        if (patienceImage_Lvl2 == null)
        {
            Debug.LogError("patienceImage_Lvl2 не привязан");
            return;
        }
        UpdatePatienceImageColor();
    }


    private void UpdatePatienceImageColor()
    {
        if (patienceImage_Lvl2 == null) return;

        Color newColor;
        if (Patience_Level_Lvl2 == 1)
            newColor = new Color32(50, 205, 50, 255);
        else if (Patience_Level_Lvl2 >= 0.75f)
            newColor = new Color32(255, 215, 0, 255);
        else if (Patience_Level_Lvl2 >= 0.5f)
            newColor = new Color32(255, 99, 71, 255);
        else
            newColor = new Color32(178, 34, 34, 255);

        patienceImage_Lvl2.color = newColor; 
    }

    public void OnButtonClick(TMP_Text buttonText)
    {
      

        if ((buttonText.text == "Вы чудесны!") || (buttonText.text == "You are wonderful!"))
        {
            Debug.Log("Переход к функции по тексту: " + buttonText.text);
            IncreasePatience();
        }
        else if ((buttonText.text == "Я здесь не ради этого.") || (buttonText.text == "That's not why I'm here."))
        {
            DecreasePatience();
        }
        else if ((buttonText.text == "Благодарю!") || (buttonText.text == "Ну..я пойду..") || (buttonText.text == "I appreciate it") || (buttonText.text == "Well..I will go.."))
        {
            if (dialog_Sirin == null)
            {
                Debug.LogError("dialog_Sirin не найден!");
            }
            else
            {
                
                dialog_Sirin.HideDialog();
            }

        }


        else if ((buttonText.text == "Вот это интерьерчик...") || (buttonText.text == "This is an interior design..."))
        {
            DecreasePatience();
        }
        else if ((buttonText.text == "Спасибо!") || (buttonText.text == "До свидания!") || (buttonText.text == "Goodbye!") || (buttonText.text == "Thanks!"))
        {
            if (dialog_Deer == null)
            {
                Debug.LogError("dialog_Deer не найден!");
            }
            else
            {

                dialog_Deer.HideDialog();
            }

        }
    

        else if ((buttonText.text == "Это птица!") || (buttonText.text == "It's a bird!"))
        {
            DecreasePatience();
        }
        else if ((buttonText.text == "Это светлячок!") || (buttonText.text == "It's a firefly!"))
        {
            IncreasePatience();
        }
        else if ((buttonText.text == "Большое спасибо!") || (buttonText.text == "Эхх..") || (buttonText.text == "Thank you very much!") || (buttonText.text == "Ehh.."))
        {
            if (dialog_Mermaid == null)
            {
                Debug.LogError("dialog_Mermaid не найден!");
            }
            else
            {

                dialog_Mermaid.HideDialog();
            }

        }


        //

        else if ((buttonText.text == "Они пугающие...") || (buttonText.text == "They're scary..."))
        {
            DecreasePatience();
        }
        else if ((buttonText.text == "У каждого своя жизнь, я уважаю") || (buttonText.text == "Everyone has their own life, I respect"))
        {
            IncreasePatience();
        }
        else if ((buttonText.text == "Простите..") || (buttonText.text == "Отлично! Спасибо!") || (buttonText.text == "Sorry..") || (buttonText.text == "Great! Thanks!"))
        {
            if (dialog_Leshii == null)
            {
                Debug.LogError("dialog_Leshii не найден!");
            }
            else
            {

                dialog_Leshii.HideDialog();
            }

        }




        else if ((buttonText.text == "Я спешу очень") || (buttonText.text == "I'm in a hurry."))
        {
            DecreasePatience();
        }
        else if ((buttonText.text == "Давай!") || (buttonText.text == "Get started!"))
        {
            IncreasePatience();
            PodskaskaKot();
        }
        else if ((buttonText.text == "/уйти/") || (buttonText.text == "/пойти проверить/") || (buttonText.text == "/leave/") || (buttonText.text == "/go check it out/"))
        {
            if (dialog_Cat == null)
            {
                Debug.LogError("dialog_Cat не найден!");
            }
            else
            {

                dialog_Cat.HideDialog();
            }

        }




    }

}

