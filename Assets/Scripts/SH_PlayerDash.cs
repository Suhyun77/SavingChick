using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_PlayerDash : MonoBehaviour
{
    SH_PlayerMove playerMove;
    public float dashSpeed = 5;
    public float dashTime = 0.25f;
    Animator anim;
    // Dash effect
    public GameObject dashLine;
    // Dash ParticleSystem
    public GameObject dashDust;

    void Start()
    {
        playerMove = GetComponent<SH_PlayerMove>();
        anim = GetComponent<Animator>();
        dashDust.SetActive(false);  // dash ������ ���� Ȱ��ȭ
    }

    void Update()
    {
        // Left Shift ������ Dash
        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine("Dash");
            //Transform dashPos = Camera.main.transform;
            dashLine.SetActive(true);
            dashDust.SetActive(true);
            //Instantiate(dashLine, dashPos);
        }
        else
        {
            dashLine.SetActive(false);
            dashDust.SetActive(false);
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