using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviro_Ignition : MonoBehaviour
{
    public Object Sprite_Ignited;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flamethrower_Hitbox")
        {
            Instantiate(Sprite_Ignited,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flamethrower_Hitbox")
        {
            Instantiate(Sprite_Ignited, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
