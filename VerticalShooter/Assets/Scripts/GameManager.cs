using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Script for controlling general behaviors in the game
public class GameManager : MonoBehaviour
{
    public CameraShake camShake;
    public AudioSource enemyDeathSound;
    public AudioSource playerDeathSound;
    public Color lerpedColor;
    public Color startColor;
    public Image Black;
    public GameObject player;
    public Text[] deathScreenText;
    public Image[] lives;
    public Text waveText;
    public Text scoreText;
    public Text finalWaveText;
    public Text finalScoreText;
    public bool playerDead = false;
    public int playerLives = 3;
    public int totalScore;
    public int waveNum;


    bool playSound = true;

    //Disable all of the death screen UI
    private void Awake()
    {
        for(int i = 0; i < deathScreenText.Length; i++)
        {
            deathScreenText[i].enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        player = GameObject.FindGameObjectWithTag("Player");
        startColor = new Color(0, 0, 0, 0);
        totalScore = 0;
        waveNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlayer();
    }

    //Decrase player's lives and updates the UI
    public void LooseLives()
    {
        playerLives--;
        lives[playerLives].enabled = false;
    }

    //Checks to see if the player should be destroyed
    void DestroyPlayer()
    {
        if(playerLives <= 0)
        {
            playerDead = true;
            if(playSound)
            {
                StartCoroutine(PlayDeathSound());
                //Create death explosion on the player
                Instantiate(player.GetComponent<PlayerController>().explosion, player.transform.position, player.transform.rotation);

                //Shake the camera
                camShake.power = 0.7f;
                camShake.shouldShake = true;

                //Switch to death screen
                StartCoroutine(FadeOut(3f));
                for (int i = 0; i < deathScreenText.Length; i++)
                {
                    deathScreenText[i].enabled = true;
                }
                finalWaveText.text = waveNum.ToString();
                finalScoreText.text = totalScore.ToString();

                //Destroy player and restart the game
                StartCoroutine(RestartGame());
                playSound = false;
                Destroy(player);
            }      
 
        }
    }

    //Updates the UI to display the current score
    public void UpdateScore()
    {
        scoreText.text = totalScore.ToString();
    }

    //Updates the UI to display the current wave
    public void UpdateWave()
    {
        waveText.text = waveNum.ToString();
    }

    //Plays the death sound for the player
    IEnumerator PlayDeathSound()
    {
        playerDeathSound.Play();
        yield return null;
    }
    
    //Fades to the death screen
    IEnumerator FadeOut(float time)
    {
        float currentTime = 0f;
        lerpedColor = Color.black;
        Color currentColor;
        do
        {
            currentColor = Color.Lerp(startColor, lerpedColor, 0.5f);
            Black.color = currentColor;
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime < time);

    }

    //Restarts the game
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadSceneAsync(1);
    }
}
