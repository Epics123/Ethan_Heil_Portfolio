using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float xSpeed = 5f;
    public float maxXSpeed = 10f;

    Rigidbody2D rb2D;

    float xMove = 0;
    float yMove = 0;
    float gravity = -3f;
    float acc = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void CheckInput()
    {
        if(Input.GetKey(KeyCode.D))
        {
            if(xMove < 0)
            {
                xMove += acc * xSpeed;
            }
            else
            {
                xMove += acc * xSpeed;
                CheckMaxSpeed();
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (xMove > 0)
            {
                xMove -= acc * xSpeed;
            }
            else
            {
                xMove -= acc * xSpeed;
                CheckMaxSpeed();
            }   
        }

    }

    void Move()
    {
        rb2D.velocity = new Vector2(xMove, rb2D.velocity.y);
    }

    void CheckMaxSpeed()
    {
        if(xMove >= maxXSpeed)
        {
             xMove = maxXSpeed;
        }
        if (xMove <= -maxXSpeed)
        {
            xMove = -maxXSpeed;
        }
    }
}
