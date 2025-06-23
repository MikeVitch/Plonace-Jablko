using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder_Throw_Projectile : MonoBehaviour
{
    public float Speed = 7f;
    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Breakable")
            Destroy(collision.gameObject);

        if (collision.gameObject.tag == "Player_Character" || collision.gameObject.tag == "Untargetable")
        { }
        else
            Destroy(gameObject);
    }
}
