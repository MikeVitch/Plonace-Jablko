using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    public bool Triggered;
    bool Trigger_Past;
    public Dialogue_Manager dialogue_manager;
    public DialogueSO dialogue;
    public Game_Event Start_Dialogue;
    void Start()
    {
        
    }

    void Update()
    {
        if (Triggered != Trigger_Past)
        {
            Start_Dialogue.Raise();
            dialogue_manager.StartDialogue(dialogue);
        }
        Trigger_Past = Triggered;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_Character")
            Triggered = true;
    }
}
