using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SH_PlayerHP : MonoBehaviour
{
    //  ������ ������ ������ �÷��̾��� ü���� ���ҽ�Ű�� �ʹ�  //

    public static SH_PlayerHP instance;
    private void Awake()
    {
        instance = this;
    }

    // ü��
    int hp;
    // �ִ� ü��
    public int maxHP = 100;
    // ü�� �����̴� UI
    public Slider playerSlider;
    //�ִϸ�����
    Animator anim;
    // TMPro : ü�� �����̴� UI Text
    TextMeshProUGUI sliderText;
    Material mat;

    void Start()
    {
        // ü�� �ִ�� �ʱ� ����
        playerSlider.maxValue = maxHP;
        HP = maxHP;
        anim = GetComponent<Animator>();
        sliderText = SSH_UIManager.Instance.playerHPtext.GetComponent<TextMeshProUGUI>();
        SkinnedMeshRenderer mr = GetComponentInChildren<SkinnedMeshRenderer>();
        mat = mr.material;
    }


    void Update()
    {
        // ü�� �����̴� UI�� ������ ������Ʈ
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
            StartCoroutine("IeAttacked");  // �ڷ� �з���
            StartCoroutine("IeRedChange");  // ������ ����
            
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
    // ���� ���ϸ� �з���
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

    

