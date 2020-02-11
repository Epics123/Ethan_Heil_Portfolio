using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamitePile : MonoBehaviour
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
    public void OnMouseDown()
    {
        player.placingDynamite = true;
        player.placingDam = false;

        if (dialogue.currentText.text == dialogue.sentences[dialogue.index])
            dialogue.ClearText();
    }

    private void OnMouseEnter()
    {
        if (dialogue.canType)
        {
            dialogue.ChangeSentence(1);
            dialogue.canType = false;
        }

        gameObject.GetComponent<SpriteRenderer>().color = highlight;
    }

    private void OnMouseExit()
    {
        if(dialogue.index == 1)
        {
            if(dialogue.currentText.text == dialogue.sentences[dialogue.index])
            {
                dialogue.ClearText();
                dialogue.canType = true;
            }
        }

        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }
}
