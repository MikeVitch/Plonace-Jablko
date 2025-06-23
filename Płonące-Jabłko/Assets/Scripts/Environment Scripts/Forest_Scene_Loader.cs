using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Forest_Scene_Loader : MonoBehaviour
{
    public Derek_Tutorial derek_tutorial;
    public Player_Logic player_logic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (derek_tutorial.End_Tutorial && Input.GetKeyDown(player_logic.Interaction_Key))
            Debug.Log("test2");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player_Character" && derek_tutorial.End_Tutorial && Input.GetKeyDown(player_logic.Interaction_Key))
        {
            SceneManager.LoadScene("Level_Forest");
            Debug.Log("test");
        }
    }
}
