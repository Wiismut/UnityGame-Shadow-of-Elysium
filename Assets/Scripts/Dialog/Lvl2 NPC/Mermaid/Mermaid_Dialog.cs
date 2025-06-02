using Dialogue;
using UnityEngine;

public class Mermaid_Dialog : MonoBehaviour {
    private Mermaid_DialogStory _dialogueStory;
    private bool _dialogActive = false;
    private bool _playerInTrigger = false;
    private bool _dialogAlreadyActivated = false;
    public GameObject objectToToggle;
    public GameObject objectToToggle1;
    public AudioClip dialogMermaidSound;
    private AudioSource audioSource;

    private void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = dialogMermaidSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("AudioSource не удалось добавить к объекту.");
        }

        if (dialogMermaidSound == null)
        {
            Debug.LogError("dialogMermaidSound не установлен в инспекторе.");
        }

        _dialogueStory = FindObjectOfType<Mermaid_DialogStory>(true);
        if (_dialogueStory == null)
        {
            Debug.LogError("DialogStory not found in the scene!");
            return;
        }
        _dialogueStory.ChangedStory += Disable;
        _dialogueStory.gameObject.SetActive(false);
        objectToToggle.SetActive(false);
        objectToToggle1.SetActive(false);
    }


    private void PlayDialogSound()
    {
        if (audioSource != null && dialogMermaidSound != null)
        {
            audioSource.PlayOneShot(dialogMermaidSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }
    private void OnDialogFinished(Mermaid_DialogStory.Story story)
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
    public void TryStartDialog()
    {
        if (!_playerInTrigger || _dialogAlreadyActivated) return;
        if (!_dialogActive)
        {
            if (_dialogueStory != null)
            {
                PlayDialogSound();
                _dialogueStory.gameObject.SetActive(true);
                _dialogueStory.Reset();
                _dialogActive = true;
                ShowDialog();
                _dialogAlreadyActivated = true;
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
            Debug.LogError("DialogStory is null");
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
            Debug.LogError("DialogStory is null during HideDialog.");
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

    private void Disable(Mermaid_DialogStory.Story story)
    {
        if (_dialogActive)
        {
            _dialogActive = false;
        }
    }
}

