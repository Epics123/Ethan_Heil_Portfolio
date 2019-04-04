using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text timer;
    public Text keys;
    public int gridRows;
    public int gridCols;
    public int numKeys = 0;
    public bool timerCount = true;
    public string nextScene;
    public float time = 30f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if(Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadSceneAsync("Level1");
        }
    }

    void UpdateTimer()
    {
        if (timerCount)
        {
            keys.text = numKeys.ToString();
            time -= Time.deltaTime;

            if(time <= 0f)
            {
                time = 0f;
                timerCount = false;
                Restart();
            }

            DisplayTimer();
        }
    }

    void DisplayTimer()
    {
        timer.text = FormatTime(time);
    }

    string FormatTime(float time)
    {
        int timeDecimal = (int)(time * 100f);
        int minutes = timeDecimal / (60 * 100);
        int seconds = (timeDecimal % (60 * 100)) / 100;
        int hundreths = timeDecimal % 100;
        return System.String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundreths);
    }

     public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    void RestartGame()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }
}
