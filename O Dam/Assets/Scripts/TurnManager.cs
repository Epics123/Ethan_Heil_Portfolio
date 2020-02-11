using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public int turnNumber;
    public int playerIndex = 1;
    public bool gameStarted = false, roundOver, roundEnding;

    public GameObject panel;
    public GridManager gridManager;

    public GameObject beaverPrefab;

    public Dialogue dialogue;

    private void Awake()
    {
        player1.Init("Player 1", Color.black, 0, false);
        player2.Init("Player 2", Color.black, 0, false);
    }


    // Start is called before the first frame update
    void Start()
    {
        roundOver = true;
        roundEnding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
       
            
            if (turnNumber % 3 == 0)
            {
                if (!roundEnding)
                {
                    gridManager.currentPlayer = player1;
                    player1.isPlaying = true;
                    player2.isPlaying = false;
                    
                }
                
                    
            }
            else if (turnNumber % 3 == 1)
            {
                if (!roundEnding)
                {
                    gridManager.currentPlayer = player2;
                    player2.isPlaying = true;
                    player1.isPlaying = false;
                    roundOver = false;
                }
                

            } 
            else if(turnNumber % 3 == 2)
            {
                player2.isPlaying = false;
                player1.isPlaying = false;

                if (!roundOver)
                    StartCoroutine(EndRound());

                roundOver = true;
            }
            
            
        }
    }



    IEnumerator EndRound()
    {
        roundEnding = true;
        GameObject[] goldList;
        goldList = GameObject.FindGameObjectsWithTag("Gold");

        if(Random.Range(0.0f,1.0f) > 0.1f)
        {
            gridManager.SpawnGold(GameObject.Find("" + Random.Range(gridManager.cols / 3, gridManager.cols - gridManager.cols / 3) + " 0"));
        }

        int i;
        for (i = 0; i < goldList.Length; i++)
        {
            goldList[i].GetComponent<Gold>().Move();
        }

        yield return null;

        if (Random.Range(0.0f, 1.0f) > 0.03333333333333f)
        {
            GameObject beaver = Instantiate(beaverPrefab);
            Beaver beaverScript = beaver.GetComponent<Beaver>();
            beaverScript.startingTile = GameObject.Find("" + Random.Range(gridManager.cols / 3, gridManager.cols - gridManager.cols / 3) + " 44").GetComponent<Tile>();
            beaverScript.ChompChomp();

            
        }

        yield return new WaitForSeconds(10f);

        //gridManager.CheckForFlood(GameObject.Find("0 5"));
        turnNumber++;
        

        roundEnding = false;
    }
}
