using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_RespawnRange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rangeObject;
    BoxCollider rangeCollider;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        //콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range(-(range_X / 2), range_X / 2);
        range_Z = Random.Range(-(range_Z / 2), range_Z / 2);
        Vector3 RandomPosition = new Vector3(range_X, 0, range_Z);

        Vector3 respawnPosition = originPosition + RandomPosition;
        return respawnPosition;
    }

    //소환할 적
    public GameObject enemy1;
    public GameObject enemy2;

    void Start()
    {
        StartCoroutine("RandomRespawn_Coroutine");
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            //생성 위치 부분에 위에서 만든 함수 Return_RandomPosition() 함수대입
            GameObject instantEnemy = Instantiate(enemy1, Return_RandomPosition(), Quaternion.identity);
            GameObject instantEnemy2 = Instantiate(enemy2, Return_RandomPosition(), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
