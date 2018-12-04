using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour {
	public GameObject o_Player;
	public spawn generator;
	public DataBase s_Database;
	public CameraFollow s_CameraFollow;
	SavePoint theMostCloseSavePoint;
	public Sprite DeadSprite;
	public RuntimeAnimatorController A_Controller;
	private bool check;
	void Start () {
		
	}
	
	void Update () {
		theMostCloseSavePoint = s_Database.theMostCloseSavePoint.GetComponent<SavePoint>();

	}
	void OnCollisionStay2D(Collision2D col)
	{
		if(col.collider.tag == "Player")
		{
			if(!check)
			{
				FindObjectOfType<AudioManager> ().play ("Dead");
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
		o_Player.transform.position = s_Database.SavePoint;
		o_Player.transform.rotation = Quaternion.Euler(0,0,0);
		if (theMostCloseSavePoint!=null)
		{
			s_Database.maxCube = theMostCloseSavePoint.maxCube;
			s_Database.UsedCube = theMostCloseSavePoint.UsedCube;
			s_Database.AchievementCount = theMostCloseSavePoint.AchievementCount;
			s_Database.DeathCount +=1;
			for(int i =0;i<s_Database.AchievementCheck.Length;i++)
			{
				s_Database.AchievementCheck[i] = theMostCloseSavePoint.AchievementCheck[i];
				if(theMostCloseSavePoint.AchievementCheck[i])
				{
					s_Database.Achievement[i].SetActive(true);
					s_Database.Achievement[i].GetComponent<Achievement>().Reset();
				}
			}
		}
		check = false;
	}
}
