using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool canHold = true;
    public bool isHolding = false;
    public GameObject orb;
    public GameObject tempParent;
    public GameObject rightOrbPos;
    public GameObject leftOrbPos;
    public PlayerMovement movement;
    public LineArcRenderer lineArc;

    Vector2 mousePos;
    float distance = 2;
    float launchX;
    float launchY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        launchX = mousePos.x - transform.position.x;
        launchY = mousePos.y - transform. position.y;

        if(launchX > distance)
        {
            launchX = distance;
        }
        if(launchX < -distance)
        {
            launchX = -distance;
        }

        if(launchY > distance)
        {
            launchY = distance;
        }
        if(launchY < -distance)
        {
            launchY = -distance;
        }

        lineArc.angle = Mathf.Rad2Deg * Mathf.Atan(launchY / launchX);

        //Debug.Log("(" + launchX + "," + launchY + ")");
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
        if(collision.gameObject.tag == "Orb")
        {
            isHolding = true;
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
            orb.GetComponent<Orb>().collisionCheck.SetActive(true);
            orb = null;
            isHolding = false;
            collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
