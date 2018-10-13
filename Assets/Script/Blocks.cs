using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
       
	}
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<Rigidbody2D>().velocity.y == 0 || this.GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
        }

		
	}
}
