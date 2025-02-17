using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeeThroughBehaviour : MonoBehaviour
{
    [SerializeField] SpriteMask playerMask;
   // [SerializeField] SpriteRenderer playerShapeMask;

    [SerializeField] int behindCassie = 9;
    [SerializeField] int inFrontCassie = 11;
    bool isPlayerBehind = false;
    SpriteRenderer thisSprite;
    //SpriteRenderer thisSpriteTrans;


    Color thisColor;
    //[SerializeField] Shader alphaShader;

    // Start is called before the first frame update
    void Start()
    {
        thisSprite = GetComponentInParent<SpriteRenderer>();
        /*thisSpriteTrans = gameObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        thisSpriteTrans.sprite = thisSprite.sprite;
        thisSpriteTrans.transform.position = thisSprite.transform.position;
        thisSpriteTrans.sortingOrder = inFrontCassie;

        thisColor = thisSpriteTrans.color;
        thisColor.a = 0.5f;
        thisSpriteTrans.color = thisColor;
        thisSpriteTrans.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        thisSpriteTrans.enabled = false;*/

    }

    // Update is called once per frame
    void Update()
    {


        if (isPlayerBehind)
        {
            //thisColor.a = 0.5f;
            thisSprite.sortingOrder = inFrontCassie;
            //playerMask.enabled = true;

            thisSprite.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            //Debug.Log(thisSprite.maskInteraction);
            //thisSpriteTrans.enabled = true;
        }
        else
        {
            //thisColor.a = 1f;
            thisSprite.sortingOrder = behindCassie;
            //playerMask.enabled = false;
            thisSprite.maskInteraction = SpriteMaskInteraction.None;

            //thisSpriteTrans.enabled = false;

        }

        //thisSprite.color = thisColor;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) 
        { 
            if (collision.tag == "Player_Character") 
            {
                //Debug.Log("Player behind!");
                isPlayerBehind = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player_Character")
            {
                //Debug.Log("Player not behind!");
                isPlayerBehind = false;
            }
        }

    }

    
}
