using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{

    public Text timerText;
    public Color lerpedColor;
    public bool stopTimer = false;

    public float time = 90;

    void Start()
    {
        StartCoundownTimer();
    }

    void StartCoundownTimer()
    {
        if (timerText != null)
        {
            time = 90;
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }

    void UpdateTimer()
    {
        if (timerText != null && stopTimer == false)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            
            if ((time / 60) <= 1 && (time % 60) <= 30)
            {
                //Debug.Log("30 seconds remaining");
                lerpedColor = Color.Lerp(Color.red, Color.white, Mathf.PingPong(Time.time, 0.5f));
                timerText.color = lerpedColor;
            }
            if ((time / 60) <= 0 && (time % 60) <= 0)
            {
                minutes = (0 * 0).ToString("00");
                seconds = (0 * 0).ToString("00");
                timerText.color = Color.red;
                time = 0;
            }
            timerText.text = minutes + ":" + seconds;
            
        }

    }
}
