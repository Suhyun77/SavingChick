using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_DialogueTrigger : MonoBehaviour
{
    public static SSH_DialogueTrigger Instance;
    private void Awake()
    {
        Instance = this;
    }
    public SSH_Dialogue info;

    public void Trigger()
    {
        var system = FindObjectOfType<SSH_ChatManager>();
        system.Begin(info);
    }
}
