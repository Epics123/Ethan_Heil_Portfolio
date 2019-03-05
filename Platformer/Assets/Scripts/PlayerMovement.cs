using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorMode
{
    BOTTOM_RIGHT,
    TOP_RIGHT,
    BOTTOM_LEFT,
    TOP_LEFT
}

public class PlayerMovement : MonoBehaviour
{
    public FloorMode mode = FloorMode.BOTTOM_RIGHT;
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

    Rigidbody2D rb2D;
    Vector2 angleNormal;

    float xMove = 0f;
    float acc = 0.25f;
    float frc = 0.3f;
    int quadrant = 0;

    bool shouldJump = false;
    bool switchQuad = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        CheckRotation();
        CheckFloorMode();
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
        Vector2 localVel = transform.InverseTransformDirection(rb2D.velocity);
        localVel.x = xMove;
        rb2D.velocity = transform.TransformDirection(localVel);
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

    void CheckFloorMode()
    {

        switch (quadrant)
        {
            case 0:
                mode = FloorMode.BOTTOM_RIGHT;
                break;
            case 1:
                mode = FloorMode.TOP_RIGHT;
                break;
            case 2:
                mode = FloorMode.TOP_LEFT;
                break;
            case 3:
                mode = FloorMode.BOTTOM_LEFT;
                break;
        }
    }

    void CheckRotation()
    {
        Debug.DrawRay(transform.position, -Vector3.up, Color.red);

        if (mode == FloorMode.BOTTOM_RIGHT)
        {
            if (Physics2D.Raycast(transform.position, -Vector3.up, 10f, ground))
            {
                RaycastHit2D hitDown;
                if (switchQuad == false)
                {
                    hitDown = Physics2D.Raycast(transform.position, -Vector3.up, 10f, ground);
                }
                else
                {
                    hitDown = Physics2D.Raycast(transform.position, -Vector3.up, 10f, ground);
                }

                Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hitDown.normal);
                angleNormal = hitDown.normal;

                float theta = Mathf.Atan(hitDown.normal.y / hitDown.normal.x);
                float newGravityX = (hitDown.normal.magnitude * Mathf.Cos(theta)) * 23;
                float newGravityY = (hitDown.normal.magnitude * Mathf.Sin(theta)) * 23;

                if (!float.IsNaN(newGravityX) && !float.IsNaN(newGravityY))
                {
                    Physics2D.gravity = new Vector2(newGravityX, newGravityY);
                }
                else
                {
                    Physics2D.gravity = new Vector2(0f, -23f);
                }

                quadrant = (int)(slopeRotation.eulerAngles.z / 90);

                if (slopeRotation.eulerAngles.z >= 70f)
                {
                    switchQuad = true;
                    mode = FloorMode.TOP_RIGHT;
                }
                else
                {
                    switchQuad = false; 
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, 10 * Time.deltaTime);
            }
        }

        Debug.DrawRay(transform.position, Vector3.right, Color.blue);
        if (mode == FloorMode.TOP_RIGHT)
        {
            if (Physics2D.Raycast(transform.position, Vector3.right, 10f, ground))
            {

                RaycastHit2D hitDown;
                if (switchQuad == false)
                {
                    hitDown = Physics2D.Raycast(transform.position, Vector3.right, 10f, ground);
                }
                else
                {
                    hitDown = Physics2D.Raycast(transform.position, Vector3.up, 10f, ground);
                }

                Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hitDown.normal);
                angleNormal = -hitDown.normal;

                float theta = Mathf.Atan(hitDown.normal.y / hitDown.normal.x);
                float newGravityX = (hitDown.normal.magnitude * Mathf.Cos(theta)) * 23;
                float newGravityY = (hitDown.normal.magnitude * Mathf.Sin(theta)) * 23;
                if (!float.IsNaN(newGravityX) && !float.IsNaN(newGravityY))
                {
                    Physics2D.gravity = new Vector2(newGravityX, newGravityY);
                }
                else
                {
                    Physics2D.gravity = new Vector2(23f, 0f);
                }

                quadrant = (int)(slopeRotation.eulerAngles.z / 90);

                if (slopeRotation.eulerAngles.z >= 160f)
                {
                    switchQuad = true;
                    mode = FloorMode.TOP_LEFT;
                }
                else
                {
                    switchQuad = false;
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, 10 * Time.deltaTime);
            }
        }

        Debug.DrawRay(transform.position, Vector3.up, Color.green);
        if (mode == FloorMode.TOP_LEFT)
        {
            if (Physics2D.Raycast(transform.position, Vector3.up, 10f, ground))
            {

                RaycastHit2D hitDown;
                if (switchQuad == false)
                {
                    hitDown = Physics2D.Raycast(transform.position, Vector3.up, 10f, ground);
                }
                else
                {
                    hitDown = Physics2D.Raycast(transform.position, -Vector3.right, 10f, ground);
                }

                Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hitDown.normal);
                angleNormal = -hitDown.normal;

                float theta = Mathf.Atan(hitDown.normal.y / hitDown.normal.x);
                float newGravityX = -(hitDown.normal.magnitude * Mathf.Cos(theta)) * 23;
                float newGravityY = -(hitDown.normal.magnitude * Mathf.Sin(theta)) * 23;

                if (!float.IsNaN(newGravityX) && !float.IsNaN(newGravityY))
                {
                    Physics2D.gravity = new Vector2(newGravityX, newGravityY);
                }
                else
                {
                    Physics2D.gravity = new Vector2(0f, 23f);
                }

                quadrant = (int)(slopeRotation.eulerAngles.z / 90);

                if (slopeRotation.eulerAngles.z >= 250f)
                {
                    switchQuad = true;
                    mode = FloorMode.BOTTOM_LEFT;
                }
                else
                {
                    switchQuad = false;
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, 10 * Time.deltaTime);
            }
        }

        Debug.DrawRay(transform.position, -Vector3.right, Color.yellow);
        if (mode == FloorMode.BOTTOM_LEFT)
        {
            if (Physics2D.Raycast(transform.position, -Vector3.right, 10f, ground))
            {

                RaycastHit2D hitDown;
                if (switchQuad == false)
                {
                    hitDown = Physics2D.Raycast(transform.position, -Vector3.right, 10f, ground);
                }
                else
                {
                    hitDown = Physics2D.Raycast(transform.position, -Vector3.up, 10f, ground);
                }

                Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hitDown.normal);
                angleNormal = hitDown.normal;

                float theta = Mathf.Atan(hitDown.normal.y / hitDown.normal.x);
                float newGravityX = -(hitDown.normal.magnitude * Mathf.Cos(theta)) * 23;
                float newGravityY = -(hitDown.normal.magnitude * Mathf.Sin(theta)) * 23;

                if (!float.IsNaN(newGravityX) && !float.IsNaN(newGravityY))
                {
                    Physics2D.gravity = new Vector2(newGravityX, newGravityY);
                }
                else
                {
                    Physics2D.gravity = new Vector2(-23f, 0f);
                }

                quadrant = (int)(slopeRotation.eulerAngles.z / 90);

                if (slopeRotation.eulerAngles.z >= 330f)
                {
                    switchQuad = true;
                    mode = FloorMode.BOTTOM_RIGHT;
                }
                else
                {
                    switchQuad = false;
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, 10 * Time.deltaTime);
            }
        }
    }

}
