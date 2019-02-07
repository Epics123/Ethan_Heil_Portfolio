using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;

    float xMove = 0f;
    float yMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
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
        transform.Translate((new Vector3(xMove, yMove, 0) * speed) * Time.deltaTime);
    }



}
