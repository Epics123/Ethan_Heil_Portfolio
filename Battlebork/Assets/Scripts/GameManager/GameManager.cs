using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gm;
    public static int boneCount;
    public static int totalScore;

    public int gameScore;
    public int lives = 3;
    public int waveNum;

    public GameObject player;
    public Image[] hearts = new Image[3];
    public Text bones;
    public Text score;
    public Text wave;
    public Text challenge;
    public Text countdown;

    public static int Score
    {
        get
        {
            return totalScore;
        }
    }
    public static int Bones
    {
        get
        {
            return boneCount;
        }
    }

    private void Awake()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        gameScore = 0;
        waveNum = 0;
    }
	
	// Update is called once per frame
	void Update () {
        displayBoneCount();
        displayScore();
        checkLives();
        displayChallenge();
        displayWave();
        displayCountdown();
        if(waveNum > 20)
        {
            lives -= 3;
        }
	}

    public void loseLives(int livesLost)
    {
        for(int i = livesLost; i > 0; i--)
        {
            if(lives > 0)
            {
                hearts[lives - 1].enabled = false;
                lives--;
            }
            
        }
             
    }

    public int getLives()
    {
        return lives;
    }

    public void setLives(int numLives)
    {
        lives = numLives;
    }

    void checkLives()
    {
        if (getLives() <= 0)
        {
            player.SetActive(false);
        }
    }

    void displayBoneCount()
    {
        if (player != null)
        {
            boneCount = player.GetComponent<PlayerAblilities>().boneCount;
            bones.text = player.GetComponent<PlayerAblilities>().boneCount.ToString();
        }
    }

    void displayScore()
    {
        if(player != null)
        {
            totalScore = gameScore;
            score.text = gameScore.ToString();
        }
    }

    void displayWave()
    {
        if (player != null)
        {
            waveNum = gameObject.GetComponent<WaveSpawner>().nextWave + 1;
            wave.text = waveNum.ToString();
        }
    }

    public WaveSpawner.Wave GetWave()
    {
        return gameObject.GetComponent<WaveSpawner>().GetCurrentWave();
    }

    void displayChallenge()
    {
       challenge.text = GetWave().challenge.numToKill + "/" + gameObject.GetComponent<WaveSpawner>().GetTotalToKill() + 
           " " + GetWave().challenge.name + " Remaining";
    }

    public void displayCountdown()
    {
        int count = (int)gameObject.GetComponent<WaveSpawner>().waveCountdown;
        countdown.text = count.ToString();

    }

    public void hideCountdown()
    {
        countdown.enabled = false;
    }

    public void showCountdown()
    {
        countdown.enabled = true;
    }

    


}
