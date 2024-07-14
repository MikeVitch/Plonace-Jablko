using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Projectile : MonoBehaviour
{

    public float Speed = 25f;
    Vector2 Hitbox_Position;
    Transform Hitbox_Transform;
    Quaternion Hitbox_Rotation;
    public Object Hitbox;
    public float Projectile_Time_Active = 0.25f;
    public float Fireball_Time_Active = 0.1f;
    public Fireball fireball;
    float Destroy_Time;

    void Start()
    {
        Destroy_Time = Time.time + Projectile_Time_Active;
    }
    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;

        Hitbox_Position = transform.position;
        if (Destroy_Time <= Time.time)
        {
            Destroy(gameObject);
            Destroy(Instantiate(Hitbox, Hitbox_Position, Hitbox_Rotation, Hitbox_Transform), Fireball_Time_Active);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player_Character" || collision.gameObject.tag == "Untargetable")
        { }
        else
        {
            Destroy(gameObject);
            Destroy(Instantiate(Hitbox, Hitbox_Position, Hitbox_Rotation, Hitbox_Transform), Fireball_Time_Active);
        }
            
    }
}