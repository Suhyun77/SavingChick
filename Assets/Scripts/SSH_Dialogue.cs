using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//private를 통해 객체의 정보 은닉, 캡슐화 유지, Inspector에서 값을 변경 가능하게 하는 키워드
[System.Serializable]
public class SSH_Dialogue
{
    public string name;
    public List<string> sentences;
}
