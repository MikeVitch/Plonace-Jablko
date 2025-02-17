using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrespassDoor : MonoBehaviour
{
    Player_Logic player_logic;
    bool Player_In_Range;
    bool animLocked = false;

    Animator animator;
    [SerializeField] bool canOpen;
    [SerializeField] bool isOpen;
    [SerializeField] float animLength = 0.45f;
    [SerializeField] Collider2D doorCollider;
    [SerializeField] SayBubble sayCassie;

    IEnumerator door_movement;


    // Start is called before the first frame update
    void Start()
    {
        player_logic = FindObjectOfType<Player_Logic>();
        animator = GetComponent<Animator>();
        door_movement = DoorMovement();


    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            doorCollider.enabled = false;
        }
        else
        {
            doorCollider.enabled = true;

        }





        if (Player_In_Range && Input.GetKeyDown(player_logic.Interaction_Key) && !animLocked)
        {
            if (canOpen)
            {
                //Debug.Log("You shall pass!");
                sayCassie.showText(1, "You shall pass!");

                animLocked = true;
                if (isOpen)
                {
                    animator.SetFloat("animSpeed", -1);

                }
                else
                {
                    animator.SetFloat("animSpeed", 1);

                }

                StartCoroutine(DoorMovement());



               // Debug.Log("anim done! locked: " + animLocked);
            }
            else
            {
                //Debug.Log("Cant open this! ");
                sayCassie.showText(1, "Can't open this! ");


            }
        }

    }

    IEnumerator DoorMovement()
    {
        yield return new WaitForSeconds(animLength);
        animator.SetFloat("animSpeed", 0);
        isOpen = !isOpen;
        animLocked = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player_Character"))
        {
            Player_In_Range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player_Character"))
        {
            Player_In_Range = false;
        }
    }
}
