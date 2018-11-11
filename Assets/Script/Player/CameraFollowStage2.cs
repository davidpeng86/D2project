using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowStage2 : MonoBehaviour {

	// Use this for initialization
private Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;
    public float distanceXR =0;
    public float distanceXL = 8;
    public float distanceYU = 0;
    public float distanceYD = 0;
    float posXR;
    float posXL;
    float posYU;
    float posYD;
    public LayerMask groundLayer;
    public GameObject Player;
	public GameObject Generator;
    bool spawnMove;


    // Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Debug.DrawLine(Player.transform.position + new Vector3(1.28f*4, -0.64f,0),Player.transform.position + new Vector3(1.28f*4, -1.28f*4+0.64f, 0),Color.gray);
        Debug.DrawLine(transform.position + new Vector3(distanceXR, distanceYU,0),transform.position + new Vector3(distanceXR, -distanceYD, 0),Color.green);
		Debug.DrawLine(transform.position + new Vector3(-distanceXL, distanceYU, 0),transform.position + new Vector3(-distanceXL, -distanceYD, 0), Color.green);
        Debug.DrawLine(transform.position + new Vector3(-distanceXL, -distanceYD, 0),transform.position + new Vector3(distanceXR, -distanceYD, 0), Color.red);
        Debug.DrawLine(transform.position + new Vector3(-distanceXL, distanceYU, 0),transform.position + new Vector3(distanceXR, distanceYU, 0), Color.red);
        posXR = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x-distanceXR, ref velocity.x, smoothTimeX);
        posXL = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x+distanceXL , ref velocity.x, smoothTimeX);
        posYU = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y-distanceYU, ref velocity.y, smoothTimeY);
        posYD = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y+distanceYD, ref velocity.y, smoothTimeY);
         //右側邊界

        if (Player.transform.position.x > transform.position.x + distanceXR)
        {
            transform.position = new Vector3(posXR, transform.position.y, transform.position.z);
        }
        //左側邊界
        if (Player.transform.position.x < transform.position.x - distanceXL)
        {
                transform.position = new Vector3(posXL, transform.position.y, transform.position.z);
        }
        //上方邊界
        if (Player.transform.position.y > transform.position.y + distanceYU)
        {
            transform.position = new Vector3(transform.position.x, posYU, transform.position.z);
        }
        //下方邊界
		if(transform.position.y<=-2f)
		{
			transform.position = new Vector3(transform.position.x,-2, transform.position.z);
		}
		else if(transform.position.y>-2f)
		{
			if (Player.transform.position.y < transform.position.y-distanceYD)
			{
				transform.position = new Vector3(transform.position.x,posYD, transform.position.z);
			}
		}
        /* if(Physics2D.Linecast(Player.transform.position + new Vector3(distanceXR, 0,0),Player.transform.position + new Vector3(distanceXR, -3, 0),groundLayer))
        {
            

            
        }
        else if(!Physics2D.Linecast(Player.transform.position + new Vector3(1.28f*4, -0.64f,0),Player.transform.position + new Vector3(1.28f*4, -1.28f*4 +0.64f, 0),groundLayer))
        {
            Debug.Log("HI~~");
            transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x,Player.transform.position.x+distanceXL, ref velocity.x, smoothTimeY)
                                            ,Mathf.SmoothDamp(transform.position.y,Player.transform.position.y+distanceYU*2, ref velocity.y, smoothTimeY)
                                            ,transform.position.z);
        }*/
        
        
        
        
		/* if (Input.GetKey(KeyCode.Z) && Generator.GetComponent<spawn>().spawnCheck)
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
        }*/
    }
    public IEnumerator CameraShake(float duration, float magnitude)
	{
		Vector3 p_camera = transform.position;

		float time =0.0f;
		while(time < duration)
		{
			float x = Random.Range(-1f,1f)*magnitude;
			float y = Random.Range(-1f,1f)*magnitude;
			transform.position = new Vector3(p_camera.x+x,p_camera.y+y,p_camera.z);
			time +=Time.deltaTime; 
			yield return null;
		}
		transform.position = p_camera;
	}
}
