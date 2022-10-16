using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_MonsterSlot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemySlot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            enemySlot.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.I))
        {
            enemySlot.SetActive(false);
        }

        //만약에 ENEMY들이 죽으면 각각의 컬러 이미지 setActive를 false로 바꾼다
    }
}
