using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCubePlus : MonoBehaviour {
	public DataBase s_Database;
	public spawn generator;
	GameObject[] lasers;

	public Animator ani;
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
				if(lasers[i]!=null)
				lasers[i].GetComponent<Laser>().AddLength();
			}
			ani.SetBool("get",true);
			StartCoroutine(waitAniEnd());
			FindObjectOfType<AudioManager> ().play ("MaxCubePlus");

		}
	}

	IEnumerator waitAniEnd(){
		yield return new WaitForSeconds(0.1f);
		ani.SetBool("get",false);
		Destroy(this.gameObject);
	}
}
