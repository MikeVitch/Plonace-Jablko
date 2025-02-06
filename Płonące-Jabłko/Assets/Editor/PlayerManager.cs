using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "PlayerManager", menuName = "ScriptableObjects/Manager/PlayerManager", order = 1)]

public class PlayerManager : ScriptableObject


{
    public GameObject player;

    public GameState GameState { get;  set; }
    public SceneEvents SceneEvents { get; private set; }

    //public GameObject characterSprite;

    /* other player logic like level etc*/
    // public Player_Logic player_Logic;

    // teleportation and scene load
    public string SpawnPlaceTag;


    // Start is called before the first frame update
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player_Character");
        SceneEvents.sceneLoaded += SpawnPlayer;

       // player_Logic = Instantiate(player_Logic); // store some starting data in different object
    }

    protected void SpawnPlayer(Transform defaultSpawnTransform)
    {

       // player = GameObject.FindGameObjectWithTag("Player_Character");

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player_Character");
        }

        if (GameState.playerSpawnLocation != "")
        {
            GameObject[] spawns = GameObject.FindGameObjectsWithTag(SpawnPlaceTag);
            bool foundSpawn = false;

            foreach(GameObject spawner in spawns) {
              
                if (spawner.name == GameState.playerSpawnLocation)
                {
                    foundSpawn = true;

                    Debug.Log("Found spawn location: "+spawner.transform.position);
                    player.GetComponent<Transform>().position = spawner.transform.position;

                    break;
                }

               
            }
            if (!foundSpawn)
            {


                // throw new MissingReferenceException("Can't find player spawn location with name: "+ GameState.playerSpawnLocation );
                player.GetComponent<Transform>().position = defaultSpawnTransform.transform.position;

                Debug.Log("Player spawned default: " + defaultSpawnTransform);

            }
        }
        else
        {
            // ActivePlayer = Instantiate(PlayerPrefab, defaultSpawnTransform.position, Quaternion.identity);
            player.GetComponent<Transform>().position = defaultSpawnTransform.transform.position;

            Debug.Log("Player spawned default: " + defaultSpawnTransform);
        }
        
    }
}
