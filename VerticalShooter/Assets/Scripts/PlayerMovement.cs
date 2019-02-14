using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float xMove = 0f;
    float yMove = 0f;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        xMove = (Input.GetAxis("Horizontal") * speed) * Time.deltaTime;
        yMove = (Input.GetAxis("Vertical") * speed) * Time.deltaTime;
        Move();
    }

    void Move()
    {
        transform.Translate(new Vector3(xMove, yMove, 0));
    }
}
