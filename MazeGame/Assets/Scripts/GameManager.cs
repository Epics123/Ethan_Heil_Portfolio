using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages the flow of the game

public class GameManager : MonoBehaviour
{
    public GameObject floor;
    public GameObject player;
    public GameObject spawnPoint;
    public GameObject[] doors;
    public Color originalColor;
    public Text timer;

    public int startLevel = 1;
    public int levelIdx;
    public int startLayer = 8;
    public int lvl2Layer = 9;
    public int lvl3Layer = 10;
    public bool win = false;
    public bool timerCount = true;

    float time;


    // Start is called before the first frame update
    void Start()
    {
        originalColor = floor.GetComponent<SpriteRenderer>().color;
        floor.layer = startLayer;
        player.layer = startLayer;
        levelIdx = startLevel;
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
        UpdateTimer();
    }

    //Restarts the game from the beginning
    void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    //Updates the game timer
    void UpdateTimer()
    {
        if(timerCount)
        {
            time += Time.deltaTime;
            //Restart the game if time >= 10 min
            if (time >= 600)
            {
                Restart();
            }
            DisplayTimer();
        }
        
    }

    //Displays the timer
    void DisplayTimer()
    {
        timer.text = FormatTime(time);
    }

    //Formats the timer to 00:00:00 format
    string FormatTime(float time)
    {
        int timeDecimal = (int)(time * 100f);
        int minutes = timeDecimal / (60 * 100);
        int seconds = (timeDecimal % (60 * 100)) / 100;
        int hundreths = timeDecimal % 100;
        return System.String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundreths);
    }


}
