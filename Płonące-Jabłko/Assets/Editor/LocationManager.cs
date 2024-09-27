using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "LocationManager", menuName = "ScriptableObjects/Manager/LocationManager", order = 1)]
public class LocationManager : ScriptableObject
{
    public GameState GameState { get; set; }

    private void OnEnable()
    {

        SceneEvents.sceneExit += OnSceneExit;
    }

    private void OnSceneExit(SceneAsset nextLocation, string playerSpawnTransormName)
    {
        GameState.playerSpawnLocation = playerSpawnTransormName;
        SceneManager.LoadScene(nextLocation.name, LoadSceneMode.Single);
    }




}
