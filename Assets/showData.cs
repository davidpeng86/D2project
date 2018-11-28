using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class showData : MonoBehaviour {
	public Text cube;
	public Text crown;
    public DataBase s_Database;
	Scene scene;

	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene();
	}
	
	// Update is called once per frame
	void Update () {
		cube.text = "Cubes used: " + s_Database.UsedCube;
		crown.text = s_Database.CrownCount.ToString() + "   /  " + s_Database.Crown.Length;
	}

	public void resume(){}
	
	public void restart(){
		SceneManager.LoadScene(scene.name);
	}
	public void exit(){
		SceneManager.LoadScene("Start");
	}
	public void nextScene(){}

}
