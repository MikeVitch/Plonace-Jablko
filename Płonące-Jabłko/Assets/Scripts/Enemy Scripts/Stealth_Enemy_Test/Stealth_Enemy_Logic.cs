using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth_Enemy_Logic : MonoBehaviour
{
    public float Alertness;
    public float Alertness_Decay_Rate;
    public float Alertness_Decay_Delay;
    public float Time_To_Lose_Aggro;
    float Last_Time_Player_Seen;
    public bool Idle = true;
    public bool Suspicious;
    public bool Aggressive;
    public List<Stealth_Vision> Vision_Areas;
    public List<Stealth_Hearing> Hearing_Areas;
    int Vision_Areas_Seeing_Player;
    int i;
    public bool Player_Seen;
    Enemy_Logic enemy_logic;
    float Past_Health;
    public string Raycast_Layer_1 = "Wall";
    public string Raycast_Layer_2 = "Feet";
    LayerMask Layer_Mask;
    public bool Player_Behind_Wall;
    Vector3 Direction_Of_Player;
    Player_Logic player_logic;
    public float Combat_Hide_Zone_Detection_Distance;
    [SerializeField] SayBubble sayGuard;
    public bool isSeen;
    public GameObject backPoint;
    public GameObject Cassie;
    
    private void Start()
    {
        player_logic = FindObjectOfType<Player_Logic>();
        enemy_logic = GetComponent<Enemy_Logic>();
        Past_Health = enemy_logic.Health;
        

    }

    private void FixedUpdate()
    {
        //Je¿eli bêdzie trzeba sprawdzaæ na wiêcej ni¿ jednej warstwie
        //Layer_Mask = (1 << LayerMask.NameToLayer("Raycast_Layer_1")) | (1 << LayerMask.NameToLayer("Raycast_Layer_2")itd.);

        // RaycastHit2D Hit;
        Layer_Mask = (1 << LayerMask.NameToLayer(Raycast_Layer_1) | (1 << LayerMask.NameToLayer(Raycast_Layer_2)));
        Direction_Of_Player = player_logic.transform.position - transform.position;
        if (Physics2D.Raycast(transform.position, Direction_Of_Player, 5.25f, Layer_Mask))
        {
            //Debug.DrawRay(transform.position, Direction_Of_Player, Color.yellow);
            Player_Behind_Wall = true;
        }
        else
        {
            //Debug.DrawRay(transform.position, Direction_Of_Player, Color.green);
            Player_Behind_Wall = false;
        }
        // Hit = Physics2D.Raycast(transform.position, Direction_Of_Player, Mathf.Infinity, Layer_Mask);
        // Debug.Log(Hit.collider);
    }

    void Update()
    {
        if (Player_Seen)
        {
            // Debug.Log("See you!!");

            sayGuard.showText(1, "Znów siê wymykasz? ");

            isSeen = true;

        }

        for (i = 0; i < Vision_Areas.Count; i++)
        {
            if (Vision_Areas[i].Player_Seen)
                Vision_Areas_Seeing_Player++;
        }

        for (i = 0; i < Hearing_Areas.Count; i++)
        {
            if (Hearing_Areas[i].Player_Seen)
                Vision_Areas_Seeing_Player++;
        }

        if (Vision_Areas_Seeing_Player > 0)
            Player_Seen = true;
        else
            Player_Seen = false;
        isSeen = false;


        Vision_Areas_Seeing_Player = 0;

        if (enemy_logic.Health < Past_Health)
        {
            Alertness = 100;
            Past_Health = enemy_logic.Health;
        }

        if (Player_Seen)
        {
            Last_Time_Player_Seen = Time.time;

        }

        if (Idle && Alertness >= 50)
        {
            Idle = false;
            Suspicious = true;
        }

        if (Suspicious && Alertness >= 100)
        {
            Suspicious = false;
            Aggressive = true;
        }

        if (Aggressive)
        {
            Alertness = 100;
            if (Time.time - Last_Time_Player_Seen >= Time_To_Lose_Aggro)
            {
                Aggressive = false;
                Suspicious = true;
            }
        }

        if (Suspicious && Time.time - Last_Time_Player_Seen >= Alertness_Decay_Delay)
        {
            Alertness -= Alertness_Decay_Rate * Time.deltaTime;
            if (Alertness < 50)
                Alertness = 50;
        }

        if (Idle && Time.time - Last_Time_Player_Seen >= Alertness_Decay_Delay)
        {
            Alertness -= Alertness_Decay_Rate * Time.deltaTime;
            if (Alertness < 0)
                Alertness = 0;
        }
        TeleportOnSeen();
    }

    public void TeleportOnSeen()
    {
        if (Aggressive == true)
        {
            Cassie.transform.position = backPoint.transform.position;
            //Time.timeScale = 0;
            Debug.Log("return");

        }
        else
        {

            return;

        }

    }

    
}
