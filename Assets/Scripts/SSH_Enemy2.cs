using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

//근거리 공격 적  
// 플레이어와의 거리가 감지거리보다 작아지면 플레이어를 따라다닌다. -> NavMeshAgent를 이용하기
// 일정시간이 되면 스킬 1,2를 발현한다
// 스킬 1: 자신 주변에서 동그란 물체 둥둥 떠다니게 하기
// 스킬 2 :갑자기 플레이어 앞으로 훅 와서 근거리 공격
// 필요속성 : 상태머신(정지, 이동, 공격), 플레이어, NavMeshAgent, PlayerAttribute, EnemyAttribute;
public class SSH_Enemy2 : MonoBehaviour
{
    public enum state
    {
        Idle,
        Move,
        Attack,
        Die
    }


    public state State;
    public GameObject player;
    NavMeshAgent agent;
    public Animator anim;
    //총알
    public GameObject enemy2BulletAll;
    SSH_CharacterAttribute playerAttribute;
    SSH_CharacterAttribute enemy2Attribute;
    //스킬 2 파티클 시스템
    public ParticleSystem skill2;

    //슬라이더
    public Slider enemySlider;
    //슬라이더 게임오브젝트로
    public GameObject genemySlider;
    //슬라이더 시작 스케일
    Vector3 startScale;
    //슬라이더 켜기 거리
    public float sliderDistance = 20;

    // Start is called before the first frame update
    //enemy2 흑백 슬롯
    public GameObject enemy2Slot;
    //아이템 공장
    public GameObject item2Factory;
    Material mat;
    // player attack Sound
    public AudioSource attackSound;
    // player skill sound
    public AudioSource skillSound;
    // Enemy Sound
    public AudioSource enemy2Sound;


