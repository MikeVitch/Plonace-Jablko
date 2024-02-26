using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_Volley_Projectile : MonoBehaviour
{
    public float Speed = 10f;
    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player_Character")
            Destroy(gameObject);
    }
}
