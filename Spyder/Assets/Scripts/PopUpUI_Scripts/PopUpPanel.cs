using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPanel : MonoBehaviour
{
    // * Variables *
    public string linkCode; // only effects things with the same link code (among the same object group.)

    public RectTransform panel;
    public TriggerBox trigger;
    public TypeText typeText;

    public float maxTimer;
    float timer = 0;

    const float minScale = 0;
    const float maxScale = 1;

    public float scaleSpeed;
    float currentScale = 0;

    // ** Update Functions **
    private void Start()
    {
        panel.localScale = new Vector3(1, 0, 1);
        trigger.linkCode = linkCode;

        timer = 0;
    }

    private void Update()
    {
        HandlePanel();
    }

    // **** Other Functions ****
    public void TurnOn()
    {
        timer = maxTimer;
        typeText.textDisplay.text = "";
        StartCoroutine(typeText.Type());
        trigger.GetComponent<BoxCollider2D>().enabled = false;
    }

    void HandlePanel()
    {
        currentScale = panel.localScale.y;

        if (timer > 0)
        {
            if (panel.localScale.y <= maxScale)
            {
                currentScale += Time.deltaTime * scaleSpeed;

                if (currentScale > 1)
                {
                    currentScale = 1;
                }
            }

            timer -= Time.deltaTime;
        }
        else
        {
            if (panel.localScale.y >= minScale)
            {
                currentScale -= Time.deltaTime * scaleSpeed;

                if (currentScale < 0)
                {
                    currentScale = 0;
                    trigger.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        panel.localScale = new Vector3(1, currentScale, 1);
    }
}
