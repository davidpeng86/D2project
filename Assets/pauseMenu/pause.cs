using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {
	public bool ppActive;
	public GameObject menu;
	
	public Animator pauseAnim;

	// Use this for initialization
	void Start () {
		ppActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void pp(){
		ppActive = !ppActive;
		if(ppActive == true){
			menu.SetActive(true);
		}
		else{
			pauseAnim.SetBool("fadeout",true);
		}
	}

	public void restart(){
		pauseAnim.SetBool("restart",true);
	}

	public void exit(){
		pauseAnim.SetBool("exit",true);
	}

}
