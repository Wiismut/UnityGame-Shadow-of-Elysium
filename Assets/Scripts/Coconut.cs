using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Coconut : MonoBehaviour {
    public PlayableDirector playableDirector;
    private bool isPlayerInTrigger = false;

    public GameObject player1;
    public GameObject player2;
    private GameObject activePlayer;

    public GameObject CoconutMessage;
    public GameObject CoconutGrid;
    private bool gridActive = true;

    public AudioClip ButtonCoconutSound;
    private AudioSource audioSource;

    private void Start()
    {
        if (CoconutMessage != null)
        {
            CoconutMessage.SetActive(false);
        }

        if (CoconutGrid != null)
        {
            CoconutGrid.SetActive(true);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = ButtonCoconutSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (ButtonCoconutSound == null)
        {
            Debug.LogError("звука нет ");
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            DetermineActivePlayer();

            if (CoconutMessage != null)
            {
                CoconutMessage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            activePlayer = null;

            if (CoconutMessage != null)
            {
                CoconutMessage.SetActive(false);
            }
        }
    }

    private void Update()
    {
        DetermineActivePlayer();

        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if ((activePlayer != null) && (gridActive == true))
            {
                CoconutMessage.SetActive(false);
                CoconutGrid.SetActive(false);
                gridActive = false;
                playableDirector.Play();
                Destroy(GetComponent<Collider2D>());
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

    private void PlayCoconutSound()
    {
        if (audioSource != null && ButtonCoconutSound != null)
        {
            audioSource.PlayOneShot(ButtonCoconutSound);
        }
        else
        {
            Debug.LogError("звуков нет");
        }
    }
}
