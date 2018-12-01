using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class showData : MonoBehaviour {
	public Text cube;
	public Text crown;

	public sceneManager sceneManager;
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
	
	public void restart(){
		sceneManager.sceneChange(scene.name);
	}
	public void exit(){
		sceneManager.sceneChange("Start");
	}
	public void nextScene(){
		sceneManager.sceneChange(
			NameFromIndex(SceneManager.GetActiveScene().buildIndex + 1));
	}

	public void disablethis(){
		this.gameObject.SetActive(false);
	}

	 private static string NameFromIndex(int BuildIndex)
	{
		string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
		int slash = path.LastIndexOf('/');
		string name = path.Substring(slash + 1);
		int dot = name.LastIndexOf('.');
		return name.Substring(0, dot);
	}
}
