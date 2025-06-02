using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Item : MonoBehaviour {
    public int index = 0;
    public Sprite itemSprite;
    private bool isPlayerInTrigger = false;
    public GameObject player1;
    public GameObject player2;
    private GameObject activePlayer;
    public PlayableDirector playableDirector;
    public GameObject hiddenObject;
    public AudioClip ticketSound;
    private AudioSource audioSource;
    private bool isDestroyed = false;

    void Start()
    {
        if (player1 == null || player2 == null)
        {
            Debug.LogError("игроки не найдены");
        }

        if (hiddenObject != null)
        {
            hiddenObject.SetActive(false);
        }
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = ticketSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (ticketSound == null)
        {
            Debug.LogError("звука нет");
        }

        DetermineActivePlayer();
    }

    private void PlayTicketSound()
    {
        if (audioSource != null && ticketSound != null)
        {
            audioSource.PlayOneShot(ticketSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            DetermineActivePlayer();

            if (hiddenObject != null)
            {
                hiddenObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            activePlayer = null;

            if (hiddenObject != null)
            {
                hiddenObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isDestroyed) return;
        DetermineActivePlayer();
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (activePlayer != null)
            {
                Items itemsComponent = activePlayer.GetComponent<Items>();
                if (itemsComponent != null)
                {
                    itemsComponent.AddItem(itemSprite);
                    playableDirector.Play();
                    PlayTicketSound();
                    isDestroyed = true;
                    Destroy(gameObject, ticketSound.length);
                }
                else
                {
                    Debug.LogError("items не найдено");
                }
            }
            else
            {
                Debug.LogError("активный игрок не определен");
            }
        }

        if (!isDestroyed && isPlayerInTrigger && hiddenObject != null && hiddenObject.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(hiddenObject);
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
