using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_StartQuestBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject minimap;
    GameObject faceCanvas;
    GameObject inventory;
    void Start()
    {
        SSH_UIManager.Instance.playerFace.SetActive(false);
        SSH_UIManager.Instance.inventory.SetActive(false);
        SSH_UIManager.Instance.playerHP.SetActive(false);
        SSH_UIManager.Instance.playerMP.SetActive(false);
        SSH_UIManager.Instance.sign.SetActive(false);
        minimap.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClick()
    {
        SSH_UIManager.Instance.startQuest.SetActive(false);
        SSH_UIManager.Instance.playerFace.SetActive(true);
        SSH_UIManager.Instance.inventory.SetActive(true);
        SSH_UIManager.Instance.playerHP.SetActive(true);
        SSH_UIManager.Instance.playerMP.SetActive(true);
        SSH_UIManager.Instance.sign.SetActive(true);
        minimap.SetActive(true);
    }
}
