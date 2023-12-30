using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Detection : MonoBehaviour
{
    public float Activation_Range;
    public Transform Player_Character;
    public Restoration restoration;
    public Fan_Of_Ice fan_of_ice;
    public Ice_Shard ice_shard;
    public Ice_Shield ice_shield;
    void Update()
    {
        if((fan_of_ice.Activate_Spell || ice_shard.Activate_Spell || restoration.Activate_Spell || restoration.Spell_Is_On || ice_shield.Spell_Is_Active) && Vector3.Distance(Player_Character.position, gameObject.transform.position) <= Activation_Range)
        {
            Debug.Log("Activate_Water");
        }
    }
}
