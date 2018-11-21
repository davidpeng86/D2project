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
			s_Database.CrownCount +=1;
			this.gameObject.SetActive(false);
			for(int i =0;i<=s_Database.Crown.Length-1;i++)
			{
				s_Database.CrownCheck[i] = s_Database.Crown[i].activeSelf;
			}
		}
	}
}
