using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryPS : MonoBehaviour {


    private GameObject myobj;
    private ParticleSystem ps;

    void Start()
    {
        myobj = this.gameObject;
        ps = myobj.GetComponent<ParticleSystem>();
    }

    void Update () {
		if(ps.IsAlive()==false)
		{
			Destroy(myobj);
		}
	       
	}    
}
