using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Material mouseOverMat;

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
        Debug.Log(name);
        spriteRenderer.material.color = mouseOverColor;
    }

    void OnMouseExit()
    {
        spriteRenderer.material.color = originalColor;
    }
}
