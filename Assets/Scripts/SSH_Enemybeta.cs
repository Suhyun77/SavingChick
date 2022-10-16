using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using System;

public class SSH_Enemybeta : MonoBehaviour
{
    // Start is called before the first frame update
    //상태 : 정지, 이동, 공격, 공격당함, 죽음

    public float speed = 7;
    public float createTIme = 2;
    //스킬이펙트
    public GameObject skillEffect;
    public ParticleSystem skillEffectPar;
    NavMeshAgent agent;
    //총알공장
    public GameObject bulletFactory;
    public GameObject firePosition;
    //제력바
    public Slider enemySlider;
    //체력바 게임 오브젝트
    public GameObject genemySlider;
    //체력바 시작 크기
    Vector3 startScale;
    //체력바 보이게 하는 거리
    public float sliderDistance = 20;
    public Animator anim;
    //플레이어
    public GameObject player;
    //현재시간
    float currentTIme;
    //리지드 바디
    Rigidbody rb;
    //머티리얼
    Material mat;
    // enemy 소리
    public AudioSource enemySound;
    //상태머신
    public enum state
    {
        Idle,
        Move,
        Attack,
        Attacked,
        Die
    }
    public state State;
    // player attack Sound
    public AudioSource attackSound;
    // player skill sound
    public AudioSource skillSound;

    


    //공격시 HP를 닳게하고 싶다

