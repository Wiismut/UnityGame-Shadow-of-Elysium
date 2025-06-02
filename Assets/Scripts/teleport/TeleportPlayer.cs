using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
public class TeleportPlayer : MonoBehaviour {
    public GameObject objectToToggle;
    [Tooltip("Точка, куда будет перемещён игрок.")]
    public Transform targetPosition;
    [Tooltip("Первый игрок.")]
    public GameObject player1;
    [Tooltip("Второй игрок.")]
    public GameObject player2;
    [Tooltip("Cinemachine Virtual Camera, следящая за игроком.")]
    public CinemachineVirtualCamera cinemachineCamera;
    public PlayableDirector playableDirector;
    private GameObject activePlayer;
    private bool isPlayerInTrigger = false;
    public AudioClip buttonlSound;
    private AudioSource audioSource;

    private void Start()
    {
        if (player1 == null || player2 == null)
        {
            Debug.LogError("Игроки не назначены");
        }

        if (cinemachineCamera == null)
        {
            Debug.LogError("Cinemachine не назначена");
        }
        objectToToggle.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = buttonlSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (buttonlSound == null)
        {
            Debug.LogError("звука нет");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectToToggle.SetActive(true);
        if (other.gameObject == player1 || other.gameObject == player2)
        {
            DetermineActivePlayer(other.gameObject);
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player1 || other.gameObject == player2)
        {
            objectToToggle.SetActive(false);
            isPlayerInTrigger = false;
            activePlayer = null;
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            if (activePlayer != null && targetPosition != null && Input.GetKeyDown(KeyCode.E))
            {
                PlaySound();
                StartCoroutine(TeleportActivePlayer());
            }
            else if (activePlayer == null || targetPosition == null)
            {
                Debug.LogError("позиция или игрок не определен");
            }
        }
    }

    private void PlaySound()
    {
        if (audioSource != null && buttonlSound != null)
        {
            audioSource.PlayOneShot(buttonlSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    private void DetermineActivePlayer(GameObject player)
    {
        if (player == player1)
        {
            activePlayer = player1;
        }
        else if (player == player2)
        {
            activePlayer = player2;
        }
        else
        {
            activePlayer = null;
        }
    }

    private IEnumerator TeleportActivePlayer()
    {
        if (cinemachineCamera != null)
        {
            CinemachineTransposer transposer = cinemachineCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null)
            {
                transposer.m_XDamping = 0;
                transposer.m_YDamping = 0;
                transposer.m_ZDamping = 0;
            }
        }
        if (playableDirector != null)
        {
            playableDirector.Play();
        }
        else
        {
            Debug.LogError("PlayableDirector не назначен.");
            yield break;
        }
        if (playableDirector.duration > 0)
        {
            yield return new WaitForSeconds((float)playableDirector.duration / 2);
        }
        if (!isPlayerInTrigger)
        {
            Debug.LogWarning("Игрок покинул триггер до завершения телепортации.");
            yield break;
        }
        if (activePlayer != null)
        {
            activePlayer.transform.position = targetPosition.position;
            if (cinemachineCamera != null)
            {
                cinemachineCamera.ForceCameraPosition(targetPosition.position, Quaternion.identity);
            }

            Debug.Log($"{activePlayer.name} перемещён");
        }
        else
        {
            yield break;
        }
        while (playableDirector.state == PlayState.Playing)
        {
            yield return null;
        }
        if (cinemachineCamera != null)
        {
            CinemachineTransposer transposer = cinemachineCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null)
            {
                transposer.m_XDamping = 1;
                transposer.m_YDamping = 1;
                transposer.m_ZDamping = 1;
            }
        }
    }
}