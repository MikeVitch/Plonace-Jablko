using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportBehaviour : MonoBehaviour
{
    public GameState GameState;

    public string destinationScene;
    public string destinationSpawn;
    //public GameObject teleportTarget;
   // public GameObject player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void teleportTO(string destinationScene, string destinationSpawn)
    {
        GameState.playerSpawnLocation = destinationSpawn;
        SceneManager.LoadScene(destinationScene, LoadSceneMode.Single);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.gameObject.tag == "Player_Character")
        {
            Debug.Log("teleport player");


           GameState.playerSpawnLocation = destinationSpawn;

            SceneManager.LoadScene(destinationScene, LoadSceneMode.Single);


           

        }


    }


    
}
