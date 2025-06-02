using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGeisha : MonoBehaviour {
    public AudioClip musicClip;
    private AudioSource musicSource;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        if (musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.volume = 0f;
            musicSource.loop = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (musicSource != null && !musicSource.isPlaying)
            {
                musicSource.Play();
            }
            StopAllCoroutines();
            StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        float startVolume = musicSource.volume;
        float targetVolume = 0.5f;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }
        musicSource.volume = targetVolume;
    }

    private IEnumerator FadeOut()
    {
        float startVolume = musicSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            yield return null;
        }
        musicSource.volume = 0f;
        musicSource.Stop();
    }
}
