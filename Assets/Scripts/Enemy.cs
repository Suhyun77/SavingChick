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


    // 체력 singleton
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

        //처음 몬스터 HP설정
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

        //일정시간마다 총알공장에서 총알
        if (currentTime > createTime)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = transform.position;
            currentTime = 0;
        }
    }


    
    SH_PlayerHP php;

    
    private void OnTriggerEnter(Collider other)
    {   //플레이어기 적에게 닿았다면 플레이어의 HP를 줄인다

        SH_PlayerMove player = other.gameObject.GetComponent<SH_PlayerMove>();
        if (player)
        {
            php.AddDamage(1);
        }

        //플레이어의 불렛에 Enemy가 맞았다면
        if (other.gameObject.name.Contains("PBullet"))
        {
            if (HP > 0)
            {
                HP -= 1;
            }
        }
    }

    
   

}
    

