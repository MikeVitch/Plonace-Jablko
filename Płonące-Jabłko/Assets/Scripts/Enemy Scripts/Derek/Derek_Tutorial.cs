using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Derek_Tutorial : MonoBehaviour
{
    public GameObject Tutorial_Trigger;
   public bool Tutorial_Trigger_Past;
   public bool Attack_Tutorial;
   public bool Dodge_Tutorial;
   public bool Parry_Tutorial;
   public bool Combat_Tutorial;
   public bool End_Tutorial;
    int Attack_Tutorial_Hit;
    public Dialogue_Manager dialogue_manager;
    public DialogueSO dialogue;
    public DialogueSO dialogue_2;
    public DialogueSO dialogue_3;
    public DialogueSO dialogue_4;
    public DialogueSO dialogue_5;
    public GameObject Player_Character;
    public Game_Event Start_Dialogue;
    [Header("Movement")]
    public float Movement_Speed;
    public float Minimal_Distance;
    [Header("Attack")]
    public float Attack_Duration = 0.1f;
    public float Attack_Windup = 0.4f;
    public float Attack_Recovery = 0.5f;
    public float Attack_Cooldown = 2.0f;
    float Next_Attack = 0;
    float Attack_Deactivation;
    float Attack_Activation;
    float Attack_End;
    public float Attack_Damage = 30f;
    public bool Attack_Is_Active = false;
    bool Windup_Done = false;
    bool Attack_Done = false;
    bool Recovery_Done;
    public bool Staggered;
    public float Stagger_Duration;
    float Stagger_End;
    bool Is_In_Range;
    Player_Logic player_logic;
    Vector3 temp;
    public GameObject Attack_Hitbox;
    int Dodge_Tutorial_Counter;
    bool Dodge_Counted;
    int Block_Tutorial_Counter;
    bool Block_Counted;
    void Start()
    {
        Player_Character = GameObject.FindWithTag("Player_Character");
        player_logic = Player_Character.GetComponent<Player_Logic>();
    }
    void Update()
    {
        //Initial dialogue trigger
        if(Tutorial_Trigger.GetComponent<Collision_Detection>().Triggered != Tutorial_Trigger_Past)
        {
            Start_Dialogue.Raise();
            dialogue_manager.StartDialogue(dialogue);
            Attack_Tutorial = true;
        }
        Tutorial_Trigger_Past = Tutorial_Trigger.GetComponent<Collision_Detection>().Triggered;

        //End of attack tutorial
        if(Attack_Tutorial_Hit == 3)
        {
            Attack_Tutorial_Hit++;
            Start_Dialogue.Raise();
            dialogue_manager.StartDialogue(dialogue_2);
            Dodge_Tutorial = true;
        }

        //Dodge tutorial
        if (Dodge_Tutorial)
        {
            if (Vector3.Distance(transform.position, Player_Character.transform.position) <= Minimal_Distance || Attack_Is_Active)
            {
                //Is_In_Range = true;
                if (Time.time >= Next_Attack && Attack_Is_Active == false && !Staggered)
                {
                    Attack_Is_Active = true;
                    Attack_Activation = Time.time + Attack_Windup;
                }
                if (Time.time >= Attack_Activation && Attack_Is_Active && Windup_Done == false)
                {
                    Attack_Hitbox.GetComponent<SpriteRenderer>().enabled = true;
                    Attack_Hitbox.GetComponent<CapsuleCollider2D>().enabled = true;
                    Attack_Hitbox.GetComponent<CapsuleCollider2D>().isTrigger = true;
                    Attack_Deactivation = Time.time + Attack_Duration;
                    Windup_Done = true;
                    if (Player_Character.GetComponent<Player_Movement>().Dodge_Is_Active == true && !Dodge_Counted)
                    {
                        Dodge_Tutorial_Counter++;
                        Dodge_Counted = true;
                    }
                }
                if (Time.time >= Attack_Deactivation && Windup_Done && Attack_Done == false)
                {
                    Attack_Hitbox.GetComponent<SpriteRenderer>().enabled = false;
                    Attack_Hitbox.GetComponent<CapsuleCollider2D>().isTrigger = false;
                    Attack_Hitbox.GetComponent<CapsuleCollider2D>().enabled = false;
                    Attack_End = Time.time + Attack_Recovery;
                    Attack_Done = true;
                    Dodge_Counted = false;
                }

                if (Time.time >= Attack_End && Attack_Done)
                {
                    Next_Attack = Time.time + Attack_Cooldown;
                    Attack_Is_Active = false;
                    Windup_Done = false;
                    Attack_Done = false;
                    if (Dodge_Tutorial_Counter == 3)
                    {
                        Dodge_Tutorial_Counter++;
                        Start_Dialogue.Raise();
                        dialogue_manager.StartDialogue(dialogue_3);
                        Parry_Tutorial = true;
                        Dodge_Tutorial = false;
                    }
                }
            }
            else
            {
                //Is_In_Range = false;
                if (Time.time > Attack_End)
                    transform.position -= Vector3.Normalize(transform.position - Player_Character.transform.position) * Movement_Speed * Time.deltaTime;
            }
        }

            //Parry tutorial
            if (Parry_Tutorial)
            {
                if (Vector3.Distance(transform.position, Player_Character.transform.position) <= Minimal_Distance || Attack_Is_Active)
                {
                    //Is_In_Range = true;
                    if (Time.time >= Next_Attack && Attack_Is_Active == false && !Staggered)
                    {
                        Attack_Is_Active = true;
                        Attack_Activation = Time.time + Attack_Windup;
                    }
                    if (Time.time >= Attack_Activation && Attack_Is_Active && Windup_Done == false)
                    {
                        Attack_Hitbox.GetComponent<SpriteRenderer>().enabled = true;
                        Attack_Hitbox.GetComponent<CapsuleCollider2D>().enabled = true;
                        Attack_Hitbox.GetComponent<CapsuleCollider2D>().isTrigger = true;
                        Attack_Deactivation = Time.time + Attack_Duration;
                        Windup_Done = true;
                        if (Player_Character.GetComponent<Block>().Block_Is_Active == true && !Block_Counted)
                        {
                            Block_Tutorial_Counter++;
                            Block_Counted = true;
                        }
                    }
                    if (Time.time >= Attack_Deactivation && Windup_Done && Attack_Done == false)
                    {
                        Attack_Hitbox.GetComponent<SpriteRenderer>().enabled = false;
                        Attack_Hitbox.GetComponent<CapsuleCollider2D>().isTrigger = false;
                        Attack_Hitbox.GetComponent<CapsuleCollider2D>().enabled = false;
                        Attack_End = Time.time + Attack_Recovery;
                        Attack_Done = true;
                        Block_Counted = false;
                    }

                    if (Time.time >= Attack_End && Attack_Done)
                    {
                        Next_Attack = Time.time + Attack_Cooldown;
                        Attack_Is_Active = false;
                        Windup_Done = false;
                        Attack_Done = false;
                        if (Block_Tutorial_Counter == 3)
                        {
                            Block_Tutorial_Counter++;
                            Start_Dialogue.Raise();
                            dialogue_manager.StartDialogue(dialogue_4);
                            Combat_Tutorial = true;
                            Parry_Tutorial = false;
                            GetComponent<Derek_Logic>().enabled = true;
                        }
                    }
                }
                else
                {
                    //Is_In_Range = false;
                    if (Time.time > Attack_End)
                        transform.position -= Vector3.Normalize(transform.position - Player_Character.transform.position) * Movement_Speed * Time.deltaTime;
                }

            }

        //Combat tutorial
        if (Combat_Tutorial)
        {
            if (Vector3.Distance(transform.position, Player_Character.transform.position) <= Minimal_Distance || Attack_Is_Active)
            {
                //Is_In_Range = true;
                if (Time.time >= Next_Attack && Attack_Is_Active == false && !Staggered)
                {
                    Attack_Is_Active = true;
                    Attack_Activation = Time.time + Attack_Windup;
                }
                if (Time.time >= Attack_Activation && Attack_Is_Active && Windup_Done == false)
                {
                    Attack_Hitbox.GetComponent<SpriteRenderer>().enabled = true;
                    Attack_Hitbox.GetComponent<CapsuleCollider2D>().enabled = true;
                    Attack_Hitbox.GetComponent<CapsuleCollider2D>().isTrigger = true;
                    Attack_Deactivation = Time.time + Attack_Duration;
                    Windup_Done = true;
                }
                if (Time.time >= Attack_Deactivation && Windup_Done && Attack_Done == false)
                {
                    Attack_Hitbox.GetComponent<SpriteRenderer>().enabled = false;
                    Attack_Hitbox.GetComponent<CapsuleCollider2D>().isTrigger = false;
                    Attack_Hitbox.GetComponent<CapsuleCollider2D>().enabled = false;
                    Attack_End = Time.time + Attack_Recovery;
                    Attack_Done = true;
                    Block_Counted = false;
                }

                if (Time.time >= Attack_End && Attack_Done)
                {
                    Next_Attack = Time.time + Attack_Cooldown;
                    Attack_Is_Active = false;
                    Windup_Done = false;
                    Attack_Done = false;
                }
            }
            else
            {
                //Is_In_Range = false;
                if (Time.time > Attack_End)
                    transform.position -= Vector3.Normalize(transform.position - Player_Character.transform.position) * Movement_Speed * Time.deltaTime;
            }

        }

        if (End_Tutorial == true)
        {
            End_Tutorial = false;
            GetComponent<Derek_Logic>().enabled = false;
            Combat_Tutorial = false;
            Start_Dialogue.Raise();
            dialogue_manager.StartDialogue(dialogue_5);
        }
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Weapon")
        {
            if (Attack_Tutorial)
            {
                Attack_Tutorial_Hit++;
            }
        }
    }
}
