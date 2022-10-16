using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SSH_Chicken : MonoBehaviour
{
    // Start is called before the first frame update
    //플레이어와의 거리가 1 이하가 되면 A를 누르면 대화를 시작하는 알림을 뜨게하고 실제로 누르면 대화를 시작하게 한다
    public float outDistance = 1;
    public GameObject target;
    public GameObject chatButton;
    AudioSource audio;
    //페이드아웃 패널
    public GameObject panel;

    public bool chatSetActive = false;

    void Start()
    {
        chatButton.SetActive(false);
        target = GameObject.Find("Player");
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if(distance<outDistance)
        {
            chatButton.SetActive(true);
            if (!chatSetActive)
            {
                //그 상태에서 E를 누르게 되면 대화를 시작하고 
                //알림을 끈다
                if (Input.GetKeyDown(KeyCode.E))
                {
                    chatButton.SetActive(false);
                    SSH_DialogueTrigger.Instance.Trigger();
                    audio.Play();
                    chatSetActive = true;
                }
            }
            else
            {
                chatButton.SetActive(false);
            }
            
        }
        else
        {
            chatButton.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Chick"))
        {
            StartCoroutine("IeScene");
        }
    }

    IEnumerator IeScene()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameClearScene");

    }
}
