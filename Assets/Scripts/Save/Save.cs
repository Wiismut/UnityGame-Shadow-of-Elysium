using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class Save : MonoBehaviour {
    public AudioClip saveSound;
    private AudioSource audioSource;

    [SerializeField] private float delayBeforeSceneLoad = 0.01f;


    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = saveSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (saveSound == null)
        {
            Debug.LogError("звука нет");
        }
    }

    private void PlaySaveSound()
    {
        if (audioSource != null && saveSound != null)
        {
            audioSource.PlayOneShot(saveSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    public void SaveGame()
    {
        PlaySaveSound();
        StartCoroutine(SaveGameDelay());
    }
    public void LoadGame()
    {
        PlaySaveSound();
        StartCoroutine(LoadGameDelay());
    }




    public IEnumerator SaveGameDelay()
    {
        yield return new WaitForSeconds(saveSound.length);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        PlayerPrefs.Save();
        Debug.Log("сохранено: Scene " + currentSceneIndex);
    }
    public IEnumerator LoadGameDelay()
    {
        yield return new WaitForSeconds(saveSound.length);
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            int savedSceneIndex = PlayerPrefs.GetInt("SavedScene");

            SceneManager.LoadScene(savedSceneIndex);
            Debug.Log("Загружено: Scene " + savedSceneIndex);
        }
        else
        {
            Debug.Log("нет сохранений");
        }
    }
}