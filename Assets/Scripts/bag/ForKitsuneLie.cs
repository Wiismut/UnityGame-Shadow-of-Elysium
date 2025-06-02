using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ForKitsuneLie : MonoBehaviour {
    private bool isPlayerInTrigger = false;

    public GameObject player1;
    public GameObject player2;
    private GameObject activePlayer;
    public PlayableDirector playableDirector;
    public GameObject aMessage;

    public AudioClip lieticketSound;
    private AudioSource audioSource;
    private bool isDestroyed = false;
    private void Start()
    {
        if (aMessage != null)
        {
            aMessage.SetActive(false);
        }
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = lieticketSound;
            audioSource.volume = 0.3f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (lieticketSound == null)
        {
            Debug.LogError("звука нет");
        }
    }

    private void PlayLieTicketSound()
    {
        if (audioSource != null && lieticketSound != null)
        {
            audioSource.PlayOneShot(lieticketSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            DetermineActivePlayer();

            if (aMessage != null)
            {
                aMessage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            activePlayer = null;

            if (aMessage != null)
            {
                aMessage.SetActive(false);
            }
        }
    }

    private void Update()
    {
        DetermineActivePlayer();

        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (activePlayer != null)
            {
                PlayLieTicketSound();
                isDestroyed = true;
                Destroy(gameObject, lieticketSound.length);
                playableDirector.Play();
            }
            else
            {
                Debug.LogError("активный игрок не определен");
            }
        }
    }

    private void DetermineActivePlayer()
    {
        if (player1.activeInHierarchy)
        {
            activePlayer = player1;
        }
        else if (player2.activeInHierarchy)
        {
            activePlayer = player2;
        }
        else
        {
            activePlayer = null;
        }
    }

}
