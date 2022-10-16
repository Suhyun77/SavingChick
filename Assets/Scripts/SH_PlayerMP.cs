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

    // mp값
    int mp;
    // 최대 mp
    public int maxMP = 30;
    // MP 슬라이더 UI
    public Slider MPslider;
    // TMPro : MP 슬라이더 UI Text
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
        // MP 최대 설정
        MPslider.maxValue = maxMP;
        MP = maxMP;
        sliderText = SSH_UIManager.Instance.playerMPtext.GetComponent<TextMeshProUGUI>();
    }



    // Update is called once per frame
    void Update()
    {
        // MP 슬라이더 UI에 변동값 업데이트
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