using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floor_Changer : MonoBehaviour
{
    public GameObject Exit;
    public GameObject Current_Floor;
    public GameObject Destination_Floor;
    Player_Logic player_logic;
    GameObject Black_Screen;
    Fade_Out_Data fade_out_data;
    bool Player_In_Range;
    [HideInInspector]
    public bool Teleport;
    IEnumerator fade_out;
    IEnumerator fade_in;

    private void Start()
    {
        player_logic = FindObjectOfType<Player_Logic>();
        Black_Screen = GameObject.Find("Black_Screen");
        fade_out_data = FindObjectOfType<Fade_Out_Data>();
        fade_out = FadeOut();
        fade_in = FadeIn();
        //Before fade in we spawn a copy of the object, because the original gets deactivated and stops working. It is then activated on start here
        //the name check is future proofing so that objects don't accidentally delete themselves if we decide to add a fade in when loading a new scene or smth
        if (Black_Screen.GetComponent<Image>().color.a >= 1f && gameObject.name.Contains("(Clone)"))
        {
            StartCoroutine(fade_in);
        }
    }

    private void Update()
    {
        if (Player_In_Range && Input.GetKeyDown(player_logic.Interaction_Key) && Black_Screen.GetComponent<Image>().color.a <= 0)
            Teleport = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player_Character" && Teleport)
        {
            StartCoroutine(fade_out);

            Teleport = false;
        }

        if (collision.tag == "Player_Character")
            Player_In_Range = true;
        else
            Player_In_Range = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_Character")
            Player_In_Range = false;
    }

    IEnumerator FadeOut()
    {
        while (true)
        {
            if(fade_out_data.Fade_Out_Speed > 0)
                Black_Screen.GetComponent<Image>().color += new Color(0, 0, 0, (1 / fade_out_data.Fade_Out_Speed) * Time.deltaTime);
            else
                Black_Screen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            //Debug.Log(Black_Screen.GetComponent<Image>().color);
            if (Black_Screen.GetComponent<Image>().color.a >= 1f)
            {
                yield return new WaitForSeconds(fade_out_data.Time_Faded_Out);
                Current_Floor.SetActive(false);
                player_logic.transform.position = Exit.transform.position;
                Destination_Floor.SetActive(true);
                Instantiate(gameObject);
                StopCoroutine(fade_out);
            }

            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        while (true)
        {
            if(fade_out_data.Fade_Out_Speed > 0)
                Black_Screen.GetComponent<Image>().color -= new Color(0, 0, 0, (1 / fade_out_data.Fade_Out_Speed) * Time.deltaTime);
            else
                Black_Screen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            //Debug.Log(Black_Screen.GetComponent<Image>().color);
            if (Black_Screen.GetComponent<Image>().color.a <= 0f)
            {
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
