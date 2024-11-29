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
    bool Teleport;
    IEnumerator fade_out;
    IEnumerator fade_in;

    private void Start()
    {
        player_logic = FindObjectOfType<Player_Logic>();
        Black_Screen = GameObject.Find("Black_Screen");
        fade_out_data = FindObjectOfType<Fade_Out_Data>();
        fade_out = FadeOut();
        fade_in = FadeIn();
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
        Debug.Log("1");
        while (true)
        {
            Debug.Log("2");
            Black_Screen.GetComponent<Image>().color += new Color(0, 0, 0, (1 / fade_out_data.Fade_Out_Speed) * Time.deltaTime);
            Debug.Log(Black_Screen.GetComponent<Image>().color);
            if (Black_Screen.GetComponent<Image>().color.a >= 1f)
            {
                Debug.Log("Fade_Out off");
                yield return new WaitForSeconds(fade_out_data.Time_Faded_Out);
                Debug.Log("Fade_Out on");
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
            Black_Screen.GetComponent<Image>().color -= new Color(0, 0, 0, (1 / fade_out_data.Fade_Out_Speed) * Time.deltaTime);
            Debug.Log(Black_Screen.GetComponent<Image>().color);
            if (Black_Screen.GetComponent<Image>().color.a <= 0f)
            {
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
