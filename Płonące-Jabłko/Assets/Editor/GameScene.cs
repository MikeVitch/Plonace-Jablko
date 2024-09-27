using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public Transform defaultPlayerSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneEvents.sceneLoaded.Invoke(defaultPlayerSpawn);
    }

  
}
