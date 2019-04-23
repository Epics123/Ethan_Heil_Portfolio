using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineArcRenderer : MonoBehaviour
{
    public float velocity;
    public float angle;
    public int resolution = 10;
    public float gravity;
    public PlayerMovement movement;
    public PickUp pickUp;
    public Transform rightOrbPos, leftOrbPos;

    float radianAngle;


    LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        gravity = Mathf.Abs(Physics2D.gravity.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.facingRight == true)
        {
            gameObject.transform.position = rightOrbPos.position;
        }
        else
        {
            gameObject.transform.position = leftOrbPos.position;
        }
        if(pickUp.isHolding == true)
        {
            lr.enabled = true;
            RenderArc();
        }
        else
        {
            lr.enabled = false;
        }
        
    }

    void RenderArc()
    {
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArcArray());
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (Mathf.Pow(velocity, 2) * Mathf.Sin(2 * radianAngle)) / gravity; 

        for(int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        if(movement.facingRight == true)
        {
            return new Vector3(x, y, 0);
        }
        else
        {
            return new Vector3(-x, y, 0);
        }
    }
}
