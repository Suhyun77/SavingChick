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


    public Image skillFilter; //쿨타임 시 스킬 이미지 필터 
    public Image skill2Filter; //쿨타임 시 스킬 이미지 필터 
    public TextMeshProUGUI coolTimeText; //남은 쿨타임 표시할 텍스트
    public TextMeshProUGUI coolTimeText2; //남은 스킬2 쿨타임 표시할 텍스트
    public GameObject coolTimeTextObject; //쿨타임 실행시 텍스트 켜지게 하기위함
    public GameObject coolTimeTextObject2; //쿨타임 스킬 2 실행시 텍스트 켜지게 하기위함
    public float coolTime = 5;
    public float coolTime2 = 10;
    float currentCoolTime; //남은 쿨타임 추적 변수
    float currentCoolTime2; //남은 스킬 2 쿨타임 추적 변수
    bool canUseSkill = true; //스킬1을 사용할 수 있는지 확인하는 변수
    bool canUseSkill2 = true; //스킬2을 사용할 수 있는지 확인하는 변수
    public AudioSource swordSwing;  // Sword Swing Sound
    SH_Weapon weapon;



    void Start()
    {
        SSH_UIManager.Instance.mpLackText.SetActive(false);
        anim = GetComponent<Animator>();
        isAttack = false;
        isSkill = false;
        isDelay = false;
        skillFilter.fillAmount = 0; //처음엔 스킬 버튼을 가리지 않음
        skill2Filter.fillAmount = 0; //처음엔 스킬 버튼을 가리지 않음
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

                // 플레이어 MP 줄이기
                SH_PlayerMP.instance.UseSkill(10);
                //StartCoroutine("AttackDelay");
                CoolTimeUseSkill();
            }
            else
            {
                Debug.Log("MP가 부족합니다");
                StartCoroutine("MpLack");
            }
        }
        //쿨타임시 스킬 불가능 UI 띄우기 //MP가 0이고 쿨타임이 남았을 때 : 아직 스킬을 사용할 수 없습니다로 뜨게 한다
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

            // Attack : Attack애니메이터가 재생되지 않을 때만 Attack 입력이 되게 하기 (Attack animation 중복 재생 방지)
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                isSkill = false;
                isAttack = true;
                anim.SetTrigger("Attack");
                // 공격이 enemy에게 닿지 않았을 때만 재생되게 하기
                if (weapon.isColliding == false && SH_PlayerAttack.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
                {
                    swordSwing.PlayDelayed(0.25f);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetBool("IsRun", true);
            anim.SetFloat("AttackSpeed", 2);  // 처음 속도 설정
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

                // 플레이어 MP 줄이기
                SH_PlayerMP.instance.UseSkill(20);
                //StartCoroutine("AttackDelay");
                CoolTimeUseSkill2();
            }
            else
            {
                Debug.Log("MP가 부족합니다");
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

    //MP부족 UI띄우는 코루틴 
    IEnumerator MpLack()
    {
        SSH_UIManager.Instance.mpLackText.SetActive(true);
        SSH_UIManager.Instance.mpLackTextUI.text = "MP가 부족합니다";
        yield return new WaitForSeconds(0.5f);
        SSH_UIManager.Instance.mpLackText.SetActive(false);
    }

    IEnumerator SKillLack()
    {
        SSH_UIManager.Instance.mpLackText.SetActive(true);
        SSH_UIManager.Instance.mpLackTextUI.text = "아직 스킬을 사용할 수 없습니다";
        yield return new WaitForSeconds(0.5f);
        SSH_UIManager.Instance.mpLackText.SetActive(false);
    }

    // Skill2 애니메이션 이벤트
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
    public AudioSource skill2Sound; //레이저 사운드
    public void skill2SoundEvent()
    {
        skill2Sound.Play();
    }

    public void CoolTimeUseSkill()
    {
        if (canUseSkill)
        {
            Debug.Log("Use SKill");
            skillFilter.fillAmount = 1; // 스킬 버튼을 가림
            StartCoroutine("CoolTime");
            currentCoolTime = coolTime;
            coolTimeText.text = "" + currentCoolTime;
            StartCoroutine("CoolTimeCounter");
            canUseSkill = false; //스킬을 사용하면 사용할 수 없는 상태로 바꾼다.
        }
        else
        {
            Debug.Log("아직 스킬을 사용할 수 없습니다.");
        }
    }

    public void CoolTimeUseSkill2()
    {
        if (canUseSkill2)
        {
            Debug.Log("Use SKill2");
            skill2Filter.fillAmount = 1; // 스킬 버튼을 가림
            StartCoroutine("CoolTime2");
            currentCoolTime2 = coolTime2;
            coolTimeText2.text = ""+ coolTime2;
            StartCoroutine("CoolTimeCounter2");
            canUseSkill2 = false; //스킬을 사용하면 사용할 수 없는 상태로 바꾼다.
        }
        else
        {
            Debug.Log("아직 스킬2을 사용할 수 없습니다.");
        }
    }

    //CoolTime 필터 사라지는 코루틴
    IEnumerator CoolTime()
    {
        while (skillFilter.fillAmount > 0)
        {
            skillFilter.fillAmount -= Time.smoothDeltaTime / coolTime;
            yield return null;
        }
        canUseSkill = true; //스킬 쿨타임이 끝나면 스킬을 사용할 수 있는 상태로 바꿈
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
        canUseSkill2 = true; //스킬 쿨타임이 끝나면 스킬을 사용할 수 있는 상태로 바꿈
        coolTimeTextObject2.SetActive(false);

        yield break;
    }

    //남은 쿨타임을 계산할 코루틴
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
