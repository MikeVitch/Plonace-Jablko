using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Room_Vision : MonoBehaviour
{
    float Unexplored_Alpha = 1;
    float Present_Alpha = 0;
    float Explored_Alpha = 0.8f;
    Color Target_Color;
    Color color;
    void Start()
    {
        Target_Color = gameObject.GetComponent<SpriteShapeRenderer>().color;
        Target_Color.a = Unexplored_Alpha;
        gameObject.GetComponent<SpriteShapeRenderer>().color = Target_Color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player_Character")
        {
            Target_Color.a = Present_Alpha;
            //gameObject.GetComponent<SpriteShapeRenderer>().color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_Character")
        {
            Target_Color.a = Explored_Alpha;
            //gameObject.GetComponent<SpriteShapeRenderer>().color = color;
        }
    }

    private void Update()
    {
        if(Target_Color.a != gameObject.GetComponent<SpriteShapeRenderer>().color.a)
        {
            color = gameObject.GetComponent<SpriteShapeRenderer>().color;
            color.a -= (gameObject.GetComponent<SpriteShapeRenderer>().color.a - Target_Color.a) * 1 * Time.deltaTime;
            gameObject.GetComponent<SpriteShapeRenderer>().color = color;
        }
    }
}
