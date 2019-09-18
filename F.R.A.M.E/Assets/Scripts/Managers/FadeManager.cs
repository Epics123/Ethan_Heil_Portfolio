using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

    public FadeIn fadeIn;
    public FadeOut fadeOut;

    public bool startFadeIn = false;
    public bool startFadeOut = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(startFadeIn == true)
        {
            fadeIn.StartFade();
            startFadeIn = false;
        }
        if(startFadeOut == true)
        {
            fadeOut.StartFade();
            startFadeOut = false;
        }
	}
}
