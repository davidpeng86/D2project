using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	private Vector2 velocity;
	public bool open;
	public bool close;
	public Transform up;
	public Transform down;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(open)
		{
			this.transform.position = new Vector3(transform.position.x,Mathf.SmoothDamp(transform.position.y,up.position.y,ref velocity.y,0.2f),transform.position.z);
		}
		else if(close)
		{
			this.transform.position = new Vector3(transform.position.x,Mathf.SmoothDamp(transform.position.y,down.position.y,ref velocity.y,0.2f),transform.position.z);
		}
		
	}
}
