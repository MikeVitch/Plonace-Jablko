using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Dialogue_Manager : MonoBehaviour
{
    [Header("GameEvents")]
    public Game_Event Conversation_Ended;
    public Game_Event Conversation_Started;
    [Space]
    public String_Variable Player_Name;
    [Tooltip("Time between characters")]
    public float Time_Between_Chars = 0.05f;
    [Header("UI")]
    public TextMeshProUGUI Text_UI;
    public TextMeshProUGUI Speaker_Name;

    public GameObject Background_Image_Object;
    public GameObject Player_Portrait_Object;
    public GameObject NPC_Portrait_Object;
    [Tooltip("Disappears when options appear")]
    public GameObject Next_Button;
    [Tooltip("Options")]
    public TextMeshProUGUI[] Options_UI;
    [Space]
    public bool Dialogue_Active;
    DialogueSO dialogue;
    Sentence Current_Sentence;

    public void StartDialogue(DialogueSO dialogueSO)
    {
        if (!dialogueSO.Is_Available)
        {
            return;
        }
        if (Conversation_Started != null)
        {
            Conversation_Started.Raise();
            Dialogue_Active = true;
        }

        Text_UI.text = null;
        HideOptions();
        Next_Button.SetActive(false);

        dialogue = dialogueSO;
        Current_Sentence = dialogue.Starting_Sentence;

        Player_Portrait_Object.GetComponent<SpriteRenderer>().sprite = dialogueSO.Player_Portrait;
        NPC_Portrait_Object.GetComponent<SpriteRenderer>().sprite = dialogueSO.NPC_Portrait;
        Time.timeScale = 0;

        DisplayDialogue();
    }

    public void GoToNextSentence()
    {
            Player_Portrait_Object.GetComponent<SpriteRenderer>().sprite = Current_Sentence.Player_Portrait;
            NPC_Portrait_Object.GetComponent<SpriteRenderer>().sprite = Current_Sentence.NPC_Portrait;
            Background_Image_Object.GetComponent <SpriteRenderer>().sprite = Current_Sentence.Background_Image;
            Current_Sentence = Current_Sentence.Next_Sentence;
            DisplayDialogue();
    }

    public void DisplayDialogue()
    {
        if (Current_Sentence == null)
        {
            EndDialogue();
            return;
        }

        if (!Current_Sentence.HasOptions())
        {
            //Sentence with no options
            Next_Button.SetActive(true);
            HideOptions();
            TextMeshProUGUI Dialogue_Text;
                Dialogue_Text = Text_UI;
            Speaker_Name.text = Current_Sentence.From.Value;

            //Display text
            StopAllCoroutines();
            StartCoroutine(Typeout(Current_Sentence.Text, Dialogue_Text));
        }
        else
        {
            //Sentence with options
            DisplayOptions();
            TextMeshProUGUI Dialogue_Text;
                Dialogue_Text = Text_UI;
            Speaker_Name.text = Current_Sentence.From.Value;

            //Display text
            StopAllCoroutines();
            StartCoroutine(Typeout(Current_Sentence.Text, Dialogue_Text));
        }
    }

    IEnumerator Typeout(string sentence, TextMeshProUGUI textbox)
    {
        textbox.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            textbox.text += letter;
            yield return new WaitForSecondsRealtime(Time_Between_Chars);
        }
    }

    public void OptionsOnClick(int index)
    {
        if (!Dialogue_Active)
            return;
        else if (!Current_Sentence.HasOptions())
            return;

        Choice option = Current_Sentence.Options[index];
        if (option.Consequence != null)
        {
            option.Consequence.Raise();

        }
        Current_Sentence = option.NextSentence;

        DisplayDialogue();

        Player_Portrait_Object.GetComponent<SpriteRenderer>().sprite = option.Player_Portrait;
        NPC_Portrait_Object.GetComponent<SpriteRenderer>().sprite = option.NPC_Portrait;
    }

    public void DisplayOptions()
    {
        Next_Button.SetActive(false);

        if (Current_Sentence.Options.Count <= Options_UI.Length)
        {
            for (int i = 0; i < Current_Sentence.Options.Count; i++)
            {
                Options_UI[i].text = Current_Sentence.Options[i].Text;
                Options_UI[i].gameObject.SetActive(true);
            }
        }
    }

    public void HideOptions()
    {
        foreach (TextMeshProUGUI option in Options_UI)
        {
            option.gameObject.SetActive(false);
        }
    }

    public void EndDialogue()
    {
        if (Conversation_Ended != null)
        {
            Conversation_Ended.Raise();
            Time.timeScale = 1.0f;
            Dialogue_Active = false;
        }

    }
}
