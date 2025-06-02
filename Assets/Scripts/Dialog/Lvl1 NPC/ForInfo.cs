using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForInfo : MonoBehaviour {
    private bool isPlayerInTrigger = false;
    public GameObject player1;
    public GameObject player2;
    private GameObject activePlayer;
    public GameObject infoObject;

    public AudioClip dialogSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = dialogSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (dialogSound == null)
        {
            Debug.LogError("звука нет");
        }


        if (player1 == null || player2 == null)
        {
            Debug.LogError("игроки не назначены");
        }

        if (infoObject != null)
        {
            infoObject.SetActive(false);
        }

        DetermineActivePlayer();
    }
    private void PlayDialogSound()
    {
        if (audioSource != null && dialogSound != null)
        {
            audioSource.PlayOneShot(dialogSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }
    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.I))
        {
            PlayDialogSound();
            ToggleInfoObject();
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            DetermineActivePlayer();
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            activePlayer = null;

            if (infoObject != null)
            {
                infoObject.SetActive(false);
            }
        }
    }

    private void ToggleInfoObject()
    {
        if (infoObject != null)
        {
            infoObject.SetActive(!infoObject.activeSelf);
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
