using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Logic : MonoBehaviour
{
    public Sword_Attack sword_attack;
    public Template_DoT_Held template_dot_held;
    public Template_AoE_OnMouse template_aoe_onmouse;
    float Attack_Duration;
    float Attack_Damage;
    float Iframes_Sword = 0f;
    float Iframes_Held_Spell = 0f;
    float Mana_Cost;
    float Mana_Cost_Held;
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
             Debug.Log(Health);
             if (Health <= 0)
                 Destroy(gameObject);

        //Template_AoE_OnMouse
        if (collision.gameObject.tag == "Template_AoE_OnMouse")
        {
            template_aoe_onmouse = FindAnyObjectByType<Template_AoE_OnMouse>();
            Mana_Cost = template_aoe_onmouse.Mana_Cost;
            Attack_Damage = template_aoe_onmouse.Damage;
            Health -= Attack_Damage;
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
