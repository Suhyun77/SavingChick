using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SSH_UIMinimap : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCamera;
    [SerializeField]
    private float zoomIn = 1;   //ī�޶��� �ʵ����� �ּ�ũ��
    [SerializeField]
    private float zoomMax = 30; //ī�޶��� �ʵ����� �ִ�ũ��
    [SerializeField]
    private float zoomOneStep = 1; //1ȸ �� �� �� ����/ ���ҵǴ� ��ġ
    [SerializeField]
    private TextMeshProUGUI textMapName;

    private void Awake()
    {
        //�� �̸��� ���� ���̸����� ����(���ϴ� �̸����� ����)
        textMapName.text = "����";
    }
    
    public void ZoomIn()
    {
        //ī�޶��� orthographicSize ���� ���ҽ��� ī�޶� ���̴� �繰 ũ�� Ȯ��
        minimapCamera.fieldOfView = Mathf.Max(minimapCamera.fieldOfView - zoomOneStep, zoomIn);

    }

    public void ZoomOut()
    {
        //ī�޶��� orthographicSize ���� ���ҽ��� ī�޶� ���̴� �繰 ũ�� Ȯ��
        minimapCamera.fieldOfView = Mathf.Min(minimapCamera.fieldOfView + zoomOneStep, zoomMax);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
