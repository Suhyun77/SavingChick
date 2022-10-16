using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ų 1�� �����Ǹ� �����߽ɿ��� ���� �ٱ����� �������� �յ� ����
//1. ���� �߽ɿ��� �Ѿ� �����Ѵ�
//2. �Ѿ��� ���� �߽ɿ��� 2m ��ŭ Ȯ �̵���Ų��
//3. �����ϰ� �ʹ�
//�ʿ�Ӽ� : Enemy2, ȸ���ӵ�
//��ų 2�� Enemy2���� ��������
public class SSH_Enemy2Bullet : MonoBehaviour
{
    //�ʿ� �Ӽ� : ��ǥ����, ����, �ӵ�, ����2
    public GameObject enemy2;
    public float speed = 15;
    Vector3 dir = Vector3.zero;
    //���� �ʿ�Ӽ� :����2(�̹�����) ȸ���ӵ�
    public float aroungSpeed; //ȸ���ӵ�
    
    // Start is called before the first frame update
    void Start()
    {
         dir = transform.forward;
    }
    //���� Enemy2�� Ȱ��ȭ �Ǿ��ִٸ�
    // Update is called once per frame
    void Update()
    {

        // �����ϰ� �ʹ�
        OrbitAround();
         
    }
    void OrbitAround()
    {
        transform.RotateAround(enemy2.transform.position, Vector3.down, speed * Time.deltaTime); ;
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
