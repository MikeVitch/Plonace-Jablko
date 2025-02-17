using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    private float Speed;
    Animator animator;
    [Header("Base Movement")]
    public float Base_Speed = 5f;
    public KeyCode Movement_Up;
    public KeyCode Movement_Down;
    public KeyCode Movement_Left;
    public KeyCode Movement_Right;
    [Header("Dodging")]
    public KeyCode Dodge_Roll;
    public float Dodge_Speed;
    public float Dodge_Length;
    float Dodge_End;
    public float Dodge_Recovery;
    public float Dodge_Recovery_Slow;
    float Dodge_Recovery_End;
    [Header("Stealth")]
    public KeyCode Crouching;
    [Tooltip("Fraction by which to multiply Speed")]
    public float Crouching_Speed;
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
    public bool Is_Crouching;


    //JitterFix mask
    LayerMask Collision_Mask;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Speed = Base_Speed;
        Past_Position = Current_Position = GetComponent<Transform>().position;
        Collision_Mask = LayerMask.GetMask("Wall");
        Collision_Mask = LayerMask.GetMask("Feet");
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
        if(player_logic.Zjawa_Push_Collision)
            Speed = 0;
        if(Is_Crouching)
            Speed *= Crouching_Speed;

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
            if(Physics2D.OverlapCapsule(gameObject.transform.position + Dodge_Direction * Dodge_Speed * Time.deltaTime, gameObject.GetComponent<CapsuleCollider2D>().size * gameObject.transform.localScale * new Vector2(0.99f,0.99f), gameObject.GetComponent<CapsuleCollider2D>().direction, 0f, Collision_Mask))
            {
                Dodge_End = Time.time;
                Dodge_Recovery_End = Dodge_End + Dodge_Recovery;
            }
            else
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

        //Stealth
        if(Input.GetKeyDown(Crouching)) 
        {
            Is_Crouching = !Is_Crouching;
        }

        animator.SetFloat("speed", 0);

        //Basic Movement
        if (Input.GetKey(Movement_Left) && !Physics2D.OverlapCapsule(gameObject.transform.position + Vector3.left * Speed * Time.deltaTime, gameObject.GetComponent<CapsuleCollider2D>().size * gameObject.transform.localScale, gameObject.GetComponent<CapsuleCollider2D>().direction, 0f, Collision_Mask))
        {
            animator.SetInteger("direction", 1);
            animator.SetFloat("speed", 1);

            GetComponent<Transform>().position += Vector3.left * Speed * Time.deltaTime;
        }
        if (Input.GetKey(Movement_Right) && !Physics2D.OverlapCapsule(gameObject.transform.position + Vector3.right * Speed * Time.deltaTime, gameObject.GetComponent<CapsuleCollider2D>().size * gameObject.transform.localScale, gameObject.GetComponent<CapsuleCollider2D>().direction, 0f, Collision_Mask))
        {
            animator.SetInteger("direction", 2);
            animator.SetFloat("speed", 1);

            GetComponent<Transform>().position += Vector3.right * Speed * Time.deltaTime;
        }
        if (Input.GetKey(Movement_Up) && !Physics2D.OverlapCapsule(gameObject.transform.position + Vector3.up * Speed * Time.deltaTime, gameObject.GetComponent<CapsuleCollider2D>().size * gameObject.transform.localScale, gameObject.GetComponent<CapsuleCollider2D>().direction, 0f, Collision_Mask))
        {
            animator.SetInteger("direction", 3);
            animator.SetFloat("speed", 1);

            GetComponent<Transform>().position += Vector3.up * Speed * Time.deltaTime;
        }
        if (Input.GetKey(Movement_Down) && !Physics2D.OverlapCapsule(gameObject.transform.position + Vector3.down * Speed * Time.deltaTime, gameObject.GetComponent<CapsuleCollider2D>().size * gameObject.transform.localScale, gameObject.GetComponent<CapsuleCollider2D>().direction, 0f, Collision_Mask))
        {
            animator.SetInteger("direction", 4);
            animator.SetFloat("speed", 1);

            GetComponent<Transform>().position += Vector3.down * Speed * Time.deltaTime;
        }

        
    }

}
