using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMQ_Test_2_Quest_Advancement : MonoBehaviour
{
    public Quest Test_Quest;
    GameObject Player_Character;
    bool Dodge_Counted;
    Player_Movement player_movement;

    // Start is called before the first frame update
    void Start()
    {
        Player_Character = GameObject.FindWithTag("Player_Character");
        player_movement = Player_Character.GetComponent<Player_Movement>();
        if (Test_Quest.Current_Quest_Step == 1)
        {
            Test_Quest.Current_Quest_Step++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player_movement.Dodge_Is_Active)
            Dodge_Counted = false;

        if ((Test_Quest.Current_Quest_Step == 2 || Test_Quest.Current_Quest_Step == 3 || Test_Quest.Current_Quest_Step == 4) && !Dodge_Counted && player_movement.Dodge_Is_Active)
        {
            Test_Quest.Current_Quest_Step++;
            Dodge_Counted = true;
        }

        if(Test_Quest.Current_Quest_Step == 5 && Player_Character.GetComponentInChildren<Sword_Attack>().Attack_Is_Active)
        {
            Test_Quest.Current_Quest_Step++;
        }
    }
}
