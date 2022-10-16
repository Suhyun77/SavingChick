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
        // walk, run �����ϰ� ��� footstep AudioSource Volume off
        footstepSound.volume = 0;

        // ���� ���� Volume on, footstep ����� pitch=1
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") == true)
        //if (player.dir != Vector3.zero)
        //{
        //    footstepSound.pitch = 1;
        //    footstepSound.volume = 1;
        //}

        // �۶��� Volume on, pitch = 2
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run") == true)
        {
            footstepSound.pitch = 2;
            footstepSound.volume = 1;
        }
    }
}