using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_Chick : MonoBehaviour
{
    // Start is called before the first frame update
    //���������� �Ǹ� ã�� ��ư�� ��Ÿ���� ã���ư�� ������ ���Ƹ��� �÷��̾� ���� ������Ű�� �ϰ� �ʹ�
    GameObject player;
    public GameObject particle;
    Vector3 target;
    public bool follow = false;
    Animator anim;
    AudioSource audio;
    //����Ʈ �˸�â
    public GameObject returnChicken;
    void Start()
    {
        returnChicken.SetActive(false);
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (follow == true)
        {
            transform.position = player.transform.position + player.transform.forward * 0.3f + player.transform.up * 1.2f ;
            transform.forward = player.transform.forward;
            particle.SetActive(false);
            anim.SetTrigger("Idle");
            audio.Stop();
            returnChicken.SetActive(true);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            
            follow = true;
        }    
      
    }
}
