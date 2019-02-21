﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to rotate and object
public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime)); //Rotates object
    }
}
