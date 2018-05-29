using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;
    float distanceY;
    float posXR;
    float posXL;
    public GameObject Player;
    bool spawnMove;
    public Transform CameraTestX;


    // Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Debug.DrawLine(transform.position+new Vector3(0,8,0), transform.position + new Vector3(0, -8, 0),Color.green);
        Debug.DrawLine(CameraTestX.position + new Vector3(0, 8, 0), CameraTestX.position + new Vector3(0, -8, 0), Color.green);
        Debug.DrawLine(Player.transform.position + new Vector3(-10, 0.82f, 0), Player.transform.position + new Vector3(+10, 0.82f, 0), Color.red);
        Debug.DrawLine(Player.transform.position + new Vector3(-10, 1.82f, 0), Player.transform.position + new Vector3(+10, 1.82f, 0), Color.red);
        posXR = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x, ref velocity.x, smoothTimeX);
        posXL = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x+8 , ref velocity.x, smoothTimeX);
        if (Player.GetComponent<Player>().direction)
        {
            if (Player.transform.position.x > transform.position.x)
            {
                transform.position = new Vector3(posXR, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (Player.transform.position.x < transform.position.x - 8)
            {
                
                transform.position = new Vector3(posXL, transform.position.y, transform.position.z);
            }
        }
        if(transform.position.y>-5.5f)
        {
            if (Player.transform.position.y+1.82f<transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, Mathf.SmoothDamp(transform.position.y, Player.transform.position.y+1.82f, ref velocity.y, smoothTimeY), transform.position.z);
            }
            if (transform.position.y<Player.transform.position.y+0.82f)
            {
                transform.position = new Vector3(transform.position.x, Mathf.SmoothDamp(transform.position.y, Player.transform.position.y+0.82f, ref velocity.y, smoothTimeY), transform.position.z);
            }
        }
        
        
        
        
        
        if (Input.GetKey(KeyCode.Z))
        {
            spawnMove =true;
        }
        if(spawnMove)
        {
            if (Player.GetComponent<Player>().direction)
            {
                transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, Player.transform.position.x-5, ref velocity.x, smoothTimeX), transform.position.y, transform.position.z);
                if(transform.position.x<Player.transform.position.x+3 )
                {
                    spawnMove = false;
                }
            }
            else
            {
                transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, Player.transform.position.x-7, ref velocity.x, smoothTimeX), transform.position.y, transform.position.z);
                if(transform.position.x<Player.transform.position.x+1 )
                {
                    spawnMove = false;
                }
            }
        }
    }
}
