using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneManager: MonoBehaviour{

	public Animator door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sceneChange(string sceneName ){
		StartCoroutine(wait(sceneName));
		
	}

	IEnumerator wait(string s){
		door.SetBool("transition", true);
		yield return new WaitForSeconds
		(door.GetCurrentAnimatorStateInfo(0).length+door.GetCurrentAnimatorStateInfo(0).normalizedTime);
		SceneManager.LoadScene(s);
	}
}
