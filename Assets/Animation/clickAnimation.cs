using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickAnimation : MonoBehaviour {
	public sceneManager scene;
	public GameObject exitButton;

	public int clickCount = 1;
	public Animator button01;
	public Animator button02;
	public Animator button03;
	public static string animationName = " ";
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void button01Clicked(){
		if(clickCount == 1){
			button01.SetBool("01_clicked",true);
			button02.SetBool("01_clicked",true);
			button03.SetBool("01_clicked",true);

			animationName = "01_clicked";
			exitButton.SetActive(true);
			clickCount += 1;
		}
		else if(clickCount == 2){
			scene.sceneChange("Tutorial");
		}

	}

	public void button02Clicked(){
		if(clickCount == 1){
			button01.SetBool("02_clicked",true);
			button02.SetBool("02_clicked",true);
			button03.SetBool("02_clicked",true);

			animationName = "02_clicked";
			exitButton.SetActive(true);
			clickCount += 1;
		}
		else if(clickCount == 2){
			scene.sceneChange("Stage1");
		}
	}

	public void button03Clicked(){
		if(clickCount == 1){
			button01.SetBool("03_clicked",true);
			button02.SetBool("03_clicked",true);
			button03.SetBool("03_clicked",true);

			animationName = "03_clicked";
			exitButton.SetActive(true);
			clickCount += 1;
		}
		else if(clickCount == 2){
			scene.sceneChange("Stage2");
		}
	}

	public void leave(){
		clickCount = 1;
		button01.SetBool(animationName,false);
		button02.SetBool(animationName,false);
		button03.SetBool(animationName,false);
		exitButton.SetActive(false);
	}
}