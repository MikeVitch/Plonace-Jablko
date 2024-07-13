using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class String_Variable : ScriptableObject
{
    public string Value;

    public void SetValue(string s)
    {
        Value = s;
    }

    public void SetValue(String_Variable s)
    {
        Value = s.Value;
    }
}