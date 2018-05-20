using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MaxCube : MonoBehaviour {
    public Text text;
    GameObject Generator;
    // Use this for initialization
    void Start () {
        Generator = GameObject.FindGameObjectWithTag("GameController");
        text.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "方塊數量限制:" + Generator.GetComponent<spawn>().maxCube;
	}
}
