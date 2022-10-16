using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_ItemDrop : MonoBehaviour
{
    // Start is called before the first frame update
    public static SSH_ItemDrop Instance;
    public enum Type { HP, MP }
    public Type type;
    public int value;
  
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    //아이템 회전
    void Update()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
 

    }
}
