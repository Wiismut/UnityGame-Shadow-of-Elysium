using UnityEngine;

public class Map : MonoBehaviour {
    private bool isPlayerInTrigger = false;

    public GameObject player1;
    public GameObject player2;
    private GameObject activePlayer;
    public GameObject MapMessage;
    public GameObject MapUI;
    private bool mapActive = false;
    public AudioClip ButtonMapSound;
    private AudioSource audioSource;
    public Puzzle puzzle;

    private void Start()
    {
        if (MapMessage != null)
        {
            MapMessage.SetActive(false);
        }

        if (MapUI != null)
        {
            MapUI.SetActive(false);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = ButtonMapSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (ButtonMapSound == null)
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

            if (MapMessage != null)
            {
                MapMessage.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            activePlayer = null;

            if (MapMessage != null)
            {
                MapMessage.SetActive(false);
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
                mapActive = !mapActive;
                MapUI.SetActive(mapActive);
                PlayMapSound();

                if (mapActive && puzzle != null)
                {
                    puzzle.ShuffleButtons();
                }
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

    private void PlayMapSound()
    {
        if (audioSource != null && ButtonMapSound != null)
        {
            audioSource.PlayOneShot(ButtonMapSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }
}
