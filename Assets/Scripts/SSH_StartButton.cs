using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SSH_StartButton : MonoBehaviour
{
    public Image panel;
    public float currentTime;
    //페이드가 몇초간 지속될지 정하는 값
    public float F_time = 2;
    public void SceneChange()
    {
        //SceneManager.LoadScene("SampleScene");
        StartCoroutine("IeScene");
    }
    public void Fade()
    {
        panel.gameObject.SetActive(true);
    }


    IEnumerator IeScene()
    {
        panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("SampleScene");
      
    }

    IEnumerator FadeFlow()
    {
        panel.gameObject.SetActive(true);
        currentTime = 0;
        Color alpha = panel.color;
        while(alpha.a<1)
        {
            currentTime += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, currentTime);
            panel.color = alpha;
            yield return null;
        }

        currentTime = 0;
        yield return new WaitForSeconds(1.0f);
        while (alpha.a > 0)
        {
            currentTime += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, currentTime);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }

    public GameObject InfoWindow;
    public void ShowInfo()
    {
        InfoWindow.SetActive(true);
    }
    public void CloseInfo()
    {
        InfoWindow.SetActive(false);
    }



    public GameObject startBtn;
    public void startMouseEnter()
    {
        //startBtn.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        iTween.ScaleTo(startBtn, iTween.Hash("x", 1.1f, "y", 1.1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.3f));
    }
    public void startMouseExit()
    {
        iTween.ScaleTo(startBtn, iTween.Hash("x", 1f, "y", 1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.3f));
    }

    public GameObject settingBtn;
    public void SettingsMouseEnter()
    {
        iTween.ScaleTo(settingBtn, iTween.Hash("x", 1.1f, "y", 1.1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.3f));
    }
    public void SettingsMouseExit()
    {
        iTween.ScaleTo(settingBtn, iTween.Hash("x", 1f, "y", 1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.3f));
    }

    public GameObject infoBtn;

    public void InfoMouseEnter()
    {
        iTween.ScaleTo(infoBtn, iTween.Hash("x", 1.1f, "y", 1.1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.3f));

    }
    public void InfoMouseExit()
    {
        iTween.ScaleTo(infoBtn, iTween.Hash("x", 1f, "y", 1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.3f));
    }
    public GameObject exitBtn;

    public void ExitMouseEnter()
    {
        iTween.ScaleTo(exitBtn, iTween.Hash("x", 1.1f, "y", 1.1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.3f));
    }
    public void ExitMouseExit()
    {
        iTween.ScaleTo(exitBtn, iTween.Hash("x", 1f, "y", 1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.3f));
    }


    public void GameQUit()
    {
        Application.Quit();
    }
}
