using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSO dialogue;

    public Dialogue_Manager dialogueManager;

    public void StartDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }

}