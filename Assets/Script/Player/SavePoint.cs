using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {
	public DataBase s_Database;
 	public float AchievementCount;
 	public float maxCube = 4;
 	public float UsedCube;
	public bool[] AchievementCheck;
	void Start () {
		AchievementCheck = new bool[s_Database.Achievement.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D Col)
	{
		if(Col.tag=="Player" && s_Database.SavePoint!=this.transform.position)
		{
			s_Database.SavePoint =this.transform.position;
			AchievementCount = s_Database.AchievementCount;
			maxCube = s_Database.maxCube;
			UsedCube = s_Database.UsedCube;
			s_Database.theMostCloseSavePoint = this.gameObject;
			for(int i=0 ; i<AchievementCheck.Length;i++)
			{
				AchievementCheck[i]=s_Database.AchievementCheck[i];
			}
		}
		
	}
}
