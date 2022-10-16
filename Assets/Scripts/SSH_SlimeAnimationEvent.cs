using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_SlimeAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public SSH_Enemybeta enemy;

    public void OnEventAttack()
    {
        enemy.OnEventAttack();
    }
                
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
