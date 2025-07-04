using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public float Mana_Cost_Initial = 10f;
    public float Mana_Cost_Held = 5f;
    public float Damage = 5f;
    public float Damage_Tickrate = 0.25f;
    public KeyCode Cast = KeyCode.Alpha7;
    public Mana_Tracker mana_tracker;
    public bool Spell_Is_On = false;
    private float Input_Time;
    public bool Activate_Spell = false;
    public float Cast_Time = 1f;
    public Player_Logic player_logic;
    public bool Unlocked = false;

    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }
    void Update()
    {
        if (Input.GetKey(Cast) && Spell_Is_On && mana_tracker.Current_Mana >= Mana_Cost_Held)
            mana_tracker.Current_Mana -= Mana_Cost_Held * Time.deltaTime;
        else
            Spell_Is_On = false;

        if(!Input.GetKey(Cast))
            Activate_Spell = false;

        if (Spell_Is_On)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<PolygonCollider2D>().enabled = true;
            GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().isTrigger = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }

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
    }

}