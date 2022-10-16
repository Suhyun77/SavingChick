using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스킬 1이 구현되면 몬스터중심에서 몬스터 바깥으로 빠져나와 둥둥 돈다
//1. 몬스터 중심에서 총알 생성한다
//2. 총알을 몬스터 중심에서 2m 만큼 확 이동시킨다
//3. 공전하고 싶다
//필요속성 : Enemy2, 회전속도
//스킬 2는 Enemy2에서 직접실행
public class SSH_Enemy2Bullet : MonoBehaviour
{
    //필요 속성 : 목표지점, 방향, 속도, 몬스터2
    public GameObject enemy2;
    public float speed = 15;
    Vector3 dir = Vector3.zero;
    //공전 필요속성 :몬스터2(이미있음) 회전속도
    public float aroungSpeed; //회전속도
    
    // Start is called before the first frame update
    void Start()
    {
         dir = transform.forward;
    }
    //만약 Enemy2가 활성화 되어있다면
    // Update is called once per frame
    void Update()
    {

        // 공전하고 싶다
        OrbitAround();
         
    }
    void OrbitAround()
    {
        transform.RotateAround(enemy2.transform.position, Vector3.down, speed * Time.deltaTime); ;
    }

   

    //플레이어에 닿았다면 HP를 줄인다
    private void OnTriggerEnter(Collider other)
    {
        SH_PlayerHP player = other.gameObject.GetComponent<SH_PlayerHP>();
        if (player)
        {
            player.AddDamage(1);
        }
    }
}
