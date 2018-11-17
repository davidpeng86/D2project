using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public GameObject o_Player;
	public spawn generator;
	public DataBase s_Databae;
	SavePoint theMostCloseSavePoint;
	private Vector2 velocity;
	public LayerMask cubeLayer;
	bool open;
	public GameObject[] DoorSwitch;
	private Vector3 Save;
	public CameraFollow s_CameraFollow;
	// Use this for initialization
	void Start () {
		Save = transform.position;
	}
	bool PlayerContact
	{
		get
		{
			Vector2 start = new Vector2 (transform.position.x-0.65f,transform.position.y-2.7f);
			Vector2 end = new Vector2 (transform.position.x +0.65f,start.y);
			Debug.DrawLine(start,end,Color.red);
			if(Physics2D.Linecast(start,end,cubeLayer))
			{
				return true;
			}
			else
			{
				return false;
			}
			
		}
	}

	// Update is called once per frame
	void Update () {
		theMostCloseSavePoint = s_Databae.theMostCloseSavePoint.GetComponent<SavePoint>();
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
	void OnCollisionStay2D(Collision2D col)
	{
		if(col.collider.tag == "Player")
		{
			if(PlayerContact)
			{
				s_CameraFollow.StartCoroutine(s_CameraFollow.CameraShake(0.15f,0.4f));
				if(o_Player.transform.localScale == new Vector3(1,1,1))
				{
					o_Player.transform.Rotate(0,0,10);
				}
				else if(o_Player.transform.localScale == new Vector3(-1,1,1))
				{
					o_Player.transform.Rotate(0,0,-10);
				}
				generator.ThrowCube(new Vector2 (0,0));
				generator.DestroyCube();
				StartCoroutine(wait(0.5f));
			}
		}
		if(col.collider.tag == "generated")
		{
			if(PlayerContact)
			{
				generator.ThrowCube(new Vector2 (0,0));
				generator.DestroyCube();
			}
		}

	}

	IEnumerator wait(float time)
	{	
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSecondsRealtime(time);
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		o_Player.transform.position = s_Databae.SavePoint;
		o_Player.transform.rotation = Quaternion.Euler(0,0,0);
		if (theMostCloseSavePoint!=null)
		{
			s_Databae.maxCube = theMostCloseSavePoint.maxCube;
			s_Databae.maxUsedcube = theMostCloseSavePoint.maxUsedcube;
			s_Databae.UsedCube = theMostCloseSavePoint.UsedCube;
			s_Databae.Crown =theMostCloseSavePoint.Crown;

		}
		
	}
}
