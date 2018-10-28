using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour {
    public GameObject o_Player;
    public GameObject o_Generator;
    [Header("記錄點位置")]
    public Vector3 SavePoint  = Vector3.zero;
    [Header("成就數")]
    public float Crown;
    [Header("方塊伸出數量限制")]
    public float maxCube = 4;
    [Header("方塊使用數量限制")]
    public float maxUsedcube;
    [Header("方塊總使用次數")]
    public float UsedCube;
    public GameObject theMostCloseSavePoint;
    void Start () {
      
		  SavePoint = o_Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
