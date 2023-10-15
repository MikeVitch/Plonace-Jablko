using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Logic : MonoBehaviour
{
    public Sword_Attack sword_attack;
    public Template_DoT_Held template_dot_held;
    public Template_AoE_OnMouse template_aoe_onmouse;
    public Template_Projectile template_projectile;
    public Boulder_Throw boulder_throw;
    public Stone_Volley stone_volley;
    public Fireball fireball;
    public Flamethrower flamethrower;
    public Ice_Shard ice_shard;
    public Fan_Of_Ice fan_of_ice;
    public float Fire_Resistance = 0f;
    public float Earth_Resistance = 0f;
    public float Water_Resistance = 0f;
    public float Air_Resistance = 0f;
    float Attack_Duration;
    float Attack_Damage;
    float Iframes_Sword = 0f;
    float Iframes_Held_Spell = 0f;
    public float Health = 100f;
    bool Is_On_Fire;
    public float Burn_Damage = 5f;
    public float Burn_Frequency = 0.5f;
    public float Burn_Duration = 2f;
    float Burn_End;
    float Burn_Next_Tick;


    private void Update()
    {
        //Burn Damage
        if (Is_On_Fire && Burn_Next_Tick <= Time.time)
        {
            Health -= Burn_Damage;
            Burn_Next_Tick += Burn_Frequency;
        }
        if (Time.time >= Burn_End)
            Is_On_Fire = false;
        Debug.Log(Health);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Sword_Attack
        if (collision.gameObject.tag == "Player_Weapon")
        {
            sword_attack = FindObjectOfType<Sword_Attack>();
            Attack_Duration = sword_attack.Attack_Duration;
            Attack_Damage = sword_attack.Attack_Damage;
            if (Time.time > Iframes_Sword)
            {
                Health -= Attack_Damage;
                Iframes_Sword = Time.time + Attack_Duration;
            }
        }

        //Template_AoE_OnMouse
        if (collision.gameObject.tag == "Template_AoE_OnMouse")
        {
            template_aoe_onmouse = FindAnyObjectByType<Template_AoE_OnMouse>();
            Attack_Damage = template_aoe_onmouse.Damage;
            Health -= Attack_Damage;
        }

        //Template_Projectile
        if(collision.gameObject.tag == "Template_Projectile_Projectile")
        {
            template_projectile = FindObjectOfType<Template_Projectile>();
            Attack_Damage = template_projectile.Damage;
            Health -= Attack_Damage;
        }

        //Stone_Volley
        if (collision.gameObject.tag == "Stone_Volley_Projectile")
        {
            stone_volley = FindObjectOfType<Stone_Volley>();
            Attack_Damage = stone_volley.Damage;
            Health -= Attack_Damage * (1 - 0.01f * Earth_Resistance);
        }

        //Boulder_Throw
        if(collision.gameObject.tag == "Boulder_Throw_Projectile")
        {
            boulder_throw = FindObjectOfType<Boulder_Throw>();
            Attack_Damage = boulder_throw.Damage;
            Health -= Attack_Damage * (1 - 0.01f * Earth_Resistance);
        }

        //Fireball
        if (collision.gameObject.tag == "Fireball_Hitbox")
        {
            fireball = FindAnyObjectByType<Fireball>();
            Attack_Damage = fireball.Damage;
            Health -= Attack_Damage * (1 - 0.01f * Fire_Resistance);
            Is_On_Fire = true;
            Burn_End = Time.time + Burn_Duration;
            Burn_Next_Tick = Time.time + Burn_Frequency;
        }

        //Ice_Shard
        if (collision.gameObject.tag == "Ice_Shard_Projectile")
        {
            ice_shard = FindObjectOfType<Ice_Shard>();
            Attack_Damage = ice_shard.Damage;
            Health -= Attack_Damage * (1 - 0.01f * Water_Resistance);
        }

        //Fan_Of_Ice
        if (collision.gameObject.tag == "Fan_Of_Ice_Projectile")
        {
            fan_of_ice = FindObjectOfType<Fan_Of_Ice>();
            Attack_Damage = fan_of_ice.Damage;
            Health -= Attack_Damage * (1 - 0.01f * Water_Resistance);
        }

        Debug.Log(Health);
        if (Health <= 0)
            Destroy(gameObject);

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //Template_DoT_Held
        if (collision.gameObject.tag == "Template_DoT_Held")
        {
            template_dot_held = FindAnyObjectByType<Template_DoT_Held>();
            Attack_Duration = template_dot_held.Damage_Tickrate;
            Attack_Damage = template_dot_held.Damage;
            if (Time.time > Iframes_Held_Spell)
            {
                Health -= Attack_Damage;
                Iframes_Held_Spell = Time.time + Attack_Duration;
            }
        }

        //Flamethrower
        if (collision.gameObject.tag == "Flamethrower_Hitbox")
        {
            flamethrower = FindAnyObjectByType<Flamethrower>();
            Attack_Duration = flamethrower.Damage_Tickrate;
            Attack_Damage = flamethrower.Damage;
            if (Time.time > Iframes_Held_Spell)
            {
                Health -= Attack_Damage * (1 - 0.01f * Fire_Resistance);
                Iframes_Held_Spell = Time.time + Attack_Duration;
                Is_On_Fire = true;
                Burn_End = Time.time + Burn_Duration;
                Burn_Next_Tick = Time.time + Burn_Frequency;
            }
        }
        Debug.Log(Health);
            if (Health <= 0)
                Destroy(gameObject);
    }
}
