using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {
	public bool ppActive;
	public GameObject menu;

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
			menu.SetActive(false);
		}
		

	}
}
