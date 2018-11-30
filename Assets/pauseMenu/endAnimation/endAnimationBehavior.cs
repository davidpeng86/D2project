using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endAnimationBehavior : MonoBehaviour {
	public Animator endAnim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void restart(){
		endAnim.SetBool("restart",true);
	}

	public void exit(){
		endAnim.SetBool("exit",true);
	}

	public void next(){
		endAnim.SetBool("nextStage",true);
	}
}
