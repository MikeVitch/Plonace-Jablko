using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]

public class Quest : ScriptableObject
{
    public string Quest_Name;
    public bool Is_Active;
    public bool Is_Tracked;
    public bool Is_Finished;
    public int Current_Quest_Step;
    //public List<Quest_Step> Quest_Steps = new List<Quest_Step>();
    [TextArea(1, 10)]
    public List<string> Quest_Steps;


}



/*[System.Serializable]
public class Quest_Step
{
    public int Step_Number;
    [TextArea(1, 10)]
    public string Text;
    bool Finished;
}*/