using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpTrigger : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public BoxCollider2D boxCollider;
    public Image background;
    public Text text;
    public LayerMask player;
    public Color currentColor;
    public string[] hintLines;
    public string specialString;
    public float lerpAmount;
    public int specialStringColor;
    public int insertIndex;
    public int insertLine;

    bool dialogueActive = false;
    float r, g, b;
    int currentLine;

    // Start is called before the first frame update
    void Start()
    {
        background.enabled = false;
        text.enabled = false;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        currentLine = 0;
        currentColor = GetComponent<SpriteRenderer>().color;
        r = 1f;
        g = 0f;
        b = 0f;

        StartCoroutine(LerpColor());
        InsertString(insertIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueActive)
        {
            ShowHint();
            if(Input.GetKeyDown(KeyCode.E))
            {
                currentLine++;
            }
        }

        if(currentLine >= hintLines.Length)
        {
            HideHint();
            currentLine = 0;
        }

        text.text = hintLines[currentLine];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(dialogueActive == false)
            {
                dialogueActive = true;
            }  
        }
    }

    void InsertString(int index)
    {
        switch(specialStringColor)
        {
            case 1: hintLines[insertLine] = hintLines[insertLine].Insert(insertIndex, "<color=red>" + specialString + "</color>");
                break;
            case 2: hintLines[insertLine] = hintLines[insertLine].Insert(insertIndex, "<color=blue>" + specialString + "</color>");
                break;
            default: hintLines[insertLine] = hintLines[insertLine].Insert(insertIndex, "<color=white>" + specialString + "</color>");
                break;
        }
        
    }

    void ShowHint()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        background.enabled = true;
        text.enabled = true;
        dialogueActive = true;
        playerMovement.canMove = false;
    }

    void HideHint()
    {
        background.enabled = false;
        text.enabled = false;
        dialogueActive = false;
        playerMovement.canMove = true;
        StartCoroutine(EnableHint());
    }

    IEnumerator EnableHint()
    {
        boxCollider.enabled = false;

        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().enabled = true;
        boxCollider.enabled = true;
    }

    IEnumerator LerpColor()
    {
        if(r == 1f && g <= 1f)
        {
            g += lerpAmount;
            if(g >= 1f)
            {
                g = 1f;
            }
            currentColor = new Color(r, g, b, 1f);
            GetComponent<SpriteRenderer>().color = currentColor;
        }
        if(g == 1f && r >= 0f)
        {
            r -= lerpAmount;
            if (r <= 0f)
            {
                r = 0f;
            }
            currentColor = new Color(r, g, b, 1f);
            GetComponent<SpriteRenderer>().color = currentColor;
        }
        if (g == 1f && b <= 1f)
        {
            b += lerpAmount;
            if (b >= 1f)
            {
                b = 1f;
            }
            currentColor = new Color(r, g, b, 1f);
            GetComponent<SpriteRenderer>().color = currentColor;
        }
        if (b == 1f && g >= 0f)
        {
            g -= lerpAmount;
            if (g <= 0f)
            {
                g = 0f;
            }
            currentColor = new Color(r, g, b, 1f);
            GetComponent<SpriteRenderer>().color = currentColor;
        }
        if (b == 1f && r <= 1f)
        {
            r += lerpAmount;
            if (r >= 1f)
            {
                r = 1f;
            }
            currentColor = new Color(r, g, b, 1f);
            GetComponent<SpriteRenderer>().color = currentColor;
        }
        if (r == 1f && b >= 0f)
        {
            b -= lerpAmount;
            if (b <= 0f)
            {
                b = 0f;
            }
            currentColor = new Color(r, g, b, 1f);
            GetComponent<SpriteRenderer>().color = currentColor;
        }

        yield return null;
        StartCoroutine(LerpColor());
    }
}
