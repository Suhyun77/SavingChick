using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_PlayerMove : MonoBehaviour
{
    // 속도
    public float speed = 0.5f;
    // 최종 속도
    float final_speed;
    // 회전 속도
    float rotationSpeed;
    // 중력
    float gravity = -9.81f;
    // 뛰는 힘
    float jumpPower = 3;
    // 수직 속력
    float yVelocity = 0;
    // 방향
    public Vector3 dir;
    // 캐릭터컨트롤러
    public CharacterController cc;
    // 애니메이터
    public Animator anim;
    // 점프 횟수 제한
    public int jumpCount = 2;
    // 땅에 닿았는지 여부
    public bool isGrounded = true;
    // 움직임 사운드
    AudioSource walkSound;
    // 점프 사운드
    public AudioSource jumpSound;
    SSH_Chicken chicken;
    //미니맵 y위치 고정
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
        // 사용자 입력 있을 경우, Animator 기본값 = Run
        anim.SetBool("IsRun", dir != Vector3.zero);

        // 사용자 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 방향
        dir = new Vector3(h, 0, v);
        dir.Normalize();

        // 중력
        yVelocity += gravity * Time.deltaTime;

        // Jump : 2단 점프까지만 가능
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

        // 최종 이동
        cc.Move(dir * final_speed * 2 * Time.deltaTime);

        // 회전
        dir.y = 0;
        transform.LookAt(transform.position + dir);  // Look At : 지정된 벡터를 향해 회전시키는 함수

        // 걷는 소리
        //WalkSound();
        playerminimapIcon.transform.position = new Vector3(transform.position.x, -1, transform.position.z);
    }

    // Animation Event : 앞으로 움직이며 공격
    internal void AttackForward()
    {
        float attackMoveSpeed = 50;
        transform.position += Vector3.forward * attackMoveSpeed * Time.deltaTime;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        isGrounded = true;
        jumpCount = 2;  // 땅에 닿으면 점프횟수 2로 초기화
    }

    void WalkSound()
    {
        if (anim.GetBool("IsRun"))
        {
            if (!walkSound.isPlaying)
            {
                walkSound.pitch = 2;  // 기본 animation = Run이므로 기본 재생속도 2
                walkSound.Play();
            }
        }
        else
        {
            walkSound.Stop();
        }
    }
}