using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan_Of_Ice : MonoBehaviour
{
    Transform Hitbox_Transform;
    Quaternion Hitbox_Rotation;
    Quaternion Hitbox_Rotation_Left;
    Quaternion Hitbox_Rotation_Right;
    Vector3 Rotation_Vector;
    public Object Hitbox;
    public KeyCode Cast = KeyCode.Alpha8;
    public float Time_Active = 1f;
    public float Cast_Time = 0f;
    public float Mana_Cost = 15f;
    public float Damage = 10f;
    public Mana_Tracker mana_tracker;
    private float Input_Time;
    public bool Activate_Spell = false;
    public Transform player;
    public float Projectile_Offset_Side;
    public float Projectile_Offset_Down;
    public float Rotation_Offset;
    public Player_Logic player_logic;
    public bool Unlocked = false;

    //Don't forget to set Character_Sprite as reference for Mana_Tracker


    void Update()
    {

        if (Activate_Spell && Time.time >= Input_Time + Cast_Time)
        {
            Hitbox_Rotation = transform.rotation;
            Rotation_Vector = Hitbox_Rotation.eulerAngles;
            Hitbox_Rotation_Left = Quaternion.Euler(new Vector3(Rotation_Vector.x, Rotation_Vector.y, Rotation_Vector.z + Rotation_Offset));
            Hitbox_Rotation_Right = Quaternion.Euler(new Vector3(Rotation_Vector.x, Rotation_Vector.y, Rotation_Vector.z - Rotation_Offset));
            Destroy(Instantiate(Hitbox, transform.position, Hitbox_Rotation, Hitbox_Transform), Time_Active);
            Destroy(Instantiate(Hitbox, transform.position + transform.up * Projectile_Offset_Side + transform.right * -1 * Projectile_Offset_Down, Hitbox_Rotation_Left, Hitbox_Transform), Time_Active);
            Destroy(Instantiate(Hitbox, transform.position + transform.up * -1 * Projectile_Offset_Side + transform.right * -1 * Projectile_Offset_Down, Hitbox_Rotation_Right, Hitbox_Transform), Time_Active);

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
