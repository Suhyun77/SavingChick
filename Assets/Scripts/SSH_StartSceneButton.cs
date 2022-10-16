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
    private void OnMouseOver() //마우스가 해당 버튼에 위치했을때
    {
        Debug.Log("마우스 올라감");
        //iTween.ScaleTo(this.gameObject, iTween.Hash("x", 0.85f, "delay", 0.1f, "time", 0.3f)); //스타트 버튼
        //iTween.ScaleTo(settingBtn, iTween.Hash("x", 0.85f, "delay", 0.1f, "time", 0.3f)); //세팅 버튼
        //iTween.ScaleTo(InfoBtn, iTween.Hash("x", 0.85f, "delay", 0.1f, "time", 0.3f)); //인포 버튼
    }
}
