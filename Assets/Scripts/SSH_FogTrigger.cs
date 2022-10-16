using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class SSH_FogTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem fog;
    public bool isFog;
    public GameObject enemyFactory;
    public AudioSource BG;
    public GameObject monsterMapUI;
    AudioSource audio;
    //플레이어블디렉터 오브젝트
    PlayableDirector pd2;
    //감독 오브젝트 모음
    public GameObject cine2;


    private void Awake()
    {
        cine2.SetActive(false);
    }

    void Start()
    {
        isFog = false;
        fog.Stop();
        enemyFactory.SetActive(false);
        audio = GetComponent<AudioSource>();
        BG = GameObject.Find("BG").GetComponent<AudioSource>();
        pd2 = GameObject.Find("Director2").GetComponent<PlayableDirector>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject panel; //페이드아웃 패널
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {  
            isFog = true;
            BG.Stop();
            fog.Play();
            enemyFactory.SetActive(true);
            audio.Play();
            monsterMapUI.SetActive(true);
            //미니맵 이름 바꾸기
            SSH_UIManager.Instance.textMapName.text = "몬스터의 숲";
            cine2.SetActive(true);
            StartCoroutine("IeCine");
        }

    }
    public GameObject chick;
    IEnumerator IeCine()
    {
        SSH_UIManager.Instance.gameObject.SetActive(false);
        pd2.Play();
        yield return new WaitForSeconds(12.8f);
        chick.SetActive(true);
        yield return new WaitForSeconds(2.3f);
        SSH_UIManager.Instance.gameObject.SetActive(true);
        chick.SetActive(false);
        pd2.gameObject.SetActive(false);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            audio.Stop();
            BG.Play();
            enemyFactory.SetActive(false);
            fog.gameObject.SetActive(false);
            monsterMapUI.SetActive(false);
            //미니맵 이름 바꾸기
            SSH_UIManager.Instance.textMapName.text = "마을";
        }

    }

}
