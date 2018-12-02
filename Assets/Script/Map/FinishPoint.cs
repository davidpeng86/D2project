using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour {
	public GameObject endMenu;
	public DataBase s_database;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			endMenu.SetActive(true);
			s_database.Save();
		}
	}
}
