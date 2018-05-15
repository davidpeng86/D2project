using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;
    float distanceX;
    float distanceY;
    float posXR;
    float posXL;
    public GameObject Player;

    // Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        distanceX = transform.position.x - Player.transform.position.x;
        
        posXR = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x, ref velocity.x, smoothTimeX);
        posXL = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x+10.15f , ref velocity.x, smoothTimeX);
        if (Player.GetComponent<Player>().direction)
        {
            if (Player.transform.position.x > transform.position.x)
            {
                transform.position = new Vector3(posXR, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (Player.transform.position.x < transform.position.x - 10.15f)
                transform.position = new Vector3(posXL, transform.position.y, transform.position.z);
        }
        if (transform.position.y <Player.transform.position.y+2.82f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.SmoothDamp(transform.position.y, Player.transform.position.y+2.82f, ref velocity.y, smoothTimeY), transform.position.z);
        }
        if (transform.position.y > Player.transform.position.y+1.82f )
        {
            transform.position = new Vector3(transform.position.x, Mathf.SmoothDamp(transform.position.y, Player.transform.position.y+1.82f, ref velocity.y, smoothTimeY), transform.position.z);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            if (Player.GetComponent<Player>().direction)
            {
                transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, Player.transform.position.x+8.0f, ref velocity.x, smoothTimeX), transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x , Player.transform.position.x, ref velocity.x, smoothTimeX), transform.position.y, transform.position.z);
            }

        }
    }
}
