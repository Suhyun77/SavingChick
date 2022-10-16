using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Weapon : MonoBehaviour
{
    // 충돌 여부
    public bool isColliding;
    // 효과 변수
    public TrailRenderer attackTrail;
    public TrailRenderer skillTrail;
    // weapon collider
    public Collider weponCol;


    private void Update()
    {
      //  isColliding = false;
        // 칼 collider & Trail Renderer default = 비활성화
        weponCol.enabled = false;
        attackTrail.enabled = false;
        skillTrail.enabled = false;
        // Attack
        if (SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true)
        {
            weponCol.enabled = true;
            attackTrail.enabled = true;
        }
        else
        {
            isColliding = false;
            if (lists.Count > 0) lists.Clear();
        }
        // Skill
        if (SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Skill") == true)
        {
            weponCol.enabled = true;
            skillTrail.enabled = true;
        }
    }
    List<Collider> lists = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if (lists.Contains(other) == true) return;
        lists.Add(other);
        // 충돌 중일 경우 메서드 끝내기
        if (isColliding)
        {
         //   return;
        }
        // 충돌 중 표시
        isColliding = true; 


        // Attack
        if (SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true)
        {
            if (other.gameObject.tag == "Enemy1")
            {
                // Camera Shake
                SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.3f); 
                // Damage 주기
                SSH_Enemybeta senemy = other.gameObject.GetComponent<SSH_Enemybeta>();
                other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
                senemy.AddDamage(1);
            }

            else if (other.gameObject.tag == "Enemy2")
            {
                // Camera Shake
                SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.3f);
                // Damage 주기
                SSH_Enemy2 senemy2 = other.gameObject.GetComponent<SSH_Enemy2>();
                other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
                senemy2.AddDamage(1);
            }
        }

        // Skill
        if (SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Skill") == true)
        {
            if (other.gameObject.tag == "Enemy1")
            {
                // Camera Shake
                SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.3f);
                // Damage 주기
                SSH_Enemybeta senemy = other.gameObject.GetComponent<SSH_Enemybeta>();
                other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
                senemy.AddDamage(3);
            }

            else if (other.gameObject.tag == "Enemy2")
            {
                // Camera Shake
                SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.3f);
                // Damage 주기
                SSH_Enemy2 senemy2 = other.gameObject.GetComponent<SSH_Enemy2>();
                other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
                senemy2.AddDamage(3);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit : " + other.name);
    }
}
