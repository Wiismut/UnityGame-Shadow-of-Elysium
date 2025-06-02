using System.Collections;
using UnityEngine;
using TMPro;
public class TextEffect : MonoBehaviour {
    public float typingSpeed = 0.05f;
    private string currentText = "";
    private TMP_Text uiText;
    void Start()
    {
        uiText = GetComponent<TMP_Text>();
        StartTyping();
    }
    public void StartTyping()
    {
        StopAllCoroutines();
        currentText = "";
        string fullText = uiText.text;
        StartCoroutine(TypeText(fullText));
    }

    private IEnumerator TypeText(string fullText)
    {
        foreach (char letter in fullText.ToCharArray())
        {
            currentText += letter;
            uiText.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void UpdateText(string newText)
    {
        uiText.text = newText;
        StartTyping();
    }
}
