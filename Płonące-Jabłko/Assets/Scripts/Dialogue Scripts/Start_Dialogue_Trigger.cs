using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Dialogue_Trigger : MonoBehaviour
{
    public bool IsdialougeStart;
    public Game_Event Start_Dialogue_scene;
    public Dialogue_Manager dialogue_manager_scene;
    public DialogueSO dialogue_1_Castle;
    public DialogueSO dialogue_2_Castle;
    public bool IsSecondDialouge;
    public Floor_Changer floor_changer;

    GameObject Black_Screen;
    Fade_Out_Data fade_out_data;
    IEnumerator fade_in;
    Player_Logic player_logic;
    Player_Movement player_movement;
    float Base_Speed;
    public bool Dialogue_1_Done;
    public bool Dialogue_2_Done;


    void Start()
    {
        player_logic = FindObjectOfType<Player_Logic>();
        player_movement = FindObjectOfType<Player_Movement>();
        Base_Speed = player_movement.Base_Speed;
        player_movement.Base_Speed = 0;
        IsdialougeStart = false;
        Black_Screen = GameObject.Find("Black_Screen");
        fade_out_data = FindObjectOfType<Fade_Out_Data>();
        Black_Screen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        fade_in = Fade_In();
        StartCoroutine(fade_in);
    }

    void Update()
    {
        if (!Dialogue_1_Done)
            player_logic.Player_Attack_Lockout = true;
        if (!Dialogue_2_Done)
            player_logic.Player_Attack_Lockout = true;


        if (IsdialougeStart)
        {
            IsdialougeStart = false;
            Start_Dialogue_scene.Raise();
            dialogue_manager_scene.StartDialogue(dialogue_1_Castle);
        }

        if(Dialogue_1_Done && Black_Screen.GetComponent<Image>().color.a >= 1f && !Dialogue_2_Done)
            IsSecondDialouge = true;

        if(IsSecondDialouge && Black_Screen.GetComponent<Image>().color.a <= 0f)
        {
            IsSecondDialouge = false;
            Start_Dialogue_scene.Raise();
            dialogue_manager_scene.StartDialogue(dialogue_2_Castle);
            Dialogue_2_Done = true;
        }

        
    }

    IEnumerator Fade_In()
    {
        while (true)
        {
            if (fade_out_data.Fade_Out_Speed > 0)
                Black_Screen.GetComponent<Image>().color -= new Color(0, 0, 0, (1 / fade_out_data.Fade_Out_Speed) * Time.deltaTime);
            else
                Black_Screen.GetComponent<Image>().color = new Color(0, 0, 0, 0);

            if (Black_Screen.GetComponent<Image>().color.a <= 0f)
            {
                IsdialougeStart = true;

                
                Dialogue_1_Done = true;
                player_movement.Base_Speed = Base_Speed;
                floor_changer.Teleport = true;
                StopCoroutine(fade_in);
                
            }
            yield return null;
        }
    }






}
