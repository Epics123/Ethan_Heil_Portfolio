using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpTrigger : MonoBehaviour
{
    public Image background;
    public Text text;
    public LayerMask player;
    public Color currentColor;
    public float lerpAmount;

    bool dialogueActive = false;
    float r, g, b;

    // Start is called before the first frame update
    void Start()
    {
        background.enabled = false;
        text.enabled = false;
        currentColor = GetComponent<SpriteRenderer>().color;
        r = 1f;
        g = 0f;
        b = 0f;

        StartCoroutine(LerpColor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(dialogueActive == false)
            {
                StartCoroutine(DisplayDialogue());
            }   
        }
    }

    IEnumerator DisplayDialogue()
    {
        background.enabled = true;
        text.enabled = true;
        dialogueActive = true;

        yield return new WaitForSeconds(3f);

        background.enabled = false;
        text.enabled = false;
        dialogueActive = false;
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
