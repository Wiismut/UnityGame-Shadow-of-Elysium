using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DiscoDialogManager : MonoBehaviour {
    public GameObject panelDialog;
    public TMP_Text dialog;
    public List<LocalizedString> localizedMessages;
    private List<string> messages = new List<string>();
    public int currentMessageIndex = 0;
    public bool dialogStart = false;
    public GameObject objectToToggle;

    public AudioClip dialogDiscoSound;
    public AudioClip typingSound;
    private AudioSource audioSource;
    private void LoadLocalizedMessages()
    {
        messages.Clear();

        foreach (var locStr in localizedMessages)
        {
            string translated = locStr.GetLocalizedString();
            messages.Add(translated);
        }
    }
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.volume = 0.25f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (dialogDiscoSound == null)
        {
            Debug.LogError("звука нет");
        }

        if (typingSound == null)
        {
            Debug.LogError("звука нет");
        }

        panelDialog.SetActive(false);
        objectToToggle.SetActive(false);
        LocalizationSettings.SelectedLocaleChanged += (_) => LoadLocalizedMessages();
        LoadLocalizedMessages();

    }
    private void PlayDialogSound()
    {
        if (audioSource != null && dialogDiscoSound != null)
        {
            audioSource.PlayOneShot(dialogDiscoSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }
    private void PlayTypingSound()
    {
        if (audioSource != null && typingSound != null)
        {
            audioSource.PlayOneShot(typingSound);
        }
    }
    private void StopAllSounds()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (objectToToggle != null) objectToToggle.SetActive(true);
            currentMessageIndex = 0;
            if (messages.Count > 0)
            {
                dialog.text = "";
                dialogStart = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ResetDialog();
        }
    }

    void Update()
    {
        if (dialogStart)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayDialogSound();
                panelDialog.SetActive(true);
                ToggleObjects(false);
                StartCoroutine(TypeMessage(messages[currentMessageIndex]));
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentMessageIndex++;
                if (currentMessageIndex < messages.Count)
                {
                    StopAllCoroutines();
                    StartCoroutine(TypeMessage(messages[currentMessageIndex]));
                }
                else
                {
                    ResetDialog();
                }
            }
        }
    }

    private IEnumerator TypeMessage(string message)
    {
        dialog.text = "";
        foreach (char letter in message.ToCharArray())
        {
            dialog.text += letter;
            PlayTypingSound();
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void ResetDialog()
    {
        StopAllCoroutines();
        StopAllSounds();
        panelDialog.SetActive(false);
        if (objectToToggle != null) objectToToggle.SetActive(false);
        dialogStart = false;
    }

    private void ToggleObjects(bool state)
    {
        if (objectToToggle != null) objectToToggle.SetActive(state);
    }
}
