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
        //esc 누르면 재생정지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume(); 
            }
            else
            {
                //이에따른 UI도 추가하자
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
    //재시작을 버튼을 눌렀을 때
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

    private bool Soundoff = false; //소리가 켜져있음

    public void SoundOn()
    {
        //소리가 꺼져있음에서 켜져있음으로(소리끄기(on) > 소리켜기(off))
        AudioListener.volume = 1;
        SSH_UIManager.Instance.soundOnBtn.SetActive(false);
        SSH_UIManager.Instance.soundOffBtn.SetActive(true);
        Soundoff = false;
    }

    public void SoundOff()
    {
        //소리가 켜져있음에서 꺼져있음으로
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
