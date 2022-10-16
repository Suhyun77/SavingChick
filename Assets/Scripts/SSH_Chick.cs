using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_Chick : MonoBehaviour
{
    // Start is called before the first frame update
    //일정간격이 되면 찾기 버튼이 나타나고 찾기버튼을 누르면 병아리가 플레이어 위에 안착시키게 하고 싶다
    GameObject player;
    public GameObject particle;
    Vector3 target;
    public bool follow = false;
    Animator anim;
    AudioSource audio;
    //퀘스트 알림창
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
