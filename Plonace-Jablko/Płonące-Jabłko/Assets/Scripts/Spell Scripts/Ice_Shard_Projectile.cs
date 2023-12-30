using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Shard_Projectile : MonoBehaviour
{

    public float Speed = 10f;
    public float Acceleration = 10f;
    public float Max_Speed = 25f;
    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;
        if (Speed < Max_Speed)
            Speed += Acceleration * Time.deltaTime;
        else
            Speed = Max_Speed;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player_Character")
            Destroy(gameObject);
    }
}