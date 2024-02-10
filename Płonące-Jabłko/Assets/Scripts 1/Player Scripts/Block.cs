using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public KeyCode Cast = KeyCode.Mouse1;
    public bool Block_Is_Active;
    public float Perfect_Parry_Window;
    public float Perfect_Parry_Cooldown;
    public bool Perfect_Parry;
    float Release_Time;
    float Press_Time;
    public float Fire_Resistance = 0f;
    public float Earth_Resistance = 0f;
    public float Water_Resistance = 0f;
    public float Air_Resistance = 0f;
    public float Physical_Resistance = 0f;
    void Update()
    {
        if(Input.GetKeyDown(Cast))
        {
            Press_Time = Time.time;
        }

        if(Input.GetKeyUp(Cast))
        {
            Release_Time = Time.time;
        }

        if(Press_Time + Perfect_Parry_Window >= Time.time && Release_Time + Perfect_Parry_Cooldown <= Time.time)
            Perfect_Parry = true;
        else
            Perfect_Parry = false;

        if (Input.GetKey(Cast))
        {
            Block_Is_Active = true;
        }
        else
            Block_Is_Active = false;

    }
}
