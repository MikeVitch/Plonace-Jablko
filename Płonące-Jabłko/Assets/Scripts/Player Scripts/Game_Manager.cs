using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    public float Current_Mana;
    public float Current_Health;
    Mana_Tracker mana_tracker;
    Player_Logic player_logic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        mana_tracker = FindAnyObjectByType<Mana_Tracker>();
        player_logic = FindAnyObjectByType<Player_Logic>();
        Current_Mana = mana_tracker.Max_Mana;
        Current_Health = player_logic.Health;
    }

    private void Update()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }



    private void OnSceneUnloaded(Scene scene)
    {
        Current_Mana = mana_tracker.Current_Mana;
        Current_Health = player_logic.Health;
    }

}

