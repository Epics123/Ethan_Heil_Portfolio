using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float xMove = 10f;
    float yMove = 10f;
    float xSpeed = 5f;
    float ySpeed = 3f;
    float upperBound = 9.5f;
    float lowerBound = -10f;
    float rightBound = 5f;
    float leftBound = -5f;

    Rigidbody2D rb2D;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal") * xSpeed;
        yMove = Input.GetAxis("Vertical") * ySpeed;
    }

    void Move()
    {
        Vector2 newVelocity = new Vector2(xMove, yMove);
        rb2D.velocity = newVelocity;
    }

    void CheckBounds()
    {
        Vector2 maxPosX, maxPosY;

        if(transform.position.x < leftBound)
        {
            maxPosX = new Vector2(leftBound, transform.position.y);
            transform.position = maxPosX;
        }
        else if(transform.position.x > rightBound)
        {
            maxPosX = new Vector2(rightBound, transform.position.y);
            transform.position = maxPosX;
        }

        if (transform.position.y < lowerBound)
        {
            maxPosY = new Vector2(transform.position.x, lowerBound);
            transform.position = maxPosY;
        }
        else if (transform.position.y > upperBound)
        {
            maxPosY = new Vector2(transform.position.x, upperBound);
            transform.position = maxPosY;
        }
    }
}
