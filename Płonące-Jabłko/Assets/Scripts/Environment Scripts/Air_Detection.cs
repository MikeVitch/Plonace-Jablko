using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air_Detection : MonoBehaviour
{
    public float Activation_Range;
    public Transform Player_Character;
    public Longstrider longstrider;
    public Misty_Step misty_step;
    public Push push;

    void Update()
    {
        if((misty_step.Activate_Spell || longstrider.Spell_Is_Cast || push.Activate_Spell) && Vector3.Distance(Player_Character.position, gameObject.transform.position) <= Activation_Range)
        {
            Debug.Log("Activate_Air");
        }
    }
}
