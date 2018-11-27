﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerDeadZone : MonoBehaviour {
	public DataBase s_Database;
	public GameObject o_Player;
	public spawn generator;
	public CameraFollow s_CameraFollow;
	SavePoint theMostCloseSavePoint;
	public Sprite DeadSprite;
	public RuntimeAnimatorController A_Controller;
	private bool check;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		theMostCloseSavePoint = s_Database.theMostCloseSavePoint.GetComponent<SavePoint>();
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			if(!check)
			{
				s_CameraFollow.StartCoroutine(s_CameraFollow.CameraShake(0.15f,0.4f));
				if(o_Player.transform.localScale == new Vector3(1,1,1))
				{
					o_Player.transform.Rotate(0,0,20);
				}
				else if(o_Player.transform.localScale == new Vector3(-1,1,1))
				{
					o_Player.transform.Rotate(0,0,-20);
				}
				generator.ThrowCube(new Vector2 (0,0));
				generator.DestroyCube();
				if(o_Player.GetComponent<Player>()._state == Player.PlayerState.s_movingTolastCube)
				{
					generator.StopAllCoroutines();
					generator.AtLastCube();
				}

				StartCoroutine(wait(0.5f));
			}
		}
	}
	IEnumerator wait(float time)
	{	
		check = true;
		o_Player.GetComponent<Animator>().runtimeAnimatorController = null;
		o_Player.GetComponent<SpriteRenderer>().sprite = DeadSprite;
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSecondsRealtime(time);
		o_Player.GetComponent<Animator>().runtimeAnimatorController = A_Controller;
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		o_Player.transform.position = s_Database.SavePoint;
		o_Player.transform.rotation = Quaternion.Euler(0,0,0);
		if (theMostCloseSavePoint!=null)
		{
			s_Database.maxCube = theMostCloseSavePoint.maxCube;
			s_Database.maxUsedcube = theMostCloseSavePoint.maxUsedcube;
			s_Database.UsedCube = theMostCloseSavePoint.UsedCube;
			s_Database.CrownCount =theMostCloseSavePoint.CrownCount;
			for(int i =0;i<s_Database.CrownCheck.Length;i++)
			{
				s_Database.CrownCheck[i] = theMostCloseSavePoint.CrownCheck[i];
				if(theMostCloseSavePoint.CrownCheck[i])
				{
					s_Database.Crown[i].SetActive(true);
				}
			}
		}
		check = false;
	}
}