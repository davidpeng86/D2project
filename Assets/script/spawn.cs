﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class child
{
	public child (string s, Transform t)
	{
		direction = s;
		spawn_position = t;
	}

	public string direction { get; set; }

	public Transform spawn_position { get; set; }
}

public class spawn : MonoBehaviour
{
	Player Player;
	//Player的Script
	[Header ("方塊伸出數量限制")]
	public float maxCube = 1;
	[Header ("方塊使用數量限制")]
	public float maxUsedcube = 30;
	[Range (0, 2.0f)]
	[Header ("感應四周距離")]
	public float distance;
	[Header ("偵測四周射線起點")]
	public Transform prefab;
	public List<child> history;
	//紀錄方塊位置的表單
	public GameObject[] gen;
	//用來儲存丟出的方塊
	public GameObject player;
	public LayerMask cubeLayer;
	public LayerMask groundLayer;
	public bool cube_exist = false;
	//紀錄方塊的開關
	public bool cubeCheck = true;
	//判定是否有方塊伸出
	public bool movingTolastCube = false;
	//是否處於正在移動至最後一個方塊的狀態
	public bool released;
	//方塊是否丟出
	int cubeCount = 0;
	//移動至最後一個方塊的函式 的計步器
	public bool is_spawning;
	//生成或回收的動畫冷卻
	bool CanMoveCheck = false;
	//重置移動至最後一個方塊的函式的變數
	public bool spawnCheck = true;
	//已生成後得丟出或移動至最後一個之後才能再生成
	public GameObject UpSign;
	public GameObject DownSign;
	public GameObject LeftSign;
	public GameObject RightSign;

	bool upCubecheck {
		get {
			Vector3 start = this.transform.position + new Vector3 (0, 0.635f, 0);
			Vector3 end = new Vector3 (start.x, start.y + distance, this.transform.position.z);
			Debug.DrawLine (start, end, Color.blue);
			if (Physics2D.Linecast (start, end, cubeLayer)) {
				return true;
			} else
				return false;
		}
	}

	bool downCubecheck {
		get {
			Vector3 start = this.transform.position + new Vector3 (0, -0.635f, 0);
			Vector3 end = new Vector3 (start.x, start.y - distance, this.transform.transform.position.z);
			Debug.DrawLine (start, end, Color.blue);
			if (Physics2D.Linecast (start, end, cubeLayer)) {
				return true;
			} else
				return false;
		}
	}

	bool leftCubecheck {
		get {
			Vector3 start = this.transform.position + new Vector3 (-0.635f, 0, 0);
			Vector3 end = new Vector3 (start.x - distance, start.y, this.transform.position.z);
			Debug.DrawLine (start, end, Color.blue);
			if (Physics2D.Linecast (start, end, cubeLayer)) {
				return true;
			} else
				return false;
            
		}
	}

	bool rightCubecheck {
		get {
			Vector3 start = this.transform.position + new Vector3 (0.635f, 0, 0);
			Vector3 end = new Vector3 (start.x + distance, start.y, this.transform.position.z);
			Debug.DrawLine (start, end, Color.blue);
			if (Physics2D.Linecast (start, end, cubeLayer)) {
				return true;
			} else
				return false;
		}
	}

	bool Upwallchecker {
		get {
			Vector3 start = this.transform.position + new Vector3 (0, 0.635f, 0);
			Vector3 end = new Vector3 (start.x, start.y + distance, this.transform.position.z);
			Debug.DrawLine (start, end, Color.red);
			if (Physics2D.Linecast (start, end, groundLayer)) {
				return true;
			} else {
				return false;
			}
		}
	}

	bool Downwallchecker {
		get {
			Vector3 start = this.transform.position + new Vector3 (0, -0.635f, 0);
			Vector3 end = new Vector3 (start.x, start.y - distance, this.transform.position.z);
			Debug.DrawLine (start, end, Color.red);
			if (Physics2D.Linecast (start, end, groundLayer)) {
				return true;
			} else {
				return false;
			}
		}
	}

