using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_LogoEvent : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    float currentTime;
    float activeTime = 3f;
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>activeTime)
        {
            anim.enabled = true;
        }
        logoMove();
    }
    //Vector3(1,131,0) Vector3(-538.833435,459.708282,0)Vector3(401.791595,459.708282,0)
    public void logoMove()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 1850f, "y", 1715f, "easetype", iTween.EaseType.easeOutBounce, "time", 3f));
    }
}
