using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Material mouseOverMat;
    public Color originalColor;
    public SpriteRenderer spriteRenderer;
    public GameObject objectOnSquare;
    public bool validSpace;
    public bool isStart;
    public bool isEnd;
    public bool hasWall;
    public bool finishLocked;

    static Color mouseOverColor;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.material.color;
        mouseOverColor = mouseOverMat.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(isEnd == true)
        {
            finishLocked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        GridManager.UpdateUI(this);
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
        GridManager.UpdateUI();
    }

    void OnMouseDown()
    {
        GridManager.OnDown(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9 && isEnd == true)
        {
            Debug.Log("Level Complete!");
        }
    }

}
