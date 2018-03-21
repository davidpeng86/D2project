using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour {
    public Transform prefab;

    public GameObject[] gen;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Transform child = Instantiate(prefab, this.transform) as Transform;
           StartCoroutine( Move(child , 1));
            //child.localPosition = Vector3.up * 1.35f;
            Animator anim = child.GetComponent<Animator>();
            anim.SetInteger("dir",1);
            Debug.Log("dir == 1");
        }
if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Transform child = Instantiate(prefab, this.transform) as Transform;
           StartCoroutine( Move(child , 2));
            //child.localPosition = Vector3.up * 1.35f;
            Animator anim = child.GetComponent<Animator>();
            anim.SetInteger("dir",1);
            Debug.Log("dir == 1");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            gen = GameObject.FindGameObjectsWithTag("generated");
            GameObject group = new GameObject("blocks");
            for (int i = 0; i < gen.Length; i++)
            {
                gen[i].transform.parent = group.transform;
            }
            group.AddComponent<Rigidbody>();
            group.transform.Translate(new Vector2(1.35f, 0), Space.World);
        }
    }
    IEnumerator Move(Transform T, int I){
        float F = 0f;
        Vector3 V3 = Vector3.zero;
        switch(I){
            case 1:
                V3 = Vector3.up;
            break;

            case 2:
                V3 = Vector3.down;
            break;
        }
        while(F<1.35f){
            F += 0.1f;
            T.localPosition =  V3 * F;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
