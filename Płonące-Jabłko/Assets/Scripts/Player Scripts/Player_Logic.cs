using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Logic : MonoBehaviour
{
    public Enemy_Attack_Template_Melee enemy_attack_template_melee;
    public Vector3 Player_Position;
    public float Fire_Resistance = 0f;
    public float Earth_Resistance = 0f;
    public float Water_Resistance = 0f;
    public float Air_Resistance = 0f;
    public float Physical_Resistance = 0f;
    public float Health = 100f;
    public float Iframes_Lenght = 0.5f;
    float Iframes_End;
    float Attack_Damage;

    void Update()
    {
        Player_Position = GetComponent<Transform>().position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Melee_Test_Attack
        if(collision.gameObject.tag == "Melee_Test_Attack")
        {
            enemy_attack_template_melee = FindObjectOfType<Enemy_Attack_Template_Melee>();
            Attack_Damage = enemy_attack_template_melee.Attack_Damage;
            if (Iframes_End < Time.time)
            {
                Health -= Attack_Damage * (1 - 0.01f * Physical_Resistance);
                Iframes_End = Time.time + Iframes_Lenght;
            }
        }
        Debug.Log(Health);
    }
}
