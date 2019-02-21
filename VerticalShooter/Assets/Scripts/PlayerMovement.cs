using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to control the player's movement
public class PlayerMovement : MonoBehaviour
{

    float xMove = 10f;
    float yMove = 10f;
    float xSpeed = 7f;
    float ySpeed = 6f;
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

    //Check movement and bounds
    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    //Check for player input 
    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal") * xSpeed;
        yMove = Input.GetAxis("Vertical") * ySpeed;
    }

    //Move player
    void Move()
    {
        Vector2 newVelocity = new Vector2(xMove, yMove);
        rb2D.velocity = newVelocity;
    }

    //Check if player is within bounds of the screen
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
