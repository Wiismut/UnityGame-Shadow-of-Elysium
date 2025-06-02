using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMainMenu : MonoBehaviour {
    public AudioClip menuSound;
    private AudioSource audioSource;

    [SerializeField] private float delayBeforeSceneLoad = 0.5f;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = menuSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (menuSound == null)
        {
            Debug.LogError("звука нет");
        }
    }

    private void PlayMenuSound()
    {
        if (audioSource != null && menuSound != null)
        {
            audioSource.PlayOneShot(menuSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    public void OpenMenu()
    {
        PlayMenuSound();
        StartCoroutine(OpenMenuWithDelay());
    }

    private IEnumerator OpenMenuWithDelay()
    {
        yield return new WaitForSeconds(menuSound.length);
        SceneManager.LoadScene(0);
    }
}
