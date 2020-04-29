using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float maxSpeed;
    public float maxAirSpeed = 10f;
    public float maxWallSlideSpeed = 3f;
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float swingForce = 4f;
    public float groundCheckDistance = 0.025f;
    public bool groundCheck;
    public bool isSwinging;
    public bool afterSwing;
    public bool canMove = true;
    public bool zoom = false;
    public LayerMask mask;
    public float alignCheckDistance = 1f;

	private playerMoveSound soundSource;
	private bool mPrevGroundState = true;

    [HideInInspector] public Vector2 ropeHook;

    [SerializeField] private float friction = 0.10f;
    [SerializeField] private float acceleration = 0.10f;
    [SerializeField] private float deceleration = 0.15f;

    private float xMove;
    private bool jumpCancel = false;
    private bool jump = false;
    private float horizontalDirection;
    private Rigidbody2D rb2D;
    private SpriteRenderer playerSprite;
    private Animator animator;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
		soundSource = GetComponent<playerMoveSound>();
    }

	// Update is called once per frame
	void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.Space) && groundCheck))
            jump = true;
        if (Input.GetKeyUp(KeyCode.Space) && !groundCheck)
            jumpCancel = true;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

		if(!mPrevGroundState && groundCheck)
		{
			soundSource.playLand();
		}
		mPrevGroundState = groundCheck;
    }

    private void FixedUpdate()
    {
        float halfWidth = playerSprite.bounds.extents.x;
        float halfHeight = playerSprite.bounds.extents.y;

        groundCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), -transform.up, groundCheckDistance, mask);
        animator.SetBool("OnGround", groundCheck);

        //GetGroundAlignment(halfWidth);
        if (groundCheck)
        {
            canMove = true;
        }
        Move();
        Jump();
    }

    private void GetGroundAlignment(float halfWidth)
    {
        if(horizontalDirection < 0f)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(new Vector2(transform.position.x - halfWidth - 0.04f, transform.position.y), -transform.up, alignCheckDistance, mask);
            Debug.DrawRay(new Vector2(transform.position.x - halfWidth - 0.04f, transform.position.y), -transform.up * alignCheckDistance, Color.red);

            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, 15f * Time.deltaTime);
        }
        else if(horizontalDirection > 0f)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(new Vector2(transform.position.x + halfWidth + 0.04f, transform.position.y), -transform.up, alignCheckDistance, mask);
            Debug.DrawRay(new Vector2(transform.position.x + halfWidth + 0.04f, transform.position.y), -transform.up * alignCheckDistance, Color.blue);

            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, 15f * Time.deltaTime);
        }             
    }

    private void Move()
    {
        if (canMove && !zoom)
        {
            if (horizontalDirection < 0f || horizontalDirection > 0f)
            {
                CalculateMovementDirection();            
            }
            else
            {
                //Slow down if no input
                if (groundCheck)
                {
                    xMove -= Mathf.Min(Mathf.Abs(xMove), friction) * Mathf.Sign(xMove);
                    //rb2D.velocity = new Vector2(xMove, rb2D.velocity.y);
                }                   
            }
            animator.SetFloat("Speed", Mathf.Abs(xMove));
        }
    }

    private void CalculateMovementDirection()
    {
        //playerSprite.flipX = horizontalDirection < 0f;

        if(horizontalDirection < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (isSwinging)
        {
            rb2D.AddForce(CalculatePerpendicularForce(), ForceMode2D.Force);
            xMove = 0;
        }
        if (groundCheck)
        {
            //Moving left on the ground
            if (horizontalDirection < 0)
            {
                //slow down if moving right
                if (xMove > 0)
                {
                    xMove -= deceleration;
                    if (xMove <= 0)
                        xMove = -0.5f;
                }
                //Cap speed
                else if (xMove > -maxSpeed)
                {
                    xMove -= acceleration;
                    if (xMove <= -maxSpeed)
                        xMove = -maxSpeed;
                }
            }

            //Moving right on the ground
            if (horizontalDirection > 0)
            {
                //Slow down if moving left
                if (xMove < 0)
                {
                    xMove += deceleration;
                    if (xMove >= 0)
                        xMove = 0.5f;
                }
                //Cap speed
                else if (xMove < maxSpeed)
                {
                    xMove += acceleration;
                    if (xMove >= maxSpeed)
                        xMove = maxSpeed;
                }
            }
            rb2D.velocity = new Vector2(xMove, rb2D.velocity.y);
        }

        //Air movement after jump
        if (!groundCheck && !isSwinging)
        {
            //Moving left in the air
            if (horizontalDirection < 0)
            {
                //slow down if moving right
                if (xMove > 0)
                {
                    xMove -= deceleration * 2;
                    if (xMove <= 0)
                        xMove = -0.5f;
                }
                //Cap speed
                else if (xMove > -maxSpeed)
                {
                    xMove -= acceleration;
                    if (xMove <= -maxSpeed)
                        xMove = -maxSpeed;
                }
            }

            //Moving right in the air
            if (horizontalDirection > 0)
            {
                //Slow down if moving left
                if (xMove < 0)
                {
                    xMove += deceleration * 2;
                    if (xMove >= 0)
                        xMove = 0.5f;
                }
                //Cap speed
                else if (xMove < maxSpeed)
                {
                    xMove += acceleration;
                    if (xMove >= maxSpeed)
                        xMove = maxSpeed;
                }
            }
            rb2D.velocity = new Vector2(xMove, rb2D.velocity.y);
        }
    }

    private void Jump()
    {
        if (!isSwinging && !zoom)
        {
            if(jump)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, maxJumpHeight);
                jump = false;  
            } 

            if (jumpCancel)
            {
                if(rb2D.velocity.y > minJumpHeight)
                    rb2D.velocity = new Vector2(rb2D.velocity.x, minJumpHeight);
                jumpCancel = false;
            }
        } 
    }

    Vector2 CalculatePerpendicularForce()
    {
        //Get a normalized direction vector from the player to the hook point
        Vector2 playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

        //Get Perpendicular direction
        Vector2 perpendicularDirection;
        if (horizontalDirection < 0)
        {
            perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
            Vector2 leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
            Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
        }
        else
        {
            perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
            Vector2 rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
            Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
        }

        Vector2 force = perpendicularDirection * swingForce;

        return force;
    }
}
