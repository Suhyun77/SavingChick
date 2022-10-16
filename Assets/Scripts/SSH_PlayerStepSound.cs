using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_PlayerStepSound : MonoBehaviour
{
    Animator anim;
    public AudioSource footstepSound;
    SH_PlayerMove player;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<SH_PlayerMove>();
    }

    void Update()
    {
        // walk, run 제외하고 모두 footstep AudioSource Volume off
        footstepSound.volume = 0;

        // 걸을 때는 Volume on, footstep 오디오 pitch=1
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") == true)
        //if (player.dir != Vector3.zero)
        //{
        //    footstepSound.pitch = 1;
        //    footstepSound.volume = 1;
        //}

        // 뛸때는 Volume on, pitch = 2
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run") == true)
        {
            footstepSound.pitch = 2;
            footstepSound.volume = 1;
        }
    }
}