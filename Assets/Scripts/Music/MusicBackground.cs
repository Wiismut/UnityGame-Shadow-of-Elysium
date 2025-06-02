using UnityEngine;

public class MusicBackground : MonoBehaviour {
    public AudioClip backgroundSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = true;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    private void Start()
    {
        if (SoundManager.Instance != null)
        {
            audioSource.volume = SoundManager.Instance.musicVolume;
        }
        else
        {
            audioSource.volume = 0.05f;
        }

        if (backgroundSound != null)
        {
            audioSource.clip = backgroundSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    public void UpdateVolume()
    {
        if (audioSource != null && SoundManager.Instance != null)
        {
            audioSource.volume = SoundManager.Instance.musicVolume;
        }
    }
}
