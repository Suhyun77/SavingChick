using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SSH_UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SSH_UIManager Instance;

    //enemy1 흑백 슬롯
    public GameObject enemy1slotblack;
    //enemy2 흑백 슬롯
    public GameObject enemy2slotBlack;

    //Enemy2 skill2
    public ParticleSystem skill2;

    //Hp 인벤토리 text
    public Text hpText;
    //Mp 인벤토리 text
    public Text mpText;
    //Quest
    public GameObject quest;
    //병아리 나타났다고 얘기하는 창
    public GameObject questChick;
    //돌아가라고 얘기하는 창
    public GameObject returnChicken;
    //얼굴 창
    public GameObject playerFace;
    //인벤토리
    public GameObject inventory;
    //시작 퀘스트
    public GameObject startQuest;
    //플레이어 HP
    public GameObject playerHP;
    //플레이어 MP
    public GameObject playerMP;
    //플레이어 HP text
    public GameObject playerHPtext;
    //플레이어 MP text
    public GameObject playerMPtext;
    //미니맵 텍스트
    public TextMeshProUGUI textMapName;
    //MP부족 알림창
    public GameObject mpLackText;
    //알림창 텍스트
    public TextMeshProUGUI mpLackTextUI;
    //알림창 백그라운드
    public GameObject sign;
    //일시정지 UI Canvas
    public GameObject pauseCanvas;
    //soundon 버튼
    public GameObject soundOnBtn;
    //사운드 끄기 버튼
    public GameObject soundOffBtn;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
