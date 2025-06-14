using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public GameObject backPoint;
    public GameObject Cassie;
    public Stealth_Enemy_Logic Seen;
    bool Checked;


    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        

        //TeleportOnSeen();
        

    }

    public void TeleportOnSeen ()
    {
        if (Seen.isSeen == true)
        {
            Cassie.transform.position = backPoint.transform.position;
            //Time.timeScale = 0;
            Debug.Log("return");

        } 
        else
        {
            
            return;
            
        }





    } 
    


}
