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
    public float Fire_Resistance = 0f;
    public float Earth_Resistance = 0f;
    public float Water_Resistance = 0f;
    public float Air_Resistance = 0f;
    float Attack_Duration;
    float Attack_Damage;
    float Iframes_Sword = 0f;
    float Iframes_Held_Spell = 0f;
    public float Health = 100f;

    //One time damage
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
            Debug.Log(Health);
            if (Health <= 0)
                Destroy(gameObject);
    }
}
