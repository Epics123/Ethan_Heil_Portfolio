using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject trigger;
    public LayerMask ground;
    public Transform groundCheck;
    public float xSpeed = 0.5f;
    public float ySpeed = 7f;
    public float jumpForce = 700f;
    public float maxXSpeed = 15f;
    public float maxYSpeed = 25f;
    public bool isGrounded = false;

    Rigidbody2D rb2D;
    Vector2 angleNormal;

    float xMove = 0;
    //float yMove = 0;
    float acc = 0.25f;
    float frc = 0.3f;
    
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
        CheckRotation();
    }

    void CheckInput()
    {
        if(Input.GetKey(KeyCode.D))
        {
            if(xMove < 0)
            {
                xMove += (acc * xSpeed)*2;
                if(xMove >= -7.5f)
                {
                    xMove = 0f;
                }
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
                xMove -= (acc * xSpeed)*2;
                if (xMove <= 7.5f)
                {
                    xMove = 0f;
                }
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

    void CheckGround()
    {
        Collider2D col = Physics2D.OverlapCircle(groundCheck.position, 0.25f, ground);
        if(col == null)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
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
        shouldJump = false;

        rb2D.AddForce((Vector2.up) * jumpForce);
    }

    void CheckRotation()
    {
        //Debug.DrawRay(transform.position, -Vector3.up, Color.blue);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -Vector3.up, 5f, ground);

        if (Physics2D.Raycast(transform.position, -Vector3.up, 5f, ground))
        {
            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hitDown.normal);

            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, 10 * Time.deltaTime);
        }


        if (trigger.GetComponent<CheckTriggers>().startLoop == true && xMove != 0f)
        {
            rb2D.gravityScale = 0;
            transform.RotateAround(trigger.GetComponent<CheckTriggers>().loopCenter.transform.position, Vector3.forward, xMove);
        }
        else
        {
            rb2D.gravityScale = 1;
        }
    }

}
