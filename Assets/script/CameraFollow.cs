using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;
    float distanceX;
    float distanceY;
    // Use this for initialization
    public GameObject Player;
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        distanceX = transform.position.x - Player.transform.position.x;
        distanceY = Mathf.Abs(transform.position.y - Player.transform.position.y);
        float posX = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(transform.position.x, posY+1 , transform.position.z);
        if (distanceX>4.5f)
        transform.position = new Vector3(Player.transform.position.x + 4.5f, transform.position.y, transform.position.z);
        if (distanceX < -4.5f)
        transform.position = new Vector3(Player.transform.position.x - 4.5f, transform.position.y, transform.position.z);


    }
}
