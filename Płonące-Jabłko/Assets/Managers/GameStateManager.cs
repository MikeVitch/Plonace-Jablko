using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
   public static GameStateManager Instance { get; private set; }
    [SerializeField]
    private GameState startingState;
    public GameState GameState { get; private set; }
    public LocationManager locationManager;

    public PlayerManager playerManager;

   

    //  player manager, ui manager?

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
                DontDestroyOnLoad(this);
        }

        GameState = Instantiate(startingState);
        locationManager.GameState = GameState;
        playerManager.GameState = GameState;
        //sceneManager
    }

}
