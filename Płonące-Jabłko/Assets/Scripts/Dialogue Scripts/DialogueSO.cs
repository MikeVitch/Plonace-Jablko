using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueSO : ScriptableObject
{              
    public Sentence Starting_Sentence;
    public bool Is_Available;
    public Sprite Player_Portrait;
    public Sprite NPC_Portrait;
}