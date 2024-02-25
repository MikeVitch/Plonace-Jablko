using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Database_enemy : MonoBehaviour
{
    public List<monster> monsters = new List<monster>();
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

   
}
[System.Serializable]
    public class monster
{
    public enum MonsterType
    {
        Fire, Water, Earth, Air ,FireWater, FireEarth, FireAir,
        WaterEarth, WaterAir, EarthAir, Any
    }

    public string name;
    public string description;
    public int ID;
    public MonsterType Type;
    public bool agresive;
    
}