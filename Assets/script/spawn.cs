using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class spawn : MonoBehaviour {
	public Transform prefab;
	
	public GameObject[] gen;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				Vector3 position = transform.position;
				position.y += 1.35f;
				transform.position = position;
				Instantiate(prefab, this.transform.position, this.transform.rotation);
			}
		if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				Vector3 position = transform.position;
				position.y -= 1.35f;
				transform.position = position;
				Instantiate(prefab, this.transform.position, this.transform.rotation);
			}
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Vector3 position = transform.position;
			position.x -= 1.35f;
			transform.position = position;
			Instantiate(prefab, this.transform.position, this.transform.rotation);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			Vector3 position = transform.position;
			position.x += 1.35f;
			transform.position = position;
			Instantiate(prefab, this.transform.position, this.transform.rotation);
		}
		
		
		if(Input.GetKeyDown(KeyCode.F)){
			gen = GameObject.FindGameObjectsWithTag("generated");
			GameObject group = new GameObject("blocks");
			for (int i = 0; i < gen.Length; i++)
			{
				gen[i].transform.parent = group.transform;
			}
            group.AddComponent<Rigidbody>();
			group.transform.Translate(new Vector2(1.35f,0),Space.World);
		}
	}

	
}
