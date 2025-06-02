using UnityEngine;

public class Heal : MonoBehaviour {
    public PlayerHealth playerHealth;
    private bool isPlayerInTrigger = false;

    public GameObject player1;
    public GameObject player2;
    private GameObject activePlayer;

    public GameObject healMessage;

    public AudioClip healSound;
    private AudioSource audioSource;

    private void Start()
    {
        if (healMessage != null)
        {
            healMessage.SetActive(false);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = healSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (healSound == null)
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

            if (healMessage != null)
            {
                healMessage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            activePlayer = null;

            if (healMessage != null)
            {
                healMessage.SetActive(false);
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
                if (playerHealth != null)
                {
                    playerHealth.Heal();
                    PlayHealSound();
                    Destroy(gameObject, healSound.length);
                }
                else
                {
                    Debug.LogError("PlayerHealth не найден");
                }
            }
            else
            {
                Debug.LogError("активный игрок не найден");
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

    private void PlayHealSound()
    {
        if (audioSource != null && healSound != null)
        {
            audioSource.PlayOneShot(healSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }
}