	bool Leftwallchecker {
		get {
			Vector3 start = this.transform.position + new Vector3 (-0.635f, 0, 0);
			Vector3 end = new Vector3 (start.x - distance, start.y, this.transform.position.z);
			Debug.DrawLine (start, end, Color.red);
			if (Physics2D.Linecast (start, end, groundLayer)) {
				return true;
			} else {
				return false;
			}
		}
	}

	bool Rightwallchecker {
		get {
			Vector3 start = this.transform.position + new Vector3 (0.635f, 0, 0);
			Vector3 end = new Vector3 (start.x + distance, start.y, this.transform.position.z);
			Debug.DrawLine (start, end, Color.red);
			if (Physics2D.Linecast (start, end, groundLayer)) {
				return true;
			} else {
				return false;
			}
		}
	}



	void Start ()
	{
		is_spawning = false;
		history = new List<child> ();
		history.Add (new child ("empty", null));
	}

	// Update is called once per frame
	void Update ()
	{
		directionCheck ();
		switch (player.GetComponent<Player> ()._state) {
		case Player.PlayerState.s_idle:
			ClearArrowSign ();
			break;
		case Player.PlayerState.s_moving:
			ClearArrowSign ();
			break;
		case Player.PlayerState.s_jumping:
			
			break;
		case Player.PlayerState.s_spawning:
			Spawncube ();
			ArrowSign ();
			break;
		case Player.PlayerState.s_Holdingidle:
			ClearArrowSign ();
			ThrowCube ();
			PlayerMovetoLast ();
			break;
		case Player.PlayerState.s_groundedHoldingidle:
			ClearArrowSign ();
			ThrowCube ();
			PlayerMovetoLast ();
			break;

		case Player.PlayerState.s_movingTolastCube:
			PlayerMovetoLast ();
			break;
		}
		//判定是否有方塊伸出，可不可以丟出來
		if (history.Count - 1 > 0) {
			cubeCheck = true;
		} else {
			cubeCheck = false;
		}

	}




