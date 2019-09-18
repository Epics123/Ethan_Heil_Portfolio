using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    float speed = 4.0f;
    float xMove = 0f;
    float yMove = 0f;
    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckInput();
        MovePlayer();
    }

    void CheckInput()
    {
        xMove = Input.GetAxisRaw("Horizontal") * speed;
        yMove = Input.GetAxisRaw("Vertical") * speed;

    }

   void MovePlayer()
    {
        rb2d.velocity = new Vector2(xMove, yMove);
    }
}
