using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Legacy : MonoBehaviour
{
    Vector2 Mouse_Position;
    Vector2 Hitbox_Position;
    Transform Hitbox_Transform;
    Quaternion Hitbox_Rotation;
    public Object Hitbox;
    public KeyCode Cast = KeyCode.Alpha6;
    public float Time_Active = 0.1f;
    public float Cast_Time = 0.25f;
    public float Mana_Cost = 20f;
    public float Damage = 20f;
    public Mana_Tracker mana_tracker;
    private float Input_Time;
    bool Activate_Spell = false;

    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    void Update()
    {


        if (Input.GetKeyDown(Cast) && mana_tracker.Current_Mana >= Mana_Cost)
        {
            mana_tracker.Current_Mana -= Mana_Cost;
            Mouse_Position = Input.mousePosition;
            Hitbox_Position = Camera.main.ScreenToWorldPoint(Mouse_Position);
            Input_Time = Time.time;
            Activate_Spell = true;
        }

        if (Activate_Spell && Time.time >= Input_Time + Cast_Time)
        {
            Destroy(Instantiate(Hitbox, Hitbox_Position, Hitbox_Rotation, Hitbox_Transform), Time_Active);
            Activate_Spell = false;
        }
    }
}
