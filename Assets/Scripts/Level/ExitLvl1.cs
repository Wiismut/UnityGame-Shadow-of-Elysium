using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLvl1 : MonoBehaviour {
    public GameObject objectToToggle;
    public GameObject Messege;
    public GameObject player1;
    public GameObject player2;


    private GameObject activePlayer;
    private Items items;
    public AudioClip notExitSound;
    private AudioSource audioSource;

    void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = notExitSound;
            audioSource.volume = 0.3f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (notExitSound == null)
        {
            Debug.LogError("звука нет");
        }


        Messege.SetActive(false);
        SetActivePlayer();

        if (activePlayer != null)
        {
            items = activePlayer.GetComponent<Items>();
        }

        if (items == null)
        {
            Debug.LogError("Items не назначен");
        }

        if (objectToToggle == null)
        {
            Debug.LogError("объект не назначен");
        }

        UpdateVisibility();
    }

    private void NotExitSound()
    {
        if (audioSource != null && notExitSound != null)
        {
            audioSource.PlayOneShot(notExitSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    void Update()
    {
        UpdateVisibility();
        SetActivePlayer();
    }

    private void SetActivePlayer()
    {
        if (player1.activeInHierarchy)
        {
            activePlayer = player1;
        }
        else if (player2.activeInHierarchy)
        {
            activePlayer = player2;
        }

        if (activePlayer != null)
        {
            items = activePlayer.GetComponent<Items>();
        }
    }

    private void UpdateVisibility()
    {
        if (items != null)
        {
            int itemCount = 0;
            foreach (bool hasItem in items.hasItems)
            {
                if (hasItem) itemCount++;
            }

            objectToToggle.SetActive(itemCount < 3);

        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        Messege.SetActive(true);
        NotExitSound();
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        Messege.SetActive(false);
    }
}
