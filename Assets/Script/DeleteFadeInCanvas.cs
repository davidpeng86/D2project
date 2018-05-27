using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteFadeInCanvas : MonoBehaviour {
	
	public Animator fadeOut;
	bool playing;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		 if (fadeOut.GetCurrentAnimatorStateInfo(0).IsName("fade_out") && fadeOut.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
 		{
			this.gameObject.SetActive(false);
		}
	}
}
