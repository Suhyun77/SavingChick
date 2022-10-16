using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SSH_MonstMap : MonoBehaviour
{
    float currentTime;
    public float loadTime = 9.5f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime> loadTime)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
