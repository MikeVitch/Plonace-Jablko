using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBubble : MonoBehaviour
{

    Player_Logic player_logic;
    bool Player_In_Range;
    [SerializeField] SayBubble sayCassie;



    // Start is called before the first frame update
    void Start()
    {

        player_logic = FindObjectOfType<Player_Logic>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Player_In_Range && Input.GetKeyDown(player_logic.Interaction_Key))
        {
            sayCassie.showText(1, "Teraz lepiej tam nie iœæ");


        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player_Character"))
        {
            Player_In_Range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player_Character"))
        {
            Player_In_Range = false;
        }
    }


}
