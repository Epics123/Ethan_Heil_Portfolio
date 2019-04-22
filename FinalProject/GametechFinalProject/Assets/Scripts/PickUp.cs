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
        if(launchX < -distance && launchX < 0)
        {
            launchX = -distance;
        }

        time = distance / launchX;
        launchY = lineArc.gravity * (time / 2);
        lineArc.velocity = Mathf.Sqrt(Mathf.Pow(launchX, 2) + Mathf.Pow(launchY, 2));
        launchVelocity = new Vector2(launchX, launchY);

        if(Input.GetMouseButtonDown(0))
        {
            if(isHolding == true)
            {
                isHolding = false;
                orb.GetComponent<Rigidbody2D>().gravityScale = 1;
                orb.GetComponent<Rigidbody2D>().velocity = launchVelocity;
                orb.GetComponent<Orb>().collisionCheck.SetActive(true);
                orb.GetComponent<Orb>().thrown = true;
            }  
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Orb")
        {
            if(movement.facingRight)
            {
                tempParent = rightOrbPos;
            }
            else
            {
                tempParent = leftOrbPos;
            }
            orb = collision.gameObject;
            isHolding = true;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            orb.GetComponent<Orb>().collisionCheck.SetActive(false);
            orb.transform.position = tempParent.transform.position;
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

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Orb")
        {
            if(orb != null)
            {
                orb.GetComponent<Orb>().collisionCheck.SetActive(true);
                orb = null;
                isHolding = false;
                collision.GetComponent<Rigidbody2D>().gravityScale = 1;
            } 
        }
    }
}
