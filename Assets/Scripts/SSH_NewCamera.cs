using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_NewCamera : MonoBehaviour
{
    //signleton
    public static SSH_NewCamera Instance;
    public Transform player;
    public Transform centralAxis;
    float mouseY;
    float mouseX;
    public float camSpeed = 7;
    private Camera mainCamera;
    public float smoothTime = 0.5f;
    public float zoomSpeed = 2;
    SSH_FogTrigger fogTrigger;
    SH_PlayerMove playerMove;
    SSH_Chicken chicken;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<SH_PlayerMove>();
        mainCamera = GetComponent<Camera>();
        fogTrigger = GameObject.Find("FogTrigger").GetComponent<SSH_FogTrigger>();
        chicken = GameObject.Find("ToonChicken").GetComponent<SSH_Chicken>();        
        
    }

    void Update()
    {
        Move();
        // fogTrigger와 충돌 시 Zoom
        if (fogTrigger.isFog == true)
        {
            StopCoroutine("Wait");
            StartCoroutine("Wait");
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 45, Time.deltaTime * smoothTime);
        }
        
        // Chicken과 대화할 때 Zoom in
        if(chicken == null)
        {
            chicken = GameObject.Find("Toon Chicken").GetComponent<SSH_Chicken>();
        }
        if (chicken.chatSetActive)
        {
            transform.LookAt(chicken.transform);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 40, Time.deltaTime * smoothTime);
        }
        else
        {
            transform.LookAt(player);
        }

    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(400);
    }

    Vector3 nearPosition;
    Vector3 farPosition;
    float wheelValue;
    Vector3 reverseDistance;
    void Move()
    {
        centralAxis.position = player.position;
        //카메라 고정
        //transform.position = player.position + new Vector3(0, 2.15f, -4.43f);
        if (Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");

            centralAxis.rotation = Quaternion.Euler(new Vector3(centralAxis.rotation.x + mouseY, centralAxis.rotation.y + mouseX, 0) * camSpeed);
        }
        nearPosition = centralAxis.position + new Vector3(0, -1, 2);
        farPosition = centralAxis.position + new Vector3(0, 2, -3.63f);

        //마우스 스크롤 휠의 움직임 값을 받는다
        wheelValue -= Input.GetAxis("Mouse ScrollWheel");
        //시작위치 종료위치 중 임의의 지점을 구한다
        wheelValue = Mathf.Clamp(wheelValue, 0, 1.0f);
        Vector3 camPosition = Vector3.Lerp(nearPosition, farPosition, wheelValue);
        //구한 임의의 지점을 카메라 위치로 설정한다
        centralAxis.transform.position = camPosition;
    }

    // 카메라 흔드는 시간
    float shakeTime;
    // 카메라 흔드는 세기
    float shakeIntensity;

    //외부에서 카메라 흔들림을 조작할 때 호출하는 메소드
    //ex) OnShakeCamera(1);               =>  1초간 0.1의 세기로 흔들림
    //ex) OnShakeCamera(0.5f, 1);               =>  0.5초간 1의 세기로 흔들림
    //<param name="shakeTime">  카메라 흔들림 지속 시간(설정하지 않으면 default 1.0f)
    //<param name = "shakeIntensity" >  카메라 흔들림 세기 ( 값이 클수록 세게 흔들린다 / 설정하지 않으면 default 0.1f)
    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByRotation");
        StartCoroutine("ShakeByRotation");
    }

    // 카메라를 shakeTime동안 shakeIntensity의 세기로 흔드는 코루틴 함수
    private IEnumerator ShakeByRotation()
    {
        // 흔들리기 직전 회전값
        Vector3 startRotation = transform.eulerAngles;

        // 회전의 경우 shakeIntensity에 더 큰 값이 필요하기 때문에 변수로 만들었음
        // (클래스 멤버변수로 선언해 외부에서 조작 가능)
        float power = 10f;

        while (shakeTime > 0.0f)
        {
            // 회전하길 원하는 축만 지정해서 사용 ( 회전하지 않을 축은 0으로 설정)
            // (클래스 멤버변수로 선언해 외부에서 조작하면 더 좋다)
            float x = 0; // Random.Range(-1f, 1f);
            float y = 0; // Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            // 시간 감소
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        // 흔들리기 전의 회전 값으로 설정
        transform.rotation = Quaternion.Euler(startRotation.x, 0, 0);
    }
}