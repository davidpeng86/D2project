using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovePoint : MonoBehaviour {
	public CameraFollow s_camera;
	public float N_smoothTimeX;
    public float N_smoothTimeY;
    public float N_distanceXR =0;
    public float N_distanceXL = 8;
    public float N_distanceYU = 0;
    public float N_distanceYD = 0; 
	float smoothTimeX;
	float smoothTimeY;
    float distanceXR =0;
    float distanceXL = 8;
    float distanceYU = 0;
    float distanceYD = 0;
	private bool check;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			if(check==false)
			{
				smoothTimeX = s_camera.smoothTimeX;
				smoothTimeY = s_camera.smoothTimeY;
				distanceXL = s_camera.distanceXL;
				distanceXR = s_camera.distanceXR;
				distanceYD = s_camera.distanceYD;
				distanceYU = s_camera.distanceYU;
				s_camera.smoothTimeX = N_smoothTimeX;
				s_camera.smoothTimeY = N_smoothTimeY;
				s_camera.distanceXL = N_distanceXL;
				s_camera.distanceXR = N_distanceXR;
				s_camera.distanceYD = N_distanceYD;
				s_camera.distanceYU = N_distanceYU;
				//StartCoroutine(CameraMove(2.0f));
			}

		}
	}
	private void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag=="Player")
		{
			s_camera.smoothTimeX = smoothTimeX;
			s_camera.smoothTimeY = smoothTimeY;
			s_camera.distanceXL = distanceXL;
			s_camera.distanceXR = distanceXR;
			s_camera.distanceYD = distanceYD;
			s_camera.distanceYU = distanceYU;
		}
	}
	IEnumerator CameraMove(float time)
	{
		check = true;
		yield return new WaitForSeconds(time);
		s_camera.smoothTimeX = smoothTimeX;
		s_camera.smoothTimeY = smoothTimeY;
		s_camera.distanceXL = distanceXL;
		s_camera.distanceXR = distanceXR;
		s_camera.distanceYD = distanceYD;
		s_camera.distanceYU = distanceYU;
		StopCoroutine(CameraMove(0));
	}
}
