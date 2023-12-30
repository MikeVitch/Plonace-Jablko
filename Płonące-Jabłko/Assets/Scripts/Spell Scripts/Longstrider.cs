using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longstrider : MonoBehaviour
{
    UnityEngine.Transform Hitbox_Transform;
    public KeyCode Cast = KeyCode.Alpha2;
    public float Time_Active = 10f;
    public float Cast_Time = 1f;
    public float Mana_Cost = 20f;
    public Mana_Tracker mana_tracker;
    private float Input_Time;
    public bool Activate_Spell = false;
    public float Speed_Boost = 0.25f;
    public Player_Logic player_logic;

    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    void Update()
    {
        if (Input.GetKeyDown(Cast) && mana_tracker.Current_Mana >= Mana_Cost && !player_logic.Player_Attack_Lockout)
        {
            mana_tracker.Current_Mana -= Mana_Cost;
            Input_Time = Time.time;
            Activate_Spell = true;
        }

        if (Activate_Spell && Time.time >= Input_Time + Time_Active && !player_logic.Player_Attack_Lockout)
        {
            Activate_Spell = false;
        }
    }
}
