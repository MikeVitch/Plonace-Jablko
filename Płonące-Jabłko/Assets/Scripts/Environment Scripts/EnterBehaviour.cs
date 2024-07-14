using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBehaviour : MonoBehaviour
{

    public bool isLocation = false;
    public bool canEnter = false;
    private EnterBuildingBehaviour onMapBahaviour;
    private TeleportBehaviour teleportBehaviour;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {




        if (collision.gameObject.tag == "Player_Character")
        {
            Debug.Log("enter Building ?");
            if (canEnter)
            {
                if (isLocation)
                {
                    Debug.Log("enter Building Location?");


                }
                else
                {
                    Debug.Log("BuildingOnMap");


                 //   onMapBahaviour.RemoveRoof();


                }
            }
            else
            {
                Debug.Log("You can't enter this building");
            }



        }


    }
}
