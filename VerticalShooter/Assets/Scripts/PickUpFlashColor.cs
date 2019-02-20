using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        outline.color = Color.Lerp(outline.color, targetColor, flashSpeed * Time.deltaTime);
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
