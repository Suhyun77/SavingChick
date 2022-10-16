using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SH_PlayerAttack : MonoBehaviour
{
    public static SH_PlayerAttack instance;
    private void Awake()
    {
        instance = this;
    }

    public Animator anim;
    public bool isAttack;
    public bool isAttack2;
    public bool isSkill;
    public bool isDelay;


    public Image skillFilter; //��Ÿ�� �� ��ų �̹��� ���� 
    public Image skill2Filter; //��Ÿ�� �� ��ų �̹��� ���� 
    public TextMeshProUGUI coolTimeText; //���� ��Ÿ�� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI coolTimeText2; //���� ��ų2 ��Ÿ�� ǥ���� �ؽ�Ʈ
    public GameObject coolTimeTextObject; //��Ÿ�� ����� �ؽ�Ʈ ������ �ϱ�����
    public GameObject coolTimeTextObject2; //��Ÿ�� ��ų 2 ����� �ؽ�Ʈ ������ �ϱ�����
    public float coolTime = 5;
    public float coolTime2 = 10;
    float currentCoolTime; //���� ��Ÿ�� ���� ����
    float currentCoolTime2; //���� ��ų 2 ��Ÿ�� ���� ����
    bool canUseSkill = true; //��ų1�� ����� �� �ִ��� Ȯ���ϴ� ����
    bool canUseSkill2 = true; //��ų2�� ����� �� �ִ��� Ȯ���ϴ� ����
    public AudioSource swordSwing;  // Sword Swing Sound
    SH_Weapon weapon;



    void Start()
    {
        SSH_UIManager.Instance.mpLackText.SetActive(false);
        anim = GetComponent<Animator>();
        isAttack = false;
        isSkill = false;
        isDelay = false;
        skillFilter.fillAmount = 0; //ó���� ��ų ��ư�� ������ ����
        skill2Filter.fillAmount = 0; //ó���� ��ų ��ư�� ������ ����
        coolTimeTextObject.SetActive(false);
        coolTimeTextObject2.SetActive(false);
        weapon = GetComponentInChildren<SH_Weapon>();
    }

    void Update()
    {
        // Skill1
        if (Input.GetKey(KeyCode.K) && canUseSkill == true)
        {
            if (SH_PlayerMP.instance.MP > 0)
            {
                coolTimeTextObject.SetActive(true);
                isSkill = true;
                anim.SetTrigger("Skill");

                // �÷��̾� MP ���̱�
                SH_PlayerMP.instance.UseSkill(10);
                //StartCoroutine("AttackDelay");
                CoolTimeUseSkill();
            }
            else
            {
                Debug.Log("MP�� �����մϴ�");
                StartCoroutine("MpLack");
            }
        }
        //��Ÿ�ӽ� ��ų �Ұ��� UI ���� //MP�� 0�̰� ��Ÿ���� ������ �� : ���� ��ų�� ����� �� �����ϴٷ� �߰� �Ѵ�
        if (Input.GetKey(KeyCode.K) && currentCoolTime < 4.9f )
        {
            if(SH_PlayerMP.instance.MP <=0)
            {
                StartCoroutine("MpLack");
            }
            else
            {
                StartCoroutine("SKillLack");
            }
            
        }

            // Attack : Attack�ִϸ����Ͱ� ������� ���� ���� Attack �Է��� �ǰ� �ϱ� (Attack animation �ߺ� ��� ����)
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                isSkill = false;
                isAttack = true;
                anim.SetTrigger("Attack");
                // ������ enemy���� ���� �ʾ��� ���� ����ǰ� �ϱ�
                if (weapon.isColliding == false && SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
                {
                    swordSwing.PlayDelayed(0.25f);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetBool("IsRun", true);
            anim.SetFloat("AttackSpeed", 2);  // ó�� �ӵ� ����
            isAttack = false;
            isSkill = false;

        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetFloat("AttackSpeed", anim.GetFloat("AttackSpeed") - 0.02f);
        }

        // Skill2
        if (Input.GetKeyDown(KeyCode.L) && canUseSkill2 == true)
        {
            
            if (SH_PlayerMP.instance.MP - 20 >= 0)
            {
                coolTimeTextObject2.SetActive(true);
                isSkill = true;
                anim.SetTrigger("Skill2");
                skill2SoundEvent();

                // �÷��̾� MP ���̱�
                SH_PlayerMP.instance.UseSkill(20);
                //StartCoroutine("AttackDelay");
                CoolTimeUseSkill2();
            }
            else
            {
                Debug.Log("MP�� �����մϴ�");
                StartCoroutine("MpLack");
            }          
        }
        if (Input.GetKey(KeyCode.L) && currentCoolTime2 < 9.9f)
        {
            if (SH_PlayerMP.instance.MP - 20 < 0)
            {
                StartCoroutine("MpLack");
            }
            else
            {
                StartCoroutine("SKillLack");
            }

        }
    }

    //MP���� UI���� �ڷ�ƾ 
    IEnumerator MpLack()
    {
        SSH_UIManager.Instance.mpLackText.SetActive(true);
        SSH_UIManager.Instance.mpLackTextUI.text = "MP�� �����մϴ�";
        yield return new WaitForSeconds(0.5f);
        SSH_UIManager.Instance.mpLackText.SetActive(false);
    }

    IEnumerator SKillLack()
    {
        SSH_UIManager.Instance.mpLackText.SetActive(true);
        SSH_UIManager.Instance.mpLackTextUI.text = "���� ��ų�� ����� �� �����ϴ�";
        yield return new WaitForSeconds(0.5f);
        SSH_UIManager.Instance.mpLackText.SetActive(false);
    }

    // Skill2 �ִϸ��̼� �̺�Ʈ
    public ParticleSystem lr;
    public Transform laserPos;
    //float attackTime = 3f;
    //float currentTime;
    public void skill2Event()
    {
        lr.transform.position = laserPos.position;
        lr.transform.forward = transform.forward;
        lr.Play();
    }
    public AudioSource skill2Sound; //������ ����
    public void skill2SoundEvent()
    {
        skill2Sound.Play();
    }

    public void CoolTimeUseSkill()
    {
        if (canUseSkill)
        {
            Debug.Log("Use SKill");
            skillFilter.fillAmount = 1; // ��ų ��ư�� ����
            StartCoroutine("CoolTime");
            currentCoolTime = coolTime;
            coolTimeText.text = "" + currentCoolTime;
            StartCoroutine("CoolTimeCounter");
            canUseSkill = false; //��ų�� ����ϸ� ����� �� ���� ���·� �ٲ۴�.
        }
        else
        {
            Debug.Log("���� ��ų�� ����� �� �����ϴ�.");
        }
    }

    public void CoolTimeUseSkill2()
    {
        if (canUseSkill2)
        {
            Debug.Log("Use SKill2");
            skill2Filter.fillAmount = 1; // ��ų ��ư�� ����
            StartCoroutine("CoolTime2");
            currentCoolTime2 = coolTime2;
            coolTimeText2.text = ""+ coolTime2;
            StartCoroutine("CoolTimeCounter2");
            canUseSkill2 = false; //��ų�� ����ϸ� ����� �� ���� ���·� �ٲ۴�.
        }
        else
        {
            Debug.Log("���� ��ų2�� ����� �� �����ϴ�.");
        }
    }

    //CoolTime ���� ������� �ڷ�ƾ
    IEnumerator CoolTime()
    {
        while (skillFilter.fillAmount > 0)
        {
            skillFilter.fillAmount -= Time.smoothDeltaTime / coolTime;
            yield return null;
        }
        canUseSkill = true; //��ų ��Ÿ���� ������ ��ų�� ����� �� �ִ� ���·� �ٲ�
        coolTimeTextObject.SetActive(false);

        yield break;
    }

    IEnumerator CoolTime2()
    {
        while (skill2Filter.fillAmount > 0)
        {
            skill2Filter.fillAmount -= Time.smoothDeltaTime / coolTime2;
            yield return null;
        }
        canUseSkill2 = true; //��ų ��Ÿ���� ������ ��ų�� ����� �� �ִ� ���·� �ٲ�
        coolTimeTextObject2.SetActive(false);

        yield break;
    }

    //���� ��Ÿ���� ����� �ڷ�ƾ
    IEnumerator CoolTimeCounter()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentCoolTime -= 1.0f;
            coolTimeText.text = "" + currentCoolTime;
        }
        yield break;
    }

    IEnumerator CoolTimeCounter2()
    {
        while (currentCoolTime2 > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentCoolTime2 -= 1.0f;
            coolTimeText2.text = "" + currentCoolTime2;
        }
        yield break;
    }
}
