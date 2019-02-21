using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that makes the outline of the pickup flash colors
public class PickUpFlashColor : MonoBehaviour
{

    public Material outline;
    public Color originalColor;
    public Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow};
    public Color targetColor;
    public float flashSpeed = 10f;

    int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = outline.color;
        targetColor = colors[0];
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Lerp the outline color to the target color
        outline.color = Color.Lerp(outline.color, targetColor, flashSpeed * Time.deltaTime);

        //Change the target color
        if(outline.color == targetColor)
        {
            if(currentIndex >= colors.Length-1)
            {
                currentIndex = 0;
                targetColor = colors[currentIndex];
            }
            else
            {
                currentIndex++;
                targetColor = colors[currentIndex];
            }
        }
    }
}
