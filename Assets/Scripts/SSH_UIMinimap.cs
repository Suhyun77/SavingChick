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
    private float zoomIn = 1;   //카메라의 필드오브뷰 최소크기
    [SerializeField]
    private float zoomMax = 30; //카메라의 필드오브뷰 최대크기
    [SerializeField]
    private float zoomOneStep = 1; //1회 줌 할 때 증가/ 감소되는 수치
    [SerializeField]
    private TextMeshProUGUI textMapName;

    private void Awake()
    {
        //맵 이름을 현재 씬이름으로 설정(원하는 이름으로 설정)
        textMapName.text = "마을";
    }
    
    public void ZoomIn()
    {
        //카메라의 orthographicSize 값을 감소시켜 카메라에 보이는 사물 크기 확대
        minimapCamera.fieldOfView = Mathf.Max(minimapCamera.fieldOfView - zoomOneStep, zoomIn);

    }

    public void ZoomOut()
    {
        //카메라의 orthographicSize 값을 감소시켜 카메라에 보이는 사물 크기 확대
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
