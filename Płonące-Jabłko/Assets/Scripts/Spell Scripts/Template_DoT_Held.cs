using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template_DoT_Held : MonoBehaviour
{
    public float Mana_Cost_Initial = 10f;
    public float Mana_Cost_Held = 5f;
    public float Damage = 5f;
    public float Damage_Tickrate = 0.25f;
    public KeyCode Cast = KeyCode.Alpha1;
    public Mana_Tracker mana_tracker;
    bool Spell_Is_On = false;

    //Don't forget to set Character_Sprite as reference for Mana_Tracker
    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }
    void Update()
    {
        if (Input.GetKey(Cast) && Spell_Is_On && mana_tracker.Current_Mana >= Mana_Cost_Held)
            mana_tracker.Current_Mana -= Mana_Cost_Held * Time.deltaTime;
        else
            Spell_Is_On = false;

        if(Spell_Is_On)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<PolygonCollider2D>().enabled = true;
            GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().isTrigger = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }

        if (Input.GetKeyDown(Cast) && mana_tracker.Current_Mana >= Mana_Cost_Initial)
        {
            mana_tracker.Current_Mana -= Mana_Cost_Initial;
            Spell_Is_On = true;
        }
    }

}
