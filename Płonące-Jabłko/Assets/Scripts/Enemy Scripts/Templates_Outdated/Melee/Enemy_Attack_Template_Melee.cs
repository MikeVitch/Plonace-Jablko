using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Enemy_Attack_Template_Melee : MonoBehaviour
{
    public Enemy_Movement_Template_Melee enemy_movement_template_melee;
    public float Attack_Duration = 0.1f;
    public float Attack_Windup = 0.4f;
    public float Attack_Recovery = 0.5f;
    public float Attack_Cooldown = 2.0f;
    float Next_Attack = 0;
    float Attack_Deactivation;
    float Attack_Activation;
    float Attack_End;
    public float Attack_Damage = 30f;
    public bool Attack_Is_Active = false;
    bool Windup_Done = false;
    bool Attack_Done = false;
    bool Recovery_Done;
    public bool Staggered;
    public float Stagger_Duration;
    float Stagger_End;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().isTrigger = false;
    }
    void Update()
    {
        if(Staggered && Time.time > Stagger_End)
            Staggered = false;

        if (enemy_movement_template_melee.Is_In_Range == true && Time.time >= Next_Attack && Attack_Is_Active == false && !Staggered)
        { 
            Attack_Is_Active = true;
            Attack_Activation = Time.time + Attack_Windup;        
        }

        if(Time.time >= Attack_Activation && Attack_Is_Active && Windup_Done == false)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().isTrigger = true;
            Attack_Deactivation = Time.time + Attack_Duration;
            Windup_Done = true;        
        }

        if(Time.time >= Attack_Deactivation && Windup_Done && Attack_Done == false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().isTrigger = false;
            GetComponent<CircleCollider2D>().enabled = false;
            Attack_End = Time.time + Attack_Recovery;
            Attack_Done = true;
        }

        if(Time.time >= Attack_End && Attack_Done)
        {
            Next_Attack = Time.time + Attack_Cooldown;
            Attack_Is_Active = false;
            Windup_Done = false;
            Attack_Done = false;
        }


    }

    Block block;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1");
        if (collision.gameObject.tag == "Player_Character")
        {
            block = collision.gameObject.GetComponent<Block>();
            if(block.Perfect_Parry)
            {
                Staggered = true;
                Stagger_End = Time.time + Stagger_Duration;
            }
        }
    }

}

