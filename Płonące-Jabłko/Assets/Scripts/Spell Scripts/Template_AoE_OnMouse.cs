using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template_AoE_OnMouse : MonoBehaviour
{
    Vector2 Mouse_Position;
    Vector2 Hitbox_Position;
    Transform Hitbox_Transform;
    Quaternion Hitbox_Rotation;
    public Object Hitbox;
    public KeyCode Cast = KeyCode.Alpha2;
    public float Time_Active = 0.1f;
    public float Mana_Cost = 20f;
    public float Damage = 35f;
    public Mana_Tracker mana_tracker;

    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    void Update()
    {
        if(Input.GetKeyDown(Cast) && mana_tracker.Current_Mana >= Mana_Cost)
        {
                Mouse_Position = Input.mousePosition;
                Hitbox_Position = Camera.main.ScreenToWorldPoint(Mouse_Position);
                Destroy(Instantiate(Hitbox, Hitbox_Position, Hitbox_Rotation, Hitbox_Transform), Time_Active);
                mana_tracker.Current_Mana -= Mana_Cost;
        }
    }
}
