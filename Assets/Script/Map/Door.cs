using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	private Vector2 velocity;
	bool open;
	public GameObject[] DoorSwitch;
	private Vector3 Save;
	// Use this for initialization
	void Start () {
		Save = transform.position;
	}

	// Update is called once per frame
	void Update () {
		ChangeDoorState ();
		if(open)
		{
			this.transform.position = new Vector3(transform.position.x,Mathf.SmoothDamp(transform.position.y,Save.y+4.2f,ref velocity.y,0.2f),transform.position.z);
		}
		else
		{
			this.transform.position = new Vector3(transform.position.x,Mathf.SmoothDamp(transform.position.y,Save.y,ref velocity.y,0.2f),transform.position.z);
		}
		
	}
	private void ChangeDoorState()
	{
		int count = 0;
		foreach (GameObject DS in DoorSwitch) {
			if (DS.GetComponent<DoorSwitch> ().DS_open == true) {
				count++;
			}
			if (count == DoorSwitch.Length) {
				open = true;
			}
			else{
				open = false;
			}
		}
	}
}
