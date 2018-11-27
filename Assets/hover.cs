using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject hoverParticle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
    {
        hoverParticle.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		hoverParticle.SetActive(false);
    }
}
