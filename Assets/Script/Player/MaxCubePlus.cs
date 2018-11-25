using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCubePlus : MonoBehaviour {
	public DataBase s_Database;
	public spawn generator;
	GameObject[] lasers;
	// Use this for initialization
	void Start () {
		lasers = GameObject.FindGameObjectsWithTag("Laser");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D Col)
	{

		if(Col.tag=="Player")
		{
			s_Database.maxCube +=1;
			generator.SpawningCube = new SpawningBlocks[(int)s_Database.maxCube];
			for(int i = 0;i<lasers.Length;i++)
			{
				lasers[i].GetComponent<Laser>().AddLength();
			}
			Destroy(this.gameObject);
		}
	}
}
