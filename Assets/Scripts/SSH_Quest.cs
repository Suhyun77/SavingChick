using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSH_Quest : MonoBehaviour
{
    //퀘스트 현황을 업데이트 하고싶다.
    //빨간 몬스터 10마리, 파란 몬스터 10마리 잡으면 
    //퀘스트 완료 표시를 뜨게 하고싶다
    //빨간몬스터 잡은 수
    public int enemy1Count =0;
    public Text enemy1CountTxt;
    //파란몬스터 잡은 수
    public int enemy2Count =0;
    
    public Text enemy2CountTxt;
    //병아리 나타났다고 얘기하는 창
    public GameObject questChick;
    //병아리
    public GameObject chick;
    void Start()
    {
        questChick = SSH_UIManager.Instance.questChick;
        questChick.SetActive(false);
        chick.SetActive(false);
    }

    
    void Update()
    {
        enemy1CountTxt.text = "빨간 슬라임 : " + enemy1Count + " / 5";
        enemy2CountTxt.text = "파란 슬라임 : " + enemy2Count + " / 5";
        if (enemy1Count > 5)
        {
            enemy1Count = 5;
        }
        if (enemy2Count > 5)
        {
            enemy2Count = 5;
        }
        if (enemy1Count ==5 && enemy2Count ==5)
        {
           
            enemy1CountTxt.color = Color.blue;
            enemy2CountTxt.color = Color.blue;
            questChick.SetActive(true);
            chick.SetActive(true);
        }
    }

    
}
