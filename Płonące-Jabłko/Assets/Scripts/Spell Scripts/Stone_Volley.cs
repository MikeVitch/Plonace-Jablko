using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_Volley : MonoBehaviour
{
    Vector2 Player_Position;
    Vector2 Hitbox_Position;
    Transform Hitbox_Transform;
    Quaternion Hitbox_Rotation;
    public Object Hitbox;
    public KeyCode Cast = KeyCode.Alpha4;
    public float Time_Active = 1f; //How long before projectile despawns
    public float Cast_Time = 1f;
    public float Mana_Cost = 15f;
    public float Damage = 10f;
    public int Amount_Of_Shots = 3;
    int Shot_Counter = 0;
    public float Shot_Interval = 0.3f;
    float Next_Shot = 0f;
    public Mana_Tracker mana_tracker;
    private float Input_Time;
    bool Activate_Spell = false;
    public Transform player;

    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    void Update()
    {


        if (Input.GetKeyDown(Cast) && mana_tracker.Current_Mana >= Mana_Cost)
        {
            mana_tracker.Current_Mana -= Mana_Cost;
            Input_Time = Time.time;
            Activate_Spell = true;
        }

        if (Activate_Spell && Time.time >= Input_Time + Cast_Time)
        {
            if(Next_Shot <= Time.time)
            {
                Hitbox_Rotation = transform.rotation;
                Player_Position = player.position;
                Hitbox_Position = Camera.main.ScreenToWorldPoint(Player_Position);
                Destroy(Instantiate(Hitbox, Player_Position, Hitbox_Rotation, Hitbox_Transform), Time_Active);
                Shot_Counter++;
                Next_Shot = Time.time + Shot_Interval;
            }
            if (Shot_Counter >= Amount_Of_Shots)
            {
                Shot_Counter = 0;
                Activate_Spell = false;
            }
        }
    }
}
