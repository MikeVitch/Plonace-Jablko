using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zjawa_Tornado : MonoBehaviour
{
    Vector2 Zjawa_Position;
    Vector2 Hitbox_Position;
    Transform Hitbox_Transform;
    Quaternion Hitbox_Rotation;
    public Object Hitbox;
    public float Time_Active = 1f;
    public float Cast_Time = 0f;
    public float Damage = 35f;
    public float Cooldown;
    public bool Activate_Spell = false;
    public Transform Zjawa;
    public float Recovery;

    void Update()
    {

        if (Activate_Spell)
        {
            Hitbox_Rotation = transform.rotation;
            Zjawa_Position = Zjawa.position;
            Hitbox_Position = Camera.main.ScreenToWorldPoint(Zjawa_Position);
            Destroy(Instantiate(Hitbox, Zjawa_Position, Hitbox_Rotation, Hitbox_Transform), Time_Active);
            Activate_Spell = false;
        }
    }
}
