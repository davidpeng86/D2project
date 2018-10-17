using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.collider.tag == "Player")
		{
			StartCoroutine(wait(0.5f));
		}
	}
	IEnumerator wait(float time)
	{
		yield return new WaitForSecondsRealtime(time);
		SceneManager.LoadScene("cubb");

	}
}
