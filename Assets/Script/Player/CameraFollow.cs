using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {
    [System.NonSerialized]
    public Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;
    public float distanceXR =0;
    public float distanceXL = 8;
    public float distanceYU = 0;
    public float distanceYD = 0;
    [System.NonSerialized]
    public float posXR;
    [System.NonSerialized]
    public float posXL;
    [System.NonSerialized]
    public float posYU;
    [System.NonSerialized]
    public float posYD;
    public GameObject Player;
	public GameObject Generator;
    Scene m_scene;


    // Use this for initialization

	void Start () {
        m_scene = SceneManager.GetActiveScene();
		
	}
	
	// Update is called once per frame
	public void FixedUpdate ()
    {
        CameraBorder();
        if(m_scene.name =="Tutorial")
        {
            CameraMove(1000,-2.76f,-27.4f,160.5f);
        }
        else if(m_scene.name == "Stage1")
        {
            CameraMove(1000,0,0.8f,1000);
        }
        else
        {
            CameraMove(6,-2,-1.8f,318.0f);
        }
        SpawningCamera();
    }
    void CameraBorder()
    {
        Debug.DrawLine(transform.position + new Vector3(distanceXR, distanceYU,0),transform.position + new Vector3(distanceXR, -distanceYD, 0),Color.green);
		Debug.DrawLine(transform.position + new Vector3(-distanceXL, distanceYU, 0),transform.position + new Vector3(-distanceXL, -distanceYD, 0), Color.green);
        Debug.DrawLine(transform.position + new Vector3(-distanceXL, -distanceYD, 0),transform.position + new Vector3(distanceXR, -distanceYD, 0), Color.red);
        Debug.DrawLine(transform.position + new Vector3(-distanceXL, distanceYU, 0),transform.position + new Vector3(distanceXR, distanceYU, 0), Color.red);
        posXR = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x-distanceXR, ref velocity.x, smoothTimeX);
        posXL = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x+distanceXL , ref velocity.x, smoothTimeX);
        posYU = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y-distanceYU, ref velocity.y, smoothTimeY);
        posYD = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y+distanceYD, ref velocity.y, smoothTimeY);

    }
    void CameraMove(float up,float down,float left,float right)
    {  
        if(transform.position.y>=up)
        {
            transform.position = new Vector3(transform.position.x,up, transform.position.z);
        }
        else
        {
            //上方邊界
            if (Player.transform.position.y > transform.position.y + distanceYU)
            {
                transform.position = new Vector3(transform.position.x, posYU, transform.position.z);
            }
        }
        //下方邊界
        if(transform.position.y<=down)
        {
            transform.position = new Vector3(transform.position.x,down, transform.position.z);
        }
        else
        {
            if (Player.transform.position.y < transform.position.y-distanceYD)
            {
                transform.position = new Vector3(transform.position.x,posYD, transform.position.z);
            } 
        }
        //右側邊界
        if(transform.position.x>=right)
        {
            transform.position = new Vector3(right,transform.position.y,transform.position.z);
        }
        else
        {
            if (Player.transform.position.x > transform.position.x + distanceXR)
            {
                transform.position = new Vector3(posXR, transform.position.y, transform.position.z);
            }
        }
        //左側邊界
        if(transform.position.x<=left)
        {
            transform.position = new Vector3(left,transform.position.y,transform.position.z);
        }
        else
        {
            if (Player.transform.position.x < transform.position.x - distanceXL)
            {
                transform.position = new Vector3(posXL, transform.position.y, transform.position.z);
            }
            
        }
    
    }
    void SpawningCamera()
    {
        if (Input.GetKey(KeyCode.Z) && Generator.GetComponent<spawn>().spawnCheck)
        {
            if(Player.GetComponent<Player>().direction)
            {
                distanceXL = 4;
                distanceXR =-4;
            }
            else
            {
                distanceXL =-4;
                distanceXR = 4;
            }
        }
        if (Input.GetKeyUp (KeyCode.Z))
        {
            distanceXL = 4;
            distanceXR = 0;
        }
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
