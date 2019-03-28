﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject gridHolder;
    public GameObject player;
    public GameObject enemy;
    public GameManager gm;
    public Square squarePrefab;
    public float startX = 0f;
    public float startY = 0f;
    public int levelStartX = 0;
    public int levelStartY = 0;
    public int rows;
    public int cols;
    readonly float spacer = 0.01f;

    public Text nameText;
    public Text rowText;
    public Text colText;

    private static GridManager instance;

    public float camX;
    public float camY;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.gridRows = rows;
        gm.gridCols = cols;

        InitGridHolder();
        BuildGrid();

        camX = (float)rows / 2f;
        camY = (float)cols / 2f;
        Camera.main.transform.position = new Vector3(camX, camY, -10);

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitGridHolder()
    {
        gridHolder = new GameObject();
        gridHolder.name = "Grid_Holder";
        gridHolder.transform.position = new Vector2(startX, startY);
    }

    void BuildGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Square square = Instantiate(squarePrefab, gridHolder.transform);
                Vector2 newPos = new Vector2(i + (spacer * i), j + (spacer * j));
                square.transform.localPosition = newPos;
                square.name = "Square_" + i + "_" + j;
                square.gridPosition = new Vector2Int(i, j);
                if (i == levelStartX && j == levelStartY)
                {
                    square.originalColor = Color.black;
                    square.spriteRenderer.material.color = square.originalColor;
                    square.isStart = true;
                    StartPlayer(square);
                }
                else
                {
                    square.isStart = false;
                }
            }
        }
    }

    public static void UpdateUI(Square square = null)
    {
        if(square == null)
        {
            instance.rowText.text = "---";
            instance.colText.text = "---";
            instance.nameText.text = "---";
            return;
        }

        instance.rowText.text = square.gridPosition.x.ToString();
        instance.colText.text = square.gridPosition.y.ToString();
        instance.nameText.text = square.name;
    }

    public static void OnDown(Square square = null)
    {
        if (instance.player.GetComponent<Player>().CheckDistance(square) == true)
        {
            instance.player.GetComponent<Player>().LerpPlayer(square);
            if(instance.enemy.GetComponent<Enemy>().CheckBounds())
            {
                instance.enemy.GetComponent<Enemy>().LerpEnemy();
            }
            
        }
        
    }

    public static void OnOver(Square square = null)
    {
        if (Mathf.Round(Vector2.Distance(square.gridPosition, instance.player.GetComponent<Player>().transform.position)) > 2)
        {
            square.validSpace = false;
        }
        else
        {
            square.validSpace = true;
        }
    }

    public void StartPlayer(Square square = null)
    {
        player.transform.position = new Vector2(square.gridPosition.x + (spacer * square.gridPosition.x), square.gridPosition.y + (spacer * square.gridPosition.y));
    }

}
