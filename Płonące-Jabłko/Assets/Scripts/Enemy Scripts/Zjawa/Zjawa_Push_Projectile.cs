using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zjawa_Push_Projectile : MonoBehaviour
{
    Zjawa_Push zjawa_push;
    private void Start()
    {
        zjawa_push = FindObjectOfType<Zjawa_Push>();
    }
    void Update()
    {
        transform.position += transform.right * zjawa_push.Projectile_Speed * Time.deltaTime;
    }
}
