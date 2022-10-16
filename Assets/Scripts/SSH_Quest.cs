using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSH_Quest : MonoBehaviour
{
    //����Ʈ ��Ȳ�� ������Ʈ �ϰ�ʹ�.
    //���� ���� 10����, �Ķ� ���� 10���� ������ 
    //����Ʈ �Ϸ� ǥ�ø� �߰� �ϰ�ʹ�
    //�������� ���� ��
    public int enemy1Count =0;
    public Text enemy1CountTxt;
    //�Ķ����� ���� ��
    public int enemy2Count =0;
    
    public Text enemy2CountTxt;
    //���Ƹ� ��Ÿ���ٰ� ����ϴ� â
    public GameObject questChick;
    //���Ƹ�
    public GameObject chick;
    void Start()
    {
        questChick = SSH_UIManager.Instance.questChick;
        questChick.SetActive(false);
        chick.SetActive(false);
    }

    
    void Update()
    {
        enemy1CountTxt.text = "���� ������ : " + enemy1Count + " / 5";
        enemy2CountTxt.text = "�Ķ� ������ : " + enemy2Count + " / 5";
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
