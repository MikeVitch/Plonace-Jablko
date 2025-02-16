using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth_Vision : MonoBehaviour
{
    public float Detection_Rate_Unstealthed;
    public float Detection_Rate_Stealthed;
    public bool Player_Seen;
    public Vector2 Last_Player_Position;
    Stealth_Enemy_Logic stealth_enemy_logic;
    Player_Movement player_movement;
    Player_Logic player_logic;
    bool Player_Behind_Wall;
    

    private void Start()
    {
        stealth_enemy_logic = GetComponentInParent<Stealth_Enemy_Logic>();
        player_movement = FindObjectOfType<Player_Movement>();
        player_logic = FindObjectOfType<Player_Logic>();
    }
    void Update()
    {
        if(Player_Seen)
        {
            if (player_movement.Is_Crouching)
                stealth_enemy_logic.Alertness += Detection_Rate_Stealthed * Time.deltaTime;
            else
                stealth_enemy_logic.Alertness += Detection_Rate_Unstealthed * Time.deltaTime;
        }

        Player_Behind_Wall = stealth_enemy_logic.Player_Behind_Wall;
        // Debug.Log(Last_Player_Position);
        //Debug.Log(Player_Behind_Wall);
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("stealthCollision tag: "+ collision.tag);

        if((collision.tag == "Player_Character" && !player_logic.Is_Hidden && !Player_Behind_Wall) || (collision.tag == "Player_Character" && Vector2.Distance(gameObject.transform.position,player_logic.transform.position) <= stealth_enemy_logic.Combat_Hide_Zone_Detection_Distance && stealth_enemy_logic.Aggressive))
        {
            Debug.Log("stealthCollision tag Player_Character: " + collision.tag);

            Player_Seen = true;
            Last_Player_Position = collision.transform.position;
        }
        else if(player_logic.Is_Hidden)
            Player_Seen = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player_Character")
            Player_Seen = false;
    }
}
