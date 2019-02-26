using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public LayerMask ground;
    public Transform groundCheck;
    public float xSpeed = 5f;
    public float ySpeed = 7f;
    public float maxXSpeed = 10f;
    public float maxYSpeed = 25f;

    Rigidbody2D rb2D;

    float xMove = 0;
    float yMove = 0;
    float gravity = 0.21f;
    float acc = 0.25f;
    float frc = 0.3f;
    float jmp = 4f;

    bool isGrounded = false;
    bool shouldJump = false;


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
        CheckJump();
        CheckGround();
        Debug.Log(rb2D.velocity.y);
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
        else if (Input.GetKey(KeyCode.A))
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
        else
        {
            xMove -= Mathf.Min(Mathf.Abs(xMove), frc) * Mathf.Sign(xMove);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            shouldJump = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) && !isGrounded)
        {
            shouldJump = false;
        }

    }

    void Move()
    {
        rb2D.velocity = new Vector2(xMove, yMove);
        //Debug.Log(rb2D.velocity);
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
        if (yMove >= maxYSpeed)
        {
            yMove = maxYSpeed;
        }
        if (yMove <= -maxYSpeed)
        {
            yMove = -maxYSpeed;
        }

    }

    void CheckGround()
    {
        Collider2D col = Physics2D.OverlapCircle(groundCheck.position, 0.5f, ground);
        if(col == null)
        {
            isGrounded = false;
            yMove -= gravity * ySpeed;
            CheckMaxSpeed();
        }
        else
        {
            isGrounded = true;
            yMove = 0f;
        }
    }

    void CheckJump()
    {
        if(shouldJump)
        {
            Jump();
        }
    }

    void Jump()
    {
        //shouldJump = false;
        yMove += ySpeed * jmp;
        if (rb2D.velocity.y >= maxYSpeed)
        {
            shouldJump = false;
        }

        rb2D.velocity = new Vector2(rb2D.velocity.x, yMove);
        
    }
}
