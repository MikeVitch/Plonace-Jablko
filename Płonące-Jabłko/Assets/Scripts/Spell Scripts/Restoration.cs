using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restoration : MonoBehaviour
{
    public float Mana_Cost_Initial = 0f;
    public float Mana_Cost_Held = 5f;
    public float Heal_Amount = 5f;
    public float Heal_Tickrate = 0.25f;
    public float Self_Slow = 0.9f;
    public KeyCode Cast = KeyCode.Alpha0;
    public Mana_Tracker mana_tracker;
    public bool Spell_Is_On = false;
    private float Input_Time;
    public bool Activate_Spell = false;
    public float Cast_Time = 0.5f;
    public Player_Logic player_logic;
    public bool Unlocked = false;
    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    void Update()
    {
        if (Input.GetKey(Cast) && Spell_Is_On && mana_tracker.Current_Mana >= Mana_Cost_Held)
            mana_tracker.Current_Mana -= Mana_Cost_Held * Time.deltaTime;
        else
            Spell_Is_On = false;


        if (Input.GetKeyDown(Cast) && mana_tracker.Current_Mana >= Mana_Cost_Initial && !player_logic.Player_Attack_Lockout && Unlocked)
        {
            Input_Time = Time.time;
            Activate_Spell = true;
        }
        //Input.GetKey(Cast) is added and mana reduction is moved so player doesnt lose mana if they tap the cast button
        if (Activate_Spell && Time.time >= Input_Time + Cast_Time && Input.GetKey(Cast))
        {
            Activate_Spell = false;
            Spell_Is_On = true;
            mana_tracker.Current_Mana -= Mana_Cost_Initial;
        }
        if (!Input.GetKeyDown(Cast) && !Input.GetKey(Cast))
            Activate_Spell = false;
    }
}
