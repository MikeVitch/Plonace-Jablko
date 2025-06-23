using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder_Throw_Detection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boulder_Throw_Projectile")
            Destroy(gameObject);
    }
}
