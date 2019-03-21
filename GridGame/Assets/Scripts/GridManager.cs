using System.Collections;
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
    readonly float spacer = 0.1f;


    private static GridManager instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
