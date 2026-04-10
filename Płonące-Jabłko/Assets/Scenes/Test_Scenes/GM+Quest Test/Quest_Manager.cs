using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest_Manager : MonoBehaviour
{
    public static Quest_Manager Instance;
    public List<Quest> Quests;
    public TextMeshProUGUI Quest_Name;
    public TextMeshProUGUI Quest_Step;
    int Tracked_Quest;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Quest_Name = GameObject.Find("QuestName").GetComponent<TextMeshProUGUI>();
        Quest_Step = GameObject.Find("QuestObjective").GetComponent<TextMeshProUGUI>();
    }


 
    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Tracked_Quest = TrackedQuest();
        Quest_Name.text = Quests[Tracked_Quest].Quest_Name;
        Quest_Step.text = Quests[Tracked_Quest].Quest_Steps[Quests[Tracked_Quest].Current_Quest_Step-1];
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Quest_Name = GameObject.Find("QuestName").GetComponent<TextMeshProUGUI>();
        Quest_Step = GameObject.Find("QuestObjective").GetComponent<TextMeshProUGUI>();
    }


    private int TrackedQuest()
    {
        for (int i = 0; i < Quests.Count; i++)
        {
            if( Quests[i].Is_Tracked)
                return i;
        }
        return 0;
    }

}
