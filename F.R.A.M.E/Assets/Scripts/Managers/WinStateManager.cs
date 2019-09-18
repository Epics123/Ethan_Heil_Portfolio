using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinStateManager : MonoBehaviour
{
    public GameObject[] Players;
    public GameTimer timer;
    public Text timerText;
    public FadeManager fadeManager;
    public bool virusWinFlag;
    public bool playerWinFlag;
    // Use this for initialization
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        timer = GameObject.FindGameObjectWithTag("GC").GetComponent<GameTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerWinFlag && !virusWinFlag)
        {
            virusWinFlag = checkVirusWin();
            playerWinFlag = checkPlayerWin();
        }
    }
    bool checkVirusWin()
    {
        foreach (GameObject player in Players)
        {
            if (!player.GetComponent<TaggingManager>().tagged)
            {
                return false;
            }
        }
        Debug.Log("Infected Win!");
        timer.stopTimer = true;
        timerText.text = "Infected Win!";
        fadeManager.startFadeOut = true;
        StartCoroutine(LoadScene());
        return true;
    }
    bool checkPlayerWin()
    {
        int uninfected = 0;
        for (int i = 0; i < Players.Length; i++)
        {
            if (!Players[i].GetComponent<TaggingManager>().tagged)
            {
                uninfected ++;
            }
        }
       if ((timerText.text == "00:00" && uninfected > 0))
        {
            Debug.Log("Players Win!");
            timer.stopTimer = true;
            timerText.text = "Players Win!";
            fadeManager.startFadeOut = true;
            StartCoroutine(LoadScene());
            return true;
        }
        return false;
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("EndGameScene", LoadSceneMode.Single);
    }
}
