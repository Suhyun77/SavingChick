using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_PlayerCS : MonoBehaviour
{
    public Animator anim;
    public GameObject Enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        // animº¯¼ö¿¡ Animator ÄÄÆ÷³ÍÆ® »ðÀÔ
        anim = GetComponent<Animator>();
    }

    void Idle_Anim()
    {
        anim.SetTrigger("Idle");
    }

    void AttackFar_Anim()
    {
        anim.SetTrigger("AttackFar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
