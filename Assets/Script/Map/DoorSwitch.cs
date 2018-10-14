using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorSwitch : MonoBehaviour {
    public GameObject Door;
    public Sprite touched;
    public Sprite touch;
    SpriteRenderer sprite;
	public bool DS_open;
	// Use this for initialization
	void Start ()
	{
        //sprite.GetComponent<SpriteRenderer>();
	}
    private void OnTriggerStay2D(Collider2D col)
    {
		this.GetComponent<SpriteRenderer>().sprite = touched;
		DS_open = true;
		
	}
    private void OnTriggerExit2D(Collider2D collision)
    {
		this.GetComponent<SpriteRenderer>().sprite = touch;
		DS_open = false;

	}
}
