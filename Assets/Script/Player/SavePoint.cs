using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {
	public DataBase s_Database;
 	public float Crown;
 	public float maxCube = 4;
 	public float maxUsedcube;
 	public float UsedCube;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D Col)
	{
		if(Col.tag=="Player" && s_Database.SavePoint!=this.transform.position)
		{
			s_Database.SavePoint =this.transform.position;
			Crown = s_Database.Crown;
			maxCube = s_Database.maxCube;
			maxUsedcube = s_Database.maxUsedcube;
			UsedCube = s_Database.UsedCube;
			s_Database.theMostCloseSavePoint = this.gameObject;
		}
	}
}
