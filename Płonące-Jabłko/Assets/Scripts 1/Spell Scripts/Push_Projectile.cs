using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_Projectile : MonoBehaviour
{

    public float Speed = 50f;
    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;
    }
}