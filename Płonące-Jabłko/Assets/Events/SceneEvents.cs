using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

public class SceneEvents
{
    public static UnityAction<Transform> sceneLoaded;
    public static UnityAction<int, string> sceneExit;
}
