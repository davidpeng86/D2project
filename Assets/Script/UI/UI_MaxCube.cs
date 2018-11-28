using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MaxCube : MonoBehaviour {
    public Text text;
    public DataBase s_Database;
    // Use this for initialization
    void Start () {
        text.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = s_Database.maxCube.ToString();
	}
}
