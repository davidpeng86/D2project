using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    GameObject generetor;
    bool check;
	// Use this for initialization
	void Start () {
        generetor = GameObject.FindGameObjectWithTag("GameController");
    }
	
	// Update is called once per frame
	void Update () {
        
        if (check)
        {
            float x = Mathf.MoveTowards(transform.rotation.y, 360, (360 - transform.rotation.y) * 0.05f);
            transform.Rotate(0,x,0);
            if (transform.rotation.y > 359)
            {
                generetor.GetComponent<spawn>().maxCube += 1;
                Destroy(this);
            }
        }
           
		
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            check = true;
           
        }
    }
}
