using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SSH_GameOverButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void SceneChange()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public GameObject reBtn;

    public void ReMouseEnter()
    {
        iTween.ScaleTo(reBtn, iTween.Hash("x", 1.1f, "y", 1.1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.2f));

    }

    public void ReMouseExit()
    {
        iTween.ScaleTo(reBtn, iTween.Hash("x", 1f, "y", 1f, "easetype", iTween.EaseType.easeInOutBack, "time", 0.2f));
    }
   

}
