using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_PlayerDash : MonoBehaviour
{
    SH_PlayerMove playerMove;
    public float dashSpeed = 5;
    public float dashTime = 0.25f;
    Animator anim;
    // Dash Line ParticleSystem
    public ParticleSystem dashLine;
    // Dash Dust ParticleSystem
    public ParticleSystem dashDust;

    void Start()
    {
        playerMove = GetComponent<SH_PlayerMove>();
        anim = GetComponent<Animator>();
        dashLine.Stop();
        dashDust.Stop();
    }

    void Update()
    {
        // Left Shift ´©¸£¸é Dash
        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine("Dash");
            dashLine.Play();
            dashDust.Play();
        }
        else
        {
            dashLine.Stop();
            dashDust.Stop();
        }
    }
    IEnumerator Dash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            anim.SetBool("IsRun", true);
            playerMove.cc.Move(playerMove.dir * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}