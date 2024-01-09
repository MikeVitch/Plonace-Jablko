using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan_Of_Ice_Projectile : MonoBehaviour
{

    public float Speed = 10f;

    void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Character" || collision.gameObject.tag == "Untargetable")
        { }
        else
            Destroy(gameObject);
    }
}

