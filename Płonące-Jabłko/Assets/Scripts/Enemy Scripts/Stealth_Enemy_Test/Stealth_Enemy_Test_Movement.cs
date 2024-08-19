using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stealth_Enemy_Test_Movement : MonoBehaviour
{
    public float Base_Movement_Speed;
    float Movement_Speed;
    [Tooltip("Angle by which to rotate per second")]
    public float Base_Rotation_Speed;
    [Tooltip("Angle by which to rotate per second")]
    public float Look_Around_Rotation_Speed;
    public float Look_Around_Delay;
    [Tooltip("In Degrees")]
    public float Look_Around_Rotation;
    float Rotation_Speed;
    Vector3 Direction_Of_Movement;
    int Next_Patrol_Point;

    [Header("Patrol Type")]
    [Tooltip("Switch on to loop patrol, off to make it back and forth")]
    public bool Loop;
    public List<Patrol_Point> Patrol_Points;
    public List<Patrol_Point> Patrol_Points_Copy;
    bool Back;
    float Rotation_Angle;
    Vector3 Target_Rotation;
    int i;
    public GameObject View_Cone_Rotation_Point;
    public float Idle_Look_Delay;
    public float Suspicious_Look_Delay;
    public float Suspicious_Move_Delay;
    bool Is_Patroling;
    bool Is_Looking_Around;
    bool Is_Waiting;
    IEnumerator Look_Around_Coroutine;
    Stealth_Enemy_Logic stealth_enemy_logic;
    Player_Logic player_logic;
    Vector3 Last_Known_Position;
    bool Is_Looking_After_Player;
    float Time_Player_Seen;
    bool In_Persuit;
    Vector3 LKP_Checked;

    private void Start()
    {
        player_logic = FindObjectOfType<Player_Logic>();
        stealth_enemy_logic = GetComponent<Stealth_Enemy_Logic>();

        for(i = 0; i < Patrol_Points.Count;i++)
        {
            Patrol_Points_Copy.Add( new Patrol_Point());
            Patrol_Points_Copy[i].Look_Around = Patrol_Points[i].Look_Around;
            Patrol_Points_Copy[i].Point = Patrol_Points[i].Point;
            Patrol_Points_Copy[i].Wait_Time = Patrol_Points[i].Wait_Time;
        }
    }



    void Update()
    {

        Movement_Speed = Base_Movement_Speed;
        Rotation_Speed = Base_Rotation_Speed;

        if(stealth_enemy_logic.Player_Seen)
        {
            Last_Known_Position = player_logic.transform.position;
        }

        if(Is_Looking_Around && stealth_enemy_logic.Player_Seen)
        {
            Is_Looking_Around = false;
            StopAllCoroutines();
        }

        if (stealth_enemy_logic.Player_Seen && stealth_enemy_logic.Idle)
        {
            if (!Is_Looking_After_Player)
            { 
                StartCoroutine(LookAfterPlayer());
            }
        }
        else if (stealth_enemy_logic.Player_Seen && (stealth_enemy_logic.Suspicious || stealth_enemy_logic.Aggressive))
        {
            if (!Is_Looking_After_Player)
            {
                StartCoroutine(LookAfterPlayer());
            }
        }
        else if(!Is_Looking_After_Player)
        {

            if (gameObject.transform.position == Patrol_Points[Next_Patrol_Point].Point.gameObject.transform.position && Patrol_Points_Copy[Next_Patrol_Point].Wait_Time > 0)
            {
                if (!Is_Looking_Around)
                    Patrol_Points_Copy[Next_Patrol_Point].Wait_Time -= Time.deltaTime;

                Is_Waiting = true;
            }
            else
                Is_Waiting = false;

            if (gameObject.transform.position == Patrol_Points[Next_Patrol_Point].Point.gameObject.transform.position && Patrol_Points_Copy[Next_Patrol_Point].Look_Around == true)
            {
                StartCoroutine(LookAround());
                Patrol_Points_Copy[Next_Patrol_Point].Look_Around = false;
            }

            //Patroling
            if (!Is_Waiting && !Is_Looking_Around)
            {
                if (gameObject.transform.position == Patrol_Points[Next_Patrol_Point].Point.gameObject.transform.position && !Back)
                {
                    Patrol_Points_Copy[Next_Patrol_Point].Look_Around = Patrol_Points[Next_Patrol_Point].Look_Around;
                    Patrol_Points_Copy[Next_Patrol_Point].Point = Patrol_Points[Next_Patrol_Point].Point;
                    Patrol_Points_Copy[Next_Patrol_Point].Wait_Time = Patrol_Points[Next_Patrol_Point].Wait_Time;

                    Next_Patrol_Point++;
                    if (Next_Patrol_Point == Patrol_Points.Count && Loop)
                        Next_Patrol_Point = 0;
                    else if (Next_Patrol_Point == Patrol_Points.Count && !Loop)
                    {
                        Back = true;
                        Next_Patrol_Point -= 2;
                    }
                }
                else if (gameObject.transform.position == Patrol_Points[Next_Patrol_Point].Point.gameObject.transform.position && Back)
                {
                    Patrol_Points_Copy[Next_Patrol_Point].Look_Around = Patrol_Points[Next_Patrol_Point].Look_Around;
                    Patrol_Points_Copy[Next_Patrol_Point].Point = Patrol_Points[Next_Patrol_Point].Point;
                    Patrol_Points_Copy[Next_Patrol_Point].Wait_Time = Patrol_Points[Next_Patrol_Point].Wait_Time;

                    Next_Patrol_Point--;
                    if (Next_Patrol_Point == -1)
                    {
                        Next_Patrol_Point += 2;
                        Back = false;
                    }
                }

                



                if (LKP_Checked != Last_Known_Position && stealth_enemy_logic.Suspicious || stealth_enemy_logic.Aggressive)
                {
                    Direction_Of_Movement = Last_Known_Position - gameObject.transform.position;
                    Direction_Of_Movement.Normalize();
                    if (Vector3.Distance(Last_Known_Position, gameObject.transform.position) < Vector3.Magnitude(Direction_Of_Movement * Movement_Speed * Time.deltaTime))
                    {
                        gameObject.transform.position = Last_Known_Position;
                        LKP_Checked = Last_Known_Position;
                        StartCoroutine(LookAround());
                    }
                    else
                        gameObject.transform.position += Direction_Of_Movement * Movement_Speed * Time.deltaTime;
                }
                else
                {
                    Direction_Of_Movement = Patrol_Points[Next_Patrol_Point].Point.gameObject.transform.position - gameObject.transform.position;
                    Direction_Of_Movement.Normalize();
                    if (Vector3.Distance(Patrol_Points[Next_Patrol_Point].Point.gameObject.transform.position, gameObject.transform.position) < Vector3.Magnitude(Direction_Of_Movement * Movement_Speed * Time.deltaTime))
                        gameObject.transform.position = Patrol_Points[Next_Patrol_Point].Point.gameObject.transform.position;
                    else
                        gameObject.transform.position += Direction_Of_Movement * Movement_Speed * Time.deltaTime;
                }

                /*public List<GameObject> View_Cones;
            for (i = 0; i < View_Cones.Count; i++)
            {
                Rotation_Angle = Vector3.Angle(Direction_Of_Movement, View_Cones[i].transform.right);
                //gameObject.transform.eulerAngles += new Vector3(0, 0, Rotation_Angle);
                Target_Rotation = View_Cones[i].transform.rotation.eulerAngles + new Vector3(0, 0, Rotation_Angle);
                View_Cones[i].transform.rotation = Quaternion.RotateTowards(View_Cones[i].transform.rotation, Quaternion.Euler(Target_Rotation), Rotation_Speed * Time.deltaTime);
            }*/
                Rotation_Angle = Vector3.SignedAngle(Direction_Of_Movement, View_Cone_Rotation_Point.transform.right, Vector3.back);
                Target_Rotation = View_Cone_Rotation_Point.transform.rotation.eulerAngles + new Vector3(0, 0, Rotation_Angle);
                View_Cone_Rotation_Point.transform.rotation = Quaternion.RotateTowards(View_Cone_Rotation_Point.transform.rotation, Quaternion.Euler(Target_Rotation), Rotation_Speed * Time.deltaTime);
            }
        }
        }


        [System.Serializable]
    public class Patrol_Point
    {
        public GameObject Point;
        public float Wait_Time;
        public bool Look_Around;
    }

    Vector3 Base_Rotation;
    int j;

    IEnumerator LookAround()
    {
        Base_Rotation = View_Cone_Rotation_Point.transform.rotation.eulerAngles;
        Vector3 Rotation_Left = Base_Rotation - new Vector3(0, 0, Look_Around_Rotation);
        Vector3 Rotation_Right = Base_Rotation + new Vector3(0, 0, Look_Around_Rotation);
        j = 0;
        Is_Looking_Around = true;
        yield return new WaitForSeconds(Look_Around_Delay);
        while (true)
        {
            if (j == 0)
            {
                View_Cone_Rotation_Point.transform.rotation = Quaternion.RotateTowards(View_Cone_Rotation_Point.transform.rotation, Quaternion.Euler(Rotation_Left), Look_Around_Rotation_Speed * Time.deltaTime);
                if (View_Cone_Rotation_Point.transform.rotation == Quaternion.Euler(Rotation_Left))
                {
                    yield return new WaitForSeconds(Look_Around_Delay);
                    j++;
                }
            }
            else if (j == 1)
            {
                View_Cone_Rotation_Point.transform.rotation = Quaternion.RotateTowards(View_Cone_Rotation_Point.transform.rotation, Quaternion.Euler(Base_Rotation), Look_Around_Rotation_Speed * Time.deltaTime);
                if (View_Cone_Rotation_Point.transform.rotation == Quaternion.Euler(Base_Rotation))
                { 
                    j++;
                }
            }
            else if (j == 2)
            {
                View_Cone_Rotation_Point.transform.rotation = Quaternion.RotateTowards(View_Cone_Rotation_Point.transform.rotation, Quaternion.Euler(Rotation_Right), Look_Around_Rotation_Speed * Time.deltaTime);
                if (View_Cone_Rotation_Point.transform.rotation == Quaternion.Euler(Rotation_Right))
                {
                    yield return new WaitForSeconds(Look_Around_Delay);
                    j++;
                }
            }
            else if (j == 3)
            {
                View_Cone_Rotation_Point.transform.rotation = Quaternion.RotateTowards(View_Cone_Rotation_Point.transform.rotation, Quaternion.Euler(Base_Rotation), Look_Around_Rotation_Speed * Time.deltaTime);
                if (View_Cone_Rotation_Point.transform.rotation == Quaternion.Euler(Base_Rotation))
                {
                    yield return new WaitForSeconds(Look_Around_Delay);
                    j++;
                }
            }
            else if (j == 4)
            {
                Is_Looking_Around = false;
                StopAllCoroutines();
            }


            yield return null;
        }
    }

    IEnumerator LookAfterPlayer()
    {
        Is_Looking_After_Player = true;

        if (stealth_enemy_logic.Idle)
        {
            yield return new WaitForSeconds(Idle_Look_Delay);
        }

        if (stealth_enemy_logic.Suspicious || stealth_enemy_logic.Aggressive)
        {
            yield return new WaitForSeconds(Suspicious_Look_Delay);
        }

        while(true)
        {
            Direction_Of_Movement = Last_Known_Position - gameObject.transform.position;
            Rotation_Angle = Vector3.SignedAngle(Direction_Of_Movement, View_Cone_Rotation_Point.transform.right, Vector3.back);
            Target_Rotation = View_Cone_Rotation_Point.transform.rotation.eulerAngles + new Vector3(0, 0, Rotation_Angle);
            View_Cone_Rotation_Point.transform.rotation = Quaternion.RotateTowards(View_Cone_Rotation_Point.transform.rotation, Quaternion.Euler(Target_Rotation), Rotation_Speed * Time.deltaTime);

            if (View_Cone_Rotation_Point.transform.rotation == Quaternion.Euler(Target_Rotation) && !stealth_enemy_logic.Player_Seen)
            {
                if(stealth_enemy_logic.Idle)
                    yield return new WaitForSeconds(Suspicious_Look_Delay);

                if (stealth_enemy_logic.Suspicious)
                    yield return new WaitForSeconds(Idle_Look_Delay);

                Is_Looking_After_Player = false;
                StopAllCoroutines();
            }
            yield return null;
        }
    }
}
