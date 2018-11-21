using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour {
	public GameObject o_Player;
	public spawn generator;
	public DataBase s_Databae;
	public CameraFollow s_CameraFollow;
	SavePoint theMostCloseSavePoint;
	public Sprite DeadSprite;
	public RuntimeAnimatorController A_Controller;
	private bool check;
	void Start () {
		
	}
	
	void Update () {
		theMostCloseSavePoint = s_Databae.theMostCloseSavePoint.GetComponent<SavePoint>();

	}
	void OnCollisionEnter2D(Collision2D col)
	{
		/* if(col.collider.tag == "Player")
		{
			Debug.Log("123");
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
			StartCoroutine(wait(0.5f));
		}*/
	}
	void OnCollisionStay2D(Collision2D col)
	{
		if(col.collider.tag == "Player")
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
		o_Player.transform.position = s_Databae.SavePoint;
		o_Player.transform.rotation = Quaternion.Euler(0,0,0);
		if (theMostCloseSavePoint!=null)
		{
			s_Databae.maxCube = theMostCloseSavePoint.maxCube;
			s_Databae.maxUsedcube = theMostCloseSavePoint.maxUsedcube;
			s_Databae.UsedCube = theMostCloseSavePoint.UsedCube;
			s_Databae.CrownCount =theMostCloseSavePoint.CrownCount;
			for(int i =0;i<s_Databae.CrownCheck.Length;i++)
			{
				s_Databae.CrownCheck[i] = theMostCloseSavePoint.CrownCheck[i];
				if(theMostCloseSavePoint.CrownCheck[i])
				{
					s_Databae.Crown[i].SetActive(true);
				}
			}
		}
		check = false;

	}
}
