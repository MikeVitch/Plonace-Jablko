using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Logic : MonoBehaviour
{

    public float Max_Health = 100f;
    public float Invincibility_On_Hit_Lenght = 0.5f;
    public KeyCode Interaction_Key = KeyCode.E;
    public float Fire_Resistance = 0f;
    public float Earth_Resistance = 0f;
    public float Water_Resistance = 0f;
    public float Air_Resistance = 0f;
    public float Physical_Resistance = 0f;

    [Header("Reference Scripts")]
    public Dialogue_Manager dialogue_manager;
    public Enemy_Attack_Template_Melee enemy_attack_template_melee;
    public Player_Movement player_movement;
    public Restoration restoration;
    public Sword_Attack sword_attack;
    public Ice_Shield ice_shield;
    public Block block;
    public Boulder_Throw boulder_throw;
    public Fan_Of_Ice fan_of_ice;
    public Fireball fireball;
    public Flamethrower flamethrower;
    public Ice_Shard ice_shard;
    public Longstrider longstrider;
    public Misty_Step misty_step;
    public Push push;
    public Stone_Volley stone_volley;
    public ScreenMenu_Manager screenMenuManager;


    Zjawa_Push zjawa_push;
    Zjawa_Tornado zjawa_tornado;
    Derek_Attack derek_attack;

    float Restoration_Next_Tick = 0;
    [Header("Script Access")]
    public Vector3 Player_Position;
    public float Health = 100f;
    float Invincibilty_On_Hit_End;
    float Attack_Damage;
    public bool Invincibility;
    public bool Player_Attack_Lockout;
    public bool Zjawa_Push_Collision;
    public bool Is_Hidden;

    void Update()
    {
        Player_Position = GetComponent<Transform>().position;
        if(Health > Max_Health)
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

        if(ice_shield.Spell_Is_Active)
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
        Player_Attack_Lockout = false;

        if (player_movement.Dodge_Is_Active || player_movement.Dodge_Recovery_Is_Active)
            Player_Attack_Lockout = true;
        else if (sword_attack.Attack_Is_Active)
            Player_Attack_Lockout = true;
        else if(boulder_throw.Activate_Spell)
            Player_Attack_Lockout = true;
        else if(fan_of_ice.Activate_Spell)
            Player_Attack_Lockout = true;
        else if(fireball.Activate_Spell || flamethrower.Spell_Is_On)
            Player_Attack_Lockout = true;
        else if(flamethrower.Activate_Spell)
            Player_Attack_Lockout = true;
        else if(ice_shard.Activate_Spell)
            Player_Attack_Lockout = true;
        else if(stone_volley.Activate_Spell)
            Player_Attack_Lockout = true;
        else if(misty_step.Activate_Spell)
            Player_Attack_Lockout = true;
        else if(restoration.Activate_Spell || restoration.Spell_Is_On)
            Player_Attack_Lockout = true;
        else if(longstrider.Spell_Is_Cast)
            Player_Attack_Lockout = true;
        else if(push.Activate_Spell)
            Player_Attack_Lockout = true;
        else if(Zjawa_Push_Collision)
            Player_Attack_Lockout = true;
        else if(dialogue_manager != null && dialogue_manager.Dialogue_Active)
            Player_Attack_Lockout = true;







        //Invincibility
        if (Time.time <= Invincibilty_On_Hit_End || player_movement.Dodge_Is_Active)
            Invincibility = true;
        else
            Invincibility= false;

       // Debug.Log(Health);

        //screenMenuManager.SetHealth(Health);          uncomment after testing

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Melee_Test_Attack
        if(collision.gameObject.tag == "Melee_Test_Attack" && !Invincibility)
        {
            enemy_attack_template_melee = FindObjectOfType<Enemy_Attack_Template_Melee>();
            Attack_Damage = enemy_attack_template_melee.Attack_Damage;
            Health -= Attack_Damage * (1 - 0.01f * Physical_Resistance);
            Invincibilty_On_Hit_End = Time.time + Invincibility_On_Hit_Lenght;
        }

        //Zjawa_Tornado
        if (collision.gameObject.tag == "Zjawa_Tornado_Projectile" && !Invincibility)
        {
            zjawa_tornado = FindObjectOfType<Zjawa_Tornado>();
            Attack_Damage = zjawa_tornado.Damage;
            Health -= Attack_Damage * (1 - 0.01f * Air_Resistance);
            Invincibilty_On_Hit_End = Time.time + Invincibility_On_Hit_Lenght;
        }

        //Derek_Attack
        if (collision.gameObject.tag == "Derek_Attack" && !Invincibility)
        {
            Attack_Damage = FindObjectOfType<Derek_Attack>().Attack_Damage;
            Health -= Attack_Damage * (1 - 0.01f * Physical_Resistance);
            Invincibilty_On_Hit_End = Time.time + Invincibility_On_Hit_Lenght;
        }
        //Debug.Log(Health);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Zjawa_Push
        if (collision.gameObject.tag == "Zjawa_Push_Projectile" && !Invincibility)
        {
            zjawa_push = FindObjectOfType<Zjawa_Push>();
            //transform.position += (gameObject.transform.position - collision.transform.position)/Vector3.Distance(gameObject.transform.position, collision.transform.position) * zjawa_push.Projectile_Speed * Time.deltaTime;
            transform.position += collision.transform.right * zjawa_push.Projectile_Speed * Time.deltaTime;
            Zjawa_Push_Collision = true;
        }
            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Zjawa_Push
        if (collision.gameObject.tag == "Zjawa_Push_Projectile")
        {
            Zjawa_Push_Collision = false;
        }
    }
}
