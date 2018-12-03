using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour {
	public GameObject endMenu;
	public DataBase s_database;
	public GameObject Up;
	private Vector3 Save;
	private Vector2 velocity;
	private bool In;
	// Use this for initialization
	void Start () {
		Save = Up.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(In)
		{
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				endMenu.SetActive(true);
				s_database.Save();
			}
			Up.transform.position = new Vector3(Up.transform.position.x,Mathf.SmoothDamp(Up.transform.position.y,Save.y+2.56f,ref velocity.y,0.2f),Up.transform.position.z);
		}
		else
		{
			Up.transform.position = new Vector3(Up.transform.position.x,Mathf.SmoothDamp(Up.transform.position.y,Save.y,ref velocity.y,0.2f),Up.transform.position.z);
			if(Up.transform.position.y <= Save.y+0.02f)
			{
				Up.SetActive(false);
			}
		}

		
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			Up.SetActive(true);
			In = true;
		}
	}
	private void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			In = false;
		}
	}
}
