using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LoadLevel loadLevel;
    public Text text;
    public static int timesPlayed = -1;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Start")
        {
            timesPlayed++;
        }
        text.text = timesPlayed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name != "Game")
        {
            loadLevel.LoadNextLevel();
        }
    }
}
