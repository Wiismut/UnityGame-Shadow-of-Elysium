using Dialogue;
using UnityEngine;
public class Geisha_Dialog : MonoBehaviour {
    private Geisha_DialogStory _dialogueStory;
    private bool _dialogActive = false;
    private bool _playerInTrigger = false;
    private bool _dialogAlreadyActivated = false;
    public GameObject objectToToggle;
    public GameObject objectToToggle1;
    public AudioClip dialogGeishaSound;
    private AudioSource audioSource;

    private void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = dialogGeishaSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (dialogGeishaSound == null)
        {
            Debug.LogError("звука нет");
        }
        _dialogueStory = FindObjectOfType<Geisha_DialogStory>(true);
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
        if (audioSource != null && dialogGeishaSound != null)
        {
            audioSource.PlayOneShot(dialogGeishaSound);
        }
        else
        {
            Debug.LogError("звука нет");
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
                        Debug.LogError("DialogStory is null during Update.");
                    }
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

    private void Disable(Geisha_DialogStory.Story story)
    {
        if (_dialogActive)
        {
            _dialogActive = false;
        }
    }
}