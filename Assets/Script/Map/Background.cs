﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
    public bool scrolling, paralax;
    public float backgroundSize;
    public float paralaxSpeed;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int rightIndex;
    private int leftIndex;
    private float lastCameraX;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
            leftIndex = 0;
            rightIndex = layers.Length - 1;
        }
    }
    private void Update()
    {
        if (paralax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * paralaxSpeed);
        }
        lastCameraX = cameraTransform.position.x;
        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
            { 
                ScrollLeft();
            }
            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
            {
                ScrollRight();
            }
        }
        

    }
    void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[lastRight].position = new Vector3(1,0,0) * (layers[leftIndex].position.x - backgroundSize);
        layers[lastRight].position = new Vector3(layers[lastRight].position.x,layers[lastRight].position.y,0);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }
    void ScrollRight()
    {
        layers[leftIndex].position = new Vector3(1,0,0) * (layers[rightIndex].position.x + backgroundSize);
        layers[leftIndex].position = new Vector3(layers[leftIndex].position.x,layers[leftIndex].position.y,0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
