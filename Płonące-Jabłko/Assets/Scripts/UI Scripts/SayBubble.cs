using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SayBubble : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] GameObject bubble;
    bool isVisible = false;
    bool hideLater = true;
    float showtime = 0;
    float timeFromDisplay = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bubble.SetActive(isVisible);

        if (hideLater)
        {
            if (timeFromDisplay > showtime)
            {
                isVisible = false;
            }
            else
            {
                timeFromDisplay += Time.deltaTime;

            }
        }
        
       
    }

    public void showText(int time, string txt)
    {
        hideLater = true;
        text.text = txt;
        isVisible = true;
        showtime = time;
        timeFromDisplay = 0;
    }

    public void displayText(string txt)
    {
        text.text = txt;
        isVisible = true;
        hideLater = false;

    }
    public void hideText()
    {
        isVisible = false;
    }
}
