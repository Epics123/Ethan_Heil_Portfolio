using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Material mouseOverMat;
    public bool validSpace;

    static Color mouseOverColor;

    Color originalColor;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.material.color;
        mouseOverColor = mouseOverMat.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        GridManager.OnOver(this);

        if(validSpace)
        {
            spriteRenderer.material.color = mouseOverColor;
        }
        else
        {
            spriteRenderer.material.color = Color.red;
        }
        
    }

    void OnMouseExit()
    {
        spriteRenderer.material.color = originalColor;
    }

    void OnMouseDown()
    {
        GridManager.OnDown(this);
    }
}
