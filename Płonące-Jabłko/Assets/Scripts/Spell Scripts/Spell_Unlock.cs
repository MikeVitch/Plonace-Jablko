using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Unlock : MonoBehaviour
{
    //This script is temporary for the purposes of the tutorial

    public bool Restoration;
    public bool Boulder_Throw;
    public bool Flamethrower;
    public Player_Logic player_logic;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(Restoration)
            player_logic.GetComponentInChildren<Restoration>().Unlocked = true;
        if (Boulder_Throw)
            player_logic.GetComponentInChildren<Boulder_Throw>().Unlocked = true;
        if(Flamethrower)
            player_logic.GetComponentInChildren<Flamethrower>().Unlocked = true;
    }
}
