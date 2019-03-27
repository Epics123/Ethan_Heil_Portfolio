﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject gridHolder;
    public Square squarePrefab;
    public float startX = 0f;
    public float startY = 0f;
    public int rows;
    public int cols;
    readonly float spacer = 0.01f;


    private static GridManager instance;

    // Start is called before the first frame update
    void Start()
    {
        InitGridHolder();
        BuildGrid();
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
        for(int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Square square = Instantiate(squarePrefab, gridHolder.transform);
                Vector2 newPos = new Vector2(j + (spacer * j), i + (spacer * i));
                square.transform.localPosition = newPos;
                square.name = "Square_" + i + "_" + j;
                square.gridPosition = new Vector2Int(i, j);
            }
        }
    }
}
