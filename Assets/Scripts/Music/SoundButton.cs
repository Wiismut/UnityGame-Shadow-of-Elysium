using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundButton : MonoBehaviour
{
    public AudioClip buttonSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = buttonSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (buttonSound == null)
        {
            Debug.LogError("звука нет");
        }

    }
    public void PlayButtonSound()
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }


}
