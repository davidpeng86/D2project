using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(playTheme());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator playTheme()
	{
		FindObjectOfType<AudioManager>().play("start");
		yield return new WaitForSeconds(FindObjectOfType<AudioManager>().findAudio("start").clip.length);
		FindObjectOfType<AudioManager>().play("theme");
	}
}
