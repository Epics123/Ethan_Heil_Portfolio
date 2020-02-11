using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamPiece : MonoBehaviour
{
    public Player player;
    public Dialogue dialogue;
    public Color originalColor, highlight;
    
    // Start is called before the first frame update
    void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        player.placingDam = true;
        player.placingDynamite = false;

        if(dialogue.currentText.text == dialogue.sentences[dialogue.index])
            dialogue.ClearText();
    }

    private void OnMouseEnter()
    {
        if (dialogue.canType)
        {
            dialogue.ChangeSentence(0);
            dialogue.canType = false;
        }

        gameObject.GetComponent<SpriteRenderer>().color = highlight;
    }

    private void OnMouseExit()
    {
        if (dialogue.index == 0)
        {
            if (dialogue.currentText.text == dialogue.sentences[dialogue.index])
            {
                dialogue.ClearText();
                dialogue.canType = true;
            }
        }

        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }
}
