using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Attack : MonoBehaviour
{
    public float Attack_Duration = 1.0f;
    public float Attack_Cooldown = 3.0f;
    public float Next_Attack = 0;
    public float Attack_Deactivation;
    public float Attack_Damage = 30f;
    public Player_Logic player_logic;
    public bool Attack_Is_Active;

 

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > Next_Attack && !player_logic.Player_Attack_Lockout && Time.timeScale!=0)
        {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<PolygonCollider2D>().enabled = true;
                GetComponent<PolygonCollider2D>().isTrigger = true;
                Next_Attack = Time.time + Attack_Cooldown;
                Attack_Deactivation = Time.time + Attack_Duration;
                Attack_Is_Active = true;
        }
        
            if (Time.time >= Attack_Deactivation) 
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().isTrigger = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            Attack_Is_Active = false;
        }
    }

}
