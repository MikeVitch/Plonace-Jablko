using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Column_Pushable : MonoBehaviour
{
    Vector3 Push_Direction;
    Column_Push_Projectile column_push_projectile;
    Column_Move_Target column_move_target;
    public float Grid_Size;
    public float Speed;
    public float Snap_Distance;
    Vector3 Movement_Direction;
    float x;
    float y;
    bool Input_Lockout;
    public LayerMask Layer_Columns_Move_On;
    private void Start()
    {
        column_move_target = gameObject.GetComponentInChildren<Column_Move_Target>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detecting Direction
        if (collision.gameObject.tag == "Column_Push_Projectile" && !Input_Lockout)
        {
            column_push_projectile = FindObjectOfType<Column_Push_Projectile>();
            Push_Direction = column_push_projectile.Player_Position - gameObject.transform.position;
            if(Mathf.Abs(Push_Direction.x) >= Mathf.Abs(Push_Direction.y))
            {
                if(Push_Direction.x >= 0)
                {
                    column_move_target.Left = true;
                }
                else
                {
                    column_move_target.Right = true;
                }
            }
            else
            {
                if (Push_Direction.y >= 0)
                {
                    column_move_target.Down = true;
                }
                else
                {
                    column_move_target.Up = true;
                }
            }
        }
    }

    private void Update()
    {
        //Movement
        Input_Lockout = false;
        if(gameObject.transform.position != column_move_target.transform.position)
        {
            Movement_Direction = column_move_target.transform.position - gameObject.transform.position;
            x = Movement_Direction.x;
            y = Movement_Direction.y;
            Movement_Direction.x /= (float)Math.Sqrt(x * x + y * y);
            Movement_Direction.y /= (float)Math.Sqrt(x * x + y * y);
            gameObject.transform.position += Movement_Direction * Speed * Time.deltaTime;
            if(Vector3.Distance(column_move_target.transform.position, gameObject.transform.position) < Snap_Distance)
            {
                gameObject.transform.position = column_move_target.transform.position;
            }
            Input_Lockout = true;
        }

    }
}
