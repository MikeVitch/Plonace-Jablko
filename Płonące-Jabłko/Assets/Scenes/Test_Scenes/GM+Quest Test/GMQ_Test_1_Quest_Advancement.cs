using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMQ_Test_1_Quest_Advancement : MonoBehaviour
{
    public Quest Test_Quest;

    // Start is called before the first frame update
    void Start()
    {
        if(Test_Quest.Current_Quest_Step == 6)
        {
            Test_Quest.Current_Quest_Step++;
            Test_Quest.Is_Finished = true;
        }
    }



}
