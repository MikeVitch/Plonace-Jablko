using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTo : MonoBehaviour
{
    [SerializeField]
    int destinationScene;
    Player_Logic player_logic;
    bool Player_In_Range = false;



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
            SceneManager.LoadScene(destinationScene);
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
