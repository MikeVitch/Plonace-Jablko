using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zjawa_Push_Projectile : MonoBehaviour
{
    Zjawa_Push zjawa_push;
    Player_Logic player_logic;
    private void Start()
    {
        zjawa_push = FindObjectOfType<Zjawa_Push>();
        player_logic = FindObjectOfType<Player_Logic>();
        transform.right = player_logic.Player_Position - transform.position;
    }
    void Update()
    {
        transform.position += transform.right * zjawa_push.Projectile_Speed * Time.deltaTime;
    }
}
