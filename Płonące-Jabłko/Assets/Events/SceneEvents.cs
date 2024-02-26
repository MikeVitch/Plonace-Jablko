using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System;

public class SceneEvents
{
    public static UnityAction<Transform> sceneLoaded;
    public static UnityAction<SceneAsset, string> sceneExit;
}
