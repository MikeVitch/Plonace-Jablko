using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Template_Projectile_Logic : MonoBehaviour
{

    public float Speed = 10f;
    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        Destroy(gameObject);
    }
}
