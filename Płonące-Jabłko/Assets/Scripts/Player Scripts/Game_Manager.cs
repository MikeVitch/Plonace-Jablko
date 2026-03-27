using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    public float Current_Mana;
    Mana_Tracker mana_tracker;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        mana_tracker = FindAnyObjectByType<Mana_Tracker>();
        Current_Mana = mana_tracker.Max_Mana;
    }

    private void Update()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }



    private void OnSceneUnloaded(Scene scene)
    {
        Current_Mana = mana_tracker.Current_Mana;
    }

}

