using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_Local : MonoBehaviour
{
    public GameObject Exit;
    Player_Logic player_logic;
     bool Player_In_Range;
     bool Teleport;

    private void Start()
    {
        player_logic = FindObjectOfType<Player_Logic>();
    }

    private void Update()
    {
        if (Player_In_Range && Input.GetKeyDown(player_logic.Interaction_Key)) 
           Teleport = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player_Character" && Teleport)
        {
            collision.transform.position = Exit.transform.position;
            Teleport = false;
        }

        if(collision.tag == "Player_Character")
            Player_In_Range = true;
        else
            Player_In_Range = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_Character")
            Player_In_Range = false;
    }
}
