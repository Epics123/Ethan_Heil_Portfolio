using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashColor : MonoBehaviour {

    public Color color;
    Color lerpedColor;
    PlayerAblilities ablilities;

	// Use this for initialization
	void Start () {
        lerpedColor = GetComponent<Text>().color;
        ablilities = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAblilities>();
    }
	
	// Update is called once per frame
	void Update () {

        if(ablilities.boneCount <= 6 && ablilities.boneCount >= 4)
        {
            lerpedColor = Color.Lerp(Color.yellow, Color.white, Mathf.PingPong(Time.time, 0.5f));
            GetComponent<Text>().color = lerpedColor;
        }
        else if(ablilities.boneCount <= 3)
        {
            lerpedColor = Color.Lerp(Color.red, Color.white, Mathf.PingPong(Time.time, 0.5f));
            GetComponent<Text>().color = lerpedColor;
        }
        else
        {
            GetComponent<Text>().color = color;
        }
        
	}
}
