using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject explosion;
    public Transform restartPoint;
    public Text timer;
    public Text doorMessage;
    public Image timerBackground;

    public bool timerCount = false;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = restartPoint.position;
        timer.enabled = false;
        timerBackground.enabled = false;

        if(doorMessage != null)
        {
            doorMessage.enabled = false;
        }  
    }

    // Update is called once per frame
    void Update()
    {
        CheckRestart();
        UpdateTimer();
    }


    void CheckRestart()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadNextLevel(string level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void KillPlayer()
    {
        Instantiate(explosion, player.transform.position, player.transform.rotation);
        player.SetActive(false);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        player.SetActive(true);
        player.transform.position = restartPoint.position;
        player.transform.rotation = restartPoint.rotation;
    }

    //Updates the game timer
    void UpdateTimer()
    {
        if (timerCount)
        {
            time += Time.deltaTime;
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

    public IEnumerator HideMessage()
    {
       yield return new WaitForSeconds(1f);
       doorMessage.text = " door unlocked!";
       doorMessage.enabled = false;
    }
}
