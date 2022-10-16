using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Laser : MonoBehaviour
{
    // 충돌한 객체가 enemy일 경우 Camera Shake & enemy hp 감소
    // Enemy의 SkillPosition에 Collider 추가 & Layer Collision Matrix 이용해 충돌 처리
    // Particle System - Collision : Collides With - Laser 선택 / Send Collision Messages 선택
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Enemy1")
        {
            // Camera Shake
            SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.5f);
            // Damage 주기
            SSH_Enemybeta senemy = other.gameObject.GetComponent<SSH_Enemybeta>();
            other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
            senemy.AddDamage(3);
        }

        else if (other.gameObject.tag == "Enemy2")
        {
            // Camera Shake
            SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.5f);
            // Damage 주기
            SSH_Enemy2 senemy2 = other.gameObject.GetComponent<SSH_Enemy2>();
            other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
            senemy2.AddDamage(3);
        }
    }
}

