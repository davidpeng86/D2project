using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour {
	public GameObject endMenu;
	public DataBase s_database;
	public GameObject Up;
	private Vector3 Save;
	private Vector2 velocity;
	// Use this for initialization
	void Start () {
		Save = Up.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			Up.transform.position = new Vector3(transform.position.x,Mathf.SmoothDamp(transform.position.y,Save.y+2.56f,ref velocity.y,0.2f),transform.position.z);
			endMenu.SetActive(true);
			s_database.Save();
		}
	}
}
