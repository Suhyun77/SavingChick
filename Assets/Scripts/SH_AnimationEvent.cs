using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_AnimationEvent : MonoBehaviour
{
    
    void OnEventAttackForward()
    {
        SH_PlayerMove player = GameObject.Find("Player").GetComponent<SH_PlayerMove>();
        player.AttackForward();
    }
}
