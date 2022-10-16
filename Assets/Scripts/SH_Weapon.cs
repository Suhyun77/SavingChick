using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Weapon : MonoBehaviour
{
    // �浹 ����
    public bool isColliding;
    // ȿ�� ����
    public TrailRenderer attackTrail;
    public TrailRenderer skillTrail;
    // weapon collider
    public Collider weponCol;


    private void Update()
    {
      //  isColliding = false;
        // Į collider & Trail Renderer default = ��Ȱ��ȭ
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
        // �浹 ���� ��� �޼��� ������
        if (isColliding)
        {
         //   return;
        }
        // �浹 �� ǥ��
        isColliding = true; 


        // Attack
        if (SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true)
        {
            if (other.gameObject.tag == "Enemy1")
            {
                // Camera Shake
                SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.3f); 
                // Damage �ֱ�
                SSH_Enemybeta senemy = other.gameObject.GetComponent<SSH_Enemybeta>();
                other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
                senemy.AddDamage(1);
            }

            else if (other.gameObject.tag == "Enemy2")
            {
                // Camera Shake
                SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.3f);
                // Damage �ֱ�
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
                // Damage �ֱ�
                SSH_Enemybeta senemy = other.gameObject.GetComponent<SSH_Enemybeta>();
                other.GetComponentInChildren<Animator>().SetTrigger("GetHit");
                senemy.AddDamage(3);
            }

            else if (other.gameObject.tag == "Enemy2")
            {
                // Camera Shake
                SSH_NewCamera.Instance.OnShakeCamera(0.5f, 0.3f);
                // Damage �ֱ�
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
