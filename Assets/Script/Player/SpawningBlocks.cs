using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBlocks : MonoBehaviour {
	float distance = 0.2f;
	public LayerMask groundLayer = 8;
	public bool Upwallchecker {
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

	public bool Downwallchecker {
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

	public bool Leftwallchecker {
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

	public bool Rightwallchecker {
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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
