using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : DataBase {
	public Text T_Achievement;
	public Text T_UsedCube;
	public Text T_DeathCount;
	public Text S1_Achievement;
	public Text S1_UsedCube;
	public Text S1_DeathCount;
	public Text S2_Achievement;
	public Text S2_UsedCube;
	public Text S2_DeathCout;
	Tutorial_Data old_tutorial_Data =null;
	Stage1_Data old_stgae1_Data = null;
	Stage2_Data old_stgae2_Data = null;


	// Use this for initialization
	void Start () {
		
		if (File.Exists(Application.dataPath +"/Tutorial_Data.json"))
		{
			old_tutorial_Data = JsonUtility.FromJson<Tutorial_Data>(File.ReadAllText(Application.dataPath +"/Tutorial_Data.json"));
		}
		if (File.Exists(Application.dataPath +"/Stage1_Data.json"))
		{
			old_stgae1_Data = JsonUtility.FromJson<Stage1_Data>(File.ReadAllText(Application.dataPath +"/Stage1_Data.json"));
		}
		if (File.Exists(Application.dataPath +"/Stage2_Data.json"))
		{
			old_stgae2_Data = JsonUtility.FromJson<Stage2_Data>(File.ReadAllText(Application.dataPath +"/Stage2_Data.json"));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(old_tutorial_Data!=null)
		{
			T_Achievement.text = "AchieveMent:" + old_tutorial_Data.AchievementCount;
			T_UsedCube.text ="UsedCube:" + old_tutorial_Data.UsedCube;
			T_DeathCount.text = "Death:" + old_tutorial_Data.DeathCount;
		}
		if(old_stgae1_Data!=null)
		{
			S1_Achievement.text = "AchieveMent:" + old_stgae1_Data.AchievementCount;
			S1_UsedCube.text ="UsedCube:" + old_stgae1_Data.UsedCube;
			S1_DeathCount.text = "Death:" + old_stgae1_Data.DeathCount;
		}
		if(old_stgae2_Data!=null)
		{
			S2_Achievement.text = "AchieveMent:" + old_stgae2_Data.AchievementCount;
			S2_UsedCube.text ="UsedCube:" + old_stgae2_Data.UsedCube;
			S2_DeathCout.text = "Death:" + old_stgae2_Data.DeathCount;
		}
		
	}
}
