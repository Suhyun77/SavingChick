using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_StartSceneButton : MonoBehaviour
{
    // Start is called before the first frame update
 

    void Start()
    {
        
    }

    void Update()
    {
       

    }
    private void OnMouseOver() //���콺�� �ش� ��ư�� ��ġ������
    {
        Debug.Log("���콺 �ö�");
        //iTween.ScaleTo(this.gameObject, iTween.Hash("x", 0.85f, "delay", 0.1f, "time", 0.3f)); //��ŸƮ ��ư
        //iTween.ScaleTo(settingBtn, iTween.Hash("x", 0.85f, "delay", 0.1f, "time", 0.3f)); //���� ��ư
        //iTween.ScaleTo(InfoBtn, iTween.Hash("x", 0.85f, "delay", 0.1f, "time", 0.3f)); //���� ��ư
    }
}
