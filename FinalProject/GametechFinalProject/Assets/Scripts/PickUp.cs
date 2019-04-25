using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float launchForce;
    public bool canHold = true;
    public bool isHolding = false;
    public GameObject orb;
    public GameObject tempParent;
    public GameObject rightOrbPos;
    public GameObject leftOrbPos;
    public PlayerMovement movement;
    public LineArcRenderer lineArc;
    public Animator anim;
    public CapsuleCollider2D playerCollider;

    Vector2 mousePos;
    Vector2 launchVelocity;
    float distance = 5;
    float distancePercentage;
    float launchX;
    float launchY;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        launchX = mousePos.x - transform.position.x;

        if (launchX > distance && launchX > 0)
        {
            launchX = distance;
        }

        if (launchX < -distance && launchX < 0)
        {
            launchX = -distance;
        }

        if(movement.facingRight == true)
        {
            time = launchX / distance;
        }
        else
        {
            time = launchX / -distance;
        }
        
        launchY = lineArc.gravity * (time / 2);
        lineArc.velocity = Mathf.Sqrt(Mathf.Pow(launchX, 2) + Mathf.Pow(launchY, 2));
        launchVelocity = new Vector2(launchX, launchY);

        if(isHolding)
        {
            if (movement.facingRight)
            {
                tempParent = rightOrbPos;
            }
            else
            {
                tempParent = leftOrbPos;
            }
            orb.transform.position = tempParent.transform.position;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(isHolding == true)
            {
                isHolding = false;
                anim.SetBool("HoldingOrb", isHolding);
                canHold = true;

                orb.GetComponent<Rigidbody2D>().velocity = launchVelocity;
                orb.GetComponent<Orb>().collisionCheck.SetActive(true);
                orb.GetComponent<Orb>().thrown = true;
                orb.GetComponent<Rigidbody2D>().gravityScale = 1;
                orb = null;
            }  
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Orb" && canHold == true)
        {
            if(collision.GetComponent<Orb>().thrown == false)
            {
                canHold = false;
                if (movement.facingRight)
                {
                    tempParent = rightOrbPos;
                }
                else
                {
                    tempParent = leftOrbPos;
                }
                orb = collision.gameObject;
                isHolding = true;
                anim.SetBool("HoldingOrb", isHolding);
                collision.GetComponent<Rigidbody2D>().gravityScale = 0;
                orb.GetComponent<Orb>().collisionCheck.SetActive(false);
                orb.transform.position = tempParent.transform.position;
            }
            else
            {
                Physics2D.IgnoreCollision(playerCollider, 
                    collision.GetComponent<Orb>().collisionCheck.GetComponent<CircleCollider2D>());
            }
            
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Orb" && isHolding == true)
        {
            if (movement.facingRight)
            {
                tempParent = rightOrbPos;
            }
            else
            {
                tempParent = leftOrbPos;
            }
            orb.transform.position = tempParent.transform.position;     
        } 
    }

}
