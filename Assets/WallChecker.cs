using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		directionCheck();
	}
	void directionCheck()
	{
		if (player.transform.localScale.x == 1)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (player.transform.localScale.x == -1)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}
}
