using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Zjawa_Tornado_Projectile : MonoBehaviour
{

    public float Speed = 10f;
    float Acceleration;
    float Max_Speed;
    Player_Logic player_logic;
    Zjawa_Tornado zjawa_tornado;
    Vector3 Direction_Of_Movement;
    Vector3 Direction_To_Player;
    Vector3 Change_In_Movement;
    float Homing_Strenght;
    float x;
    float y;

    private void Start()
    {

        player_logic = FindObjectOfType<Player_Logic>();
        zjawa_tornado = FindObjectOfType<Zjawa_Tornado>(); 
        Acceleration = zjawa_tornado.Acceleration;
        Max_Speed = zjawa_tornado.Max_Speed;
        Direction_Of_Movement = (player_logic.Player_Position - transform.position) / Vector3.Distance(player_logic.Player_Position, transform.position);
        Homing_Strenght = zjawa_tornado.Homing_Percentage / 10;
    }
    void Update()
    {
        Direction_To_Player = (player_logic.Player_Position - transform.position) / Vector3.Distance(player_logic.Player_Position, transform.position);
        Change_In_Movement = (Direction_To_Player * Homing_Strenght + Direction_Of_Movement * Homing_Strenght * -1) - Direction_Of_Movement;
        Direction_Of_Movement += Change_In_Movement * Time.deltaTime;
        //Normalizing the direction
        x = Direction_Of_Movement.x;
        y = Direction_Of_Movement.y;
        Direction_Of_Movement.x /= (float)Math.Sqrt(x * x + y * y);
        Direction_Of_Movement.y /= (float)Math.Sqrt(x * x + y * y);

        transform.position += Direction_Of_Movement * Speed * Time.deltaTime;
        if (Speed < Max_Speed)
            Speed += Acceleration * Time.deltaTime;
        else
            Speed = Max_Speed;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Zjawa")
            Destroy(gameObject);
    }
}
