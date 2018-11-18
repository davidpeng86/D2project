using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownBlocks : MonoBehaviour {
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0,-5);
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector2(0,-5);
		
	}
}
