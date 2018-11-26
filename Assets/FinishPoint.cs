using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour {

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
			if(m_scene.name =="Tutorial")
			{
				SceneManager.LoadScene("Stage1");
			}
			else if(m_scene.name =="Stage1")
			{
				SceneManager.LoadScene("Stage2");
			}
			else
			{
				SceneManager.LoadScene("Start");
			}
		}
	}
}
