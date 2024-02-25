using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Logic : MonoBehaviour
{
    public Attack_Activation attack_activation;
    float Attack_Duration;
    float Attack_Damage;
    float Iframes;
    public float Health = 100f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Weapon")
        {
            attack_activation = FindObjectOfType<Attack_Activation>();
            Attack_Duration = attack_activation.Attack_Duration;
            Attack_Damage = attack_activation.Attack_Damage;
            if (Time.time > Iframes)
            {
                Health -= Attack_Damage;
                Iframes = Time.time + Attack_Duration;
            }
            Debug.Log(Health);
            if (Health <= 0)
                Destroy(gameObject);
        }
    }
}
