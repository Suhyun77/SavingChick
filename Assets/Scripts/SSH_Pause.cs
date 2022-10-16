using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SSH_Pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SSH_UIManager.Instance.pauseCanvas.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //esc ������ �������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume(); 
            }
            else
            {
                //�̿����� UI�� �߰�����
                Pause();
            }
        }
        if (Soundoff == false)
        {
            SSH_UIManager.Instance.soundOffBtn.SetActive(false);
            SSH_UIManager.Instance.soundOnBtn.SetActive(true);
        }
        else
        {
            SSH_UIManager.Instance.soundOffBtn.SetActive(true);
            SSH_UIManager.Instance.soundOnBtn.SetActive(false);
        }
    }

    private bool GameIsPaused = false;
    //������� ��ư�� ������ ��
    public void Resume()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        SSH_UIManager.Instance.pauseCanvas.SetActive(false);
    }
    

    public void Pause()
    {
        Time.timeScale = 0;
        GameIsPaused = true;
        SSH_UIManager.Instance.pauseCanvas.SetActive(true);
       
    }

    private bool Soundoff = false; //�Ҹ��� ��������

    public void SoundOn()
    {
        //�Ҹ��� ������������ ������������(�Ҹ�����(on) > �Ҹ��ѱ�(off))
        AudioListener.volume = 1;
        SSH_UIManager.Instance.soundOnBtn.SetActive(false);
        SSH_UIManager.Instance.soundOffBtn.SetActive(true);
        Soundoff = false;
    }

    public void SoundOff()
    {
        //�Ҹ��� ������������ ������������
        AudioListener.volume = 0;
        SSH_UIManager.Instance.soundOnBtn.SetActive(true);
        SSH_UIManager.Instance.soundOffBtn.SetActive(false);
        Soundoff = true;
    }

    public void GobackStart()
    {
        SceneManager.LoadScene("startScene");
    }
}
