using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enable_0103 : MonoBehaviour {

	public GameObject stage_01;
	public GameObject stage_03;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void enable(){
		stage_01.SetActive(true);
		stage_03.SetActive(true);
	}

}
