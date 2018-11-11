using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    public GameObject o_Player;
    public spawn spawn;
    public bool Exist = false;
    public bool CubeExist = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    
	void Update () {
        if (o_Player.GetComponent<Rigidbody2D>().gravityScale > 0)
        {
           o_Player.GetComponent<Animator>().SetBool("Reverse",false);
        }
        else if (o_Player.GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            o_Player.GetComponent<Animator>().SetBool("Reverse",true);
        } 
    }
    private void OnTriggerExit2D(Collider2D col)
    {   
        if (col.tag == "Player" )
        {
            Exist = false;
            col.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
        if (col.tag == "generated" && col.transform.parent!=null)
        {
            if (col.transform.parent.tag =="Blocks")
            {
                CubeExist = false;
                col.GetComponentInParent<Rigidbody2D>().gravityScale = 3;
            }
        }
        if (col.tag == "generated" && o_Player.GetComponent<Player>()._state == Player.PlayerState.s_spawning && !Exist)
        {
            o_Player.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
        if (col.tag == "generated" && !Exist && CubeExist && o_Player.GetComponent<Player>()._state == Player.PlayerState.s_Holdingidle )
        {
            o_Player.GetComponent<Rigidbody2D>().gravityScale = -3;
        }
        if (col.tag == "generated" && !Exist)
        {
            o_Player.GetComponent<Rigidbody2D>().gravityScale = 3;
        }

    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "generated")
        {
            CubeExist = true;
            col.GetComponentInParent<Rigidbody2D>().gravityScale = -3;
        }
        if (col.tag == "Player" )
        {
            Exist = true;
            col.GetComponent<Rigidbody2D>().gravityScale = -3;
        }

    }
}
