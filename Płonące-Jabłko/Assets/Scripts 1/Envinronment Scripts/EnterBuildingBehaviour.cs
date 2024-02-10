using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnterBuildingBehaviour : MonoBehaviour
{

    public bool isLocation = false;
    public bool canEnter = false;
    public GameObject visibilityMask;
    public GameObject doorLock;
    public GameObject player;
    public bool isInside = false;
    public string buildingScene;
    public string buildingSpawn;
    public GameState GameState;




    // Start is called before the first frame update
    void Start()
    {
        visibilityMask.SetActive(false);
        //teleportBehaviour = GetComponent<TeleportBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log("Building: " + GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<Collider2D>()));

        isInside = GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<Collider2D>());
        if (isInside)
        {
            visibilityMask.SetActive(true);

        }
        else
        {
            visibilityMask.SetActive(false);

        }

    }

    public void teleportTO(string destinationScene, string destinationSpawn)
    {
        GameState.playerSpawnLocation = destinationSpawn;
        SceneManager.LoadScene(destinationScene, LoadSceneMode.Single);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

      


        if (collision.gameObject.tag == "Player_Character" )
        {
            Debug.Log("enter Building?");
            if (canEnter)
            {
                doorLock.SetActive(false);


                if (isLocation)
                {
                    Debug.Log("enter Building Location?" + buildingScene);
                    // SceneManager.LoadScene(buildingScene, LoadSceneMode.Single);
                    teleportTO(buildingScene, buildingSpawn);


                }
                /*   else
                   {
                       Debug.Log("remove roof");

                       if (isInside)
                       {
                           visibilityMask.SetActive(false);

                       }
                       else
                       {
                           visibilityMask.SetActive(true);

                       }



                   }*/
            }
            else
            {
                Debug.Log("You can't enter this building");
                doorLock.SetActive(true);

            }



        }
       

    }

  
}
