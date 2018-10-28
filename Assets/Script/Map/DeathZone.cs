using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour {

	public GameObject o_Player;
	public GameObject o_Camera;
	public DataBase s_Databae;
	float speed = 50.0f;
	float amount = 0.08f;
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
			StartCoroutine(CameraShake(0.15f,0.4f));
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
	IEnumerator CameraShake(float duration, float magnitude)
	{
		Vector3 p_camera = o_Camera.transform.position;

		float time =0.0f;
		while(time < duration)
		{
			float x = Random.Range(-1f,1f)*magnitude;
			float y = Random.Range(-1f,1f)*magnitude;
			o_Camera.transform.position = new Vector3(p_camera.x+x,p_camera.y+y,p_camera.z);
			time +=Time.deltaTime; 
			yield return null;
		}
		o_Camera.transform.position = p_camera;
	}
}
