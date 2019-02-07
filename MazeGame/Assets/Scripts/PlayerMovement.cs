using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves the player

public class PlayerMovement : MonoBehaviour
{

    public float speed;

    float xMove = 0f;
    float yMove = 0f;


    // Update is called once per frame
    void FixedUpdate()
    {
        CheckInput();
    }

    //Check for player input
    void CheckInput()
    {
       
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        Move();
    }

    //Moves player based on user input
    void Move()
    {
        transform.Translate((new Vector3(xMove, yMove, 0) * speed) * Time.deltaTime);
    }



}
