using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
//full explanation on https://www.youtube.com/watch?v=6OT43pvUyfY

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	public static AudioManager instance;
	private void Start()
	{
		//StartCoroutine(playTheme());
		FindObjectOfType<AudioManager>().play("theme");
	}
	private void Update()
	{
	}
	void Awake () {
		//the music will continue when loading a new scene
		DontDestroyOnLoad (gameObject);
		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}
		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

	}

	public Sound findAudio(string name){
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null) {
			Debug.Log ("sound doesn't exist!!!");
			return null;
		}
		return s;
	}


	public void play(string name){
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null) {
			Debug.Log ("sound doesn't exist!!!");
			return;
		}
		s.source.Play();
		//Debug.Log("playing" + name);
	}
	public void Stop(string name)
	{	
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null) {
			Debug.Log ("sound doesn't exist!!!");
			return;
		}
		s.source.Stop();

	}
IEnumerator playTheme()
	{
		FindObjectOfType<AudioManager>().play("start");
		yield return new WaitForSeconds(FindObjectOfType<AudioManager>().findAudio("start").clip.length-1.5f);
		FindObjectOfType<AudioManager>().play("theme");
	}
}
