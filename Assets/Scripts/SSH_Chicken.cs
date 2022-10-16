using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SSH_Chicken : MonoBehaviour
{
    // Start is called before the first frame update
    //�÷��̾���� �Ÿ��� 1 ���ϰ� �Ǹ� A�� ������ ��ȭ�� �����ϴ� �˸��� �߰��ϰ� ������ ������ ��ȭ�� �����ϰ� �Ѵ�
    public float outDistance = 1;
    public GameObject target;
    public GameObject chatButton;
    AudioSource audio;
    //���̵�ƿ� �г�
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
                //�� ���¿��� E�� ������ �Ǹ� ��ȭ�� �����ϰ� 
                //�˸��� ����
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
