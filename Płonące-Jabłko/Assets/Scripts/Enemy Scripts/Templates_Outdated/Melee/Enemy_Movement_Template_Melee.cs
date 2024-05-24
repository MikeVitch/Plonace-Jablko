using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy_Movement_Template_Melee : MonoBehaviour
{
    public Enemy_Attack_Template_Melee enemy_attack_template_melee;
    Player_Logic player_logic;
    public float Base_Movement_Speed = 7.5f;
    float Movement_Speed;
    public float Aggro_Range = 10f;
    public float Minimum_Distance = 1.5f;
    public bool Is_In_Range = false;

    private void Start()
    {
        player_logic = FindObjectOfType<Player_Logic>();
    }
    void Update()
    {
        if (Is_In_Range || enemy_attack_template_melee.Staggered)
            Movement_Speed = 0;
        else
            Movement_Speed = Base_Movement_Speed;

        if (enemy_attack_template_melee.Attack_Is_Active == false)
        {
            if (Vector3.Distance(transform.position, player_logic.Player_Position) <= Minimum_Distance)
            {
                Is_In_Range = true;
            }
            else
                Is_In_Range = false;
            if (Vector3.Distance(transform.position, player_logic.Player_Position) <= Aggro_Range)
            {
                transform.right = player_logic.Player_Position - transform.position;
                transform.position += transform.right * Movement_Speed * Time.deltaTime;
            }
        }
    }
}
