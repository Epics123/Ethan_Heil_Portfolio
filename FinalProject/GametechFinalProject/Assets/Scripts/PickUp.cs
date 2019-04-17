using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float throwForce;
    public bool canHold = true;
    public bool isHolding = false;
    public GameObject orb;
    public GameObject tempParent;

    Vector2 objectPos;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Orb")
        {
            Debug.Log("Pickup");
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
