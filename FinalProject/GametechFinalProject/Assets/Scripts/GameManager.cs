﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] keys;
    public int numKeys;

    // Start is called before the first frame update
    void Start()
    {
        keys = GameObject.FindGameObjectsWithTag("Key");
        numKeys = keys.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
