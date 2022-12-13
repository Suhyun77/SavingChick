using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SH_PlayerHP : MonoBehaviour
{
    //  몬스터의 공격을 받으면 플레이어의 체력을 감소시키고 싶다  //

    public static SH_PlayerHP instance;
    private void Awake()
    {
        instance = this;
    }

    // 체력
    int hp;
    // 최대 체력
    public int maxHP = 100;
    // 체력 슬라이더 UI
    public Slider playerSlider;
    //애니메이터
    Animator anim;
    // TMPro : 체력 슬라이더 UI Text
    TextMeshProUGUI sliderText;
    Material mat;

    void Start()
    {
        // 체력 최대로 초기 설정
        playerSlider.maxValue = maxHP;
        HP = maxHP;
        anim = GetComponent<Animator>();
        sliderText = SSH_UIManager.Instance.playerHPtext.GetComponent<TextMeshProUGUI>();
        SkinnedMeshRenderer mr = GetComponentInChildren<SkinnedMeshRenderer>();
        mat = mr.material;
    }


    void Update()
    {
        // 체력 슬라이더 UI에 변동값 업데이트
        sliderText.text = hp + " / " + maxHP;
    }

    // property
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            playerSlider.value = value;
            if(hp>maxHP)
            {
                hp = maxHP;
            }
            if(hp<0)
            {
                hp = 0;
            }
        }
    }

    public void AddDamage(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            StartCoroutine("IeAttacked");  // 뒤로 밀려남
            StartCoroutine("IeRedChange");  // 빨갛게 변함
            
            if (HP <= 0)
            {
                anim.SetTrigger("Die");
                SceneManager.LoadScene("GameOutScene");
            }
        }
    }


    IEnumerator IeRedChange()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mat.color = Color.white;
    }
    // 공격 당하면 밀려남
    IEnumerator IeAttacked()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + transform.forward * -0.5f;
        float currentTime = 0;
        while (true)
        {
            if (currentTime < 1)
            {
                currentTime += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, currentTime * 5);
                yield return null;
            }


            else
            {
                yield break;
            }
        }
    }

}

    

