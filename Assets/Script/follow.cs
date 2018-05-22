using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class follow : MonoBehaviour {

	public Sprite jumpSprite;
	public Sprite normalSprite;
	public Transform[] marker = new Transform[2];
	Rigidbody2D player_rb;
	float x = 0f;
	int cubeWidth = 30;

	// No double jump
	bool is_Jump = false;

	bool is_Walk = true;
	
	// Use this for initialization
	void Start () {
		player_rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
		x = ((Input.mousePosition.x) - transform.position.x)/100;
		if(is_Walk == true){
			cubeDecision();
		}
		leapDecision();
		 
		/*if(Input.GetKeyDown(KeyCode.Space) && is_Jump == false){
			jump();
			FindObjectOfType<AudioManager>().play("jump");
		}*/

		if(x>0){
			transform.localScale = new Vector3(1f, 1, 1);
		}
		else if(x<0){
			transform.localScale = new Vector3(-1f, 1, 1);
		}
		/* 
		// if cube landed, can jump + can move
		if (this.transform.localPosition.y < -24){
				is_Jump = false;
				is_Walk = true;
		}
		else{
			is_Jump = true;
			is_Walk = false;
		}
		*/
	}

	void leapDecision(){

		if(transform.position.x>marker[1].position.x-30 && transform.position.x<marker[1].position.x&& is_Jump==false && x>0)
		{
			jump();
		}
		if(transform.position.x<marker[2].position.x+30 &&transform.position.x>marker[2].position.x&& is_Jump ==false && x<0)
		{
			jump();
		}
		if(transform.position.x>marker[3].position.x-30 && transform.position.x<marker[3].position.x&& is_Jump==false && x>0)
		{
			jump();
		}
		if(transform.position.x<marker[4].position.x+30 &&transform.position.x>marker[4].position.x&& is_Jump ==false && x<0)
		{
			jump();
		}
		/* 
		for(int i = 1; i < marker.Length-1; i++){
			if(i%2 == 1){
				if(transform.position.x>marker[i].position.x-30 &&transform.position.x < marker[i].position.x && is_Jump==false){
						jump();
				}
			}
			if(i%2 == 0){
				if(transform.position.x>marker[i].position.x+30 &&transform.position.x < marker[i+1].position.x && is_Jump==false)
				{ 
						jump();
				}
			}
		}
		*/
		
	}
	void cubeDecision(){
		for(int i = 0; i < marker.Length; i++){	
				if(i%2 == 0){
					// if between marker[i] & marker[i+1]
					if(transform.position.x>marker[i].position.x &&transform.position.x < marker[i+1].position.x){
						if((transform.position.x < marker[i].position.x+cubeWidth) && x>0 || // position.x < R_marker && mouse is at cube's right 
						transform.position.x > marker[i+1].position.x  - cubeWidth&&x<0|| // position.x > L_marker @@ mouse is at cube's left
						// position.x is between marker[i]& marker[i+1]
						transform.position.x > marker[i].position.x +cubeWidth && transform.position.x < marker[i+1].position.x-cubeWidth)
						{followmouse(x);}
					}
				}		
		}	
	}

	void followmouse(float x){
		transform.position = new Vector2(transform.position.x+x, transform.position.y);
	}

	void jump(){
		is_Jump = true;
		is_Walk = false;
		StartCoroutine(leap());
	}
	IEnumerator leap(){
		float t = 0;
		while (t<10){
			GetComponent<Image>().sprite = jumpSprite;
			t+=1;
			yield return new WaitForSeconds(0.001f);
		}
		FindObjectOfType<AudioManager>().play("jump");
		GetComponent<Image>().sprite = normalSprite;
		int dir;
		if(x > 0){
			dir = 1;
		}
		else if(x < 0){
			dir = -1;
		}
		else{
			dir = 0;
		}
		float h = 0f;
		float y = transform.localPosition.y;
		player_rb.bodyType = RigidbodyType2D.Kinematic;
		while(h <= 1*Mathf.PI){
			h += 5* Mathf.PI/100;
			transform.localPosition = new Vector2(transform.localPosition.x + 6*dir, y + Mathf.Sin(h) * 50);
			yield return new WaitForSeconds(0.01f);
		}
		player_rb.bodyType = RigidbodyType2D.Dynamic;
		is_Walk =true;
		is_Jump =false;
	}
}