    //공격시 닳는다
    int hp;
    public int maxHP = 2;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            enemySlider.value = value;

        }
    }

    public void SetHP(int damage)
    {
        hp -= damage;
        enemySlider.value = hp;
    }

    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        playerAttribute = player.GetComponent<SSH_CharacterAttribute>();
        enemy2Attribute = gameObject.GetComponent<SSH_CharacterAttribute>();
        skill2 = SSH_UIManager.Instance.skill2;

        //시작은 Idle
        State = state.Idle;

        //처음 몬스터 HP설정
        enemySlider.maxValue = maxHP;
        HP = maxHP;

        //slider의 스케일을 정한다
        startScale = genemySlider.transform.localScale;
        // 처음에는 Slider가 보이지 않았다가
        genemySlider.SetActive(false);
        //머티리얼 할당
        SkinnedMeshRenderer mr = GetComponentInChildren<SkinnedMeshRenderer>();
        mat = mr.material;
        //원래 자기 위치
        originPos = transform.position;

    }
    
    // Update is called once per frame
    // Y값을 고정시키고 싶다
    
    void Update()
    {
        if (State == state.Idle)
        { UpdateIdle(); }
        if (State == state.Move)
        { UpdateMove(); }
        if (State == state.Attack)
        { UpdateAttack(); }
        if(State == state.Die)
        { UpdateDie(); }


        //적 HP 따라가기
        enemySlider.transform.position = transform.position + new Vector3(0, 1, 0); //적 HP 따라가기

        //카메라와의 일정거리가 되면 slider을 보이게 만든다
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (distance < sliderDistance)
        {
            //slider가 보인다
            genemySlider.SetActive(true);
            //플레이어와의 거리를 기준으로 슬라이더의 크기가 달라진다
            //거리가 커지면 scale은 작아져야한다
            Vector3 newScale = startScale * sliderDistance / (distance * 2);
            genemySlider.transform.localScale = newScale;
            genemySlider.transform.rotation = Camera.main.transform.rotation;
            if(HP<=0)
            {
                UpdateDie();
            }

        }
        else if (distance >= sliderDistance)
        { 
            genemySlider.SetActive(false);
         }
        //Idle상태에서 일정 범위내에서 돌아다닐 수 있게하도록 하는 변수들
        originDis = (originPos - transform.position).magnitude;
    }



    // 멈춰있다가 감지거리 안으로 들어오면 Move상태로 전환한다
    Vector3 originPos;
    Vector3 target;
    float originDis;
    float targetDis;
    public float moveRange = 8;
    //필요속성 : 감지거리
    public float findDistance = 5;
    private void UpdateIdle()

    {
        // 멈춰있는 상태에서도 어느정도 움직이고 싶다
        anim.SetTrigger("Move");
        target = new Vector3(transform.position.x + UnityEngine.Random.Range(-1 * moveRange, moveRange), 0, transform.position.z + UnityEngine.Random.Range(-1 * moveRange, moveRange));
        agent.SetDestination(target);
        if (originDis >= moveRange)
        {
            target = originPos;
            agent.SetDestination(target);
        }

        //플레이어와 나와의 거리
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < findDistance)
        {   State = state.Move;
            anim.SetTrigger("Move");
        }
    }

    //플레이어를 따라다니다가 일정 시간이 되면 Attack 상태로 전환한다.
    //필요속성: 현재시간, 공격시간
    public float traceTime = 5;
    float currentTime;
    private void UpdateMove()
    {
        //플레이어 바라보게 만들기
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(lookAt);

        agent.destination = player.transform.position;
        //거리를 구해서 플레이어와의 거리가 2가 되면 몬스터를 멈춘다.
        float distance = Vector3.Distance(player.transform.position, transform.position);
        currentTime += Time.deltaTime;

        //현재시간이 tracetime을 넘었을 때 attack으로 바꾼다(한번에 확 다가간 후 공격, + 스킬1 켜기) -> originTime이 되면 다시 스피드 바꾸고 시간 초기화하고 Move로 돌린다.  
        //만약 그때 플레이어와의 거리가 멈추는 거리보다 멀때
        //enemy2의 속도를 20까지 올려서 player를 따라간다
        //그렇지 않다면(멈추는 거리가 됐다면) enemy2를 멈춘다
        if (currentTime > traceTime)
        {
            State = state.Attack;
            anim.SetTrigger("Attack");
            
        }

        if (distance <= playerAttribute.radius + enemy2Attribute.radius)
           {
              agent.destination = player.transform.position;
              agent.isStopped = true;
              agent.velocity = Vector3.zero;
              State = state.Attack;
              anim.SetTrigger("Attack");
           }
        if(distance > findDistance)
        {
            State = state.Idle;
            anim.SetTrigger("Idle");
            agent.isStopped = true;
        }
        

    }

    //스킬 1 스킬 2를 일정시간 간격으로 발현해 공격한다.
    //스킬 1에서는 구 돌아가는 스킬 구현
    //스킬 2에서는 플레이어 앞으로 훅 다가가서 공격하는 스킬을 구현한다
    //필요속성 : 현재시간, 스킬2 변환시간
    private void UpdateAttack()
    {
        //플레이어 바라보게 만들기
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(lookAt);

        //시간이 흐른다
        currentTime += Time.deltaTime;
        //스킬 1 발현
        enemy2BulletAll.SetActive(true);
        float distance = Vector3.Distance(player.transform.position, transform.position);
        //스킬2 : player에게 확 다가가서 일정 거리가 되면 멈추고 공격한다
        agent.speed = 40;
        if (distance > playerAttribute.radius + enemy2Attribute.radius)
        {
            agent.destination = player.transform.position + new Vector3(1, 0, 1);
        }
        else
        {
            //멈춘상태
            agent.isStopped = true;
            agent.speed = 0;
            
        }

        //만약 현재시간이 traceTime +3 이 됐다면
        //Move 상태로 바꾸고, anim상태도 Move로 한다
        //agent의 속도를 3.5로 바꾸고,
        //현재시간 초기화 한다
        if (currentTime > traceTime + 3)
        {
            //스킬 1을 끈다
            enemy2BulletAll.SetActive(false);
            State = state.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false;
            agent.speed = 3.5f;
            currentTime = 0;

        }
        if(distance>findDistance)
        {
            State = state.Idle;
        }

    }
    //공격 당했을 때 빨갛게 변하고 싶다
    IEnumerator IeRedChange()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mat.color = Color.white;
    }

    public GameObject damageTextFactory; //데미지텍스트

    //플레이어에게 공격 당했을 때 HP 줄어드는 함수
    public void AddDamage(int damage)
    {
        //HP가 0보다 크다면 HP를 줄인다
        if (HP > 0)
        {
            //데미지 텍스트 생성
            GameObject damageText = Instantiate(damageTextFactory, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            damageText.GetComponent<TextMeshProUGUI>().text = damage.ToString();
            //데미지 텍스트 없애기
            Destroy(damageText, 0.5f);
            //HP -= damage;
            SetHP(damage);
            StartCoroutine("IeAttacked");
            StartCoroutine("IeRedChange");
            enemy2Sound.Play();


            //감소시킨 순간에 HP가 0보다 작다면 죽는다
            if (HP <= 0)
            {
                State = state.Die;
                anim.SetTrigger("Die");
                SSH_UIManager.Instance.quest.GetComponent<SSH_Quest>().enemy2Count++;
            }
        }
    }

    private void UpdateDie()
    {
        agent.speed = 0;
        //skill1 끈다
        enemy2BulletAll.SetActive(false);

        //아이템 나오는 시간 늦추기
        Invoke("ItemDrop", 2);
        Destroy(gameObject, 2f);
        genemySlider.SetActive(false);
        //흑백 슬롯을 끈다
        SSH_UIManager.Instance.enemy2slotBlack.SetActive(false);
    }

    public void ItemDrop()
    {
        GameObject item2 = Instantiate(item2Factory);
        item2.transform.position = transform.position;
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

    // 공격 효과 발생 지점
    Transform attackPosition;
    // 스킬 효과 발생 지점
    Transform skillPosition;
    public GameObject attackEffectFactory;  // 공격 효과
    public GameObject skillEffectFactory;  // 스킬 효과
    //플레이어와 닿는다면 enemy2가 쏜 Ray의 Normal 방향으로 파티클 시스템을 가져다놓는다
    private void OnTriggerEnter(Collider other)
    {
        SH_PlayerHP player = other.gameObject.GetComponent<SH_PlayerHP>();
        // 자기 자식 공격 위치 
        attackPosition = transform.Find("AttackPosition2");
        // 자기 자식 스킬 위치 
        skillPosition = transform.Find("SkillPosition2");
        if (player)
        {
            skill2.transform.position = player.transform.position + new Vector3(0, 0, 1.0f);
            skill2.Stop();
            skill2.Play();
            if(hp>0)
            {
                //닿는다면 Player의 체력을 감소시킨다
                player.AddDamage(1);
            }
            
        }
        // Attack : Enemy 앞에 공격 효과 만들기
        if (other.gameObject.name == "Weapon" && SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true)
        {
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

