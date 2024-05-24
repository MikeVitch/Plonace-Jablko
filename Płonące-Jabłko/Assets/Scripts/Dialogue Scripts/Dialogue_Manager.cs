using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue_Manager : MonoBehaviour
{
    [Header("GameEvents")]
    public Game_Event ConversationEnded;
    public Game_Event ConversationStarted;
    [Space]
    public String_Variable playerName;
    [Tooltip("For the typing animation. Determine how long it takes for each character to appear")]
    public float timeBetweenChars = 0.05f;
    [Header("UI")]
    public TextMeshProUGUI TextUI;

    [Tooltip("The part of UI that display the UI")]
    public GameObject DialogueUI;
    [Tooltip("The text UIs that display options")]
    public TextMeshProUGUI[] optionsUI;

    DialogueSO dialogue;
    Sentence currentSentence;


    public void StartDialogue(DialogueSO dialogueSO)
    {
        if (!dialogueSO.isAvailable)
        {
            return;
        }
        if (ConversationStarted != null)
        {
            ConversationStarted.Raise();

        }
        //animator.SetTrigger("InDialogue");

        TextUI.text = null;
        HideOptions();
        DialogueUI.SetActive(false);

        dialogue = dialogueSO;
        currentSentence = dialogue.startingSentence;

        DisplayDialogue();
    }

    public void GoToNextSentence()
    {
        currentSentence = currentSentence.nextSentence;
        DisplayDialogue();
    }

    public void DisplayDialogue()
    {
        if (currentSentence == null)
        {
            EndDialogue();
            return;
        }

        if (!currentSentence.HasOptions())
        {
            DialogueUI.SetActive(true);
            HideOptions();
            // sentence with no options
            TextMeshProUGUI dialogueText;
                dialogueText = TextUI;

            // display the text
            StopAllCoroutines();
            StartCoroutine(Typeout(currentSentence.text, dialogueText));
        }
        else
        {
            // with options. can only be from player
            DisplayOptions();
            TextMeshProUGUI dialogueText;
            dialogueText = TextUI;
            StopAllCoroutines();
            StartCoroutine(Typeout(currentSentence.text, dialogueText));
        }
    }

    IEnumerator Typeout(string sentence, TextMeshProUGUI textbox)
    {
        textbox.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            textbox.text += letter;
            yield return new WaitForSeconds(timeBetweenChars);

        }
    }

    public void OptionsOnClick(int index)
    {
        Choice option = currentSentence.options[index];
        if (option.consequence != null)
        {
            Debug.Log("Raise Events");
            option.consequence.Raise();

        }
        currentSentence = option.nextSentence;

        DisplayDialogue();
    }

    public void DisplayOptions()
    {
        //Debug.Log(currentSentence.options.Count);
        DialogueUI.SetActive(false);
        //OptionsUI.SetActive(true);


        if (currentSentence.options.Count <= optionsUI.Length)
        {
            for (int i = 0; i < currentSentence.options.Count; i++)
            {
                Debug.Log(currentSentence.options[i].text);
                optionsUI[i].text = currentSentence.options[i].text;
                optionsUI[i].gameObject.SetActive(true);
            }
        }
    }

    public void HideOptions()
    {
        foreach (TextMeshProUGUI option in optionsUI)
        {
            option.gameObject.SetActive(false);
        }
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue ended");
        //animator.SetTrigger("OutDialogue");
        if (ConversationEnded != null)
        {
            ConversationEnded.Raise();

        }

    }
}
