using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
	public DataBase s_Database;
	float distance =5;
	public bool CubeStay;
	public GameObject o_Player;
	public Transform lazer;
	private GameObject[] Cubes;
	void Start () {
		Cubes = new GameObject[(int)s_Database.maxCube];
	}
	void Update () {
		Cubes =GameObject.FindGameObjectsWithTag("generated");
	}

	void OnTriggerStay2D(Collider2D col)
	{
		lazer.localScale = new Vector3(1,distance,1);
		distance = 5;
		if(col.tag=="generated")
		{
			for(int i =0; i<Cubes.Length;i++)
			{
				if(Cubes[i]!=null)
				{
					if(Mathf.Abs((Cubes[i].transform.position.y-this.transform.position.y)) <1.0f)
					{
						if(((Cubes[i].transform.position.x) - this.transform.position.x)/1.28f<distance)
						{
							if(Cubes[i].transform.position.x - this.transform.position.x >0)
							{
								distance = (Mathf.Abs((Cubes[i].transform.position.x-0.64f) - this.transform.position.x)/1.28f);
							}
							else
							{
								distance = (Mathf.Abs((Cubes[i].transform.position.x+0.64f) - this.transform.position.x)/1.28f);
							}
						}
					}
				}
			}
			lazer.localScale = new Vector3(1,distance,1);
		}
	}
}
