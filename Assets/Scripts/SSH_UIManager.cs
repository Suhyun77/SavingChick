using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SSH_UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SSH_UIManager Instance;

    //enemy1 ��� ����
    public GameObject enemy1slotblack;
    //enemy2 ��� ����
    public GameObject enemy2slotBlack;

    //Enemy2 skill2
    public ParticleSystem skill2;

    //Hp �κ��丮 text
    public Text hpText;
    //Mp �κ��丮 text
    public Text mpText;
    //Quest
    public GameObject quest;
    //���Ƹ� ��Ÿ���ٰ� ����ϴ� â
    public GameObject questChick;
    //���ư���� ����ϴ� â
    public GameObject returnChicken;
    //�� â
    public GameObject playerFace;
    //�κ��丮
    public GameObject inventory;
    //���� ����Ʈ
    public GameObject startQuest;
    //�÷��̾� HP
    public GameObject playerHP;
    //�÷��̾� MP
    public GameObject playerMP;
    //�÷��̾� HP text
    public GameObject playerHPtext;
    //�÷��̾� MP text
    public GameObject playerMPtext;
    //�̴ϸ� �ؽ�Ʈ
    public TextMeshProUGUI textMapName;
    //MP���� �˸�â
    public GameObject mpLackText;
    //�˸�â �ؽ�Ʈ
    public TextMeshProUGUI mpLackTextUI;
    //�˸�â ��׶���
    public GameObject sign;
    //�Ͻ����� UI Canvas
    public GameObject pauseCanvas;
    //soundon ��ư
    public GameObject soundOnBtn;
    //���� ���� ��ư
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
