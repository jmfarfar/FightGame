using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject nextButton;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        didDialogueStart = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDialogue()
    {
        if (lineIndex < dialogueLines.Length)
        {

            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;

        StartCoroutine(ShowLine());

    }

    private IEnumerator ShowLine()
    {
        nextButton.SetActive(false);
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
    }

}
