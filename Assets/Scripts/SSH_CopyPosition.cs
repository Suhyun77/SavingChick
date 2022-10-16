using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_CopyPosition : MonoBehaviour
{
    [SerializeField]
    private bool x, y, z;   //�� ����  true�̸� target�� ��ǥ, false�̸� ���� ��ǥ�� �״�� ���
    [SerializeField]
    private Transform target; //�Ѿư����� ��� Transform
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�Ѿư����� ����� ������ ����
        if (!target) return;

        transform.position = new Vector3(
            (x ? target.position.x : transform.position.x),
            (y ? target.position.y : transform.position.y),
            (z ? target.position.z : transform.position.z));
    }
}
