using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Area : MonoBehaviour
{
    Player_Movement player_movement;
    Player_Logic player_logic;
    void Start()
    {
        player_movement = FindObjectOfType<Player_Movement>();
        player_logic = FindObjectOfType<Player_Logic>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player_Character")
        {
            if(player_movement.Is_Crouching)
                player_logic.Is_Hidden = true;
            else
                player_logic.Is_Hidden = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player_logic.Is_Hidden = false;
    }
}
