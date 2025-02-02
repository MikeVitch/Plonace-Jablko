using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Vision : MonoBehaviour
{
    float Unexplored_Alpha = 1;
    float Present_Alpha = 0;
    float Explored_Alpha = 0.8f;
    Color Target_Color;
    Color color;
    void Start()
    {
        Target_Color = gameObject.GetComponent<SpriteRenderer>().color;
        Target_Color.a = Unexplored_Alpha;
        gameObject.GetComponent<SpriteRenderer>().color = Target_Color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player_Character")
        {
            Target_Color.a = Present_Alpha;
            //gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_Character")
        {
            Target_Color.a = Explored_Alpha;
            //gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void Update()
    {
        if(Target_Color.a != gameObject.GetComponent<SpriteRenderer>().color.a)
        {
            color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a -= (gameObject.GetComponent<SpriteRenderer>().color.a - Target_Color.a) * 1 * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
