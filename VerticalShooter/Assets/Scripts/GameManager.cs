using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    public bool playerDead = false;
    public int playerLives = 3;

    bool playSound = true;

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
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlayer();
    }

    public void LooseLives()
    {
        playerLives--;
    }

    void DestroyPlayer()
    {
        if(playerLives <= 0)
        {
            playerDead = true;
            if(playSound)
            {
                StartCoroutine(PlayDeathSound());
                Instantiate(player.GetComponent<PlayerController>().explosion, player.transform.position, player.transform.rotation);
                camShake.power = 0.7f;
                camShake.shouldShake = true;
                StartCoroutine(FadeOut(3f));
                for (int i = 0; i < deathScreenText.Length; i++)
                {
                    deathScreenText[i].enabled = true;
                }
                StartCoroutine(RestartGame());
                playSound = false;
                Destroy(player);
            }      
 
        }
    }

    IEnumerator PlayDeathSound()
    {
        playerDeathSound.Play();
        yield return null;
    }

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

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadSceneAsync(1);
    }
}
