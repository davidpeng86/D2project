using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class child {
	public child (string s, Transform t) {
		direction = s;
		spawn_position = t;
	}

	public string direction { get; set; }

	public Transform spawn_position { get; set; }
}

public class spawn : MonoBehaviour {
	public DataBase s_Database;
	[Range (0, 2.0f)]
	[Header ("感應四周距離")]
	public float distance;
	public Transform prefab;
	[Header ("紀錄方塊位置的表單 ")]
	public List<child> history;
	[Header ("用來儲存丟出的方塊")]
	private GameObject[] gen;
	private GameObject[] MovingTolastCube;
	public GameObject player;

	public LayerMask cubeLayer;
	public LayerMask groundLayer;
	//紀錄方塊的開關
	public bool cube_exist = false;
	[Header ("判定是否有方塊伸出")]
	public bool cubeCheck = true;
	[Header ("是否處於正在移動至最後一個方塊的狀態")]
	public bool movingTolastCube = false;
	[Header ("方塊是否丟出")]
	public bool released;
	//移動至最後一個方塊的函式 的計步器
	int cubeCount = 0;
	[Header ("生成或回收的動畫冷卻")]
	public bool is_spawning;
	//重置移動至最後一個方塊的函式的變數
	bool CanMoveCheck = false;
	[Header ("生成後得丟出或移動至最後一個的開關")]
	public bool spawnCheck = true;
	public bool MovingTolastCubeGroundCheck;
	public GameObject UpSign;
	public GameObject DownSign;
	public GameObject LeftSign;
	public GameObject RightSign;
	public SpawningBlocks[] SpawningCube;
	private Rigidbody2D rb;

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
			Vector3 start = this.transform.position + new Vector3 (0.55f, 0.6f, 0);
			Vector3 end = new Vector3 (start.x, start.y + distance, this.transform.position.z);
			Vector3 start2 = this.transform.position + new Vector3 (-0.55f, 0.6f, 0);
			Vector3 end2 = new Vector3 (start2.x, start2.y + distance, this.transform.position.z);
			Debug.DrawLine (start, end, Color.red);
			Debug.DrawLine (start2, end2, Color.red);
			if (Physics2D.Linecast (start, end, groundLayer)||Physics2D.Linecast (start2, end2, groundLayer)) {
				return true;
			} else {
				return false;
			}
		}
	}

	bool Downwallchecker {
		get {
			Vector3 start = this.transform.position + new Vector3 (0.55f, -0.6f, 0);
			Vector3 end = new Vector3 (start.x, start.y - distance, this.transform.position.z);
			Vector3 start2 = this.transform.position + new Vector3 (-0.55f, -0.6f, 0);
			Vector3 end2 = new Vector3 (start2.x, start2.y - distance, this.transform.position.z);
			Debug.DrawLine (start, end, Color.red);
			Debug.DrawLine (start2, end2, Color.red);
			if (Physics2D.Linecast (start, end, groundLayer)||Physics2D.Linecast (start2, end2, groundLayer)) {
				return true;
			} else {
				return false;
			}
		}
	}

	bool Leftwallchecker {
		get {
			Vector3 start = this.transform.position + new Vector3 (-0.6f, 0.55f, 0);
			Vector3 end = new Vector3 (start.x - distance, start.y, this.transform.position.z);
			Vector3 start2 = this.transform.position + new Vector3 (-0.6f, -0.55f, 0);
			Vector3 end2 = new Vector3 (start2.x - distance, start2.y, this.transform.position.z);
			Debug.DrawLine (start, end, Color.red);
			Debug.DrawLine (start2, end2, Color.red);
			if (Physics2D.Linecast (start, end, groundLayer)||Physics2D.Linecast (start2, end2, groundLayer)) {
				return true;
			} else {
				return false;
			}
		}
	}

	bool Rightwallchecker {
		get {
			Vector3 start = this.transform.position + new Vector3 (0.6f, 0.55f, 0);
			Vector3 end = new Vector3 (start.x + distance, start.y, this.transform.position.z);
			Vector3 start2 = this.transform.position + new Vector3 (0.6f, -0.55f, 0);
			Vector3 end2 = new Vector3 (start2.x + distance, start2.y, this.transform.position.z);
			Debug.DrawLine (start, end, Color.red);
			Debug.DrawLine (start2, end2, Color.red);
			if (Physics2D.Linecast (start, end, groundLayer)||Physics2D.Linecast (start2, end2, groundLayer)) {
				return true;
			} else {
				return false;
			}
		}
	}

	void Start () {
		rb = player.GetComponent<Rigidbody2D>();
		SpawningCube = new SpawningBlocks[(int)s_Database.maxCube];
		is_spawning = false;
		history = new List<child> ();
		history.Add (new child ("empty", null));
	}

	// Update is called once per frame
	void Update () {
		switch (player.GetComponent<Player> ()._state) {
			case Player.PlayerState.s_idle:
				DeleteCube ();
				ClearArrowSign ();
				break;
			case Player.PlayerState.s_moving:
				ClearArrowSign ();
				DeleteCube ();
				break;
			case Player.PlayerState.s_jumping:
				DeleteCube ();
				Throw();
				break;
			case Player.PlayerState.s_spawning:
				Spawncube ();
				ArrowSign ();
				break;
			case Player.PlayerState.s_Holdingidle:
				ClearArrowSign ();
				Throw();
				PlayerStartMovetoLast ();
				break;
			case Player.PlayerState.s_groundedHoldingidle:
				ClearArrowSign ();
				Throw();
				PlayerStartMovetoLast ();
				break;

			case Player.PlayerState.s_movingTolastCube:
				PlayerMovingToLastCube();
				break;
		}
		//判定是否有方塊伸出，可不可以丟出來
		if (history.Count - 1 > 0) {
			cubeCheck = true;
		} else {
			cubeCheck = false;
		}
		//方塊生成狀態下放掉Z時偵測是否有方塊伸出，有即實體化
		 if (Input.GetKeyUp (KeyCode.Z) && cubeCheck == true) {
			s_Database.UsedCube +=(history.Count-1);
			gen = GameObject.FindGameObjectsWithTag ("generated");
			for (int i = 0; i < gen.Length; i++) {
				gen[i].GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f,255f);
			}
			spawnCheck = false;
		}
		if(player.GetComponent<Player>().Isground)
		{
			MovingTolastCubeGroundCheck = true;
		}
	}

	//方塊伸出.收回.防回堵
	void Spawncube () {
		//伸出方塊 同時判定周邊是否可以伸出方塊 且伸出時刪除既有丟出方塊
		if (Input.GetKey (KeyCode.Z) && spawnCheck) {
			//向上生成方塊 正常狀態
			if (rb.gravityScale > 0) {
				if (Input.GetKeyDown (KeyCode.UpArrow) && is_spawning == false && upCubecheck == false && (history.Count - 1) < s_Database.maxCube && Downwallchecker == false && player.GetComponent<Player> ().Downwallchecker == false ||
					Input.GetKeyDown (KeyCode.UpArrow) && is_spawning == false && upCubecheck == false && (history.Count - 1) < s_Database.maxCube && Upwallchecker == false) {
					DestroyCube();
					ClearArrowSign ();
					is_spawning = true;
					Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
					child.transform.parent = player.transform;
					PushSpawningCube(child);
					StartCoroutine (Move (child, "up"));
					Move_self ("up");
					if (cube_exist == false) {
						record2history ("up", child);
					}
				}
			}
			//向上生成 重力相反狀態
			if (rb.gravityScale < 0) {
				if (Input.GetKeyDown (KeyCode.UpArrow) && is_spawning == false && upCubecheck == false && history.Count > 1 && (history.Count - 1) < s_Database.maxCube && Downwallchecker == false && player.GetComponent<Player> ().Downwallchecker == false ||
					Input.GetKeyDown (KeyCode.UpArrow) && is_spawning == false && upCubecheck == false && history.Count > 1 && (history.Count - 1) < s_Database.maxCube && Upwallchecker == false) {
					DestroyCube();
					ClearArrowSign ();
					is_spawning = true;
					Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
					child.transform.parent = player.transform;
					PushSpawningCube(child);
					StartCoroutine (Move (child, "up"));
					Move_self ("up");
					if (cube_exist == false) {
						record2history ("up", child);
					}
				}
			}
			//向下生成方塊 重力正常狀態
			if (rb.gravityScale > 0) {
				if (Input.GetKeyDown (KeyCode.DownArrow) && is_spawning == false && downCubecheck == false && history.Count > 1 && (history.Count - 1) < s_Database.maxCube && Upwallchecker == false && player.GetComponent<Player> ().Upwallchecker == false ||
					Input.GetKeyDown (KeyCode.DownArrow) && is_spawning == false && downCubecheck == false && history.Count > 1 && (history.Count - 1) < s_Database.maxCube && Downwallchecker == false) {
					DestroyCube();
					ClearArrowSign ();
					is_spawning = true;
					Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
					child.transform.parent = player.transform;
					PushSpawningCube(child);
					StartCoroutine (Move (child, "down"));
					Move_self ("down");
					if (cube_exist == false) {
						record2history ("down", child);
					}
				}
			}
			//向下生成方塊 重力相反狀態
			if (rb.gravityScale < 0) {
				if (Input.GetKeyDown (KeyCode.DownArrow) && is_spawning == false && downCubecheck == false && (history.Count - 1) < s_Database.maxCube && Upwallchecker == false && player.GetComponent<Player> ().Upwallchecker == false ||
					Input.GetKeyDown (KeyCode.DownArrow) && is_spawning == false && downCubecheck == false && (history.Count - 1) < s_Database.maxCube && Downwallchecker == false) {
					DestroyCube();
					ClearArrowSign ();
					is_spawning = true;
					Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
					child.transform.parent = player.transform;
					PushSpawningCube(child);
					StartCoroutine (Move (child, "down"));
					Move_self ("down");
					if (cube_exist == false) {
						record2history ("down", child);
					}
				}
			}

			if (Input.GetKeyDown (KeyCode.LeftArrow) && is_spawning == false && leftCubecheck == false && (history.Count - 1) < s_Database.maxCube && Rightwallchecker == false && player.GetComponent<Player> ().Rightwallchecker == false ||
				Input.GetKeyDown (KeyCode.LeftArrow) && is_spawning == false && leftCubecheck == false && (history.Count - 1) < s_Database.maxCube && Leftwallchecker == false) {
				DestroyCube();
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				PushSpawningCube(child);
				StartCoroutine (Move (child, "left"));
				Move_self ("left");
				if (cube_exist == false) {
					record2history ("left", child);
				}
			}
			if (Input.GetKeyDown (KeyCode.RightArrow) && is_spawning == false && rightCubecheck == false && (history.Count - 1) < s_Database.maxCube && Leftwallchecker == false && player.GetComponent<Player> ().Leftwallchecker == false ||
				Input.GetKeyDown (KeyCode.RightArrow) && is_spawning == false && rightCubecheck == false && (history.Count - 1) < s_Database.maxCube && Rightwallchecker == false) {
				DestroyCube();
				ClearArrowSign ();
				is_spawning = true;
				Transform child = Instantiate (prefab, this.transform.position, this.transform.rotation);
				child.transform.parent = player.transform;
				PushSpawningCube(child);
				StartCoroutine (Move (child, "right"));
				Move_self ("right");
				if (cube_exist == false) {
					record2history ("right", child);
				}
			}
			//回收方塊
			TakeBackCube();
		}
	}

	//回收方塊
	void TakeBackCube()
	{
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
	public void Throw(){
		if(Input.GetKeyDown (KeyCode.Z) && Input.GetKey(KeyCode.DownArrow))
		{
			ThrowCube(new Vector2 (0,0));
		}
		else if(Input.GetKeyDown (KeyCode.Z))
		{
			ThrowCube(new Vector2(3,5));
		}
	}
	//方塊丟出
	public void ThrowCube (Vector2 force) {
		if (cubeCheck) {
			spawnCheck = true;
			FindObjectOfType<AudioManager> ().play ("cube_put");
			released = true;
			gen = GameObject.FindGameObjectsWithTag ("generated");
			GameObject group = new GameObject ("blocks");
			for (int i = 0; i < gen.Length; i++) {
				gen[i].GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f,255f);
				gen[i].transform.parent = group.transform;
			}
			group.AddComponent<Rigidbody2D> ();
			group.AddComponent<Blocks> ();
			if(rb.gravityScale>0)
				if (player.GetComponent<Player> ().direction == true) {
					group.GetComponent<Rigidbody2D>().AddForce(force,ForceMode2D.Impulse);
				} else {
					group.GetComponent<Rigidbody2D>().AddForce(new Vector2(force.x*-1,force.y*1),ForceMode2D.Impulse);
				}
			else
			{
				if (player.GetComponent<Player> ().direction == true) {
					group.GetComponent<Rigidbody2D>().AddForce(new Vector2(force.x,force.y*-1),ForceMode2D.Impulse);
				} else {
					group.GetComponent<Rigidbody2D>().AddForce(new Vector2(force.x*-1,force.y*-1),ForceMode2D.Impulse);
				}
			}
			group.GetComponent<Rigidbody2D> ().freezeRotation = true;
			group.GetComponent<Rigidbody2D> ().gravityScale = 3;
			group.tag = "Blocks";
			for (int i = history.Count - 1; i > 0; i--) {
				history.RemoveAt (i);
			}
			transform.position = transform.parent.position;
		}
	}

	private void DeleteCube () {
		
		if (Input.GetKeyDown (KeyCode.X)) {
			DestroyCube();
		}
	}
	public void DestroyCube()
	{
		if(released){
			FindObjectOfType<AudioManager> ().play ("DestroyCube");
			GameObject.Find ("blocks").GetComponent<Blocks> ().destory = true;
			released = false;
		}
	}
	//紀錄list
	private void record2history (string direction, Transform child_position) {
		history.Add (new child (direction, child_position));
	}

	//generator位置控制
	private void Move_self (string dir) {
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
	private void PlayerStartMovetoLast () {
		//在已確認可移動的情況下 按下X後開啟移動至最後一個方塊的StartCoroutine 並且改成剛體以及所生成出的方塊的碰撞
		if (Input.GetKeyDown (KeyCode.X) && cubeCheck && movingTolastCube == false) { 
			movingTolastCube = true;
			CanMoveCheck = true;
			cubeCheck = false;
			cubeCount = 0; //cubecount是記錄Player已走到第幾個方塊 
			rb.velocity = Vector2.zero;
			rb.isKinematic = true;
			player.GetComponent<Collider2D> ().isTrigger = true;
			MovingTolastCube = GameObject.FindGameObjectsWithTag ("generated");
			for (int i = 0; i < MovingTolastCube.Length; i++) {
				MovingTolastCube[i].transform.parent = null;
				MovingTolastCube[i].GetComponent<Collider2D> ().isTrigger = true;
			}

			transform.position = player.transform.position;
		}

		if (CanMoveCheck) { //開始StartCoroutine
			StartCoroutine (MoveToLast (history.ElementAt (cubeCount + 1).direction, cubeCount));
		}

	}

	void PlayerMovingToLastCube()
	{
		
		if (cubeCount == history.Count - 1) { //當移動至最後一個方塊後"移動至最後一個方塊"的狀態解除,重設狀態並且關閉StartCoroutine,最後把List內的所有元素刪除
			spawnCheck = true;
			movingTolastCube = false;
			cubeCount = 0;
			rb.isKinematic = false;
			CanMoveCheck = false;
			player.GetComponent<Collider2D> ().isTrigger = false;
			for (int i = history.Count - 1; i > 0; i--) {
				history.RemoveAt (i);
			}
			for(int i = MovingTolastCube.Length-1;i>=0;i--)
			{
				Destroy(MovingTolastCube[i]);
			}
			MovingTolastCubeGroundCheck = false;
		}
		else if (CanMoveCheck) { //開始StartCoroutine
			StartCoroutine (MoveToLast (history.ElementAt (cubeCount + 1).direction, cubeCount));
		}
	}

	//生成方塊移動控制
	IEnumerator Move (Transform T, string I) {
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
			if(T!=null)
			T.position += V3;
			yield return new WaitForSeconds (0.01f);
		}
		if(T!=null)
		T.position = transform.position;
		is_spawning = false;

		if (cube_exist == true) {
			Destroy (T.gameObject);
			cube_exist = false;
		}
		StopCoroutine (Move (T, I));
	}

	//移動至最後一個方塊
	IEnumerator MoveToLast (string direction, int cube) {
		CanMoveCheck = false;
		float moveTime = 0;
		float moveSpeed = 0.128f;
		Vector3 directionCheck = Vector3.zero;
		switch (direction) { //判斷下一顆方塊位置給予不同方向的位移量
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
		MovingTolastCube[cubeCount].SetActive(false); //刪除已移動過後的軌跡方塊	
		CanMoveCheck = true; //重啟StartCoroutine直至已移動至最後一個方塊
		cubeCount += 1; //計算移動至第幾塊方塊
		StopCoroutine (MoveToLast (direction, cube));
		
	}

	//可生成方向指示
	private void ArrowSign () {
		if (rb.gravityScale > 0) {
			if (is_spawning == false && upCubecheck == false && (history.Count - 1) < s_Database.maxCube && player.GetComponent<Player> ().Downwallchecker == false && Downwallchecker == false && spawnCheck ||
				is_spawning == false && upCubecheck == false && (history.Count - 1) < s_Database.maxCube && Upwallchecker == false && spawnCheck ||
				is_spawning == false && upCubecheck && history.ElementAt (history.Count - 1).direction == "down" && spawnCheck) {
				UpSign.SetActive (true);
			} else {
				UpSign.SetActive (false);
			}
		} else {
			if (is_spawning == false && upCubecheck == false && history.Count > 1 && (history.Count - 1) < s_Database.maxCube && player.GetComponent<Player> ().Downwallchecker == false && Downwallchecker == false && spawnCheck ||
				is_spawning == false && upCubecheck == false && history.Count > 1 && (history.Count - 1) < s_Database.maxCube && Upwallchecker == false && spawnCheck ||
				is_spawning == false && upCubecheck && history.ElementAt (history.Count - 1).direction == "down" && spawnCheck) {
				UpSign.SetActive (true);
			} else {
				UpSign.SetActive (false);
			}
		}
		if (rb.gravityScale > 0) {
			if (is_spawning == false && downCubecheck == false && history.Count > 1 && (history.Count - 1) < s_Database.maxCube && player.GetComponent<Player> ().Upwallchecker == false && Upwallchecker == false && spawnCheck ||
				is_spawning == false && downCubecheck == false && history.Count > 1 && (history.Count - 1) < s_Database.maxCube && Downwallchecker == false && spawnCheck ||
				is_spawning == false && downCubecheck && history.ElementAt (history.Count - 1).direction == "up" && spawnCheck) {
				DownSign.SetActive (true);
			} else {
				DownSign.SetActive (false);
			}
		} else {
			if (is_spawning == false && downCubecheck == false && (history.Count - 1) < s_Database.maxCube && player.GetComponent<Player> ().Upwallchecker == false && Upwallchecker == false && spawnCheck ||
				is_spawning == false && downCubecheck == false && (history.Count - 1) < s_Database.maxCube && Downwallchecker == false && spawnCheck ||
				is_spawning == false && downCubecheck && history.ElementAt (history.Count - 1).direction == "up" && spawnCheck) {
				DownSign.SetActive (true);
			} else {
				DownSign.SetActive (false);
			}
		}


		if (is_spawning == false && leftCubecheck == false && (history.Count - 1) < s_Database.maxCube && player.GetComponent<Player> ().Rightwallchecker == false && Rightwallchecker == false && spawnCheck ||
			is_spawning == false && leftCubecheck == false && (history.Count - 1) < s_Database.maxCube && Leftwallchecker == false && spawnCheck ||
			is_spawning == false && leftCubecheck && history.ElementAt (history.Count - 1).direction == "right" && spawnCheck) {
			LeftSign.SetActive (true);
		} else {
			LeftSign.SetActive (false);
		}
		if (is_spawning == false && rightCubecheck == false && (history.Count - 1) < s_Database.maxCube && player.GetComponent<Player> ().Leftwallchecker == false && Leftwallchecker == false && spawnCheck ||
			is_spawning == false && rightCubecheck == false && (history.Count - 1) < s_Database.maxCube && Rightwallchecker == false && spawnCheck ||
			is_spawning == false && rightCubecheck && history.ElementAt (history.Count - 1).direction == "left" && spawnCheck) {
			RightSign.SetActive (true);
		} else {
			RightSign.SetActive (false);
		}

	}
	private void ClearArrowSign () {
		UpSign.SetActive (false);
		DownSign.SetActive (false);
		LeftSign.SetActive (false);
		RightSign.SetActive (false);
	}
	private void PushSpawningCube(Transform cube)
	{
		SpawningCube[history.Count-1] = cube.GetComponent<SpawningBlocks>();
	}
	
}