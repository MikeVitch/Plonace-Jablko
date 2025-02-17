using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derek_Attack : MonoBehaviour
{
    [HideInInspector]
    public float Attack_Damage;

    private void Start()
    {
        Attack_Damage = GetComponentInParent<Derek_Tutorial>().Attack_Damage;
    }
    void Update()
    {
        if (GetComponentInParent<Derek_Tutorial>().Combat_Tutorial)
            tag = "Derek_Attack";
        else
            tag = "Untagged";
    }
}
