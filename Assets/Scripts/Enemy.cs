using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 7;
    public float createTime = 2;   
    public GameObject bulletFactory;
    public Slider enemySlider;
    float currentTime;
    public GameObject player;
    public Animator anim;


    // ü�� singleton
    int hp;
    public int maxHP = 10;
    int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            enemySlider.value = value;

        }
    }


    void Start()
    {
        player = GameObject.Find("Player");

        //ó�� ���� HP����
        enemySlider.maxValue = maxHP;
        HP = maxHP;
        anim = GetComponent<Animator>();
    }

    void Dead_Anim()
    {
        anim.SetTrigger("Dead");
    }

    void GetHit_Anim()
    {
        anim.SetTrigger("GetHit");
    }


    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        currentTime += Time.deltaTime;
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;
        enemySlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
        gameObject.transform.LookAt(player.transform);

        //�����ð����� �Ѿ˰��忡�� �Ѿ�
        if (currentTime > createTime)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = transform.position;
            currentTime = 0;
        }
    }


    
    SH_PlayerHP php;

    
    private void OnTriggerEnter(Collider other)
    {   //�÷��̾�� ������ ��Ҵٸ� �÷��̾��� HP�� ���δ�

        SH_PlayerMove player = other.gameObject.GetComponent<SH_PlayerMove>();
        if (player)
        {
            php.AddDamage(1);
        }

        //�÷��̾��� �ҷ��� Enemy�� �¾Ҵٸ�
        if (other.gameObject.name.Contains("PBullet"))
        {
            if (HP > 0)
            {
                HP -= 1;
            }
        }
    }

    
   

}
    

