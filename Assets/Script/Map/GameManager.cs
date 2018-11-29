using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public DataBase s_Database;
	public spawn generator;
	GameObject o_player;
	public GameObject[] SavePoint;
	GameObject[] lasers;
	void Start () {
		lasers = GameObject.FindGameObjectsWithTag("Laser");
		o_player = GameObject.FindGameObjectWithTag("Player");
		
	}
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1))
		{
			o_player.transform.position = SavePoint[SavePoint.Length-1].transform.position;
		}
		if(Input.GetKeyDown(KeyCode.F2))
		{
			MaxCubePlus();
		}

	}
	void MaxCubePlus()
	{
		s_Database.maxCube +=1;
		generator.SpawningCube = new SpawningBlocks[(int)s_Database.maxCube];
		for(int i = 0;i<lasers.Length;i++)
		{
			if(lasers[i]!=null)
			lasers[i].GetComponent<Laser>().AddLength();
		}
	}
}
