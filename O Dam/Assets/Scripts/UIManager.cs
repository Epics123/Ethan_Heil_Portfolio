using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public Button greenButton;
    public Button redButton;

    public Text colorPrompt, greenDamCount, redDamCount, greenDynamiteCount, redDynamiteCount;
    public GameObject startScreen, gameScreen, player1Sprite, player2Sprite, panel;
    public GridManager gridManager;
    public TurnManager turnManager;
    public Dialogue dialogue;
    public AudioSource gameMusic;

    public int playerIndex = 1;

    private void Awake()
    {
        colorPrompt.text = "Player 1 Choose Your Character";
        startScreen.SetActive(true);
        gameScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        greenDamCount.text = player1.playerInventory.GetTotalDam().ToString();
        greenDynamiteCount.text = player1.playerInventory.GetTotalDynamite().ToString();
        redDamCount.text = player2.playerInventory.GetTotalDam().ToString();
        redDynamiteCount.text = player2.playerInventory.GetTotalDynamite().ToString();
    }

    public void ChoosePlayer(Button button)
    {
        switch (playerIndex)
        {
            case 1:
                player1.playerColor = Color.red;
                player1.playerState = Player.State.Active;

                player1Sprite.GetComponent<Image>().sprite = button.spriteState.selectedSprite;
                if (button.tag == "Red")
                {
                    player1Sprite.transform.localScale = new Vector3(-1, 1, 1);
                    player1Sprite.GetComponent<RectTransform>().anchoredPosition = new Vector3(224, -8, 0);
                }
                    
                colorPrompt.text = "Player 2 Choose Your Character";
                break;
            case 2:
                player2.playerColor = Color.green;

                player2Sprite.GetComponent<Image>().sprite = button.spriteState.selectedSprite;
                if (button.tag == "Green")
                {
                    player2Sprite.transform.localScale = new Vector3(-1, 1, 1);
                    player2Sprite.GetComponent<RectTransform>().anchoredPosition = new Vector3(-224, -8, 0);
                }

                colorPrompt.enabled = false;

                gridManager.currentPlayer = player1;
                StartCoroutine(HideColorPrompt());
                break;
            default:
                break;
        }
        playerIndex++;

        button.gameObject.SetActive(false);
    }

    private IEnumerator HideColorPrompt()
    {
        gameScreen.SetActive(true);
        panel.GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, false);
        yield return new WaitForSeconds(0.5f);

        startScreen.SetActive(false);
        turnManager.gameStarted = true;
        dialogue.canType = true;
    }
}
