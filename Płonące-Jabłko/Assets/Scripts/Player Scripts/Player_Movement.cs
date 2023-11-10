using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float Speed;
    public float Base_Speed = 5f;

    private void Start()
    {
        Speed = Base_Speed;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Transform>().position += Vector3.left * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Transform>().position += Vector3.right * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Transform>().position += Vector3.up * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Transform>().position += Vector3.down * Speed * Time.deltaTime;
        }
    }


    public Boulder_Throw boulder_throw;
    public Longstrider longstrider;
    public Misty_Step misty_step;
    public Restoration restoration;
    void Update()
    {
        Speed = Base_Speed;
        if (boulder_throw.Activate_Spell)
            Speed *= 1 - boulder_throw.Self_Slow;
        if (longstrider.Activate_Spell)
            Speed *= 1 + longstrider.Speed_Boost;
        if(misty_step.Activate_Spell)
            Speed *= 1 + misty_step.Speed_Boost;
        if(restoration.Activate_Spell ||  restoration.Spell_Is_On)
            Speed *= 1 - restoration.Self_Slow;
    }

}