	//方塊伸出.收回.防回堵
	void Spawncube ()
	{
		//伸出方塊 同時判定周邊是否可以伸出方塊 且伸出時刪除既有丟出方塊
		if (Input.GetKey (KeyCode.Z) && spawnCheck) {
            

			if (Input.GetKeyDown (KeyCode.UpArrow) && is_spawning == false && upCubecheck == false && (history.Count - 1) < maxCube && player.GetComponent<Player> ().Downwallchecker == false ||
			    Input.GetKeyDown (KeyCode.UpArrow) && is_spawning == false && upCubecheck == false && (history.Count - 1) < maxCube && Upwallchecker == false) {   

				if (released) {
					Destroy (GameObject.Find ("blocks"));
				}
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				StartCoroutine (Move (child, "up"));
				Move_self ("up");
				if (cube_exist == false) {
					record2history ("up", child);
				}


			}
			if (Input.GetKeyDown (KeyCode.DownArrow) && is_spawning == false && downCubecheck == false && (history.Count - 1) < maxCube && player.GetComponent<Player> ().Upwallchecker == false ||
			    Input.GetKeyDown (KeyCode.DownArrow) && is_spawning == false && downCubecheck == false && (history.Count - 1) < maxCube && Downwallchecker == false) {
				if (released) {
					Destroy (GameObject.Find ("blocks"));
				}
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				StartCoroutine (Move (child, "down"));
				Move_self ("down");
				if (cube_exist == false) {
					record2history ("down", child);
				}
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow) && is_spawning == false && leftCubecheck == false && (history.Count - 1) < maxCube && player.GetComponent<Player> ().Rightwallchecker == false ||
			    Input.GetKeyDown (KeyCode.LeftArrow) && is_spawning == false && leftCubecheck == false && (history.Count - 1) < maxCube && Leftwallchecker == false) {
				if (released) {
					Destroy (GameObject.Find ("blocks"));
				}
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				StartCoroutine (Move (child, "left"));
				Move_self ("left");
				if (cube_exist == false) {
					record2history ("left", child);
				}
			}
			if (Input.GetKeyDown (KeyCode.RightArrow) && is_spawning == false && rightCubecheck == false && (history.Count - 1) < maxCube && player.GetComponent<Player> ().Leftwallchecker == false ||
			    Input.GetKeyDown (KeyCode.RightArrow) && is_spawning == false && rightCubecheck == false && (history.Count - 1) < maxCube && Rightwallchecker == false) {
				if (released) {
					Destroy (GameObject.Find ("blocks"));
				}
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				StartCoroutine (Move (child, "right"));
				Move_self ("right");
				if (cube_exist == false) {
					record2history ("right", child);
				}
			}

			//回收方塊

			if (Input.GetKeyDown (KeyCode.UpArrow) && is_spawning == false && upCubecheck && history.ElementAt (history.Count - 1).direction == "down") {
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				StartCoroutine (Move (child, "up"));
				Move_self ("up");
				if (cube_exist == false) {
					record2history ("up", child);
				}

			}
			if (Input.GetKeyDown (KeyCode.DownArrow) && is_spawning == false && downCubecheck && history.ElementAt (history.Count - 1).direction == "up") {
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				StartCoroutine (Move (child, "down"));
				Move_self ("down");
				if (cube_exist == false) {
					record2history ("down", child);
				}
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow) && is_spawning == false && leftCubecheck && history.ElementAt (history.Count - 1).direction == "right") {
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				StartCoroutine (Move (child, "left"));
				Move_self ("left");
				if (cube_exist == false) {
					record2history ("left", child);
				}
			}
			if (Input.GetKeyDown (KeyCode.RightArrow) && is_spawning == false && rightCubecheck && history.ElementAt (history.Count - 1).direction == "left") {
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				StartCoroutine (Move (child, "right"));
				Move_self ("right");
				if (cube_exist == false) {
					record2history ("right", child);
				}
			}
		}
	}


	//方塊丟出
	void ThrowCube ()
	{
        

		if (Input.GetKeyUp (KeyCode.Z) && cubeCheck == true) {
			spawnCheck = false;
		}
		if (Input.GetKeyDown (KeyCode.Z) && cubeCheck) {
            

			spawnCheck = true;
			//StopAllCoroutines();
			StartCoroutine (landingSound ());
			released = true;
			gen = GameObject.FindGameObjectsWithTag ("generated");
			GameObject group = new GameObject ("blocks");
			for (int i = 0; i < gen.Length; i++) {
				gen [i].transform.parent = group.transform;
			}
			group.AddComponent<Rigidbody2D> ();
			group.AddComponent<Blocks> ();
			if (player.GetComponent<Player> ().direction == true) {
				group.GetComponent<Rigidbody2D> ().velocity = new Vector2 (10, 3.5f);
			} else {
				group.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-10, 3.5f);
			}
			group.GetComponent<Rigidbody2D> ().freezeRotation = true;
			group.GetComponent<Rigidbody2D> ().gravityScale = 2;
			for (int i = history.Count - 1; i > 0; i--) {
				history.RemoveAt (i);
			}
			transform.position = transform.parent.position;
		}
	}


    
	IEnumerator landingSound ()
	{
		yield return new WaitForSeconds (0.05f);
		//play audio
		FindObjectOfType<AudioManager> ().play ("cube_put");
		StopCoroutine (landingSound ());
	}




	void directionCheck ()
	{
		if (player.transform.localScale.x == 1) {
			transform.localScale = new Vector3 (1, 1, 1);
		} else if (player.transform.localScale.x == -1) {
			transform.localScale = new Vector3 (-1, 1, 1);
		}
	}





	//紀錄list
	void record2history (string direction, Transform child_position)
	{
		history.Add (new child (direction, child_position));
	}



	//generator位置控制
	void Move_self (string dir)
	{
		Vector3 position = transform.position;
		switch (dir) {
		case "up":
			position.y += 1.28f;
			break;

		case "down":
			position.y -= 1.28f;
			break;

		case "left":
			position.x -= 1.28f;
			break;

		case "right":
			position.x += 1.28f;
			break;
		}
		transform.position = position;
	}



	//玩家移動至最後一個方塊
	void PlayerMovetoLast ()
	{   
		Rigidbody2D rb = player.GetComponent<Rigidbody2D> ();

		if (Input.GetKeyDown (KeyCode.X) && cubeCheck && movingTolastCube == false) {//在已確認可移動的情況下 按下X後開啟移動至最後一個方塊的StartCoroutine 並且關閉重力以及所生成出的方塊的碰撞
			movingTolastCube = true;
			CanMoveCheck = true;
			cubeCheck = false;
			cubeCount = 0;//cubecount是記錄Player已走到第幾個方塊 
			rb.gravityScale = 0;
			rb.velocity = Vector2.zero;
			player.GetComponent<Collider2D> ().isTrigger = true;
			gen = GameObject.FindGameObjectsWithTag ("generated");
			for (int i = 0; i < gen.Length; i++) {
				gen [i].transform.parent = null;
				gen [i].GetComponent<Collider2D> ().isTrigger = true;
			}

            
			transform.position = player.transform.position;
		}
		if (cubeCount == history.Count - 1) {//當移動至最後一個方塊後"移動至最後一個方塊"的狀態解除,重設重力並且關閉StartCoroutine,最後把List內的所有元素刪除
			spawnCheck = true;
			movingTolastCube = false;
			cubeCount = 0;
			rb.gravityScale = 3;
			CanMoveCheck = false;
			player.GetComponent<Collider2D> ().isTrigger = false;
			for (int i = history.Count - 1; i > 0; i--) {
				history.RemoveAt (i);
			}
		}
		if (CanMoveCheck) {//開始StartCoroutine
			StartCoroutine (MoveToLast (history.ElementAt (cubeCount + 1).direction, cubeCount));
		}

	}


	//生成方塊移動控制
	IEnumerator Move (Transform T, string I)
	{
		float F = 0f;
		float spawn_speed = 0.0853f;
		int n = history.Count - 1;
		Vector3 V3 = Vector3.zero;
		// play sound (AudioManager來自start scene,需要從startscene 開始才存取的到)
		FindObjectOfType<AudioManager> ().play ("cube_born");

		switch (I) {
		case "up":
			V3 = Vector3.up * spawn_speed;
			if (n != 0 && history.ElementAt (n).direction == "down") { //destroy last cube
				Destroy (history.ElementAt (n).spawn_position.gameObject);
				history.RemoveAt (n);
				cube_exist = true;
			}
			break;

		case "down":
			V3 = Vector3.down * spawn_speed;
			if (n != 0 && history.ElementAt (n).direction == "up") {
				Destroy (history.ElementAt (n).spawn_position.gameObject);
				history.RemoveAt (n);
				cube_exist = true;
			}


			break;

		case "left":
			V3 = Vector3.left * spawn_speed;
			if (n != 0 && history.ElementAt (n).direction == "right") {
				Destroy (history.ElementAt (n).spawn_position.gameObject);
				history.RemoveAt (n);
				cube_exist = true;
			}

			break;

		case "right":
			V3 = Vector3.right * spawn_speed;
			if (n != 0 && history.ElementAt (n).direction == "left") {
				Destroy (history.ElementAt (n).spawn_position.gameObject);
				history.RemoveAt (n);
				cube_exist = true;
			}

			break;
		}
		while (F < 1.28f) {
			F += spawn_speed;
			T.position += V3;
			yield return new WaitForSeconds (0.01f);
		}

		T.position = transform.position;
		is_spawning = false;

		if (cube_exist == true) {
			Destroy (T.gameObject);
			cube_exist = false;
		}
		StopCoroutine (Move (T, I));
	}


	//移動至最後一個方塊
	IEnumerator MoveToLast (string direction, int cube)
	{
		CanMoveCheck = false;
		float moveTime = 0;
		float moveSpeed = 0.128f;
		Vector3 directionCheck = Vector3.zero;
		switch (direction) {//判斷下一顆方塊位置給予不同方向的位移量
		case "up":
			directionCheck = Vector3.up * moveSpeed;
			break;
		case "down":
			directionCheck = Vector3.down * moveSpeed;
			break;
		case "left":
			directionCheck = Vector3.left * moveSpeed;
			break;
		case "right":
			directionCheck = Vector3.right * moveSpeed;
			break;
		}
		while (moveTime < 1.28f) {
			moveTime += moveSpeed;
			player.transform.Translate (directionCheck);
			yield return new WaitForSeconds (0.01f);
		}
		Destroy (gen [cubeCount]);//刪除已移動過後的軌跡方塊
		CanMoveCheck = true;//重啟StartCoroutine直至已移動至最後一個方塊
		cubeCount += 1;//計算移動至第幾塊方塊
		StopCoroutine (MoveToLast (direction, cube));
	}
    



	//可生成方向指示
	private void ArrowSign ()
	{
		if (is_spawning == false && upCubecheck == false && (history.Count - 1) < maxCube && player.GetComponent<Player> ().Downwallchecker == false&&spawnCheck ||
			is_spawning == false && upCubecheck == false && (history.Count - 1) < maxCube && Upwallchecker == false &&spawnCheck||
			is_spawning == false && upCubecheck && history.ElementAt (history.Count - 1).direction == "down"&&spawnCheck) 
		{
			UpSign.SetActive(true);
		} 
		else 
		{
			UpSign.SetActive (false);
		}

		if (is_spawning == false && downCubecheck == false && (history.Count - 1) < maxCube && player.GetComponent<Player> ().Upwallchecker == false &&spawnCheck||
			is_spawning == false && downCubecheck == false && (history.Count - 1) < maxCube && Downwallchecker == false&&spawnCheck||
			is_spawning == false && downCubecheck && history.ElementAt (history.Count - 1).direction == "up"&&spawnCheck) 
		{
			DownSign.SetActive (true);
		}
		else
		{
			DownSign.SetActive (false);
		}

		if (is_spawning == false && leftCubecheck == false && (history.Count - 1) < maxCube && player.GetComponent<Player> ().Rightwallchecker == false&&spawnCheck ||
			is_spawning == false && leftCubecheck == false && (history.Count - 1) < maxCube && Leftwallchecker == false&&spawnCheck||
			is_spawning == false && leftCubecheck && history.ElementAt (history.Count - 1).direction == "right" &&spawnCheck) 
		{
			LeftSign.SetActive (true);
		}
		else
		{
			LeftSign.SetActive (false);
		}
		if (is_spawning == false && rightCubecheck == false && (history.Count - 1) < maxCube && player.GetComponent<Player> ().Leftwallchecker == false &&spawnCheck||
			is_spawning == false && rightCubecheck == false && (history.Count - 1) < maxCube && Rightwallchecker == false&&spawnCheck||
			is_spawning == false && rightCubecheck && history.ElementAt (history.Count - 1).direction == "left"&&spawnCheck) 
		{
			RightSign.SetActive (true);
		}
		else
		{
			RightSign.SetActive (false);
		}

	}
	private void ClearArrowSign()
	{
		UpSign.SetActive (false);
		DownSign.SetActive (false);
		LeftSign.SetActive (false);
		RightSign.SetActive (false);
	}
}
