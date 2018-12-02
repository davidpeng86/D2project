using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour {
	public DataBase s_Database;
	private bool Check;
	private Vector3 Save;
	private Vector2 velocity;
	float x = 30;
	float a =255;
	Transform Particle;
	SpriteRenderer rd;
	Color orginal;

	// Use this for initialization
	void Start () {
		rd = this.GetComponent<SpriteRenderer>();
		orginal = rd.color;
		Particle = this.gameObject.transform.GetChild(0);
		Save = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if(Check)
		{	
			x-=0.411f;
			if(x<=0)
			{
				rd.color -= new Color(0,0,0,0.02f);
				x = 0;
			}
			FindObjectOfType<AudioManager> ().play ("Achievement");
			this.transform.position = new Vector3(transform.position.x,Mathf.SmoothDamp(transform.position.y,Save.y+2.0f,ref velocity.y,0.2f),transform.position.z);
			this.transform.Rotate(new Vector3(0,x,0));
			Particle.gameObject.SetActive(false);
		}
		
	}
	void OnTriggerEnter2D(Collider2D Col)
	{

		if(Col.tag=="Player")
		{
			s_Database.AchievementCount +=1;
			Check = true;
			StartCoroutine(Effect());
			for(int i =0;i<=s_Database.Achievement.Length-1;i++)
			{
				s_Database.AchievementCheck[i] = s_Database.Achievement[i].activeSelf;
			}
		}
	}
	public void Reset()
	{
		Check = false;
		this.transform.rotation = Quaternion.Euler(Vector3.zero);
		this.transform.position = Save;
		x = 30;
		rd.color =orginal;
		Particle.gameObject.SetActive(true);
		StopCoroutine(Effect());
	}
	IEnumerator Effect()
	{
		yield return new WaitForSeconds(5f);
		this.gameObject.SetActive(false);
		Reset();
	}
}
