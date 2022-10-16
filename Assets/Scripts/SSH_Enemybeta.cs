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
    //���� : ����, �̵�, ����, ���ݴ���, ����

    public float speed = 7;
    public float createTIme = 2;
    //��ų����Ʈ
    public GameObject skillEffect;
    public ParticleSystem skillEffectPar;
    NavMeshAgent agent;
    //�Ѿ˰���
    public GameObject bulletFactory;
    public GameObject firePosition;
    //���¹�
    public Slider enemySlider;
    //ü�¹� ���� ������Ʈ
    public GameObject genemySlider;
    //ü�¹� ���� ũ��
    Vector3 startScale;
    //ü�¹� ���̰� �ϴ� �Ÿ�
    public float sliderDistance = 20;
    public Animator anim;
    //�÷��̾�
    public GameObject player;
    //����ð�
    float currentTIme;
    //������ �ٵ�
    Rigidbody rb;
    //��Ƽ����
    Material mat;
    // enemy �Ҹ�
    public AudioSource enemySound;
    //���¸ӽ�
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

    


    //���ݽ� HP�� ����ϰ� �ʹ�

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
        //ó�� ���� HP ������
        enemySlider.maxValue = maxHP;
        HP = maxHP;
        State = state.Idle;
        player = GameObject.Find("Player");
        //agent ����
        agent = GetComponent<NavMeshAgent>();
        
        //��ų����Ʈ�� ��ƼŬ �ý��� ������Ʈ�� �����´�
        skillEffectPar = skillEffect.GetComponent<ParticleSystem>();
        //ü�¹��� ũ�� ���ϱ�
        startScale = genemySlider.transform.localScale;
        //ü�¹ٴ� ó���� ������ �ʰ��Ѵ�
        genemySlider.SetActive(false);
        //������ �ٵ� �Ҵ�
        rb = GetComponent<Rigidbody>();
        SkinnedMeshRenderer mr = GetComponentInChildren<SkinnedMeshRenderer>();
        mat = mr.material;
        //���� �ڱ� ��ġ
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
        {    //�����̴��� ��ġ
            enemySlider.transform.position = transform.position + new Vector3(0, 1, 0); //�� HP ���󰡱�

           

            //ī�޶���� �����Ÿ��� �Ǹ� slider�� ���̰� �����
            float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
            if (distance < sliderDistance)
            {
                //slider�� ���δ�
                genemySlider.SetActive(true);
                //�÷��̾���� �Ÿ��� �������� �����̴��� ũ�Ⱑ �޶�����
                //�Ÿ��� Ŀ���� scale�� �۾������Ѵ�
                Vector3 newScale = startScale * sliderDistance / (distance * 3);
                genemySlider.transform.localScale = newScale;
                genemySlider.transform.rotation = Camera.main.transform.rotation;

            }
            else if (distance >= sliderDistance)
            { genemySlider.SetActive(false); }

        }

        //Idle���¿��� ���� ���������� ���ƴٴ� �� �ְ��ϵ��� �ϴ� ������
        originDis = (originPos - transform.position).magnitude;
    }

    Vector3 originPos;
    Vector3 target;
    float originDis;
    float targetDis;
    public float moveRange = 2;
    //���� �Ÿ�
    public float findDistance = 10;
    private void UpdateIdle()
    {
        //�����ִ� ���¿����� ������� �����̰� �ʹ�
        anim.SetTrigger("Move");
        target = new Vector3(transform.position.x + UnityEngine.Random.Range(-1 * moveRange, moveRange), 0, transform.position.z + UnityEngine.Random.Range(-1 * moveRange, moveRange));
        agent.SetDestination(target);
        if (originDis >= moveRange)
        {
            target = originPos;
            agent.SetDestination(target);
        }
        //�÷��̾���� �Ÿ�
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < findDistance)
        {
            State = state.Move;
        }
    }

    public float attackDistance = 3;
    private void UpdateMove()
    {

        //�÷��̾� �ٶ󺸰� �����
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(lookAt);

        //�÷��̾�� ��ġ�� �ʰ� ��ǥ������ �����Ѵ� 
        agent.destination = player.transform.position - new Vector3(0,0,-2);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < attackDistance)
        {
            anim.SetTrigger("Attack");
        }
        //�����Ÿ����� �Ÿ��� Ŀ���ٸ� Idle�� �ٲ۴�
        if(distance > findDistance)
        {
            State = state.Idle;
            anim.SetTrigger("Idle");
        }
    }

    //�ش� �κп��� skill1 : �Ѿ� �߻� + 3�� ��ٷ��� Skill2 : ü�� ȸ��
    
    public void OnEventAttack()
    {
        //�÷��̾� �ٶ󺸰� �����
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(lookAt);
        //������ ���缭 �Ѵ�
        agent.isStopped = true;
        //Skill1 :�Ѿ� ���忡�� �Ѿ� �߻�
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.position = firePosition.transform.position;
        bullet.transform.forward = firePosition.transform.forward;

        StartCoroutine("IeSkill");
        State = state.Move;
        anim.SetTrigger("Move");
        agent.isStopped = false;
    }

    //Skill2 : ü��ȸ��
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

    //������ �ؽ�Ʈ ����
    public GameObject damageTextFactory; 
    
    public void AddDamage(int damage)
    {
        //HP�� 0���� ũ�ٸ�
        if(HP>0)
        {

            //anim.SetTrigger("GetHit");
            //������ �ؽ�Ʈ ����
            GameObject damageText = Instantiate(damageTextFactory, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            damageText.GetComponent<TextMeshProUGUI>().text = damage.ToString();
            Destroy(damageText, 0.5f);
            //HP�� damage��ŭ ���δ�
            HP -= damage;
            enemySound.Play();
            StartCoroutine("IeAttacked");
            StartCoroutine("IeBlackChange");


            //���ҽ�Ų ������ HP�� 0���� �۴ٸ� �״´�
            if (HP <= 0)
            {
                //2�� �ڿ� �ױ� + ü�¹� ���ֱ�
                anim.SetTrigger("Die");
                //������ ������ �ð� ���߱�
                Invoke("Item1Drop", 2);
                Destroy(gameObject, 2f);
                genemySlider.SetActive(false);
                //�и��� �ʰ��ϱ�
                agent.speed = 0;
                agent.isStopped = true;                
                //��齽�� ����
                SSH_UIManager.Instance.enemy1slotblack.SetActive(false);
                SSH_UIManager.Instance.quest.GetComponent<SSH_Quest>().enemy1Count++;
            }

        }
        


    }

    //���� ������ �� ��İ� ���ϰ� �ʹ�
    IEnumerator IeBlackChange()
    {
        mat.color = Color.black;
        yield return new WaitForSeconds(0.5f);
        mat.color = Color.white;
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

    //������ ����
    public GameObject itemFactory;
    //������ ���
    public void Item1Drop()
    {
        GameObject item1 = Instantiate(itemFactory);
        item1.transform.position = transform.position;
    }

    //private void UpdateDie()
    //{

    //}

    // ���� ȿ�� �߻� ����
    Transform attackPosition;
    // ��ų ȿ�� �߻� ����
    Transform skillPosition;
    public GameObject attackEffectFactory;  // ���� ȿ��
    public GameObject skillEffectFactory;  // ��ų ȿ��
    private void OnTriggerEnter(Collider other)
    {
        // Player : �ε����� Damage �ֱ�
        SH_PlayerHP player = other.gameObject.GetComponent<SH_PlayerHP>();
        // �ڱ� �ڽ� ���� ��ġ ������Ʈ
        attackPosition = transform.Find("AttackPosition");
        // �ڱ� �ڽ� ��ų ��ġ ������Ʈ
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

        // Attack : Enemy �տ� ���� ȿ�� �����
        if (other.gameObject.name == "Weapon" && SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true)
        {
            // getcomponent�ؼ� ������� enemy�� attackPosition
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

             