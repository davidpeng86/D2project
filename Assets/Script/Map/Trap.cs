using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour {
	public GameObject o_Player;
	public DataBase s_Databae;
	public CameraFollow s_CameraFollow;
	SavePoint theMostCloseSavePoint;
	void Start () {
		
	}
	
	void Update () {
		theMostCloseSavePoint = s_Databae.theMostCloseSavePoint.GetComponent<SavePoint>();

	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.collider.tag == "Player")
		{
			s_CameraFollow.StartCoroutine(s_CameraFollow.CameraShake(0.15f,0.4f));
			StartCoroutine(wait(0.5f));
		}
	}
	IEnumerator wait(float time)
	{	
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSecondsRealtime(time);
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		o_Player.transform.position = s_Databae.SavePoint;
		s_Databae.maxCube = theMostCloseSavePoint.maxCube;
		s_Databae.maxUsedcube = theMostCloseSavePoint.maxUsedcube;
		s_Databae.UsedCube = theMostCloseSavePoint.UsedCube;
		s_Databae.Crown =theMostCloseSavePoint.Crown;

	}
}
