using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float Speed;

    [Header("Base Movement")]
    public float Base_Speed = 5f;
    public KeyCode Movement_Up;
    public KeyCode Movement_Down;
    public KeyCode Movement_Left;
    public KeyCode Movement_Right;
    //Colliders below are separete as to allow the Player to slide of off terrain with a capsule collider while preventing them from jittering when running into a wall
    [Header("Anti-Jitter Colliders")]
    [Tooltip("Size of the collision box spawned")]
    public Vector2 Collider_Vertical;
    [Tooltip("Size of the collision box spawned")]
    public Vector2 Collider_Horizontal;
    [Header("Dodging")]
    public KeyCode Dodge_Roll;
    public float Dodge_Speed;
    public float Dodge_Length;
    float Dodge_End;
    public float Dodge_Recovery;
    public float Dodge_Recovery_Slow;
    float Dodge_Recovery_End;
    [Header("Reference Scripts")]
    public Boulder_Throw boulder_throw;
    public Longstrider longstrider;
    public Misty_Step misty_step;
    public Restoration restoration;
    public Sword_Attack sword_attack;
    public Player_Logic player_logic;
    Vector3 Past_Position;
    Vector3 Current_Position;
    Vector3 Dodge_Direction;
    float x;
    float y;
    [Header("Script Access")]
    public Vector3 Direction_Of_Movement;
    public bool Dodge_Is_Active;
    public bool Dodge_Recovery_Is_Active;


    //JitterFix mask
    LayerMask Collision_Mask;
    private void Start()
    {
        Speed = Base_Speed;
        Past_Position = Current_Position = GetComponent<Transform>().position;
        Collision_Mask = LayerMask.GetMask("Wall");
    }

    void Update()
    {
        Speed = Base_Speed;

        //Current Movement Speed calculations
        if (boulder_throw.Activate_Spell)
            Speed *= 1 - boulder_throw.Self_Slow;
        if (longstrider.Activate_Spell)
            Speed *= 1 + longstrider.Speed_Boost;
        if (misty_step.Activate_Spell)
            Speed = 0;
        if(restoration.Activate_Spell ||  restoration.Spell_Is_On)
            Speed *= 1 - restoration.Self_Slow;
        if (Dodge_Recovery_Is_Active)
            Speed *= 1 - Dodge_Recovery_Slow;
        if (sword_attack.Attack_Is_Active)
            Speed = 0;

        //Dodging
            Past_Position = Current_Position;
        Current_Position = GetComponent<Transform>().position;
        Direction_Of_Movement = Current_Position - Past_Position;
        if (Input.GetKeyDown(Dodge_Roll) && Time.time > Dodge_Recovery_End && (Direction_Of_Movement.x != 0 || Direction_Of_Movement.y !=0) && !player_logic.Player_Attack_Lockout)
        {
            Dodge_Direction = Direction_Of_Movement;
            x = Dodge_Direction.x;
            y = Dodge_Direction.y;
            Dodge_Direction.x /= (float)Math.Sqrt(x * x + y * y);
            Dodge_Direction.y /= (float)Math.Sqrt(x * x + y * y);
            Dodge_End = Time.time + Dodge_Length;
            Dodge_Recovery_End = Dodge_End + Dodge_Recovery;
        }
        if (Time.time < Dodge_End)
        {
            Speed = 0;
            GetComponent<Transform>().position += Dodge_Direction * Dodge_Speed * Time.deltaTime;
            Dodge_Is_Active = true;
        }
        else if (Time.time >= Dodge_End && Time.time < Dodge_Recovery_End)
        {
            Dodge_Is_Active = false;
            Dodge_Recovery_Is_Active = true;
        }
        else
        {
            Dodge_Is_Active = false;
            Dodge_Recovery_Is_Active = false;
        }


        //Basic Movement
        if (Input.GetKey(Movement_Left) && !Physics2D.OverlapBox(gameObject.transform.position + Vector3.left * Speed * Time.deltaTime, Collider_Horizontal, 0f, Collision_Mask))
        {
            GetComponent<Transform>().position += Vector3.left * Speed * Time.deltaTime;
        }
        if (Input.GetKey(Movement_Right) && !Physics2D.OverlapBox(gameObject.transform.position + Vector3.right * Speed * Time.deltaTime, Collider_Horizontal, 0f, Collision_Mask))
        {
            GetComponent<Transform>().position += Vector3.right * Speed * Time.deltaTime;
        }
        if (Input.GetKey(Movement_Up) && !Physics2D.OverlapBox(gameObject.transform.position + Vector3.up * Speed * Time.deltaTime, Collider_Vertical, 0f, Collision_Mask))
        {
            GetComponent<Transform>().position += Vector3.up * Speed * Time.deltaTime;
        }
        if (Input.GetKey(Movement_Down) && !Physics2D.OverlapBox(gameObject.transform.position + Vector3.down * Speed * Time.deltaTime, Collider_Vertical, 0f, Collision_Mask))
        {
            GetComponent<Transform>().position += Vector3.down * Speed * Time.deltaTime;
        }
    }

}
