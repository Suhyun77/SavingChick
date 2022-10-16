using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10;
    Vector3 dir;
    
    
    void Start()
    {
        //player = GameObject.Find("Player");
        //dir = player.transform.position - transform.position;
        //dir.Normalize();
        dir = transform.forward;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += dir * speed * Time.deltaTime;
    }

    

    //�÷��̾ ��Ҵٸ� HP�� ���δ�
    private void OnTriggerEnter(Collider other)
    {
        SH_PlayerHP player = other.gameObject.GetComponent<SH_PlayerHP>();
        if (player)
        {
            player.AddDamage(1);
        }
    }
}
