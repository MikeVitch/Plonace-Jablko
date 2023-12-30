using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Detection : MonoBehaviour
{
    public float Activation_Range;
    public Transform Player_Character;
    public Stone_Volley stone_volley;
    public Boulder_Throw boulder_throw;
    void Update()
    {
        if((stone_volley.Activate_Spell || boulder_throw.Activate_Spell) && Vector3.Distance(Player_Character.position, gameObject.transform.position) <= Activation_Range)
        {
            Debug.Log("Activate_Earth");
        }
    }
}
