using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misty_Step : MonoBehaviour
{
    UnityEngine.Transform Hitbox_Transform;
    public KeyCode Cast = KeyCode.Alpha2;
    public float Time_Active = 0.5f;
    public float Cast_Time = 0f;
    public float Mana_Cost = 10f;
    public Mana_Tracker mana_tracker;
    private float Input_Time;
    bool Activate_Spell = false;
    public Player_Movement Player_Movement;
    public float Speed_Boost = 10f;

    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    void Update()
    {
        if (Input.GetKeyDown(Cast) && mana_tracker.Current_Mana >= Mana_Cost)
        {
            mana_tracker.Current_Mana -= Mana_Cost;
            Input_Time = Time.time;
            Activate_Spell = true;
            Player_Movement.Speed += Player_Movement.Base_Speed * Speed_Boost;
        }

        if (Activate_Spell && Time.time >= Input_Time + Time_Active)
        {
            Player_Movement.Speed -= Player_Movement.Base_Speed * Speed_Boost;
            Activate_Spell = false;
        }
    }
}
