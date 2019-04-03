using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Square location;
    readonly float spacer = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        location.hasWall = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
