using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Unlock : MonoBehaviour
{
    //This script is temporary for the purposes of the tutorial
    public Player_Logic player_logic;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_Character")
            player_logic.Combat_Lock = false;
    }
}
