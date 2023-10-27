using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float Speed = 1f;
    public float Base_Speed;

    private void Start()
    {
        Base_Speed = Speed;
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

}
