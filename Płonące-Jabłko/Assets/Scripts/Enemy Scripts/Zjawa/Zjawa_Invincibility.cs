using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zjawa_Invincibility : MonoBehaviour
{
    public bool Activate_Spell;
    public bool Spell_Is_Active;
    float Time_Of_Cast;
    public float Time_Active;
    public float Speed_Boost;
    public float Cooldown;
    Color color;
    SpriteRenderer Sprite_Renderer;

    //Making Zjawa halftransparent during Invincibility, probably obsolete after proper animations are added
    private void Start()
    {
        Sprite_Renderer = gameObject.GetComponent<SpriteRenderer>();
        color = Sprite_Renderer.color;
    }
    void Update()
    {
        if(Activate_Spell)
        {
            Spell_Is_Active = true;
            Activate_Spell = false;
            Time_Of_Cast = Time.time;
        }

        //Making Zjawa halftransparent during Invincibility pt.2
        if (Spell_Is_Active)
        {
            color.a = 0.5f;
            Sprite_Renderer.color = color;
        }else
        {
            color.a = 1f;
            Sprite_Renderer.color = color;
        }

            if (Time.time >= Time_Of_Cast + Time_Active)
        {
            Spell_Is_Active = false;
        }
    }
}
