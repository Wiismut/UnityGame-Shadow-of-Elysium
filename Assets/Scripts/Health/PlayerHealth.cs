using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    [Header("UI Components")]
    public Slider HealthSlider;
    public Image HealthImage;
    [Header("Health Settings")]
    public float HP = 1;
    private bool isUpdatingSliderHeal = false;

    private void Start()
    {
        HealthSlider.value = HP;
        UpdateHealthImageColor();
    }

    public void TakeDamage()
    {
        if (HP > 0)
        {
            HP -= 0.25f;
            HP = Mathf.Max(HP, 0);
            UpdateSliderAndImage();
            Debug.Log("Получен урон ХП: " + HP);
        }
        if (HP <= 0)
        {
            LoadLossScene();
        }
    }


    public void TakeDamageWater()
    {
        if (HP > 0)
        {
            HP -= 0.1f;
            HP = Mathf.Max(HP, 0);
            UpdateSliderAndImage();
            Debug.Log("Получен урон ХП: " + HP);
        }
        if (HP <= 0)
        {
            LoadLossScene();
        }
    }


    public void HealMAX()
    {
        HP = 1;
        UpdateSliderAndImage();
        Debug.Log("Хилл до максимума: " + HP);
    }

    public void Heal()
    {
        if (HP < 1)
        {
            HP += 0.25f;
            HP = Mathf.Min(HP, 1);
            UpdateSliderAndImage();
            Debug.Log("Хилл ХП: " + HP);
        }
    }

    private void UpdateSliderAndImage()
    {
        if (!isUpdatingSliderHeal)
        {
            isUpdatingSliderHeal = true;
            HealthSlider.value = HP;
            UpdateHealthImageColor();
            isUpdatingSliderHeal = false;
        }
    }

    private void UpdateHealthImageColor()
    {
        if (HealthImage == null) return;

        Color newColor;
        if (HP == 1)
            newColor = new Color32(50, 205, 50, 255);
        else if (HP >= 0.75f)
            newColor = new Color32(255, 215, 0, 255);
        else if (HP >= 0.5f)
            newColor = new Color32(255, 99, 71, 255);
        else
            newColor = new Color32(178, 34, 34, 255);

        HealthImage.color = newColor;
    }

    private void LoadLossScene()
    {
        int selectedPlayer = PlayerPrefs.GetInt("SelectPlayer", 0);
        string lossSceneName = selectedPlayer == 0 ? "LossHPMale" : "LossHPFemale";
        SceneManager.LoadScene(lossSceneName);
    }
}
