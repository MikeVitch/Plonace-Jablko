using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Shard : MonoBehaviour
{
    Vector2 Player_Position;
    Vector2 Hitbox_Position;
    Transform Hitbox_Transform;
    Quaternion Hitbox_Rotation;
    public Object Hitbox;
    public KeyCode Cast = KeyCode.Alpha8;
    public float Time_Active = 1f;
    public float Cast_Time = 0f;
    public float Mana_Cost = 20f;
    public float Damage = 35f;
    public Mana_Tracker mana_tracker;
    private float Input_Time;
    public bool Activate_Spell = false;
    public Transform player;
    public Player_Logic player_logic;
    public bool Unlocked = false;

    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    void Update()
    {

        if (Activate_Spell && Time.time >= Input_Time + Cast_Time)
        {
            Hitbox_Rotation = transform.rotation;
            Player_Position = player.position;
            Hitbox_Position = Camera.main.ScreenToWorldPoint(Player_Position);
            Destroy(Instantiate(Hitbox, Player_Position, Hitbox_Rotation, Hitbox_Transform), Time_Active);
            Activate_Spell = false;
        }

        if (Input.GetKeyDown(Cast) && mana_tracker.Current_Mana >= Mana_Cost && !player_logic.Player_Attack_Lockout && Unlocked)
        {
            mana_tracker.Current_Mana -= Mana_Cost;
            Input_Time = Time.time;
            Activate_Spell = true;
        }
    }
}
