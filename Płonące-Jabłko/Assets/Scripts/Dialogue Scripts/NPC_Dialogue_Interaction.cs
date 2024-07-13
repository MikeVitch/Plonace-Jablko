using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue_Interaction : MonoBehaviour
{
    Player_Dialogue_Interaction player_dialogue_interaction;
    KeyCode Interaction_Key;
    public Dialogue_Manager dialogue_manager;
    public DialogueSO dialogue;
    public float Interaction_Radius;
    public GameObject Player_Character;
    public Game_Event Start_Dialogue;
    void Start()
    {
        player_dialogue_interaction = FindObjectOfType<Player_Dialogue_Interaction>();
        Interaction_Key = player_dialogue_interaction.Ineraction_Key;
        Player_Character = GameObject.FindWithTag("Player_Character");
    }

    void Update()
    {
        if(Input.GetKeyDown(Interaction_Key) && !dialogue_manager.Dialogue_Active) 
        {
            if(Vector2.Distance(gameObject.transform.position,Player_Character.transform.position) <= Interaction_Radius)
            {
                Start_Dialogue.Raise();
                dialogue_manager.StartDialogue(dialogue);
            }
        }
    }
}
