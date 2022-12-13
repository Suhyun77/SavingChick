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

    [Header("Camera Shake ���� ����")]
    public bool xShake;
    public bool yShake;
    public bool zShake;
    public float power = 10f;

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
        // fogTrigger�� �浹 �� Zoom
        if (fogTrigger.isFog == true)
        {
            StopCoroutine("Wait");
            StartCoroutine("Wait");
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 45, Time.deltaTime * smoothTime);
        }
        
        // Chicken�� ��ȭ�� �� Zoom in
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
        //ī�޶� ����
        //transform.position = player.position + new Vector3(0, 2.15f, -4.43f);
        if (Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");

            centralAxis.rotation = Quaternion.Euler(new Vector3(centralAxis.rotation.x + mouseY, centralAxis.rotation.y + mouseX, 0) * camSpeed);
        }
        nearPosition = centralAxis.position + new Vector3(0, -1, 2);
        farPosition = centralAxis.position + new Vector3(0, 2, -3.63f);

        //���콺 ��ũ�� ���� ������ ���� �޴´�
        wheelValue -= Input.GetAxis("Mouse ScrollWheel");
        //������ġ ������ġ �� ������ ������ ���Ѵ�
        wheelValue = Mathf.Clamp(wheelValue, 0, 1.0f);
        Vector3 camPosition = Vector3.Lerp(nearPosition, farPosition, wheelValue);
        //���� ������ ������ ī�޶� ��ġ�� �����Ѵ�
        centralAxis.transform.position = camPosition;
    }

    // ī�޶� ���� �ð�
    float shakeTime;
    // ī�޶� ���� ����
    float shakeIntensity;

    /// <summary>
    /// �ܺο��� ī�޶� ��鸲�� ������ �� ȣ���ϴ� �޼ҵ�
    /// </summary>
    /// <param name="shakeTime"> ī�޶� ��鸲 ���� �ð�(default 1.0f) </param>
    /// <param name="shakeIntensity"> ī�޶� ��鸲 ���� ( ���� Ŭ���� ���� ��鸰�� / default 0.1f) </param>
    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByRotation");
        StartCoroutine("ShakeByRotation");
    }
    
    // ī�޶� shakeTime���� shakeIntensity�� ����� ���� �ڷ�ƾ �Լ�
    private IEnumerator ShakeByRotation()
    {
        // ��鸮�� ���� ȸ����
        Vector3 startRotation = transform.eulerAngles;

        while (shakeTime > 0.0f)
        {
            // ȸ���ϱ� ���ϴ� �ุ �����ؼ� ��� ( ȸ������ ���� ���� 0���� ����)
            float x = xShake == true ? Random.Range(-1f, 1f) : 0; 
            float y = yShake == true ? Random.Range(-1f, 1f) : 0;
            float z = zShake == true ? Random.Range(-1f, 1f) : 0;
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            // �ð� ����
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        // ��鸮�� ���� ȸ�� ������ ����
        transform.rotation = Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z);
    }
}