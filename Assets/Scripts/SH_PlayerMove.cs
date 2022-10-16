using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_PlayerMove : MonoBehaviour
{
    // �ӵ�
    public float speed = 0.5f;
    // ���� �ӵ�
    float final_speed;
    // ȸ�� �ӵ�
    float rotationSpeed;
    // �߷�
    float gravity = -9.81f;
    // �ٴ� ��
    float jumpPower = 3;
    // ���� �ӷ�
    float yVelocity = 0;
    // ����
    public Vector3 dir;
    // ĳ������Ʈ�ѷ�
    public CharacterController cc;
    // �ִϸ�����
    public Animator anim;
    // ���� Ƚ�� ����
    public int jumpCount = 2;
    // ���� ��Ҵ��� ����
    public bool isGrounded = true;
    // ������ ����
    AudioSource walkSound;
    // ���� ����
    public AudioSource jumpSound;
    SSH_Chicken chicken;
    //�̴ϸ� y��ġ ����
    public GameObject playerminimapIcon;

    void Start()
    {
        final_speed = speed;
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        walkSound = GetComponent<AudioSource>();
        chicken = GameObject.Find("ToonChicken").GetComponent<SSH_Chicken>();
    }


    void Update()
    {
        // Animator
        // ����� �Է� ���� ���, Animator �⺻�� = Run
        anim.SetBool("IsRun", dir != Vector3.zero);

        // ����� �Է�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // ����
        dir = new Vector3(h, 0, v);
        dir.Normalize();

        // �߷�
        yVelocity += gravity * Time.deltaTime;

        // Jump : 2�� ���������� ����
        if (isGrounded == true && jumpCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpPower;
                jumpCount--;
                jumpSound.Play();
                Camera.main.fieldOfView = 75;
            }
            if (chicken.chatSetActive == false)
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime);
            }
        }


        dir.y = yVelocity;

        // ���� �̵�
        cc.Move(dir * final_speed * 2 * Time.deltaTime);

        // ȸ��
        dir.y = 0;
        transform.LookAt(transform.position + dir);  // Look At : ������ ���͸� ���� ȸ����Ű�� �Լ�

        // �ȴ� �Ҹ�
        //WalkSound();
        playerminimapIcon.transform.position = new Vector3(transform.position.x, -1, transform.position.z);
    }

    // Animation Event : ������ �����̸� ����
    internal void AttackForward()
    {
        float attackMoveSpeed = 50;
        transform.position += Vector3.forward * attackMoveSpeed * Time.deltaTime;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        isGrounded = true;
        jumpCount = 2;  // ���� ������ ����Ƚ�� 2�� �ʱ�ȭ
    }

    void WalkSound()
    {
        if (anim.GetBool("IsRun"))
        {
            if (!walkSound.isPlaying)
            {
                walkSound.pitch = 2;  // �⺻ animation = Run�̹Ƿ� �⺻ ����ӵ� 2
                walkSound.Play();
            }
        }
        else
        {
            walkSound.Stop();
        }
    }
}