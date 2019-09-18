using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenChooser : MonoBehaviour {

    public Image[] screens;
    int randIdx;

    private void Awake()
    {
        for(int i = 0; i < screens.Length; i++)
        {
            screens[i].enabled = false;
        }
    }

    // Use this for initialization
    void Start () {
        randIdx = Random.Range(0, screens.Length);
        screens[randIdx].enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
