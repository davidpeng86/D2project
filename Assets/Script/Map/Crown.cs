using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour {
	public DataBase s_Database;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D Col)
	{

		if(Col.tag=="Player")
		{
			s_Database.Crown +=1;
			Destroy(this.gameObject);
		}
	}
}
