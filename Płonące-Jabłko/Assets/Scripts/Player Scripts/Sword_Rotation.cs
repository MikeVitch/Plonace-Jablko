using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword_Rotation : MonoBehaviour
{
    public float offset = 0f;
    public Sword_Attack sword_attack;
    float Attack_Deactivation = 0f;
    public void Start()
    {
        
    }
    private void Update()
    {
        sword_attack = FindObjectOfType<Sword_Attack>();
        float Attack_Duration = sword_attack.Attack_Duration;
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Attack_Deactivation = Time.time + Attack_Duration;
        if (Attack_Deactivation<Time.time)
        {
            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
        }
    }
}
