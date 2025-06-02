using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;
namespace Dialogue {
    public class Pele_DialogWindow : MonoBehaviour {
        private TMP_Text _text;
        private Pele_DialogStory _dialogueStory;
        private Coroutine _typingCoroutine;
        public AudioClip typingSound;
        private AudioSource _audioSource;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            if (_text == null)
            {
                return;
            }

            _dialogueStory = FindObjectOfType<Pele_DialogStory>();
            if (_dialogueStory == null)
            {
                return;
            }

            _dialogueStory.ChangedStory += ChangeAnswers;
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.volume = 0.25f;
        }
        private void Start()
        {
            StartCoroutine(InitializeFirstText());
        }

        private IEnumerator InitializeFirstText()
        {
            yield return null;

            if (_dialogueStory != null && _dialogueStory._stories.Count > 0)
            {
                ChangeAnswers(_dialogueStory._stories[0]);
            }
        }

        private void ChangeAnswers(Pele_DialogStory.Story story)
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
            }

            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            _text.text = "";
            _typingCoroutine = StartCoroutine(TypeText(story.TextKey));
        }

        private IEnumerator TypeText(string key)
        {
            var localizedString = new LocalizedString("DialogPele", key);
            var textOperation = localizedString.GetLocalizedStringAsync();

            yield return textOperation;

            string localizedText = textOperation.Result;
            _text.text = "";

            foreach (char letter in localizedText)
            {
                _text.text += letter;
                PlayTypingSound();
                yield return new WaitForSeconds(0.02f);
            }
        }

        private void PlayTypingSound()
        {
            if (_audioSource != null && typingSound != null)
            {
                _audioSource.PlayOneShot(typingSound);
            }
        }
    }
}
