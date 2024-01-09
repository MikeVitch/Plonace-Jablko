using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zjawa_Skills : MonoBehaviour
{
    Player_Logic player_logic;
    float Distance_To_Player;
    public Zjawa_Movement zjawa_movement;
    Zjawa_Push zjawa_push;
    Zjawa_Tornado zjawa_tornado;
    Zjawa_Invincibility zjawa_invincibility;
    Zjawa_Logic zjawa_logic;
    float Push_Time_Of_Cast;
    float Tornado_Time_Of_Cast;
    float Invincibility_Time_Of_Cast;
    float Current_Health;
    float Past_Health;
    public float Push_Activation_Range;
    public float Tornado_Activation_Range;
    public float Invincibility_Activation_Range;
    bool Attack_Lockout;
    private void Start()
    {
        zjawa_push = GetComponent<Zjawa_Push>();
        zjawa_tornado = GetComponent<Zjawa_Tornado>();
        zjawa_invincibility = GetComponent<Zjawa_Invincibility>();
        zjawa_logic = GetComponent<Zjawa_Logic>();
        player_logic = FindObjectOfType<Player_Logic>();

        //Making all skills available at spawn
        Push_Time_Of_Cast -= zjawa_push.Cooldown;
        Tornado_Time_Of_Cast -= zjawa_tornado.Cooldown;
        Invincibility_Time_Of_Cast -= zjawa_invincibility.Cooldown;
        Push_Time_Of_Cast -= zjawa_push.Recovery;
        Tornado_Time_Of_Cast -= zjawa_tornado.Recovery;
    }

    void Update()
    {
        Past_Health = Current_Health;
        Current_Health = zjawa_logic.Health;

        //Attack Lockout
        Attack_Lockout = false;
        if(Time.time <= Push_Time_Of_Cast + zjawa_push.Recovery)
            Attack_Lockout = true;
        if(Time.time <= Tornado_Time_Of_Cast + zjawa_tornado.Recovery)
            Attack_Lockout = true;

        Distance_To_Player = Vector3.Distance(transform.position, player_logic.Player_Position);
        if (Distance_To_Player <= Push_Activation_Range && Time.time > Push_Time_Of_Cast + zjawa_push.Cooldown && !Attack_Lockout)
        {
            zjawa_push.Activate_Spell = true;
            Push_Time_Of_Cast = Time.time;
        }
        else if (Distance_To_Player <= Tornado_Activation_Range && Distance_To_Player > zjawa_movement.Minimum_Distance && Time.time > Tornado_Time_Of_Cast + zjawa_tornado.Cooldown && !Attack_Lockout)
        {
            zjawa_tornado.Activate_Spell = true;
            Tornado_Time_Of_Cast = Time.time;
        } else if (Distance_To_Player <= Invincibility_Activation_Range && Current_Health != Past_Health && Time.time > Invincibility_Time_Of_Cast + zjawa_invincibility.Cooldown)
        {
            zjawa_invincibility.Activate_Spell = true;
            Invincibility_Time_Of_Cast = Time.time;
        }
    }
}
