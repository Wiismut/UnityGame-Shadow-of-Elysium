using Dialogue;
using UnityEngine;
public class Pele_Dialog : MonoBehaviour {
    private Pele_DialogStory _dialogueStory;
    private bool _dialogActive = false;
    private bool _playerInTrigger = false;
    private bool _dialogAlreadyActivated = false;
    public GameObject objectToToggle;
    public GameObject objectToToggle1;
    public AudioClip dialogPeleSound;
    private AudioSource audioSource;

    private void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = dialogPeleSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (dialogPeleSound == null)
        {
            Debug.LogError("звука нет");
        }
        _dialogueStory = FindObjectOfType<Pele_DialogStory>(true);
        if (_dialogueStory == null)
        {
            return;
        }
        _dialogueStory.ChangedStory += Disable;
        _dialogueStory.gameObject.SetActive(false);
        objectToToggle.SetActive(false);
        objectToToggle1.SetActive(false);
    }


    private void PlayDialogSound()
    {
        if (audioSource != null && dialogPeleSound != null)
        {
            audioSource.PlayOneShot(dialogPeleSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }
    private void OnDialogFinished(Pele_DialogStory.Story story)
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
            Debug.LogError("DialogStory is null during ShowDialog.");
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

    private void Disable(Pele_DialogStory.Story story)
    {
        if (_dialogActive)
        {
            _dialogActive = false;
        }
    }
}

