using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zjawa_Movement : MonoBehaviour
{
    public Zjawa_Logic zjawa_logic;
    Player_Logic player_logic;
    Zjawa_Invincibility zjawa_invincibility;
    public float Base_Movement_Speed;
    float Movement_Speed;
    public float Aggro_Range;
    public float Range;
    public float Minimum_Distance;
    public bool Is_In_Range;
    float Distance_To_Player;

    // Start is called before the first frame update
    void Start()
    {
        zjawa_invincibility = GetComponent<Zjawa_Invincibility>();
        player_logic = FindObjectOfType<Player_Logic>();
    }

    // Update is called once per frame
    void Update()
    {
        Distance_To_Player = Vector3.Distance(transform.position, player_logic.Player_Position);
        //transform.right = player_logic.Player_Position - transform.position;

        //if (Is_In_Range /*|| zjawa_logic.Staggered*/)
            //Movement_Speed = 0;
        //else
            Movement_Speed = Base_Movement_Speed;
        if(zjawa_invincibility.Spell_Is_Active)
        {
            Movement_Speed *= 1 + zjawa_invincibility.Speed_Boost;
        }


        if (/*zjawa_logic.Attack_Is_Active == false*/true)
        {
            if (Distance_To_Player <= Range)
            {
                Is_In_Range = true;
            }
            else
                Is_In_Range = false;

            if (Distance_To_Player <= Minimum_Distance)
            {               
                //transform.position -= transform.right * Movement_Speed * Time.deltaTime;
                gameObject.GetComponent<Rigidbody2D>().AddForce((player_logic.Player_Position - transform.position) / Vector3.Distance(player_logic.Player_Position, transform.position) * Movement_Speed * Time.deltaTime * -1);
            }
            else if (Distance_To_Player <= Aggro_Range && Distance_To_Player >= Range)
                 {
                //transform.position += transform.right * Movement_Speed * Time.deltaTime;
                gameObject.GetComponent<Rigidbody2D>().AddForce((player_logic.Player_Position - transform.position) / Vector3.Distance(player_logic.Player_Position, transform.position) * Movement_Speed * Time.deltaTime);
            }
        }
    }
}
