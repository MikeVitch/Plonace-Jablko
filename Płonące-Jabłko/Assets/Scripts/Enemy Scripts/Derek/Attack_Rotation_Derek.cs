using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Rotation_Derek : MonoBehaviour
{
    GameObject Player_Character;
    Vector2 Vector_To_Player;
    void Start()
    {
       Player_Character = GameObject.FindGameObjectWithTag("Player_Character");
    }
    void Update()
    {
        if (!GetComponentInParent<Derek_Tutorial>().Attack_Is_Active)
        { 
                Vector_To_Player = Player_Character.transform.position - gameObject.transform.position;
            if (Player_Character.transform.position.y >= transform.position.y)
                transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector_To_Player, Vector2.right));
            else
                transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector_To_Player, Vector2.right) * (-1));
        }
    }
}
