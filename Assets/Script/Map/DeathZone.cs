using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour {
	public GameObject deathFade;

	void OnTriggerEnter2D(Collider2D Col)
	{

		if(Col.tag == "Player")
		{
			deathFade.SetActive(true);
			StartCoroutine(wait(0.5f));
		}
	}
	IEnumerator wait(float time)
	{
		yield return new WaitForSecondsRealtime(time);
		SceneManager.LoadScene("cubb");
	}
}
