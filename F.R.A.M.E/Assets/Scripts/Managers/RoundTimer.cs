using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour {

    public Text timerText;
    public Color lerpedColor;
    public EndScreen endScreen;
    public float time = 10f;

	// Use this for initialization
	void Start () {
        StartCoundownTimer();
	}
	

    void StartCoundownTimer()
    {
        if (timerText != null)
        {
            time = 10f;
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }

    void UpdateTimer()
    {

        if ((time % 60) <= 0)
        {
            time = 0;
            timerText.text = "00";
            StartCoroutine(endScreen.LoadScene());
        }
        else{
            time -= Time.deltaTime;
            string seconds = (time % 60).ToString("00");
            timerText.text = seconds;
            lerpedColor = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1f));
            timerText.color = lerpedColor;
        }
           
    }
}
