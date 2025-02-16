using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana_Tracker : MonoBehaviour
{
    public float Max_Mana = 100f;
    public float Mana_Regen = 1f;
    public float Current_Mana;

    public ScreenMenu_Manager screenMenuManager;

    void Start()
    {
        Current_Mana = Max_Mana;
    }

    void Update()
    {
        if (Current_Mana < Max_Mana)
        {
            Current_Mana += Mana_Regen * Time.deltaTime;
        }
        else
            Current_Mana = Max_Mana;
        //Debug.Log(Current_Mana);
        screenMenuManager.SetMana(Current_Mana);
    }
}
