using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;


public class Puzzle : MonoBehaviour {
    public GameObject puzzle;
    public TMP_Text messageText;
    private int[] correctSequence = { 3, 1, 2 };
    public LocalizedString correctMessage;
    public LocalizedString wrongMessage;
    private List<int> pressedSequence = new List<int>();
    public List<Button> buttons;

    public void OnButtonPressed(int buttonNumber)
    {
        pressedSequence.Add(buttonNumber);

        if (pressedSequence.Count == correctSequence.Length)
        {
            bool isCorrect = true;
            for (int i = 0; i < correctSequence.Length; i++)
            {
                if (pressedSequence[i] != correctSequence[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            var message = isCorrect ? correctMessage : wrongMessage;
            message.StringChanged += (value) => messageText.text = value;
            message.RefreshString();

            if (isCorrect)
            {
                Debug.Log("Правильно!");
                DeletePuzzle();
            }
            else
            {
                Debug.Log("Неправильно!");
            }

            pressedSequence.Clear();
        }
    }

    public void DeletePuzzle()
    {
        puzzle.SetActive(false);
    }

    public void ShuffleButtons()
    {
        if (buttons == null || buttons.Count == 0) return;

        System.Random rng = new System.Random();
        int count = buttons.Count;

        for (int i = 0; i < count; i++)
        {
            int randomIndex = rng.Next(i, count);
            Vector3 tempPos = buttons[i].transform.position;
            buttons[i].transform.position = buttons[randomIndex].transform.position;
            buttons[randomIndex].transform.position = tempPos;
        }
    }
}


