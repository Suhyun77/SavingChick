using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	void Start(){
		//iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
		iTween.ScaleBy(gameObject, iTween.Hash("x", 2, "easeType", iTween.EaseType.easeInOutBounce, "loopType", "pingPong", "delay", .1));
		
	}
}

