using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask ground;
    public Animator anim;
    public bool isGrounded = false;
    public bool facingRight = true;
    public float xSpeed = 0.5f;
    public float ySpeed = 7f;
    public float jumpForce = 300f;
    public float maxXSpeed = 10f;
    public float jumpTime;

    Rigidbody2D rb2D;
    float xMove = 0f;
    float acc = 0.25f;
    float frc = 0.3f;
    float jumpTimer;
    bool shouldJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(11, 10);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Flip();
    }

    void FixedUpdate()
    {
        Move();
        CheckJump();
        CheckGround();
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            facingRight = true;
            if (xMove < 0)
            {
                xMove += (acc * xSpeed) * 2;
                if (xMove >= -7.5f)
                {
                    xMove = 0f;
                }
            }
            else
            {
                xMove += acc * xSpeed;
                CheckMaxSpeed();
            }
            anim.SetFloat("xSpeed", Mathf.Abs(xMove));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            facingRight = false;
            if (xMove > 0)
            {
                xMove -= (acc * xSpeed) * 2;
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
            anim.SetFloat("xSpeed", Mathf.Abs(xMove));
        }
        else
        {
            xMove -= Mathf.Min(Mathf.Abs(xMove), frc) * Mathf.Sign(xMove);
            anim.SetFloat("xSpeed", Mathf.Abs(xMove));
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            shouldJump = true;
            jumpTimer = jumpTime;
        }

        if (Input.GetKey(KeyCode.Space) && shouldJump == true)
        {
            if (jumpTimer > 0)
            {
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                shouldJump = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            shouldJump = false;
        }
    }

    void Move()
    {
        rb2D.velocity = new Vector2(xMove, rb2D.velocity.y);
    }

    void CheckMaxSpeed()
    {
        if (xMove >= maxXSpeed)
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
        if (col == null)
        {
            isGrounded = false;
            anim.SetBool("onGround", false);
        }
        else
        {
            isGrounded = true;
            anim.SetBool("onGround", true);
        }
    }

    void CheckJump()
    {
        if (shouldJump)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb2D.velocity = new Vector2( rb2D.velocity.x, jumpForce);
    }

    void Flip()
    {
        if(facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
