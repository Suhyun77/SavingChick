using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

//�ٰŸ� ���� ��  
// �÷��̾���� �Ÿ��� �����Ÿ����� �۾����� �÷��̾ ����ٴѴ�. -> NavMeshAgent�� �̿��ϱ�
// �����ð��� �Ǹ� ��ų 1,2�� �����Ѵ�
// ��ų 1: �ڽ� �ֺ����� ���׶� ��ü �յ� ���ٴϰ� �ϱ�
// ��ų 2 :���ڱ� �÷��̾� ������ �� �ͼ� �ٰŸ� ����
// �ʿ�Ӽ� : ���¸ӽ�(����, �̵�, ����), �÷��̾�, NavMeshAgent, PlayerAttribute, EnemyAttribute;
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
    //�Ѿ�
    public GameObject enemy2BulletAll;
    SSH_CharacterAttribute playerAttribute;
    SSH_CharacterAttribute enemy2Attribute;
    //��ų 2 ��ƼŬ �ý���
    public ParticleSystem skill2;

    //�����̴�
    public Slider enemySlider;
    //�����̴� ���ӿ�����Ʈ��
    public GameObject genemySlider;
    //�����̴� ���� ������
    Vector3 startScale;
    //�����̴� �ѱ� �Ÿ�
    public float sliderDistance = 20;

    // Start is called before the first frame update
    //enemy2 ��� ����
    public GameObject enemy2Slot;
    //������ ����
    public GameObject item2Factory;
    Material mat;
    // player attack Sound
    public AudioSource attackSound;
    // player skill sound
    public AudioSource skillSound;
    // Enemy Sound
    public AudioSource enemy2Sound;


    //���ݽ� ��´�
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

        //������ Idle
        State = state.Idle;

        //ó�� ���� HP����
        enemySlider.maxValue = maxHP;
        HP = maxHP;

        //slider�� �������� ���Ѵ�
        startScale = genemySlider.transform.localScale;
        // ó������ Slider�� ������ �ʾҴٰ�
        genemySlider.SetActive(false);
        //��Ƽ���� �Ҵ�
        SkinnedMeshRenderer mr = GetComponentInChildren<SkinnedMeshRenderer>();
        mat = mr.material;
        //���� �ڱ� ��ġ
        originPos = transform.position;

    }
    
    // Update is called once per frame
    // Y���� ������Ű�� �ʹ�
    
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


        //�� HP ���󰡱�
        enemySlider.transform.position = transform.position + new Vector3(0, 1, 0); //�� HP ���󰡱�

        //ī�޶���� �����Ÿ��� �Ǹ� slider�� ���̰� �����
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (distance < sliderDistance)
        {
            //slider�� ���δ�
            genemySlider.SetActive(true);
            //�÷��̾���� �Ÿ��� �������� �����̴��� ũ�Ⱑ �޶�����
            //�Ÿ��� Ŀ���� scale�� �۾������Ѵ�
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
        //Idle���¿��� ���� ���������� ���ƴٴ� �� �ְ��ϵ��� �ϴ� ������
        originDis = (originPos - transform.position).magnitude;
    }



    // �����ִٰ� �����Ÿ� ������ ������ Move���·� ��ȯ�Ѵ�
    Vector3 originPos;
    Vector3 target;
    float originDis;
    float targetDis;
    public float moveRange = 8;
    //�ʿ�Ӽ� : �����Ÿ�
    public float findDistance = 5;
    private void UpdateIdle()

    {
        // �����ִ� ���¿����� ������� �����̰� �ʹ�
        anim.SetTrigger("Move");
        target = new Vector3(transform.position.x + UnityEngine.Random.Range(-1 * moveRange, moveRange), 0, transform.position.z + UnityEngine.Random.Range(-1 * moveRange, moveRange));
        agent.SetDestination(target);
        if (originDis >= moveRange)
        {
            target = originPos;
            agent.SetDestination(target);
        }

        //�÷��̾�� ������ �Ÿ�
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < findDistance)
        {   State = state.Move;
            anim.SetTrigger("Move");
        }
    }

    //�÷��̾ ����ٴϴٰ� ���� �ð��� �Ǹ� Attack ���·� ��ȯ�Ѵ�.
    //�ʿ�Ӽ�: ����ð�, ���ݽð�
    public float traceTime = 5;
    float currentTime;
    private void UpdateMove()
    {
        //�÷��̾� �ٶ󺸰� �����
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(lookAt);

        agent.destination = player.transform.position;
        //�Ÿ��� ���ؼ� �÷��̾���� �Ÿ��� 2�� �Ǹ� ���͸� �����.
        float distance = Vector3.Distance(player.transform.position, transform.position);
        currentTime += Time.deltaTime;

        //����ð��� tracetime�� �Ѿ��� �� attack���� �ٲ۴�(�ѹ��� Ȯ �ٰ��� �� ����, + ��ų1 �ѱ�) -> originTime�� �Ǹ� �ٽ� ���ǵ� �ٲٰ� �ð� �ʱ�ȭ�ϰ� Move�� ������.  
        //���� �׶� �÷��̾���� �Ÿ��� ���ߴ� �Ÿ����� �ֶ�
        //enemy2�� �ӵ��� 20���� �÷��� player�� ���󰣴�
        //�׷��� �ʴٸ�(���ߴ� �Ÿ��� �ƴٸ�) enemy2�� �����
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

    //��ų 1 ��ų 2�� �����ð� �������� ������ �����Ѵ�.
    //��ų 1������ �� ���ư��� ��ų ����
    //��ų 2������ �÷��̾� ������ �� �ٰ����� �����ϴ� ��ų�� �����Ѵ�
    //�ʿ�Ӽ� : ����ð�, ��ų2 ��ȯ�ð�
    private void UpdateAttack()
    {
        //�÷��̾� �ٶ󺸰� �����
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(lookAt);

        //�ð��� �帥��
        currentTime += Time.deltaTime;
        //��ų 1 ����
        enemy2BulletAll.SetActive(true);
        float distance = Vector3.Distance(player.transform.position, transform.position);
        //��ų2 : player���� Ȯ �ٰ����� ���� �Ÿ��� �Ǹ� ���߰� �����Ѵ�
        agent.speed = 40;
        if (distance > playerAttribute.radius + enemy2Attribute.radius)
        {
            agent.destination = player.transform.position + new Vector3(1, 0, 1);
        }
        else
        {
            //�������
            agent.isStopped = true;
            agent.speed = 0;
            
        }

        //���� ����ð��� traceTime +3 �� �ƴٸ�
        //Move ���·� �ٲٰ�, anim���µ� Move�� �Ѵ�
        //agent�� �ӵ��� 3.5�� �ٲٰ�,
        //����ð� �ʱ�ȭ �Ѵ�
        if (currentTime > traceTime + 3)
        {
            //��ų 1�� ����
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
    //���� ������ �� ������ ���ϰ� �ʹ�
    IEnumerator IeRedChange()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mat.color = Color.white;
    }

    public GameObject damageTextFactory; //�������ؽ�Ʈ

    //�÷��̾�� ���� ������ �� HP �پ��� �Լ�
    public void AddDamage(int damage)
    {
        //HP�� 0���� ũ�ٸ� HP�� ���δ�
        if (HP > 0)
        {
            //������ �ؽ�Ʈ ����
            GameObject damageText = Instantiate(damageTextFactory, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            damageText.GetComponent<TextMeshProUGUI>().text = damage.ToString();
            //������ �ؽ�Ʈ ���ֱ�
            Destroy(damageText, 0.5f);
            //HP -= damage;
            SetHP(damage);
            StartCoroutine("IeAttacked");
            StartCoroutine("IeRedChange");
            enemy2Sound.Play();


            //���ҽ�Ų ������ HP�� 0���� �۴ٸ� �״´�
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
        //skill1 ����
        enemy2BulletAll.SetActive(false);

        //������ ������ �ð� ���߱�
        Invoke("ItemDrop", 2);
        Destroy(gameObject, 2f);
        genemySlider.SetActive(false);
        //��� ������ ����
        SSH_UIManager.Instance.enemy2slotBlack.SetActive(false);
    }

    public void ItemDrop()
    {
        GameObject item2 = Instantiate(item2Factory);
        item2.transform.position = transform.position;
    }

    //���� ������ �� �и��� �ʹ�, �� �� Ȯ �и��� ��ĥ ����
    //Lerp ��� ���ҽ� ó���� �ӵ��� �ø��� ���� ���δ� 0���δ� ���߰�
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
                //rb.AddForce(new Vector3(0,0,-1) * force, ForceMode.Impulse); rb velocity�� �ٿ������ 
                yield return null;
            }

            else
            {
                anim.SetTrigger("Move");
                yield break;
            }
        }
    }

    // ���� ȿ�� �߻� ����
    Transform attackPosition;
    // ��ų ȿ�� �߻� ����
    Transform skillPosition;
    public GameObject attackEffectFactory;  // ���� ȿ��
    public GameObject skillEffectFactory;  // ��ų ȿ��
    //�÷��̾�� ��´ٸ� enemy2�� �� Ray�� Normal �������� ��ƼŬ �ý����� �����ٳ��´�
    private void OnTriggerEnter(Collider other)
    {
        SH_PlayerHP player = other.gameObject.GetComponent<SH_PlayerHP>();
        // �ڱ� �ڽ� ���� ��ġ 
        attackPosition = transform.Find("AttackPosition2");
        // �ڱ� �ڽ� ��ų ��ġ 
        skillPosition = transform.Find("SkillPosition2");
        if (player)
        {
            skill2.transform.position = player.transform.position + new Vector3(0, 0, 1.0f);
            skill2.Stop();
            skill2.Play();
            if(hp>0)
            {
                //��´ٸ� Player�� ü���� ���ҽ�Ų��
                player.AddDamage(1);
            }
            
        }
        // Attack : Enemy �տ� ���� ȿ�� �����
        if (other.gameObject.name == "Weapon" && SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true)
        {
            Vector3 pos = attackPosition.position;
            GameObject explosion = Instantiate(attackEffectFactory);
            explosion.transform.position = pos;
            attackSound.Play();
        }
        // Skill : Enemy �տ� ��ų ȿ�� �����
        if (other.gameObject.name == "Weapon" && SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Skill") == true)
        {
            Vector3 pos = skillPosition.position;
            GameObject explosion = Instantiate(skillEffectFactory);
            explosion.transform.position = pos;
            skillSound.Play();
        }
    }
}

