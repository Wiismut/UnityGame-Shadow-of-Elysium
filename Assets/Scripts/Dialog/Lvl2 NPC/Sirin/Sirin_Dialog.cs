using Dialogue;
using UnityEngine;

public class Sirin_Dialog : MonoBehaviour {
    private Sirin_DialogStory _dialogueStory;
    private bool _dialogActive = false;
    private bool _playerInTrigger = false;
    private bool _dialogAlreadyActivated = false;
    public GameObject objectToToggle;
    public GameObject objectToToggle1;

    public AudioClip dialogSirinSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = dialogSirinSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (dialogSirinSound == null)
        {
            Debug.LogError("звука нет");
        }

        _dialogueStory = FindObjectOfType<Sirin_DialogStory>(true);
        if (_dialogueStory == null)
        {
            Debug.LogError("DialogStory не найден");
            return;
        }

        _dialogueStory.ChangedStory += OnDialogFinished;
        _dialogueStory.gameObject.SetActive(false);
        objectToToggle.SetActive(false);
        objectToToggle1.SetActive(false);
    }

    private void PlayDialogSound()
    {
        if (audioSource != null && dialogSirinSound != null)
        {
            audioSource.PlayOneShot(dialogSirinSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    private void OnDialogFinished(Sirin_DialogStory.Story story)
    {
        if (_dialogActive)
        {
            _dialogActive = false;
            _dialogAlreadyActivated = true;
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
                TryStartDialog();
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
        if (_dialogueStory != null)
        {
            _dialogueStory.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("DialogStory не найдено");
        }
    }

    public void HideDialog()
    {
        if (_dialogueStory != null)
        {
            _dialogueStory.gameObject.SetActive(false);
            _dialogueStory.Reset();
            _dialogActive = false;
        }
        else
        {
            Debug.LogError("DialogStory is null");
        }
    }

    private void Reset()
    {
        HideDialog();

        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
        }

        if (objectToToggle1 != null)
        {
            objectToToggle1.SetActive(false);
        }
    }

    public void TryStartDialog()
    {
        if (!_playerInTrigger || _dialogAlreadyActivated) return;

        if (!_dialogActive)
        {
            if (_dialogueStory != null)
            {
                PlayDialogSound();
                _dialogueStory.Reset();
                _dialogActive = true;
                ShowDialog();
            }
            else
            {
                Debug.LogError("DialogStory is null");
            }
        }
        else
        {
            HideDialog();
        }
    }
}

