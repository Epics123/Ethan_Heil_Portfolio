﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteMaze : MonoBehaviour
{
    public Text winText;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            if(gm.GetComponent<CameraManager>().level == CameraManager.LevelState.LEVEL3)
            {
                gm.floor.GetComponent<SpriteRenderer>().color = gm.originalColor;
                winText.enabled = true;
            }
            else
            {
                gm.GetComponent<CameraManager>().SwapCameras();
                gm.player.transform.position = gm.spawnPoint.transform.position;
                gm.floor.GetComponent<SpriteRenderer>().color = gm.originalColor;
            }
            
        }
    }
}
