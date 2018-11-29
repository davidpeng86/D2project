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
    public float CrownCount;
    [Header("方塊伸出數量限制")]
    public float maxCube = 4;
    [Header("方塊總使用次數")]
    public float UsedCube;
    public GameObject theMostCloseSavePoint =null;
    public GameObject[] Crown;
    [System.NonSerialized]    
    public bool[] CrownCheck;
    private Scene m_scene;
    void Start () {
        CrownCheck =new bool[Crown.Length];
        for(int i=0;i<Crown.Length;i++)
        {
            CrownCheck[i] = Crown[i].activeSelf;
        }
	    SavePoint = o_Player.transform.position + Vector3.up;
        m_scene = SceneManager.GetActiveScene();
	}
	void Update () {
        
		
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
                if(CrownCount>=old_tutorial_Data.CrownCount)
                    tutorial_Data.CrownCount =CrownCount;
                else
                    tutorial_Data.CrownCount = old_tutorial_Data.CrownCount;
                if(UsedCube <= old_tutorial_Data.UsedCube)
                    tutorial_Data.UsedCube = UsedCube;
                else
                    tutorial_Data.UsedCube = old_tutorial_Data.UsedCube;
            }
            else
            {
                tutorial_Data.CrownCount =CrownCount;
                tutorial_Data.UsedCube = UsedCube;
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
                if(CrownCount>=old_stage1_Data.CrownCount)
                {
                    stage1_Data.CrownCount = CrownCount;
                }
                else
                {
                    stage1_Data.CrownCount = old_stage1_Data.CrownCount;
                }
                if(UsedCube<=old_stage1_Data.UsedCube)
                {
                    stage1_Data.UsedCube = UsedCube;
                }
                else
                {
                    stage1_Data.UsedCube = old_stage1_Data.UsedCube;
                }
            }
            else
            {
                stage1_Data.CrownCount = CrownCount;
                stage1_Data.UsedCube = UsedCube;
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
                if(CrownCount>=old_stage2_Data.CrownCount)
                {
                    stage2_Data.CrownCount =CrownCount;
                }
                else
                {
                    stage2_Data.CrownCount =old_stage2_Data.CrownCount;
                }
                if(UsedCube<=old_stage2_Data.UsedCube)
                {
                    stage2_Data.UsedCube = UsedCube;
                }
                else
                {
                    stage2_Data.UsedCube = old_stage2_Data.UsedCube;
                }
            }
            else
            {
                stage2_Data.CrownCount = CrownCount;
                stage2_Data.UsedCube = UsedCube;
            }
            string json = JsonUtility.ToJson(stage2_Data);
            File.WriteAllText(Application.dataPath + "/Stage2_Data.json",json);
        }
    }
    private class Tutorial_Data
    {
        public float CrownCount;
        public float UsedCube;
    }
    private class Stage1_Data
    {
        public float CrownCount;
        public float UsedCube;
    }
    private class Stage2_Data
    {
        public float CrownCount;
        public float UsedCube;
    }
}
