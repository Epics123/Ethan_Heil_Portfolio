using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{

    public float speed = 10f;

    float xMove = 0f;
    float yMove = 0f;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }


    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        Move();
    }

    void Move()
    {
        rb2d.AddForce(new Vector2(xMove, yMove) * speed);
    }

}
