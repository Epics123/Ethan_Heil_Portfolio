using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public GameObject platform;
    public float speed = 5f;
    public Transform[] movePoints;
    public int pointIndex = 0;
    public bool isLooping = false;

    Transform currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = movePoints[pointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        platform.transform.position = Vector2.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * speed);

        if (platform.transform.position == currentPoint.position)
        {
            if (pointIndex < movePoints.Length - 1)
            {
                pointIndex++;
                currentPoint = movePoints[pointIndex];
            }
            else if (isLooping == true)
            {
                pointIndex = 0;
                currentPoint = movePoints[pointIndex];
            }
        }
    }
}
