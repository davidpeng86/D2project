using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour {

	public GameObject o_Player;
	public DataBase s_Databae;
	public CameraFollow s_CameraFollow;
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
			s_CameraFollow.StartCoroutine(s_CameraFollow.CameraShake(0.15f,0.4f));
			StartCoroutine(wait(1f));
		}
	}
	IEnumerator wait(float time)
	{
		yield return new WaitForSecondsRealtime(time);
		o_Player.transform.position = s_Databae.SavePoint;
		s_Databae.maxCube = theMostCloseSavePoint.maxCube;
		s_Databae.maxUsedcube = theMostCloseSavePoint.maxUsedcube;
		s_Databae.UsedCube = theMostCloseSavePoint.UsedCube;
		s_Databae.Crown =theMostCloseSavePoint.Crown;

	}
}
