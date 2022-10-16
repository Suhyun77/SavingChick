using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_StartSceneChick : MonoBehaviour
{
    float currentTime;
    float changeTIme = 11f;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>changeTIme)
        {
            anim.SetTrigger("Idle");
        }
    }
}
