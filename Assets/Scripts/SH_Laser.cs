using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Laser : MonoBehaviour
{
    // �浹�� ��ü�� enemy�� ��� Camera Shake & enemy hp ����
    // Enemy�� SkillPosition�� Collider �߰� & Layer Collision Matrix �̿��� �浹 ó��
    // Particle System - Collision : Collides With - Laser ���� / Send Collision Messages ����
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Enemy1")
        {
            // Camera Shake
            SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.5f);
            // Damage �ֱ�
            SSH_Enemybeta senemy = other.gameObject.GetComponent<SSH_Enemybeta>();
            other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
            senemy.AddDamage(3);
        }

        else if (other.gameObject.tag == "Enemy2")
        {
            // Camera Shake
            SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.5f);
            // Damage �ֱ�
            SSH_Enemy2 senemy2 = other.gameObject.GetComponent<SSH_Enemy2>();
            other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
            senemy2.AddDamage(3);
        }
    }
}

