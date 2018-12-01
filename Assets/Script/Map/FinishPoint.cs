using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour {
	public GameObject endMenu;
	public DataBase s_database;
 	Scene m_scene;
	// Use this for initialization
	void Start () {
		m_scene = SceneManager.GetActiveScene();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			endMenu.SetActive(true);
		}
	}
}
