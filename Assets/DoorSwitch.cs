using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorSwitch : MonoBehaviour {
    public GameObject Door;
    public Sprite touched;
    public Sprite touch;
    SpriteRenderer sprite;
	// Use this for initialization
	void Start ()
	{
        //sprite.GetComponent<SpriteRenderer>();
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
		this.GetComponent<SpriteRenderer>().sprite = touched;
		Door.GetComponent<Animator>().SetBool("On",true);
		Door.GetComponent<Animator>().SetBool("Off", false);
	}
    private void OnTriggerExit2D(Collider2D collision)
    {
		this.GetComponent<SpriteRenderer>().sprite = touch;
		Door.GetComponent<Animator>().SetBool("Off", true);
		Door.GetComponent<Animator>().SetBool("On", false);
	}
}
