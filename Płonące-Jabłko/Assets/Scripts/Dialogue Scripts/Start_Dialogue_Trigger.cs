using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Dialogue_Trigger : MonoBehaviour
{
    public bool IsdialougeStart;
    public Game_Event Start_Dialogue_scene;
    public Dialogue_Manager dialogue_manager_scene;
    public DialogueSO dialogue_1_Castle;
    public DialogueSO dialogue_2_Castle;
    public bool IsSecondDialouge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsdialougeStart)
        {
            Start_Dialogue_scene.Raise();
            dialogue_manager_scene.StartDialogue(dialogue_1_Castle);
        }


    }






}
