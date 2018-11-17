using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour {

	public GameObject o_Player;
	public spawn generator;
	public DataBase s_Databae;
	public CameraFollow s_CameraFollow = null;
	public CameraFollowStage2 s_CameraFollowStage2 = null;
	SavePoint theMostCloseSavePoint;
	void Start () {
		
	}
	
	void Update () {
		theMostCloseSavePoint = s_Databae.theMostCloseSavePoint.GetComponent<SavePoint>();

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		
		if(col.tag == "Player")
		{
			if(s_CameraFollow!=null)
			s_CameraFollow.StartCoroutine(s_CameraFollow.CameraShake(0.15f,0.4f));
			else if(s_CameraFollowStage2 != null)
			s_CameraFollowStage2.StartCoroutine(s_CameraFollowStage2.CameraShake(0.15f,0.4f));

			generator.ThrowCube(new Vector2 (0,0));
			generator.DestroyCube();
			StartCoroutine(wait(1f));
		}
		if(col.tag =="FallDownBlock")
		{
			col.transform.position = new Vector3(col.transform.position.x,col.transform.position.y+20,col.transform.position.z);
		}
	}
	IEnumerator wait(float time)
	{
		yield return new WaitForSecondsRealtime(time);
		o_Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		o_Player.transform.position = s_Databae.SavePoint;
		if(theMostCloseSavePoint!=null)
		{
			s_Databae.maxCube = theMostCloseSavePoint.maxCube;
			s_Databae.maxUsedcube = theMostCloseSavePoint.maxUsedcube;
			s_Databae.UsedCube = theMostCloseSavePoint.UsedCube;
			s_Databae.Crown =theMostCloseSavePoint.Crown;
		}
	}
}
