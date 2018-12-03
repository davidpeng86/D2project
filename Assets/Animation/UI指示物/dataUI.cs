using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dataUI : MonoBehaviour {
	public DataBase data;
	public Text leaf;
	public Text cube;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cube.text = data.maxCube.ToString();
		leaf.text = data.AchievementCount.ToString();
	}
}
