using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	public DataBase s_Database;
	public GameObject o_player;
	public spawn generator;
	public float distance ;
	public float length;
	public bool Horizontal;
	public bool Vertical;
	[Header("左到右.下到上 True")]
	public bool direction;
	public Transform lazer;
	public GameObject[] FallDownCubes;
	private GameObject[] Cubes;
	private Renderer rd;

	void Start () {
		rd = lazer.GetComponentInChildren<Renderer>();
		Cubes = new GameObject[(int)s_Database.maxCube];
	}
	void FixedUpdate () {
		distance = length;
		if(rd.isVisible)
		{
			if(generator.history.Count>1||generator.released)
			{
				Cubes =GameObject.FindGameObjectsWithTag("generated");
				if(Horizontal)
				{
					for(int i =0; i<Cubes.Length;i++)
					{
						if(Cubes[i]!=null)
						{
							if(Mathf.Abs((Cubes[i].transform.position.y-this.transform.position.y)) <1.0f)
							{
								if(direction)
								{
									if((Cubes[i].transform.position.x-this.transform.position.x)/1.28f < distance)
									{
										distance = (Cubes[i].transform.position.x - 0.62f - this.transform.position.x)/1.28f;
										 if(distance<=-0.2f)
										{
											distance = length;
										}
									}
								}
								else
								{
									if((this.transform.position.x - Cubes[i].transform.position.x)/1.28f<distance)
									{
										distance = ((this.transform.position.x-Cubes[i].transform.position.x-0.62f))/1.28f;
										if(distance<=-0.2f)
										{
											Debug.Log(" ");
											distance = length;
										}
									}
								}
							}
						}
					}
					lazer.localScale = new Vector3(1,distance,1);
				}
				else if(Vertical)
				{
					for(int i =0; i<Cubes.Length;i++)
					{
						if(Cubes[i]!=null)
						{
							if(Mathf.Abs((Cubes[i].transform.position.x-this.transform.position.x)) <0.9f)
							{
								if(direction)
								{
									if((Cubes[i].transform.position.y-this.transform.position.y)/1.28f < distance)
									{
										distance = (Cubes[i].transform.position.y - 0.64f - this.transform.position.y)/1.28f;
										if(distance<=-0.2f)
										{
											distance = length;
										}
									}
								}
								else
								{
									if((this.transform.position.y - Cubes[i].transform.position.y)/1.28f<distance)
									{
										distance = ((this.transform.position.y-Cubes[i].transform.position.y-0.64f))/1.28f;
										if(distance<=-0.2f)
										{
											distance = length;
										}
									}
								}
							}
						}
					}
					lazer.localScale = new Vector3(1,distance,1);
				}

			}
			else
			{
				lazer.localScale = new Vector3(1,distance,1);
			}
			FallDownCube();
		}
	}
	void FallDownCube()
	{
		if(FallDownCubes.Length!=0)
		{
			if(Horizontal)
			{
				for(int i=0;i<FallDownCubes.Length;i++)
				{
					if(Mathf.Abs((FallDownCubes[i].transform.position.y-this.transform.position.y)) <1.0f)
					{
						if(direction)
						{
							if((FallDownCubes[i].transform.position.x-this.transform.position.x)/1.28f < distance)
							{
								distance = (FallDownCubes[i].transform.position.x - 0.64f - this.transform.position.x)/1.28f;
								if(distance<=-0.1f)
								{
									distance = length;
								}
							}
						}
						else
						{
							if((this.transform.position.x - FallDownCubes[i].transform.position.x)/1.28f<distance)
							{
								distance = ((this.transform.position.x-FallDownCubes[i].transform.position.x-0.64f))/1.28f;
								if(distance<=-0.1f)
								{
									distance = length;
								}
							}
						}
					}
				}
			}
			else if(Vertical)
			{
				 for(int i =0; i<FallDownCubes.Length;i++)
				{
					if(Mathf.Abs((FallDownCubes[i].transform.position.x-this.transform.position.x)) <0.9f)
					{
						if(direction)
						{
							if((FallDownCubes[i].transform.position.y-this.transform.position.y)/1.28f < distance)
							{
								distance = (FallDownCubes[i].transform.position.y - 0.64f - this.transform.position.y)/1.28f;
								if(distance<=-0.1f)
								{
									distance = length;
								}
							}
						}
						else
						{
							if((this.transform.position.y - FallDownCubes[i].transform.position.y)/1.28f<distance)
							{
								distance = ((this.transform.position.y-FallDownCubes[i].transform.position.y-0.64f))/1.28f;
								if(distance<=-0.1f)
								{
									distance = length;
								}
							}
						}
					}
				}
			}
			lazer.localScale = new Vector3(1,distance,1);
		}
	}




	public void AddLength()
	{
		Cubes = new GameObject[(int)s_Database.maxCube];
	}











}
