using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SH_PlayerMP : MonoBehaviour
{

    public static SH_PlayerMP instance;
    private void Awake()
    {
        instance = this;
    }

    // mp��
    int mp;
    // �ִ� mp
    public int maxMP = 30;
    // MP �����̴� UI
    public Slider MPslider;
    // TMPro : MP �����̴� UI Text
    TextMeshProUGUI sliderText;

    public int MP
    {
        get { return mp; }
        set
        {
            mp = value;
            MPslider.value = value;
            if (mp > maxMP)
            {
                mp = maxMP;
            }
            if (mp < 0)
            {
                mp = 0;
            }
        }
    }

    void Start()
    {
        // MP �ִ� ����
        MPslider.maxValue = maxMP;
        MP = maxMP;
        sliderText = SSH_UIManager.Instance.playerMPtext.GetComponent<TextMeshProUGUI>();
    }



    // Update is called once per frame
    void Update()
    {
        // MP �����̴� UI�� ������ ������Ʈ
        sliderText.text = mp + " / " + maxMP;
    }

    public void UseSkill(int mp)
    {
        if (MP > 0)
        {
            MP -= mp;
        }
    }
}