using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainMenuSound;
    private AudioSource audioSource;

    [SerializeField] private float delayBeforeSceneLoad = 0.5f;


    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = mainMenuSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (mainMenuSound == null)
        {
            Debug.LogError("звука нет");
        }
    }

    private void PlayMainMenuSound()
    {
        if (audioSource != null && mainMenuSound != null)
        {
            audioSource.PlayOneShot(mainMenuSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }


    public IEnumerator PlayGameDelay()
    {
        yield return new WaitForSeconds(mainMenuSound.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGame()
    {
        PlayMainMenuSound();
        StartCoroutine(PlayGameDelay());
    }

    public IEnumerator ExitGameDelay()
    {
        yield return new WaitForSeconds(mainMenuSound.length);
        Application.Quit();
    }
    public void ExitGame()
    {
        PlayMainMenuSound();
        StartCoroutine(ExitGameDelay());
    }

}
