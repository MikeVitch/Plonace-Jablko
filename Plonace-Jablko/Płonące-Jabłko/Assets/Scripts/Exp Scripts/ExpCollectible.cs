using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCollectible : MonoBehaviour
{
    public GameObject character;
    public float timer;

    public bool moveToCharacter;
    public float speed;
    public Rigidbody2D rb;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player_Character");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(moveToCharacter == false)
        {
            if (timer < 1)
            {
                timer += Time.fixedDeltaTime;
            }
            else
            {
                moveToCharacter = true;
                rb.gravityScale = 0;
            }
        }

        if (moveToCharacter == true)
        {
            Vector3 movementVector = character.transform.position - transform.position;
            rb.velocity = movementVector * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player_Character")
        {
            
            other.gameObject.GetComponent<PlayerStats>().currentExp += 10;
            Destroy(gameObject);
        }
    }
}
