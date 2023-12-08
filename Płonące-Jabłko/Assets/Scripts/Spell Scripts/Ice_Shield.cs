using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Shield : MonoBehaviour
{
    public Player_Logic player_logic;
    public float Fire_Resistance = 60f;
    public float Earth_Resistance = 20f;
    public float Water_Resistance = 20f;
    public float Air_Resistance = 20f;
    public float Physical_Resistance = 40f;
    public float Time_Active = 5f;
    float Deactivation_Time;
    public KeyCode Cast = KeyCode.Alpha0;
    public bool Spell_Is_Active;
    public float Mana_Cost = 15f;
    public Mana_Tracker mana_tracker;

    void Update()
    {
        if (Input.GetKeyDown(Cast) && Spell_Is_Active && mana_tracker.Current_Mana >= Mana_Cost && !player_logic.Player_Attack_Lockout)
        {
            mana_tracker.Current_Mana -= Mana_Cost;
            Deactivation_Time = Time.time + Time_Active;
        }

        if (Input.GetKeyDown(Cast) && !Spell_Is_Active && mana_tracker.Current_Mana >= Mana_Cost && !player_logic.Player_Attack_Lockout)
        {
            Deactivation_Time = Time.time + Time_Active;
            Spell_Is_Active = true;
            mana_tracker.Current_Mana -= Mana_Cost;
            /*player_logic.Fire_Resistance += Fire_Resistance;
            player_logic.Earth_Resistance += Earth_Resistance;
            player_logic.Water_Resistance += Water_Resistance;
            player_logic.Air_Resistance += Air_Resistance;
            player_logic.Physical_Resistance += Physical_Resistance;*/
        }

        if(Deactivation_Time <= Time.time && Spell_Is_Active) 
        {
            Spell_Is_Active = false;
            /*player_logic.Fire_Resistance -= Fire_Resistance;
            player_logic.Earth_Resistance -= Earth_Resistance;
            player_logic.Water_Resistance -= Water_Resistance;
            player_logic.Air_Resistance -= Air_Resistance;
            player_logic.Physical_Resistance -= Physical_Resistance;*/
        }
    }
}
