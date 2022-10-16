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
    //����Ʈ �����
    AudioSource questAudio;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject bg;
    public GameObject bg2;
    //�÷��̾����� ������Ʈ
    PlayableDirector pd;
    //���� ������Ʈ ����
    public GameObject cine;
    //ġŲ ��ũ��Ʈ
    SSH_Chicken chicken;
    //��ݵ�
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

    //����� ����
    List<string> sentences = new List<string>();

    int i = 0;
    

    public void Begin(SSH_Dialogue info)
    {
        chatUI.SetActive(true);
        //�ϴ� ��δ� �����
        sentences.Clear();
        //�̸� �Ҵ�
        txtName.text = info.name;

        foreach (var sentence in info.sentences)
        {
            sentences.Add(sentence);
        }

        //���� ������ ������
        Next();
    }

    private bool yesButtonClick = false;
    public void Next()
    {
        //������
        if(i==6) //i=6�� ������ 7���϶��� �ش�
        {
            i = 0; // ��ȭ ó������ ���ư���
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
        //ù��° ����� ����
        bgm2Set = false;
        yield return new WaitForSeconds(1f);
        pd.gameObject.SetActive(false);
        
        
        
    }   
    
    public void BGM()
    {
        if(bgm2Set)
        {

        }
        //���࿡ bgm2set�� false���
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
        //���� ������ ��ȭ �Ѿ
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Next();
        }
        //���� sentence ��ȣ�� ������ ��ȣ�� �ƴٸ� ���ù�ư�� ���� ���ÿ� ���� ���� ���� �ٲ۴�
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

    //���� ������ ��
    public void yesButtonPull()
    {

        i++;
        Next();
        yesButtonClick = true;
        quest.SetActive(true);
        quest.GetComponent<Animator>().SetTrigger("FadeIn");
        SSH_UIManager.Instance.quest.GetComponent<AudioSource>().Play();
       

    }
    //�ƴϿ��� ������ ��
    public void noButtonPull()
    {

        Next();
        i++;
    }

    

    
    

}
 