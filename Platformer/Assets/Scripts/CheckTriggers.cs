using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggers : MonoBehaviour
{
    public GameManager gm;
    public Color deathColor;
    public Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        deathColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<SpriteRenderer>().color == deathColor)
        {
            gm.KillPlayer();
        }

        if(collision.gameObject.tag == "Platform")
        {
            gm.player.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            gm.player.transform.parent = null;
        }
    }
}
