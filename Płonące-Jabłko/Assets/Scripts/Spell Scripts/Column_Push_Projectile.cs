using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column_Push_Projectile : MonoBehaviour
{
    public Vector3 Player_Position;
    GameObject Player_Character;
    public float Speed;

    private void Start()
    {
        Player_Character = GameObject.FindWithTag("Player_Character");
        Player_Position = Player_Character.transform.position;
    }
    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player_Character")
        {
            Destroy(gameObject);
        }
    
    }
}
