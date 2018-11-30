using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crownShow : MonoBehaviour {
	public Text text;
    public DataBase s_Database;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		text.text = s_Database.CrownCount.ToString() + "   /  " + s_Database.Crown.Length;
	}
}
