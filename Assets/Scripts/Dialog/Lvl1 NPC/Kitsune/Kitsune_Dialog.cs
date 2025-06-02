using Dialogue;
using UnityEngine;
public class Kitsune_Dialog : MonoBehaviour {
    private Kitsune_DialogStory _dialogueStory;
    private bool _dialogActive = false;
    private bool _playerInTrigger = false;
    private bool _dialogAlreadyActivated = false;
    public GameObject objectToToggle;
    public GameObject objectToToggle1;
    public AudioClip dialogKitsuneSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = dialogKitsuneSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (dialogKitsuneSound == null)
        {
            Debug.LogError("звука нет");
        }


        _dialogueStory = FindObjectOfType<Kitsune_DialogStory>(true);
        if (_dialogueStory == null)
        {
            Debug.LogError("DialogStory не найдено");
            return;
        }
        _dialogueStory.ChangedStory += Disable;
        _dialogueStory.gameObject.SetActive(false);
        objectToToggle.SetActive(false);
        objectToToggle1.SetActive(false);
    }

    private void PlayDialogSound()
    {
        if (audioSource != null && dialogKitsuneSound != null)
        {
            audioSource.PlayOneShot(dialogKitsuneSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    private void Disable(Kitsune_DialogStory.Story story)
    {
        if (_dialogActive)
        {
            _dialogActive = false;
        }
    }

    private void Update()
    {
        if (_playerInTrigger && !_dialogAlreadyActivated)
        {
            objectToToggle.SetActive(true);
            objectToToggle1.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_dialogActive)
                {
                    PlayDialogSound();
                    _dialogueStory.Reset();
                    _dialogActive = true;
                    ShowDialog();
                    _dialogAlreadyActivated = true;
                }
                else
                {
                    HideDialog();
                }
            }
        }
        else
        {
            objectToToggle.SetActive(false);
            objectToToggle1.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            _playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            Reset();
            _playerInTrigger = false;
        }
    }

    private void ShowDialog()
    {
        _dialogueStory.gameObject.SetActive(true);
    }

    public void HideDialog()
    {
        _dialogueStory.gameObject.SetActive(false);
        _dialogueStory.Reset();
        _dialogActive = false;
    }

    private void Reset()
    {
        HideDialog();
        objectToToggle.SetActive(false);
        objectToToggle1.SetActive(false);
    }
}
