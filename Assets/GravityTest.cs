using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTest : MonoBehaviour {
    public GameObject o_Player;
    public spawn spawn;
    private bool Exist; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (o_Player.GetComponent<Rigidbody2D>().gravityScale > 0)
        {
            o_Player.transform.localScale = new Vector3(o_Player.transform.localScale.x, 1, 1);
        }
        else if (o_Player.GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            o_Player.transform.localScale = new Vector3(o_Player.transform.localScale.x, -1, 1);
        }
            if (spawn.released)
        {
            if (!Exist)
            {
                o_Player.GetComponent<Rigidbody2D>().gravityScale = 3;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "generated" && o_Player.GetComponent<Player>()._state == Player.PlayerState.s_spawning && !spawn.movingTolastCube)
        {
            o_Player.GetComponent<Rigidbody2D>().gravityScale = 3;

            //o_Player.transform.localScale = new Vector3(o_Player.transform.localScale.x, 1, 1);
        }
        if (col.tag == "Player"  &&!spawn.movingTolastCube)
        {
            Exist = false;
            col.GetComponent<Rigidbody2D>().gravityScale = 3;
            //o_Player.transform.localScale = new Vector3(o_Player.transform.localScale.x, 1, 1);

        }
        if (col.tag == "generated" && !spawn.movingTolastCube)
        {
            if (col.transform.parent.tag =="Blocks")
            {
                col.GetComponentInParent<Rigidbody2D>().gravityScale = 3;
            }
        }

    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "generated" && o_Player.GetComponent<Player>()._state == Player.PlayerState.s_Holdingidle ||
            col.tag == "generated" && o_Player.GetComponent<Player>()._state == Player.PlayerState.s_groundedHoldingidle ||
            col.tag == "generated" && o_Player.GetComponent<Player>()._state == Player.PlayerState.s_spawning)
        {
            o_Player.GetComponent<Rigidbody2D>().gravityScale = -3;
            //o_Player.transform.localScale = new Vector3(o_Player.transform.localScale.x, -1, 1);
        }
        if (col.tag == "generated" &&!spawn.movingTolastCube)
        {
            col.GetComponentInParent<Rigidbody2D>().gravityScale = -3;
        }

         if (col.tag == "Player" && !spawn.movingTolastCube)
         {
             Exist = true;
             col.GetComponent<Rigidbody2D>().gravityScale = -3;
             //col.transform.localScale = new Vector3(col.transform.localScale.x, -1, 1);
         }
         

    }
}
