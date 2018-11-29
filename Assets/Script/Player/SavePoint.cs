using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {
	public DataBase s_Database;
 	public float CrownCount;
 	public float maxCube = 4;
 	public float UsedCube;
	public bool[] CrownCheck;
	void Start () {
		CrownCheck = new bool[s_Database.Crown.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D Col)
	{
		if(Col.tag=="Player" && s_Database.SavePoint!=this.transform.position)
		{
			s_Database.SavePoint =this.transform.position;
			CrownCount = s_Database.CrownCount;
			maxCube = s_Database.maxCube;
			UsedCube = s_Database.UsedCube;
			s_Database.theMostCloseSavePoint = this.gameObject;
			for(int i=0 ; i<CrownCheck.Length;i++)
			{
				CrownCheck[i]=s_Database.CrownCheck[i];
			}
		}
		
	}
}
