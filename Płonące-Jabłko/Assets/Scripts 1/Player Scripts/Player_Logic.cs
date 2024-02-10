using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Logic : MonoBehaviour
{
    public Enemy_Attack_Template_Melee enemy_attack_template_melee;
    public Player_Movement player_movement;
    public Restoration restoration;
    public Sword_Attack sword_attack;
    public Ice_Shield ice_shield;
    public Block block;
 

    float Restoration_Next_Tick = 0;
    public Vector3 Player_Position;
    public float Fire_Resistance = 0f;
    public float Earth_Resistance = 0f;
    public float Water_Resistance = 0f;
    public float Air_Resistance = 0f;
    public float Physical_Resistance = 0f;
    public float Max_Health = 100f;
    public float Health = 100f;
    public float Invincibility_On_Hit_Lenght = 0.5f;
    float Invincibilty_On_Hit_End;
    float Attack_Damage;
    public bool Invincibility;
    public bool Player_Attack_Lockout;

    void Update()
    {
        Player_Position = GetComponent<Transform>().position;
        if (Health > Max_Health)
            Health = Max_Health;

        //Restoration
        if (restoration.Spell_Is_On)
        {
            if (Time.time > Restoration_Next_Tick)
            {
                Health += restoration.Heal_Amount;
                Restoration_Next_Tick = Time.time + restoration.Heal_Tickrate;
            }
        }

        //Player_Resistance
        Fire_Resistance = 0;
        Earth_Resistance = 0;
        Water_Resistance = 0;
        Air_Resistance = 0;
        Physical_Resistance = 0;

        if (ice_shield.Spell_Is_Active)
        {
            Fire_Resistance += (100 - Fire_Resistance) * ice_shield.Fire_Resistance * 0.01f;
            Earth_Resistance += (100 - Earth_Resistance) * ice_shield.Earth_Resistance * 0.01f;
            Water_Resistance += (100 - Water_Resistance) * ice_shield.Water_Resistance * 0.01f;
            Air_Resistance += (100 - Air_Resistance) * ice_shield.Air_Resistance * 0.01f;
            Physical_Resistance += (100 - Physical_Resistance) * ice_shield.Physical_Resistance * 0.01f;
        }

        if (block.Block_Is_Active)
        {
            Fire_Resistance += (100 - Fire_Resistance) * block.Fire_Resistance * 0.01f;
            Earth_Resistance += (100 - Earth_Resistance) * block.Earth_Resistance * 0.01f;
            Water_Resistance += (100 - Water_Resistance) * block.Water_Resistance * 0.01f;
            Air_Resistance += (100 - Air_Resistance) * block.Air_Resistance * 0.01f;
            Physical_Resistance += (100 - Physical_Resistance) * block.Physical_Resistance * 0.01f;
        }

        //Player_Attack_Lockout
        if (player_movement.Dodge_Is_Active || player_movement.Dodge_Recovery_Is_Active)
            Player_Attack_Lockout = true;
        else if (sword_attack.Attack_Is_Active)
            Player_Attack_Lockout = true;
        else
            Player_Attack_Lockout = false;

        //Invincibility
        if (Time.time <= Invincibilty_On_Hit_End || player_movement.Dodge_Is_Active)
            Invincibility = true;
        else
            Invincibility = false;


        //Debug.Log(Health);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Melee_Test_Attack
        if (collision.gameObject.tag == "Melee_Test_Attack" && !Invincibility)
        {
            enemy_attack_template_melee = FindObjectOfType<Enemy_Attack_Template_Melee>();
            Attack_Damage = enemy_attack_template_melee.Attack_Damage;
            Health -= Attack_Damage * (1 - 0.01f * Physical_Resistance);
            Invincibilty_On_Hit_End = Time.time + Invincibility_On_Hit_Lenght;
        }
        //Debug.Log(Health);



    }


}
