using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_ClearSceneChicken : MonoBehaviour
{

    float currentTime;
    float changeTime = 6f;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > changeTime)
        {
            anim.SetTrigger("Idle");
        }
    }
}
