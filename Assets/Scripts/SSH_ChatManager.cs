using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Playables;
using Cinemachine;

public class SSH_ChatManager : MonoBehaviour
{
    public Text txtName;
    public Text txtSentence;
    public GameObject chatUI;
    GameObject quest;
    //퀘스트 오디오
    AudioSource questAudio;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject bg;
    public GameObject bg2;
    //플레이어블디렉터 오브젝트
    PlayableDirector pd;
    //감독 오브젝트 모음
    public GameObject cine;
    //치킨 스크립트
    SSH_Chicken chicken;
    //브금들
    AudioSource bgm1;
    AudioSource bgm2;

    private void Awake()
    {
        cine.SetActive(false);
    }
    private void Start()
    {
        quest = SSH_UIManager.Instance.quest;
        questAudio = quest.GetComponent<AudioSource>();
        chatUI.SetActive(false);
        quest.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        pd = GameObject.Find("Director").GetComponent<PlayableDirector>();
        chicken = GameObject.Find("ToonChicken").GetComponent<SSH_Chicken>();
        bg.SetActive(false);
        bgm2 = bg2.GetComponent<AudioSource>();
        bgm1 = bg.GetComponent<AudioSource>();



    }

    //문장들 선언
    List<string> sentences = new List<string>();

    int i = 0;
    

    public void Begin(SSH_Dialogue info)
    {
        chatUI.SetActive(true);
        //일단 모두다 지운다
        sentences.Clear();
        //이름 할당
        txtName.text = info.name;

        foreach (var sentence in info.sentences)
        {
            sentences.Add(sentence);
        }

        //다음 문장을 보여줌
        Next();
    }

    private bool yesButtonClick = false;
    public void Next()
    {
        //끝날때
        if(i==6) //i=6은 문장이 7개일때만 해당
        {
            i = 0; // 대화 처음으로 돌아가기
            End();                        
            chicken.chatSetActive = false;
           if(yesButtonClick)
            {
                cine.SetActive(true);
                StartCoroutine("IeCine");
            }
            return;
           
        }

       
        txtSentence.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences[i]));
        i++;
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach(var letter in sentence)
        {
            txtSentence.text += letter;
            yield return new  WaitForSeconds(0.07f);
        }
    }

    private bool bgm2Set = true;
    IEnumerator IeCine()
    {

        pd.Play();
        yield return new WaitForSeconds(15f);
        //첫번째 브금을 끈다
        bgm2Set = false;
        yield return new WaitForSeconds(1f);
        pd.gameObject.SetActive(false);
        
        
        
    }   
    
    public void BGM()
    {
        if(bgm2Set)
        {

        }
        //만약에 bgm2set이 false라면
        else
        {
            bgm2.volume -= Time.deltaTime;
            if (bgm2.volume < 0.15f)
            {
                bg2.SetActive(false);
                bg.SetActive(true);
            }
        }
    }

    private void End()
    {
        txtSentence.text = string.Empty;
        chatUI.SetActive(false);

    }
    private void Update()
    {
        //엔터 누르면 대화 넘어감
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Next();
        }
        //만약 sentence 번호가 마지막 번호가 됐다면 선택버튼을 띄우고 선택에 따라 닭의 말을 바꾼다
        if (i==4)
        {
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }
        else
        {
            yesButton.SetActive(false);
            noButton.SetActive(false);
        }
        BGM();
    }

    //예를 눌렀을 때
    public void yesButtonPull()
    {

        i++;
        Next();
        yesButtonClick = true;
        quest.SetActive(true);
        quest.GetComponent<Animator>().SetTrigger("FadeIn");
        SSH_UIManager.Instance.quest.GetComponent<AudioSource>().Play();
       

    }
    //아니오를 눌렀을 때
    public void noButtonPull()
    {

        Next();
        i++;
    }

    

    
    

}
 