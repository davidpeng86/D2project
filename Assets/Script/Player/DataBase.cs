using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DataBase : MonoBehaviour {
    public GameObject o_Player;
    public GameObject o_Generator;
    [Header("記錄點位置")]
    public Vector3 SavePoint  = Vector3.zero;
    [Header("成就數")]
    public float AchievementCount;
    [Header("方塊伸出數量限制")]
    public float maxCube = 4;
    [Header("方塊總使用次數")]
    public float UsedCube;
    [Header("死亡次數")]
    public float DeathCount;
    public GameObject theMostCloseSavePoint =null;
    public GameObject[] Achievement;
    [System.NonSerialized]    
    public bool[] AchievementCheck;
    private Scene m_scene;
    void Start () {
        AchievementCheck =new bool[Achievement.Length];
        for(int i=0;i<Achievement.Length;i++)
        {
            AchievementCheck[i] = Achievement[i].activeSelf;
        }
	    SavePoint = o_Player.transform.position + Vector3.up;
        m_scene = SceneManager.GetActiveScene();
	}
	void Update () {   

        if(Input.GetKeyDown(KeyCode.F3))
        {
            load();
        }
	}
    void load()
    {
        if(m_scene.name =="Tutorial")
        {
            Tutorial_Data old_tutorial_Data =null;
            if (File.Exists(Application.dataPath +"/Tutorial_Data.json"))
            {
                old_tutorial_Data = JsonUtility.FromJson<Tutorial_Data>(File.ReadAllText(Application.dataPath +"/Tutorial_Data.json"));
            }
            UsedCube = old_tutorial_Data.UsedCube;
            AchievementCount = old_tutorial_Data.AchievementCount;
            DeathCount = old_tutorial_Data.DeathCount;
        }
    }
    public void Save()
    {   
        if(m_scene.name =="Tutorial")
        {
            Tutorial_Data tutorial_Data = new Tutorial_Data();
            Tutorial_Data old_tutorial_Data =null;
            if (File.Exists(Application.dataPath +"/Tutorial_Data.json"))
            {
                old_tutorial_Data = JsonUtility.FromJson<Tutorial_Data>(File.ReadAllText(Application.dataPath +"/Tutorial_Data.json"));
            }
            if(old_tutorial_Data !=null)
            {
                if(AchievementCount>=old_tutorial_Data.AchievementCount)
                    tutorial_Data.AchievementCount =AchievementCount;
                else
                    tutorial_Data.AchievementCount = old_tutorial_Data.AchievementCount;
                if(UsedCube <= old_tutorial_Data.UsedCube)
                    tutorial_Data.UsedCube = UsedCube;
                else
                    tutorial_Data.UsedCube = old_tutorial_Data.UsedCube;
                if(DeathCount<=old_tutorial_Data.DeathCount)
                    tutorial_Data.DeathCount = DeathCount;
                else
                    tutorial_Data.DeathCount = old_tutorial_Data.DeathCount;
            }
            else
            {
                tutorial_Data.AchievementCount =AchievementCount;
                tutorial_Data.UsedCube = UsedCube;
                tutorial_Data.DeathCount = DeathCount;
            }
            string json = JsonUtility.ToJson(tutorial_Data);
            File.WriteAllText(Application.dataPath + "/Tutorial_Data.json",json);
        }
        else if(m_scene.name =="Stage1")
        {
            Stage1_Data stage1_Data = new Stage1_Data();
            Stage1_Data old_stage1_Data =null;
            if(File.Exists(Application.dataPath + "/Stage1_Data.json"))
            {
                old_stage1_Data = JsonUtility.FromJson<Stage1_Data>(File.ReadAllText(Application.dataPath +"/Stage1_Data.json"));
            }
            if(old_stage1_Data!=null)
            {
                if(AchievementCount>=old_stage1_Data.AchievementCount)
                {
                    stage1_Data.AchievementCount = AchievementCount;
                }
                else
                {
                    stage1_Data.AchievementCount = old_stage1_Data.AchievementCount;
                }
                if(UsedCube<=old_stage1_Data.UsedCube)
                {
                    stage1_Data.UsedCube = UsedCube;
                }
                else
                {
                    stage1_Data.UsedCube = old_stage1_Data.UsedCube;
                }
                if(DeathCount<=old_stage1_Data.DeathCount)
                {
                    stage1_Data.DeathCount = DeathCount;
                }
                else
                {
                    stage1_Data.DeathCount = old_stage1_Data.DeathCount;
                }
            }
            else
            {
                stage1_Data.AchievementCount = AchievementCount;
                stage1_Data.UsedCube = UsedCube;
                stage1_Data.DeathCount = DeathCount;
            }
            string json = JsonUtility.ToJson(stage1_Data);
            File.WriteAllText(Application.dataPath + "/Stage1_Data.json",json);
        }
        else
        {
            Stage2_Data stage2_Data = new Stage2_Data();
            Stage2_Data old_stage2_Data =null;
            if(File.Exists(Application.dataPath +"/Stage2_Data.json"))
            {
                old_stage2_Data = JsonUtility.FromJson<Stage2_Data>(File.ReadAllText(Application.dataPath +"/Stage2_Data.json"));
            }
            if(old_stage2_Data!=null)
            {
                if(AchievementCount>=old_stage2_Data.AchievementCount)
                {
                    stage2_Data.AchievementCount =AchievementCount;
                }
                else
                {
                    stage2_Data.AchievementCount =old_stage2_Data.AchievementCount;
                }
                if(UsedCube<=old_stage2_Data.UsedCube)
                {
                    stage2_Data.UsedCube = UsedCube;
                }
                else
                {
                    stage2_Data.UsedCube = old_stage2_Data.UsedCube;
                }
                if(DeathCount<=old_stage2_Data.DeathCount)
                {
                    stage2_Data.DeathCount = DeathCount;
                }
                else
                {
                    stage2_Data.DeathCount = old_stage2_Data.DeathCount;
                }
            }
            else
            {
                stage2_Data.AchievementCount = AchievementCount;
                stage2_Data.UsedCube = UsedCube;
                stage2_Data.DeathCount = DeathCount;
            }
            string json = JsonUtility.ToJson(stage2_Data);
            File.WriteAllText(Application.dataPath + "/Stage2_Data.json",json);
        }
    }   
    private class Tutorial_Data
    {
        public float AchievementCount;
        public float UsedCube;
        public float DeathCount;
    }
    private class Stage1_Data
    {
        public float AchievementCount;
        public float UsedCube;
        public float DeathCount;
    }
    private class Stage2_Data
    {
        public float AchievementCount;
        public float UsedCube;
        public float DeathCount;
    }
}
