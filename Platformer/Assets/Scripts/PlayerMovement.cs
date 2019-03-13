using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public GameManager gm;
    public GameObject trigger;
    public LayerMask ground;
    public Transform groundCheck;
    public PlayerAnimation anim;
    public float xSpeed = 0.5f;
    public float ySpeed = 7f;
    public float jumpForce = 700f;
    public float maxXSpeed = 15f;
    public float maxYSpeed = 25f;
    public bool isGrounded = false;
    public bool canMove = true;

    Rigidbody2D rb2D;
    Vector2 angleNormal;

    float xMove = 0f;
    float acc = 0.25f;
    float frc = 0.3f;

    bool shouldJump = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
            CheckInput();
    }

    void FixedUpdate()
    {
        CheckRotation();
        Move();
        CheckJump();
        CheckGround();
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            anim.facingRight = true;
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
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.facingRight = false;
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
        if(canMove)
        {
            Vector2 localVel = transform.InverseTransformDirection(rb2D.velocity);
            localVel.x = xMove;
            rb2D.velocity = transform.TransformDirection(localVel);
        }
        else
        {
            rb2D.velocity = Vector2.zero;
        }
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
        }
        else
        {
            isGrounded = true;
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
        shouldJump = false;
        rb2D.AddForce(transform.up * jumpForce);
    }


    void CheckRotation()
    {

        if(Physics2D.Raycast(transform.position, -transform.up, 20f, ground))// && isGrounded)
        {
            RaycastHit2D hitDown;
            hitDown = Physics2D.Raycast(transform.position, -transform.up, 20f, ground);

            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hitDown.normal);
            angleNormal = hitDown.normal;

            float theta = Mathf.Atan(hitDown.normal.y / hitDown.normal.x);

            float newGravityX = 0f;
            float newGravityY = 0f;

            if(slopeRotation.eulerAngles.z <= 180f)
            {
                newGravityX = (hitDown.normal.magnitude * Mathf.Cos(theta)) * 23;
                newGravityY = (hitDown.normal.magnitude * Mathf.Sin(theta)) * 23;
            }
            else if(slopeRotation.eulerAngles.z >= 180f)
            {
                newGravityX = (hitDown.normal.magnitude * -Mathf.Cos(theta)) * 23;
                newGravityY = (hitDown.normal.magnitude * -Mathf.Sin(theta)) * 23;
            }

            Physics2D.gravity = new Vector2(newGravityX, newGravityY);

            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, 15 * Time.deltaTime);
        }
       
    }

}
