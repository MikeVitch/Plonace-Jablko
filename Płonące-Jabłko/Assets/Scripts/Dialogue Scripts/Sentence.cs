using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sentence : ScriptableObject
{
    [Tooltip("String_Variable with the name of the character talking")]
    public String_Variable From;

    [TextArea(3, 10)]
    public string Text = "Text";

    [Tooltip("Activates only when there aren't options")]
    public Sentence Next_Sentence;

    [Tooltip("Activates only when there aren't options")]
    public Sprite Player_Portrait;
    [Tooltip("Activates only when there aren't options")]
    public Sprite NPC_Portrait;

    public List<Choice> Options = new List<Choice>();

    public bool HasOptions()
    {
        if (Options.Count == 0)
            return false;
        else
            return true;
    }
}

[System.Serializable]
public class Choice
{
    [TextArea(3, 10)]
    public string Text;
    public Sentence NextSentence;
    public Game_Event Consequence;
    public Sprite Player_Portrait;
    public Sprite NPC_Portrait;
}