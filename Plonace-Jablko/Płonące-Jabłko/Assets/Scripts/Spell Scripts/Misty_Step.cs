using System;
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
    public bool Activate_Spell = false;
    public float Speed_Boost = 10f;
    public Player_Logic player_logic;
    public Player_Movement player_movement;
    Vector3 Direction_Of_Movement;
    float x;
    float y;
    float Spell_End;
    public Transform Player_Character;


    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    void Update()
    {
        if (Input.GetKeyDown(Cast) && mana_tracker.Current_Mana >= Mana_Cost && !player_logic.Player_Attack_Lockout && (player_movement.Direction_Of_Movement.x != 0 || player_movement.Direction_Of_Movement.y != 0))
        {
            Direction_Of_Movement = player_movement.Direction_Of_Movement;
            x = Direction_Of_Movement.x;
            y = Direction_Of_Movement.y;
            Direction_Of_Movement.x /= (float)Math.Sqrt(x * x + y * y);
            Direction_Of_Movement.y /= (float)Math.Sqrt(x * x + y * y);
            Spell_End = Time.time + Time_Active;
            mana_tracker.Current_Mana -= Mana_Cost;
        }
        if (Time.time < Spell_End)
        {
            Player_Character.position += Direction_Of_Movement * Speed_Boost * Time.deltaTime;
            Activate_Spell = true;
        }
        else Activate_Spell = false;
    }
}