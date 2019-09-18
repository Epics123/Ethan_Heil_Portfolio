using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    int uninfected = 0;
    public GameObject[] Players;
    public Sprite RedButton;
    public GameTimer timer;
    public WinStateManager winState;
    public CapsuleCollider2D[] playerColliders;
    public Text timerText;
    CapsuleCollider2D capsul2D;

    void Start ()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        timer = GameObject.FindGameObjectWithTag("GC").GetComponent<GameTimer>();
        winState = GameObject.FindGameObjectWithTag("GC").GetComponent<WinStateManager>();
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
        playerColliders = new CapsuleCollider2D[4];
        for(int i = 0; i < Players.Length; i++)
        {
            playerColliders[i] = Players[i].GetComponent<CapsuleCollider2D>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        uninfected = 0;
		foreach(GameObject player in Players)
        {
            if (!player.GetComponent<TaggingManager>().tagged)
            {
                uninfected++;
            }
            
        }
        if (uninfected == 1 || ((timer.time / 60) <= 1 && (timer.time % 60) <= 30))
        {
            this.GetComponent<SpriteRenderer>().sprite = RedButton;
            for(int i = 0; i < Players.Length; i++)
            {
                if (!Players[i].GetComponent<TaggingManager>().tagged)
                {
                    capsul2D = playerColliders[i];
                }
            }
            if (capsul2D.IsTouching(GetComponent<CircleCollider2D>())) 
            {
                winState.playerWinFlag = true;
                timer.stopTimer = true;
                timerText.text = "Players Win!";
                StartCoroutine(LoadScene());
            }
        }
        
	}

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("EndGameScene", LoadSceneMode.Single);
    }



}
