using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_StartScenePlayer : MonoBehaviour
{
    float pcurrentTime;
    float pchangeTIme = 11.1f;
    Animator panim;
    void Start()
    {
        panim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pcurrentTime += Time.deltaTime;
        if (pcurrentTime > pchangeTIme)
        {
            panim.SetTrigger("Idle");
        }
    }
}

