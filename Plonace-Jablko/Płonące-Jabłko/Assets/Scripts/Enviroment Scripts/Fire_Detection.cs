using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Detection : MonoBehaviour
{
    public float Activation_Range;
    public Transform Player_Character;
    public Flamethrower flamethrower;
    public Fireball fireball;
    void Update()
    {
        if((fireball.Activate_Spell || flamethrower.Spell_Is_On || flamethrower.Activate_Spell) && Vector3.Distance(Player_Character.position, gameObject.transform.position) <= Activation_Range)
        {
            Debug.Log("Activate_Fire");
        }
    }
}
