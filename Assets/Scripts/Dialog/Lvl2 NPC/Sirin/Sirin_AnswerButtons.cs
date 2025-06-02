using Dialogue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;
public class Sirin_AnswerButtons : MonoBehaviour {
    public Button[] buttons;

    private string[] currentReplyTags;
    private Sirin_DialogStory dialogueStory;
    public TMP_Text[] buttonsText;

    void Awake()
    {
        buttonsText = GetComponentsInChildren<TMP_Text>(true);
    }

    private void Start()
    {
        dialogueStory = GetComponent<Sirin_DialogStory>();
        dialogueStory.ChangedStory += ChangeAnswers;
        currentReplyTags = new string[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => SendAnswer(buttonIndex));
            buttonsText[i] = buttons[i].GetComponentInChildren<TMP_Text>();
        }
    }

    private void ChangeAnswers(Sirin_DialogStory.Story story)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i >= story.Answers.Count)
            {
                buttonsText[i].text = null;
                buttons[i].interactable = false;
                continue;
            }

            var localizedText = new LocalizedString("DialogSirin", story.Answers[i].TextKey);
            int index = i;

            localizedText.StringChanged += (value) =>
            {
                buttonsText[index].text = value;
            };

            currentReplyTags[i] = story.Answers[i].ReposeTextKey;
            buttons[i].interactable = true;
        }

    }

    private void SendAnswer(int button) => dialogueStory.Sirin_ChangeStory(currentReplyTags[button]);
}