    int hp;
    public int maxHP = 2;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if(hp<0)
            {
                hp = 0;
            }
            enemySlider.value = hp;

        }
    }


    void Start()
    {
        //처음 몬스터 HP 설정값
        enemySlider.maxValue = maxHP;
        HP = maxHP;
        State = state.Idle;
        player = GameObject.Find("Player");
        //agent 설정
        agent = GetComponent<NavMeshAgent>();
        
        //스킬이펙트의 파티클 시스템 컴포넌트를 가져온다
        skillEffectPar = skillEffect.GetComponent<ParticleSystem>();
        //체력바의 크기 정하기
        startScale = genemySlider.transform.localScale;
        //체력바는 처음에 보이지 않게한다
        genemySlider.SetActive(false);
        //리지드 바디 할당
        rb = GetComponent<Rigidbody>();
        SkinnedMeshRenderer mr = GetComponentInChildren<SkinnedMeshRenderer>();
        mat = mr.material;
        //원래 자기 위치
        originPos = transform.position;
    }

    void Update()
    {
        if(State == state.Idle)
        {
            UpdateIdle();
        }
        if (State == state.Move)
        {
            UpdateMove();
        }
        //if (State == state.Attack)
        //{
        //    UpdateAttack();
        //}
        if (State == state.Attacked)
        {
            UpdateAttacked();
        }
        


        if (HP > 0)
        {    //슬라이더의 위치
            enemySlider.transform.position = transform.position + new Vector3(0, 1, 0); //적 HP 따라가기

           

            //카메라와의 일정거리가 되면 slider을 보이게 만든다
            float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
            if (distance < sliderDistance)
            {
                //slider가 보인다
                genemySlider.SetActive(true);
                //플레이어와의 거리를 기준으로 슬라이더의 크기가 달라진다
                //거리가 커지면 scale은 작아져야한다
                Vector3 newScale = startScale * sliderDistance / (distance * 3);
                genemySlider.transform.localScale = newScale;
                genemySlider.transform.rotation = Camera.main.transform.rotation;

            }
            else if (distance >= sliderDistance)
            { genemySlider.SetActive(false); }

        }

        //Idle상태에서 일정 범위내에서 돌아다닐 수 있게하도록 하는 변수들
        originDis = (originPos - transform.position).magnitude;
    }

    Vector3 originPos;
    Vector3 target;
    float originDis;
    float targetDis;
    public float moveRange = 2;
    //감지 거리
    public float findDistance = 10;
    private void UpdateIdle()
    {
        //멈춰있는 상태에서도 어느정도 움직이고 싶다
        anim.SetTrigger("Move");
        target = new Vector3(transform.position.x + UnityEngine.Random.Range(-1 * moveRange, moveRange), 0, transform.position.z + UnityEngine.Random.Range(-1 * moveRange, moveRange));
        agent.SetDestination(target);
        if (originDis >= moveRange)
        {
            target = originPos;
            agent.SetDestination(target);
        }
        //플레이어와의 거리
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < findDistance)
        {
            State = state.Move;
        }
    }

    public float attackDistance = 3;
    private void UpdateMove()
    {

        //플레이어 바라보게 만들기
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(lookAt);

        //플레이어와 겹치지 않게 목표지점을 설정한다 
        agent.destination = player.transform.position - new Vector3(0,0,-2);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < attackDistance)
        {
            anim.SetTrigger("Attack");
        }
        //감지거리보다 거리가 커진다면 Idle로 바꾼다
        if(distance > findDistance)
        {
            State = state.Idle;
            anim.SetTrigger("Idle");
        }
    }

    //해당 부분에서 skill1 : 총알 발사 + 3초 기다려서 Skill2 : 체력 회복
    
    public void OnEventAttack()
    {
        //플레이어 바라보게 만들기
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(lookAt);
        //공격은 멈춰서 한다
        agent.isStopped = true;
        //Skill1 :총알 공장에서 총알 발사
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.position = firePosition.transform.position;
        bullet.transform.forward = firePosition.transform.forward;

        StartCoroutine("IeSkill");
        State = state.Move;
        anim.SetTrigger("Move");
        agent.isStopped = false;
    }

    //Skill2 : 체력회복
    IEnumerator IeSkill()
    {

        skillEffect.SetActive(true);
        skillEffectPar.Play();
        yield return new WaitForSeconds(3.0f);
        skillEffect.SetActive(false);
    }

    private void UpdateAttacked()
    {
      
    }

    //데미지 텍스트 공장
    public GameObject damageTextFactory; 
    
    public void AddDamage(int damage)
    {
        //HP가 0보다 크다면
        if(HP>0)
        {

            //anim.SetTrigger("GetHit");
            //데미지 텍스트 생성
            GameObject damageText = Instantiate(damageTextFactory, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            damageText.GetComponent<TextMeshProUGUI>().text = damage.ToString();
            Destroy(damageText, 0.5f);
            //HP를 damage만큼 줄인다
            HP -= damage;
            enemySound.Play();
            StartCoroutine("IeAttacked");
            StartCoroutine("IeBlackChange");


            //감소시킨 순간에 HP가 0보다 작다면 죽는다
            if (HP <= 0)
            {
                //2초 뒤에 죽기 + 체력바 없애기
                anim.SetTrigger("Die");
                //아이템 나오는 시간 늦추기
                Invoke("Item1Drop", 2);
                Destroy(gameObject, 2f);
                genemySlider.SetActive(false);
                //밀리지 않게하기
                agent.speed = 0;
                agent.isStopped = true;                
                //흑백슬롯 끄기
                SSH_UIManager.Instance.enemy1slotblack.SetActive(false);
                SSH_UIManager.Instance.quest.GetComponent<SSH_Quest>().enemy1Count++;
            }

        }
        


    }

    //공격 당했을 때 까맣게 변하고 싶다
    IEnumerator IeBlackChange()
    {
        mat.color = Color.black;
        yield return new WaitForSeconds(0.5f);
        mat.color = Color.white;
    }

    //공격 당했을 때 밀리고 싶다, 좀 더 확 밀리게 고칠 예정
    //Lerp 사용 안할시 처음에 속도를 늘리고 점차 줄인다 0으로는 멈추게
    public float force = 10;
    IEnumerator IeAttacked()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + transform.forward * -1.5f;
        float currentTime = 0;
       
        while (true)
        {
            if (currentTime < 1)
            {
                currentTime += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, currentTime * 10);
                //rb.AddForce(new Vector3(0,0,-1) * force, ForceMode.Impulse); rb velocity를 줄여줘야함 
                yield return null;
            }

            else
            {
                anim.SetTrigger("Move");
                yield break;
            }
        }
    }

    //아이템 공장
    public GameObject itemFactory;
    //아이템 드랍
    public void Item1Drop()
    {
        GameObject item1 = Instantiate(itemFactory);
        item1.transform.position = transform.position;
    }

    //private void UpdateDie()
    //{

    //}

    // 공격 효과 발생 지점
    Transform attackPosition;
    // 스킬 효과 발생 지점
    Transform skillPosition;
    public GameObject attackEffectFactory;  // 공격 효과
    public GameObject skillEffectFactory;  // 스킬 효과
    private void OnTriggerEnter(Collider other)
    {
        // Player : 부딪히면 Damage 주기
        SH_PlayerHP player = other.gameObject.GetComponent<SH_PlayerHP>();
        // 자기 자식 공격 위치 컴포넌트
        attackPosition = transform.Find("AttackPosition");
        // 자기 자식 스킬 위치 컴포넌트
        skillPosition = transform.Find("SkillPosition");
        print(attackPosition);
        print(skillPosition);
        if (player)
        {
            if(hp>0)
            {
                player.AddDamage(1);
            }
           
        }

        // Attack : Enemy 앞에 공격 효과 만들기
        if (other.gameObject.name == "Weapon" && SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true)
        {
            // getcomponent해서 만들어진 enemy의 attackPosition
            Vector3 pos = attackPosition.position;
            GameObject explosion = Instantiate(attackEffectFactory);
            explosion.transform.position = pos;
            attackSound.Play();
        }
        // Skill : Enemy 앞에 스킬 효과 만들기
        if (other.gameObject.name == "Weapon" && SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Skill") == true)
        {
            Vector3 pos = skillPosition.position;
            GameObject explosion = Instantiate(skillEffectFactory);
            explosion.transform.position = pos;
            skillSound.Play();
        }
    }
}

             